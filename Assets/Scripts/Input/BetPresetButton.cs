using TMPro;
using UnityEngine;

/// <summary>
/// This class represents a button that sets the player's bet to a predefined amount when clicked.
/// </summary>
public class BetPresetButton : MonoBehaviour
{
    [SerializeField] private BetControls betControlsUI;
    [SerializeField] private TextMeshProUGUI amountText;
    [SerializeField] private int betAmount;

    private void Awake()
    {
        amountText.text = betAmount.ToString();
    }

    // When the button is clicked, set the bet to the preset amount
    public void ApplyBet()
    {
        betControlsUI.SetBet(betAmount);
    }
}