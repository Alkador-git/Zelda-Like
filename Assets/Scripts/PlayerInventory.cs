using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    [Header("Données")]
    [SerializeField] private int _totalRupees = 0;

    public UnityEvent<int> onRupeesChanged;

    // Initialise les rubis et notifie les observateurs
    private void Start()
    {
        onRupeesChanged?.Invoke(_totalRupees);
    }

    // Ajoute des rubis à l'inventaire
    public void AddRupees(int amount)
    {
        if (amount < 0) return;

        _totalRupees += amount;
        Debug.Log($"Rubis collectés ! Nouveau total : {_totalRupees}");

        onRupeesChanged?.Invoke(_totalRupees);
    }

    // Retourne le nombre total de rubis
    public int GetTotalRupees() => _totalRupees;
}