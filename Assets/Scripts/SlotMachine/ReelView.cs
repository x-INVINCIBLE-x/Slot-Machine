using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class manages the visual representation of a single reel in the slot machine.
/// </summary>
public class ReelView : MonoBehaviour
{
    [SerializeField] private SymbolType[] reelSymbols;
    [SerializeField] private float spinSpeed = 2500f;

    // Event triggered when the reel has stopped spinning, providing a reference to the ReelView that stopped.
    public event Action<ReelView> OnSpinStopped;

    private RectTransform rectTransform;

    private float symbolHeight;
    private float loopHeight;

    private int originalSymbolCount;

    private float spinPosition;

    private bool isSpinning;

    private bool stopRequested;
    private int targetSymbolIndex;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        VerticalLayoutGroup layout = GetComponent<VerticalLayoutGroup>();
        RectTransform firstChild = rectTransform.GetChild(0) as RectTransform;

        symbolHeight = firstChild.rect.height + layout.spacing;
        originalSymbolCount = rectTransform.childCount / 3;
        loopHeight = symbolHeight * originalSymbolCount;
    }

    /// <summary>
    /// Spins the reel by updating its position based on the spin speed and time.
    /// </summary>
    private void Update()
    {
        if (!isSpinning)
            return;

        spinPosition -= spinSpeed * Time.deltaTime;

        float wrappedY = spinPosition % loopHeight;

        Vector2 position = rectTransform.anchoredPosition;

        position.y = wrappedY;

        rectTransform.anchoredPosition = position;

        CheckStop();
    }

    /// <summary>
    /// Starts spinning the reel.
    /// </summary>
    public void StartSpin()
    {
        isSpinning = true;
    }

    /// <summary>
    /// Checks if a stop has been requested and if the reel has reached the target symbol index.
    /// Snaps the reel to the target symbol position and triggers the OnSpinStopped event when the reel has stopped spinning.
    /// </summary>
    private void CheckStop()
    {
        if (!stopRequested)
            return;

        float normalizedY = Mathf.Abs(rectTransform.anchoredPosition.y);

        int currentIndex = Mathf.RoundToInt(normalizedY / symbolHeight) % originalSymbolCount;

        if (currentIndex == targetSymbolIndex)
        {
            Vector2 position = rectTransform.anchoredPosition;

            position.y = -(targetSymbolIndex * symbolHeight);

            rectTransform.anchoredPosition = position;

            isSpinning = false;
            stopRequested = false;

            OnSpinStopped?.Invoke(this);
        }
    }

    /// <summary>
    /// Stops the reel at the specified symbol by calculating the target symbol index and
    /// setting the stopRequested flag to true.
    /// </summary>
    /// <param name="symbol">The symbol at which the reel should stop.</param>
    public void StopAt(SymbolType symbol)
    {
        targetSymbolIndex = GetSymbolIndex(symbol);

        stopRequested = true;
    }

    /// <summary>
    /// Get the index of the specified symbol in the reelSymbols array.
    /// </summary>
    /// <param name="symbol"></param>
    /// <returns></returns>
    private int GetSymbolIndex(SymbolType symbol)
    {
        for (int i = 0; i < reelSymbols.Length; i++)
        {
            if (reelSymbols[i] == symbol)
            {
                return originalSymbolCount - 1 - i;
            }
        }

        return 0;
    }
}