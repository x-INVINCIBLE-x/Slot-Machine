using System;
using UnityEngine;

/// <summary>
/// This class serves as the central controller for the game, 
/// managing interactions between the bet confirmation UI, handle controller, slot machine, bet controller, 
/// and player wallet.
/// </summary>
public class GameController : MonoBehaviour
{
    [SerializeField] private BetConfirmationUI betConfirmationUI;
    [SerializeField] private HandleController handleController;
    [SerializeField] private SlotMachine slotMachine;
    [SerializeField] private BetController betController;
    [SerializeField] private PlayerWallet playerWallet;

    private void OnEnable()
    {
        betConfirmationUI.OnConfirm += HandlePull;
        betConfirmationUI.OnCancel += HandleBetCancelled;

        handleController.OnHandlePulled += HandleBetConfirmed;
        slotMachine.OnSpinCompleted += HandleSpinCompleted;
    }

    // This method is called when the player confirms their bet.
    // It triggers the handle pull animation and initiates the betting process.
    private void HandlePull()
    {
        handleController.PullHandle();
    }

    /// <summary>
    /// Handles bet confirmation by placing the bet and starting the slot machine spin if successful.
    /// </summary>
    private void HandleBetConfirmed()
    {
        if (betController.PlaceBet())
        {
            slotMachine.StartSpin(betController.CurrentBet);
        }
    }

    /// <summary>
    /// Handles bet cancellation by clearing the current bet, 
    /// allowing the player to adjust their bet before confirming again.
    /// </summary>
    private void HandleBetCancelled()
    {
        betController.ClearBet();
    }

    /// <summary>
    /// Handles the completion of a slot machine spin by adding the payout to the player's wallet.
    /// </summary>
    /// <param name="result"></param>
    /// <param name="payout"></param>
    private void HandleSpinCompleted(SpinResult result, int payout)
    {
        playerWallet.AddMoney(payout);
    }

    private void OnDisable()
    {
        betConfirmationUI.OnConfirm -= HandlePull;
        betConfirmationUI.OnCancel -= HandleBetCancelled;

        handleController.OnHandlePulled -= HandleBetConfirmed;
        slotMachine.OnSpinCompleted -= HandleSpinCompleted;
    }
}
