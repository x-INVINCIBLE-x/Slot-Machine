using System;
using TMPro;
using UnityEngine;

public class BetUI : MonoBehaviour
{
    [SerializeField] private BetController betController;

    [SerializeField] private TextMeshProUGUI currentBetText;

    private void OnEnable()
    {
        betController.OnBetChanged += HandleBetUpdate;
    }

    private void HandleBetUpdate(BetChangeResult result)
    {
        currentBetText.text = $"{result.BetAmount}G";
    }

    private void OnDisable()
    {
        betController.OnBetChanged -= HandleBetUpdate;
    }
}
