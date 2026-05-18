using System;
using UnityEngine;

public class BetUIAnimator : MonoBehaviour
{
    [SerializeField] private SlotMachine slotMachine;
    [SerializeField] private Animator animator;
    private static readonly int ShowBool = Animator.StringToHash("Show");

    private void OnEnable()
    {
        slotMachine.OnSpinStarted += Hide;
        slotMachine.OnSpinCompleted += Show;
    }

    public void Show(SpinResult result, int payout)
    {
        animator.SetBool(ShowBool, true);
    }

    public void Hide()
    {
        animator.SetBool(ShowBool, false);
    }

    private void OnDisable()
    {
        slotMachine.OnSpinStarted -= Hide;
        slotMachine.OnSpinCompleted -= Show;
    }

}
