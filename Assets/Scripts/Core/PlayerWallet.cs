using System;
using UnityEngine;

/// <summary>
/// This class represents the player's wallet, 
/// managing their balance and providing methods to add or subtract money. 
/// It also includes an event to notify when the balance changes, 
/// allowing other parts of the game to react accordingly.
/// </summary>
public class PlayerWallet : MonoBehaviour
{
    [SerializeField] private int startingBalance = 1000;

    // Event triggered when the balance changes, passing the new balance and the delta as arguments
    public event Action<int, int> OnBalanceChanged;

    public int Balance { get; private set; } = 0;

    private void Start()
    {
        AddMoney(startingBalance);
    }

    /// <summary>
    /// Adds money to the player's wallet and triggers the OnBalanceChanged event with the new balance.
    /// </summary>
    /// <param name="amount">The amount of money to add to the wallet.</param>
    /// <returns>True if the money was successfully added, false otherwise.</returns>
    public bool AddMoney(int amount)
    {
        if (amount == 0)
        {
            return false;
        }

        Balance += amount;
        OnBalanceChanged?.Invoke(Balance, amount);
        return true;
    }

    /// <summary>
    /// Removes money from the player's wallet if there are sufficient funds and triggers the OnBalanceChanged event with the new balance. 
    /// Returns true if the transaction was successful, false otherwise.
    /// </summary>
    /// <param name="amount">The amount of money to subtract from the wallet.</param>
    /// <returns>True if the money was successfully subtracted, false otherwise.</returns>
    public bool SubtractMoney(int amount)
    {
        if (amount > Balance)
        {
            Debug.LogWarning("Not enough money to subtract.");
            return false;
        }

        Balance -= amount;
        OnBalanceChanged?.Invoke(Balance, -amount);
        return true;
    }

    /// <summary>
    /// Checks if the player's wallet has enough balance to cover a specified amount.
    /// </summary>
    /// <param name="amount">The amount to check against the wallet's balance.</param>
    /// <returns>True if the wallet has enough balance, false otherwise.</returns>
    internal bool HasEnough(int amount)
    {
        return Balance >= amount;
    }

    public void ResetWallet()
    {
        int delta = startingBalance - Balance;
        Balance = startingBalance;
        OnBalanceChanged?.Invoke(Balance, delta);
    }
}
