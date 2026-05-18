using UnityEngine;

public class TestStopper : MonoBehaviour
{
    public ReelView reelView;
    public SymbolType symbol;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            reelView.StartSpin();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            reelView.StopAt(symbol);
        }
    }
}
