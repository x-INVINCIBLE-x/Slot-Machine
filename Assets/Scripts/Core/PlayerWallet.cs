using System;
using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    [SerializeField] private int startingBalance = 1000;

    public event Action<int> OnBalanceChanged;

    public int Balance { get; private set; } = 0;

    private void Start()
    {
        AddMoney(startingBalance);
    }

    public bool AddMoney(int amount)
    {
        Balance += amount;
        OnBalanceChanged?.Invoke(Balance);
        return true;
    }

    public bool SubtractMoney(int amount)
    {
        if (amount > Balance)
        {
            Debug.LogWarning("Not enough money to subtract.");
            return false;
        }

        Balance -= amount;
        OnBalanceChanged?.Invoke(Balance);
        return true;
    }

    internal bool HasEnough(int amount)
    {
        return Balance >= amount;
    }
}
