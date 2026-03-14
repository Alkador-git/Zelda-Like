using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HealthUI : MonoBehaviour
{
    [Header("Configuration")]
    public Health playerHealth;
    public GameObject heartPrefab;

    [Header("Sprites de Coeurs")]
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

    private List<Image> _hearts = new List<Image>();

    void Start()
    {
        if (playerHealth != null)
        {
            InitializeHearts();
            playerHealth.onHealthChanged.AddListener(UpdateHearts);
            UpdateHearts(playerHealth.GetMaxHealth(), playerHealth.GetMaxHealth());
        }
    }

    void InitializeHearts()
    {
        foreach (Transform child in transform) Destroy(child.gameObject);
        _hearts.Clear();

        for (int i = 0; i < (int)playerHealth.GetMaxHealth(); i++)
        {
            GameObject newHeart = Instantiate(heartPrefab, transform);
            Image heartImage = newHeart.GetComponent<Image>();

            if (heartImage != null) _hearts.Add(heartImage);
            else Debug.LogError("Le heartPrefab n'a pas de composant Image !");
        }
    }

    public void UpdateHearts(float currentHealth, float maxHealth)
    {
        for (int i = 0; i < _hearts.Count; i++)
        {
            if (_hearts[i] == null) continue;

            if (currentHealth >= i + 1)
            {
                _hearts[i].sprite = fullHeart;
            }
            else if (currentHealth > i)
            {
                _hearts[i].sprite = halfHeart;
            }
            else
            {
                _hearts[i].sprite = emptyHeart;
            }
        }
    }
}