using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CollectibleRupee : MonoBehaviour
{
    [Header("Réglages")]
    [SerializeField] private int _rupeeValue = 1;
    [SerializeField] private AudioClip _collectSound;

    private bool _hasBeenCollected = false;

    // Configure le collider en tant que trigger au démarrage
    private void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    // Détecte la collision avec le joueur pour le collecte
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !_hasBeenCollected)
        {
            Collect(other.gameObject);
        }
    }

    // Récupère le rubis et l'ajoute à l'inventaire du joueur
    private void Collect(GameObject player)
    {
        _hasBeenCollected = true;

        if (player.TryGetComponent(out PlayerInventory inventory))
        {
            inventory.AddRupees(_rupeeValue);
        }

        if (_collectSound != null)
        {
            AudioSource.PlayClipAtPoint(_collectSound, transform.position);
        }

        Destroy(gameObject);
    }
}