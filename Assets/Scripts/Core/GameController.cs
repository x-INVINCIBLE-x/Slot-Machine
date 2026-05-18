using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private BetConfirmationUI betConfirmationUI;
    [SerializeField] private BetController betController;

    private void OnEnable()
    {
        betConfirmationUI.OnConfirm += HandleBetConfirmed;
        betConfirmationUI.OnCancel += HandleBetCancelled;
    }

    private void HandleBetConfirmed()
    {
        betController.PlaceBet();
    }

    private void HandleBetCancelled()
    {
        betController.ClearBet();
    }

    private void OnDisable()
    {
        betConfirmationUI.OnConfirm -= HandleBetConfirmed;
        betConfirmationUI.OnCancel -= HandleBetCancelled;
    }
}
