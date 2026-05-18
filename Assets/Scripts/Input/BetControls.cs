using UnityEngine;

/// <summary>
/// Used to control the player's bet amount through UI buttons.
/// </summary>
public class BetControls : MonoBehaviour
{
    [SerializeField] private BetController betController;

    // Increase the bet by a specified amount
    public void IncreaseBet(int betAmount)
    {
        BetChangeResult result = betController.AddBet(betAmount);
        Debug.Log(result.Message);
    }
    
    // Decrease the bet by a specified amount
    public void DecreaseBet(int betAmount)
    {
        BetChangeResult result = betController.SubtractBet(betAmount);
        Debug.Log(result.Message);
    }

    // Set the bet to a specific amount
    public void SetBet(int betAmount)
    {
        BetChangeResult result = betController.SetBet(betAmount);
        Debug.Log(result.Message);
    }
}
