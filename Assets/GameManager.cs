using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; } // Let's us grab this script from anywhere in the game

    public int PlayerScore { get; private set; }

    //Check if below is necessary, research says it isn't
    //public string Suit { get; set; }
    //public int Value { get; set; }
    //public bool IsRedFive { get; set; }


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
/*   ADD WHEN NECESSARY
    public void AddScore(int points)
    {
        PlayerScore += points;
        Debug.Log($"Score updated: {PlayerScore}");
    }
*///    ADD WHEN NECESSARY
}
