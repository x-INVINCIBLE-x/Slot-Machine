using System;
using TMPro;
using UnityEngine;

/// <summary>
/// UI component responsible for displaying the current bet amount to the player.
/// </summary>
public class BetUI : MonoBehaviour
{
    // Reference to the BetController, which manages the current bet state and triggers updates when the bet changes.
    [SerializeField] private BetController betController;

    // Text element to display the current bet amount, formatted as "XG" where X is the bet amount.
    [SerializeField] private TextMeshProUGUI currentBetText;

    private void OnEnable()
    {
        betController.OnBetChanged += HandleBetUpdate;
    }

    /// <summary>
    /// Handles updates to the bet amount by subscribing to the BetController's OnBetChanged event.
    /// </summary>
    /// <param name="result"></param>
    private void HandleBetUpdate(BetChangeResult result)
    {
        currentBetText.text = $"{result.BetAmount}G";
    }

    private void OnDisable()
    {
        betController.OnBetChanged -= HandleBetUpdate;
    }
}
