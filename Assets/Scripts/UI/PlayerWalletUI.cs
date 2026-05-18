using System;
using System.Collections;
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

    // Text element to display the change in balance (delta) after a spin, showing how much was won or lost.
    [SerializeField] private TextMeshProUGUI deltaAmount;

    [SerializeField] private Color winColor = Color.green;
    [SerializeField] private Color lossColor = Color.red;

    [SerializeField] private float deltaDisplayDuration = 2f;

    private Coroutine deltaRoutine;

    private void OnEnable()
    {
        playerWallet.OnBalanceChanged += HandleBalanceUpdate;
    }

    /// <summary>
    /// Handles updates to the player's balance by subscribing to the PlayerWallet's OnBalanceChanged event.
    /// </summary>
    /// <param name="newBalance">The new balance amount.</param>
    /// <param name="amount">The change in balance (delta) amount.</param>
    private void HandleBalanceUpdate(int newBalance, int amount)
    {
        balanceText.text = $"{newBalance}G";

        if (deltaRoutine != null)
        {
            StopCoroutine(deltaRoutine);
        }

        deltaRoutine = StartCoroutine(BalanceUpdateRoutine(amount));
    }

    private IEnumerator BalanceUpdateRoutine(int amount)
    {
        deltaAmount.gameObject.SetActive(true);

        deltaAmount.color = amount >= 0 ? winColor : lossColor;
        deltaAmount.text = amount >= 0 ? $"+{amount}G" : $"{amount}G";

        yield return new WaitForSeconds(deltaDisplayDuration);
        deltaAmount.gameObject.SetActive(false);

        deltaRoutine = null;
    }

    private void OnDisable()
    {
        playerWallet.OnBalanceChanged -= HandleBalanceUpdate;
    }
}
