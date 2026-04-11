using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth = 3f;
    private float _currentHealth;

    public UnityEvent<float, float> onHealthChanged;
    public UnityEvent onDeath;

    // Initialise la santé au maximum
    private void Awake()
    {
        _currentHealth = maxHealth;
    }

    // Notifie les observateurs de la santé initiale
    private void Start()
    {
        onHealthChanged?.Invoke(_currentHealth, maxHealth);
    }

    // Retourne la santé maximale
    public float GetMaxHealth() => maxHealth;

    // Réduit la santé et appelle Die si nécessaire
    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0f, maxHealth);

        onHealthChanged?.Invoke(_currentHealth, maxHealth);

        if (_currentHealth <= 0f) Die();
    }

    // Gère la mort de l'entité
    private void Die()
    {
        onDeath?.Invoke();
        if (gameObject.CompareTag("Enemy")) Destroy(gameObject, 0.1f);
    }
}