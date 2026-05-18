using UnityEngine;

public class BetControls : MonoBehaviour
{
    [SerializeField] private BetController betController;

    public void IncreaseBet(int betAmount)
    {
        var result = betController.AddBet(betAmount);
        Debug.Log(result.Message);
    }

    public void DecreaseBet(int betAmount)
    {
        var result = betController.SubtractBet(betAmount);
        Debug.Log(result.Message);
    }

    public void SetBet(int betAmount)
    {
        var result = betController.SetBet(betAmount);
        Debug.Log(result.Message);
    }
}
