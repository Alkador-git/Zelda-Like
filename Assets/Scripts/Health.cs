using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth = 3f;
    private float _currentHealth;

    public UnityEvent<float, float> onHealthChanged;
    public UnityEvent onDeath;

    private void Awake()
    {
        _currentHealth = maxHealth;
    }

    private void Start()
    {
        onHealthChanged?.Invoke(_currentHealth, maxHealth);
    }

    public float GetMaxHealth() => maxHealth;

    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0f, maxHealth);

        onHealthChanged?.Invoke(_currentHealth, maxHealth);

        if (_currentHealth <= 0f) Die();
    }

    private void Die()
    {
        onDeath?.Invoke();
        if (gameObject.CompareTag("Enemy")) Destroy(gameObject, 0.1f);
    }
}