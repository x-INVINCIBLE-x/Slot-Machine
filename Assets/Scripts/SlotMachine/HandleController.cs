using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// This class manages the behavior of the slot machine's handle, 
/// including the pull animation and notifying when the handle has been pulled.
/// </summary>
public class HandleController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float pullDuration = 0.5f;

    // Event triggered when the handle has been pulled.
    public event Action OnHandlePulled;

    private readonly int Pull = Animator.StringToHash("Pull");

    private Coroutine pullCoroutine;

    /// <summary>
    /// Initiates the pull animation by stopping any existing coroutine and starting a new one.
    /// </summary>
    public void PullHandle()
    {
        if (pullCoroutine != null)
        {
            StopCoroutine(pullCoroutine);
        }

        pullCoroutine = StartCoroutine(PullAnimation());
    }

    /// <summary>
    /// Holds Pull animation for a specified duration, 
    /// then resets the animation state and notifies that the handle has been pulled.
    /// </summary>
    /// <returns></returns>
    private IEnumerator PullAnimation()
    {
        animator.SetBool(Pull, true);
        yield return new WaitForSeconds(pullDuration);
        animator.SetBool(Pull, false);

        NotifyHandlePulled();
    }

    /// <summary>
    /// Invokes the OnHandlePulled event to notify subscribers that the handle has been pulled.
    /// </summary>
    public void NotifyHandlePulled()
    {
        OnHandlePulled?.Invoke();
    }
}