using UnityEngine;

public class Tile : MonoBehaviour
{

    public string Suit;
    public int Value;
    public bool IsRedFive;

    public void Initialize(string suit, int value, bool isRedFive)
    {
        Suit = suit;
        Value = value;
        IsRedFive = isRedFive;
    }
}
