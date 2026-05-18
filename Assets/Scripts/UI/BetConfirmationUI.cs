using UnityEngine;

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
