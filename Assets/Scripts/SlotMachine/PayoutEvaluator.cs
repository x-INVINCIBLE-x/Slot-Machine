using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Evaluates slot machine spin results and calculates payouts based on predefined symbol multipliers.
/// </summary>
public sealed class PayoutEvaluator : MonoBehaviour
{
    [System.Serializable]
    private class PayoutRule
    {
        public SymbolType symbol;
        public int multiplier;
    }

    [SerializeField]
    private List<PayoutRule> payoutRules = new()
    {
        new() { symbol = SymbolType.Cherry, multiplier = 2 },
        new() { symbol = SymbolType.Bell, multiplier = 5 },
        new() { symbol = SymbolType.Bar, multiplier = 10 },
        new() { symbol = SymbolType.Seven, multiplier = 20 }
    };

    private Dictionary<SymbolType, int> payoutTable;

    private void Awake()
    {
        payoutTable = new Dictionary<SymbolType, int>();

        foreach (PayoutRule rule in payoutRules)
            payoutTable[rule.symbol] = rule.multiplier;
    }

    /// <summary>
    /// Evaluates the payout for a given spin result and current bet.
    /// </summary>
    /// <param name="result">The result of the spin, containing the symbols that appeared.</param>
    /// <param name="currentBet">The current bet amount placed by the player.</param>
    /// <returns>The payout amount based on the spin result and current bet.</returns>
    public int Evaluate(SpinResult result, int currentBet)
    {
        SymbolType[] symbols = result.Symbols;

        if (symbols == null || symbols.Length == 0)
            return 0;

        if (!AllSymbolsMatch(symbols))
            return 0;

        if (!payoutTable.TryGetValue(symbols[0], out int multiplier))
            return 0;

        Debug.Log($"Payout: {multiplier}x for symbol {symbols[0]}");
        return currentBet * multiplier;
    }

    /// <summary>
    /// Makes sure all symbols in the array are the same, which is a common condition for winning in slot machines.
    /// </summary>
    /// <param name="symbols">An array of symbols to check.</param>
    /// <returns>True if all symbols match, false otherwise.</returns>
    private static bool AllSymbolsMatch(SymbolType[] symbols)
    {
        for (int i = 1; i < symbols.Length; i++)
        {
            if (symbols[i] != symbols[0])
                return false;
        }

        return true;
    }
}