using UnityEngine;

/// <summary>
/// This class manages the user interface for confirming a bet in the slot machine game.
/// </summary>
public class BetConfirmationUI : MonoBehaviour
{
    public event System.Action OnConfirm;
    public event System.Action OnCancel;

    public void Confirm()
    {
        OnConfirm?.Invoke();
    }

    public void Cancel()
    {
        OnCancel?.Invoke();
    }
}
