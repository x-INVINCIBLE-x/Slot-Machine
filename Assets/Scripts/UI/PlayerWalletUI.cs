using System;
using TMPro;
using UnityEngine;

public class PlayerWalletUI : MonoBehaviour
{
    [SerializeField] private PlayerWallet playerWallet;
    [SerializeField] private TextMeshProUGUI balanceText;

    private void OnEnable()
    {
        playerWallet.OnBalanceChanged += HandleBalanceUpdate;
    }

    private void HandleBalanceUpdate(int newBalance)
    {
        balanceText.text = $"{newBalance}G";
    }

    private void OnDisable()
    {
        playerWallet.OnBalanceChanged -= HandleBalanceUpdate;
    }
}
