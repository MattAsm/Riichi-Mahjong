using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; } // Let's us grab this script from anywhere in the game

    public int PlayerScore { get; private set; }

    private bool isPlayerTurn;

    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Prevent duplicates
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Keeps this code when scenes change
    }

    public void AddScore(int points)
    {
        PlayerScore += points;
        UnityEngine.Debug.Log($"Score updated: {PlayerScore}");
    }

}
