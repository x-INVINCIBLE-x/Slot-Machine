using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class ErrorDisplayUI : MonoBehaviour
{
    [SerializeField] private BetController betController;
    [SerializeField] private GameObject messageDisplayBox;
    [SerializeField] private TextMeshProUGUI textBox;
    [SerializeField] private float displayDuration = 2f;

    private Coroutine messageRoutine = null;

    private void OnEnable()
    {
        betController.OnBetUpdate += HandleBetUpdate;
    }

    private void Start()
    {
        messageDisplayBox.SetActive(false);
    }

    private void HandleBetUpdate(BetChangeResult result)
    {
        if (result.Success)
        {
            if (messageRoutine != null)
            {
                StopCoroutine(messageRoutine);
                messageRoutine = null;
            }

            return;
        }
        
        messageRoutine = StartCoroutine(DisplayMessageRoutine(result.Message));
    }

    private IEnumerator DisplayMessageRoutine(string message)
    {
        messageDisplayBox.SetActive(true);
        textBox.text = message;
        textBox.enabled = true;

        yield return new WaitForSeconds(displayDuration);

        messageDisplayBox.SetActive(false);
        textBox.enabled = false;
        messageRoutine = null;
    }

    private void OnDisable()
    {
       betController.OnBetUpdate -= HandleBetUpdate; 
    }
}
