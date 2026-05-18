using System;
using System.Collections;
using UnityEngine;

public sealed class SlotMachine : MonoBehaviour
{
    [SerializeField] private ReelView[] reels;
    [SerializeField] private ReelGenerator reelGenerator;
    [SerializeField] private PayoutEvaluator payoutEvaluator;

    // Event triggered when a spin is initiated, allowing other parts of the game to react to the start of a spin.
    public event Action OnSpinStarted;

    // Event triggered when a spin is completed, providing the spin result and the calculated payout based on the current bet.
    public event Action<SpinResult, int> OnSpinCompleted;

    private SpinResult currentResult;
    private int stoppedReels;
    private int currentBet;

    private Coroutine stopRoutine = null;

    /// <summary>
    /// Starts a new spin of the slot machine with the specified bet amount. 
    /// It generates a new spin result, initiates the spinning of each reel, 
    /// and sets up event handlers to track when each reel stops. 
    /// Once all reels have stopped, it evaluates the payout based on the spin result and the current bet.
    /// </summary>
    /// <param name="currentBet">The amount of the current bet.</param>
    public void StartSpin(int currentBet)
    {
        if (stopRoutine != null) { return;}
        
        OnSpinStarted?.Invoke();

        this.currentBet = currentBet;
        currentResult = reelGenerator.GenerateSpin(reels.Length);
        stoppedReels = 0;


        foreach (ReelView reel in reels)
            reel.OnSpinStopped += HandleReelStopped;

        foreach (ReelView reel in reels)
            reel.StartSpin();

        stopRoutine = StartCoroutine(StopReelsSequentially());
    }

    /// <summary>
    /// Stops each reel in sequence with a delay between each stop.
    /// </summary>
    /// <returns>An enumerator for coroutine execution.</returns>
    private IEnumerator StopReelsSequentially()
    {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < reels.Length; i++)
        {
            reels[i].StopAt(currentResult.Symbols[i]);
            yield return new WaitForSeconds(0.5f);
        }

        stopRoutine = null;
    }

    /// <summary>
    /// Handles the logic when a reel stops spinning, increments the stopped reel count, evaluates the payout when all
    /// reels have stopped, and triggers the spin completion event.
    /// </summary>
    /// <param name="reel">The reel that has stopped spinning.</param>
    private void HandleReelStopped(ReelView reel)
    {
        stoppedReels++;

        if (stoppedReels < reels.Length)
            return;

        foreach (ReelView r in reels)
            r.OnSpinStopped -= HandleReelStopped;

        int payout = payoutEvaluator.Evaluate(currentResult, currentBet);
        OnSpinCompleted?.Invoke(currentResult, payout);
    }
}