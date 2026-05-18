public enum SymbolType
{
    Seven,
    Bar,
    Bell,
    Cherry,
}

/// <summary>
/// Contains the result of a slot machine spin, 
/// including the symbols that appeared on the reels.
/// </summary>
public readonly struct SpinResult
{
    public SymbolType[] Symbols { get; }

    public SpinResult(SymbolType[] symbols)
    {
        Symbols = symbols;
    }
}