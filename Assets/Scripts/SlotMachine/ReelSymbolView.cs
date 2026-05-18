using UnityEngine;

/// <summary>
/// This class represents a visual element for a symbol on the slot machine reel.
/// </summary>
public class ReelSymbolView : MonoBehaviour
{
    // The type of symbol this view represents.
    [field: SerializeField] public SymbolType Symbol { get; private set; }
}