public readonly struct BetChangeResult
{
    public int BetAmount { get; }
    public bool Success { get; }
    public string Message { get; }

    public BetChangeResult(int newBet, bool success, string message)
    {
        BetAmount = newBet;
        Success = success;
        Message = message;
    }
}
