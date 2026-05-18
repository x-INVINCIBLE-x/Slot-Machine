using System;
using UnityEngine;

/// <summary>
/// This class manages the player's betting actions, including adding, subtracting, and setting bets.
/// It interacts with the player's wallet to ensure that bets are valid and within the player's available balance.
/// </summary>
public class BetController : MonoBehaviour
{
    [SerializeField] private PlayerWallet wallet;

    // Event triggered whenever the bet changes, providing details about the change through BetChangeResult.
    public event Action<BetChangeResult> OnBetChanged;

    public int CurrentBet { get; private set; }

    private void Start()
    {
        // Initialize the current bet to zero at the start of the game.
        ClearBet();
    }

    /// <summary>
    /// Increases the current bet by the specified amount, 
    /// ensuring that the player has enough balance in their wallet to cover the new bet. 
    /// </summary>
    /// <param name="amount">The amount to increase the bet by.</param>
    /// <returns>A BetChangeResult indicating the success or failure of the operation.</returns>
    public BetChangeResult AddBet(int amount)
    {
        int potentialBet = CurrentBet + amount;

        if (!wallet.HasEnough(potentialBet))
        {
            return CreateResult(false, "Not enough balance to increase bet.");
        }

        CurrentBet = potentialBet;

        return CreateResult(
            true,
            $"Bet increased by {amount}. Current Bet: {CurrentBet}"
        );
    }

    /// <summary>
    /// Reduces the current bet by the specified amount, ensuring that the bet does not go below zero.
    /// </summary>
    /// <param name="amount">The amount to decrease the bet by.</param>
    /// <returns>A BetChangeResult indicating the success or failure of the operation.</returns>
    public BetChangeResult SubtractBet(int amount)
    {
        if (amount <= 0)
        {
            return CreateResult(false, "Bet amount must be greater than zero.");
        }

        if (amount > CurrentBet)
        {
            return CreateResult(false, "Cannot subtract more than the current bet.");
        }

        CurrentBet -= amount;

        return CreateResult(
            true,
            $"Bet decreased by {amount}. Current Bet: {CurrentBet}"
        );
    }

    /// <summary>
    /// Sets the current bet to a specific amount, 
    /// ensuring that the player has enough balance in their wallet to cover the new bet.
    /// </summary>
    /// <param name="amount">The amount to set the bet to.</param>
    /// <returns>A BetChangeResult indicating the success or failure of the operation.</returns>
    public BetChangeResult SetBet(int amount)
    {
        if (amount <= 0)
        {
            return CreateResult(false, "Bet amount must be greater than zero.");
        }

        if (!wallet.HasEnough(amount))
        {
            return CreateResult(false, "Not enough balance to place this bet.");
        }

        CurrentBet = amount;

        return CreateResult(
            true,
            $"Bet set to {CurrentBet}"
        );
    }

    /// <summary>
    /// Initiates the betting process by checking if the current bet is valid and 
    /// if the player has enough balance to cover it.
    /// </summary>
    /// <returns>True if the bet was successfully placed; otherwise, false.</returns>
    public bool PlaceBet()
    {
        if (CurrentBet <= 0)
        {
            CreateResult(false, "No bet placed. Please set a bet before spinning.");
            return false;
        }
        if (!wallet.HasEnough(CurrentBet))
        {
            CreateResult(false, "Not enough balance to place the bet.");
            return false;
        }

        wallet.SubtractMoney(CurrentBet);
        CreateResult(true, $"Bet of {CurrentBet} placed successfully.");
        return true;
    }

    /// <summary>
    /// Resets the current bet to zero, allowing the player to start fresh with their betting.
    /// </summary>
    public void ClearBet()
    {
        CurrentBet = 0;

        OnBetChanged?.Invoke(
            new BetChangeResult(CurrentBet, true, "Bet cleared.")
        );
    }

    /// <summary>
    /// Results in a BetChangeResult object that encapsulates the current bet amount.
    /// </summary>
    /// <param name="success">Indicates whether the operation was successful.</param>
    /// <param name="message">A message describing the result of the operation.</param>
    /// <returns>A BetChangeResult object containing the current bet amount, success status, and message.</returns>
    private BetChangeResult CreateResult(bool success, string message)
    {
        var result = new BetChangeResult(CurrentBet, success, message);

        OnBetChanged?.Invoke(result);

        if (success)
        {
            Debug.Log(message);
        }
        else
        {
            Debug.LogWarning(message);
        }

        return result;
    }
}