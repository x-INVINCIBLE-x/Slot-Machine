using TMPro;
using UnityEngine;

public class BetPresetButton : MonoBehaviour
{
    [SerializeField] private BetControls betControlsUI;
    [SerializeField] private TextMeshProUGUI amountText;
    [SerializeField] private int betAmount;

    private void Awake()
    {
        amountText.text = betAmount.ToString();
    }

    public void ApplyBet()
    {
        betControlsUI.SetBet(betAmount);
    }
}