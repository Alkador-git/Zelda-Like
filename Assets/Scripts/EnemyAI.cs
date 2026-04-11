using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Réglages")]
    public float moveSpeed = 2f;
    public float detectionRange = 5f;
    public float attackRange = 0.8f;
    public float damageAmount = 0.5f;
    public float attackCooldown = 1.5f;
    public float hitStunDuration = 0.2f;

    [Header("Composants")]
    public SpriteAnimator spriteAnimator;
    public Rigidbody2D rb;
    public Health health;

    private Transform _player;
    private float _nextAttackTime;
    private Vector2 _movement;
    private string _currentDir = "Down";
    private bool _isBeingHit = false;
    private float _hitTimer;

    // Initialise l'ennemi et cherche le joueur
    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) _player = playerObj.transform;

        if (health == null) health = GetComponent<Health>();

        if (health != null)
        {
            health.onHealthChanged.AddListener(OnTakeDamage);
        }
    }

    // Gère le comportement de l'ennemi : poursuite ou attaque
    void Update()
    {
        if (_isBeingHit)
        {
            _hitTimer -= Time.deltaTime;
            spriteAnimator.PlayAnimation("Hit" + _currentDir);

            if (_hitTimer <= 0) _isBeingHit = false;
            return;
        }

        if (_player == null) return;

        float distance = Vector2.Distance(transform.position, _player.position);

        if (distance < detectionRange && distance > attackRange)
        {
            _movement = (_player.position - transform.position).normalized;
            UpdateAnimation("Walk");
        }
        else
        {
            _movement = Vector2.zero;
            UpdateAnimation("Idle");

            if (distance <= attackRange && Time.time >= _nextAttackTime)
            {
                AttackPlayer();
            }
        }
    }

    // Reçoit les dommages et active l'état de choc
    public void OnTakeDamage(float current, float max)
    {
        _isBeingHit = true;
        _hitTimer = hitStunDuration;
        _movement = Vector2.zero;
    }

    // Applique le mouvement à l'ennemi
    void FixedUpdate()
    {
        if (_isBeingHit) return;
        rb.MovePosition(rb.position + _movement * moveSpeed * Time.fixedDeltaTime);
    }

    // Met à jour l'animation et la direction de l'ennemi
    void UpdateAnimation(string state)
    {
        if (_movement.magnitude > 0)
        {
            if (Mathf.Abs(_movement.x) > Mathf.Abs(_movement.y))
                _currentDir = _movement.x > 0 ? "Right" : "Left";
            else
                _currentDir = _movement.y > 0 ? "Up" : "Down";
        }

        spriteAnimator.PlayAnimation(state + _currentDir);
    }

    // Inflige des dégâts au joueur
    void AttackPlayer()
    {
        _nextAttackTime = Time.time + attackCooldown;
        if (_player.TryGetComponent(out Health playerHealth))
        {
            playerHealth.TakeDamage(damageAmount);
        }
    }
}