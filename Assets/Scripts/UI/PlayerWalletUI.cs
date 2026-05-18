using System;
using TMPro;
using UnityEngine;

/// <summary>
/// This class manages the user interface for displaying the player's current balance in the slot machine game.
/// </summary>
public class PlayerWalletUI : MonoBehaviour
{
    [SerializeField] private PlayerWallet playerWallet;

    // Text element to display the player's current balance, formatted as "XG" where X is the balance amount.
    [SerializeField] private TextMeshProUGUI balanceText;

    private void OnEnable()
    {
        playerWallet.OnBalanceChanged += HandleBalanceUpdate;
    }

    /// <summary>
    /// Handles updates to the player's balance by subscribing to the PlayerWallet's OnBalanceChanged event.
    /// </summary>
    /// <param name="newBalance">The new balance amount.</param>
    private void HandleBalanceUpdate(int newBalance)
    {
        balanceText.text = $"{newBalance}G";
    }

    private void OnDisable()
    {
        playerWallet.OnBalanceChanged -= HandleBalanceUpdate;
    }
}
