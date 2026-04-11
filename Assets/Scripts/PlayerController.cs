using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public SpriteAnimator spriteAnimator;

    private Vector2 _movement;
    private Vector2 _lastDirection = Vector2.down;
    private bool _isAttacking = false;

    // Gère l'entrée du joueur et met à jour l'animation
    void Update()
    {
        if (_isAttacking) return;

        Vector2 rawInput = Vector2.zero;
        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) rawInput.y += 1;
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) rawInput.y -= 1;
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) rawInput.x -= 1;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) rawInput.x += 1;
        }

        _movement = rawInput.normalized;

        UpdateAnimation();
    }

    // Met à jour l'animation selon le mouvement ou l'inactivité
    private void UpdateAnimation()
    {
        if (_movement.magnitude > 0.1f)
        {
            _lastDirection = _movement;
            spriteAnimator.PlayAnimation("Walk" + GetDirectionName(_movement));
        }
        else
        {
            spriteAnimator.PlayAnimation("Idle" + GetDirectionName(_lastDirection));
        }
    }

    // Retourne le nom de la dernière direction
    public string GetLastDirectionName() => GetDirectionName(_lastDirection);

    // Convertit un vecteur en direction (Up, Down, Left, Right)
    private string GetDirectionName(Vector2 dir)
    {
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            return dir.x > 0 ? "Right" : "Left";
        return dir.y > 0 ? "Up" : "Down";
    }

    // Définit l'état d'attaque du joueur
    public void SetAttacking(bool attacking) => _isAttacking = attacking;

    // Applique le mouvement au joueur
    void FixedUpdate()
    {
        if (!_isAttacking)
            rb.MovePosition(rb.position + _movement * moveSpeed * Time.fixedDeltaTime);
    }
}