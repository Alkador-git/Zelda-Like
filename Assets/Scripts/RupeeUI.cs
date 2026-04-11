using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RupeeUI : MonoBehaviour
{
    [Header("Configuration")]
    public PlayerInventory playerInventory;
    public TMP_Text rubyText;

    [Header("Apparence")]
    public string prefix = "x ";

    // Initialise l'affichage des rubis
    void Start()
    {
        if (playerInventory != null)
        {
            playerInventory.onRupeesChanged.AddListener(UpdateRubyDisplay);

            UpdateRubyDisplay(playerInventory.GetTotalRupees());
        }
    }

    // Met à jour l'affichage du nombre total de rubis
    public void UpdateRubyDisplay(int totalRupees)
    {
        if (rubyText != null)
        {
            rubyText.text = prefix + totalRupees.ToString("D3");
        }
    }
}