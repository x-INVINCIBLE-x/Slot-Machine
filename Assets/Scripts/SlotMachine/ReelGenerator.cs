using UnityEngine;

/// <summary>
/// Provides functionality to generate random spin results for slot machine reels using a predefined set of symbols.
/// </summary>
public class ReelGenerator : MonoBehaviour
{
    [SerializeField] private SymbolType[] availableSymbols;

    /// <summary>
    /// Generates a random spin result for the specified number of reels by selecting symbols from the available set.
    /// </summary>
    /// <param name="reelCount">The number of reels to generate symbols for.</param>
    /// <returns>A SpinResult containing the randomly selected symbols for each reel.</returns>
    public SpinResult GenerateSpin(int reelCount)
    {
        SymbolType[] results = new SymbolType[reelCount];

        for (int i = 0; i < reelCount; i++)
        {
            int randomIndex = Random.Range(0, availableSymbols.Length);

            results[i] = availableSymbols[randomIndex];
        }

        return new SpinResult(results);
    }
}