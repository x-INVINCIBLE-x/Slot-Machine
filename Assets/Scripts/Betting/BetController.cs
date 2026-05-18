using System;
using UnityEngine;

public class BetController : MonoBehaviour
{
    [SerializeField] private PlayerWallet wallet;

    public event Action<BetChangeResult> OnBetChanged;

    public int CurrentBet { get; private set; }

    private void Start()
    {
        AddBet(0);
    }

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

    public void ClearBet()
    {
        CurrentBet = 0;

        OnBetChanged?.Invoke(
            new BetChangeResult(CurrentBet, true, "Bet cleared.")
        );
    }

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