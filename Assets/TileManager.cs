using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.XR;
using UnityEditor.SceneManagement;
using System.Linq;

public class TileManager : MonoBehaviour {

    public static TileManager Instance { get; private set; } // Let's us grab this script from anywhere in the game

    public List<Tile> TileWall; //The Wall where you draw from
    public List<Tile> DeadWall; //The Wall containing the Dora, Ura-Dora, and Kan Tiles

    public List<Tile> Dora; //The Dora
    public List<Tile> UraDora; //The Hidden (Ura) Dora
    public List<Tile> KanTiles; //Draw from these when calling a CLOSED Kan... not used for open Kan!!!

    [Tooltip("East Seat on Turn 1")]
    public List<Tile> P1Hand; //The players hand...
    [Tooltip("South Seat on Turn 1")]
    public List<Tile> P2Hand;
    [Tooltip("West Seat on Turn 1")]
    public List<Tile> P3Hand;
    [Tooltip("North Seat on Turn 1")]
    public List<Tile> P4Hand;

    public GameObject handLocation;

    public List<Tile> DiscardPile; //Still in debugging phase... Discard Pile... where you discard tiles...

    public GameObject[] TilePrefabs;

    //Below is used to setup where the Dora will be places to align it with the camera!
    public GameObject DoraPlacement1;
    public GameObject DoraPlacement2;
    public GameObject DoraPlacement3;
    public GameObject DoraPlacement4;
    public GameObject DoraPlacement5;

    private Dictionary<string, GameObject> tilePrefabMap;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Prevent duplicates
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Keeps this code when scenes change... Remove if I dont need it in every scene

        InitializePrefabMap();
        InitializeWall();
        CreateDeadWall();
        MoveDoraTiles();

        MakeHand();

        //Anything below is for debugging purposes
        //Hand.Add(DrawTiles(TileWall)); //Example of drawing to hand when I create turns
        //DiscardTiles(Tile, Hand, DiscardPile); //Example of discarding... Both this and drawing happens in 1 turn unless a callout was made


        //   DrawAndDiscardDebug();
        DebuggingTest();
    }

    void InitializePrefabMap()
    {
        tilePrefabMap = new Dictionary<string, GameObject>();

        foreach (var prefab in TilePrefabs)
        {
            Tile tileScript = prefab.GetComponent<Tile>();
            if (tileScript != null)
            {
                string key = GetTileKey(tileScript.Suit, tileScript.Value, tileScript.IsRedFive);
                tilePrefabMap[key] = prefab;
            }
        }
    }

    string GetTileKey(string suit, int value, bool isRedFive)
    {
        return $"{suit}_{value}_{isRedFive}";
    }

    void InitializeWall()
    {
        TileWall = new List<Tile>();
        bool isRedFive;
        //Create the 136 tiles for the wall
        //First we inialize all the suits that follow the 1-9 pattern
        string[] suits = { "Characters", "Dots", "Bamboo" }; //This array should simplify how we fill the list

        foreach (string suit in suits)
        {
            for (int value = 1; value <= 9; value++) //Assigning the tiles 1-9 value
            {
                for (int x = 0; x < 4; x++) //Making sure we have 4 of each tile
                {
                    if (value == 5 && x == 0)
                    {
                        isRedFive = true;
                    }
                    else
                    {
                        isRedFive = false;
                    }

                    string key = GetTileKey(suit, value, isRedFive);

                    if (tilePrefabMap.TryGetValue(key, out GameObject prefab))
                    {
                        GameObject TileObject = Instantiate(prefab);
                        Tile tile = TileObject.GetComponent<Tile>();
                        TileWall.Add(tile);
                    }
                }
            }
        }

        string[] honors = { "East", "South", "West", "North", "White", "Red", "Green" }; //This array should simplify how we fill the list

        foreach (string honor in honors)
        {
            for (int x = 0; x < 4; x++) //Making sure we have 4 of each tile
            {
                string key = GetTileKey(honor, 0, false);

                if (tilePrefabMap.TryGetValue(key, out GameObject prefab))
                {
                    GameObject TileObject = Instantiate(prefab);
                    Tile tile = TileObject.GetComponent<Tile>();
                    TileWall.Add(tile);
                }
            }
        }

        Shuffle();
    }

    void Shuffle()
    {
        for (int i = TileWall.Count - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            Tile temp = TileWall[i];
            TileWall[i] = TileWall[j];
            TileWall[j] = temp;
        }

    }

    void CreateDeadWall()
    {
        int totalTiles = TileWall.Count - 1;
        for (int x = totalTiles; x > totalTiles - 14; x--)
        {
            DeadWall.Add(TileWall[x]);
            TileWall.Remove(TileWall[x]);
        }
        //Below created the Kan Tiles, Dora, And Ura-Dora from the DeadWall!!!
        for (int x = 0; x <= 3; x++)
        {
            KanTiles.Add(DeadWall[x]);
        }
        for (int x = 4; x <= 8; x++)
        {
            Dora.Add(DeadWall[x]);
        }
        for (int x = 9; x <= 13; x++)
        {
            UraDora.Add(DeadWall[x]);
        }

    } //From the dead wall I will create multiple arrays or lists being dora, ura-dora, and the kan tiles. Make the dead wall private after this as it should only be a temporary list used to simplify the other 3.

    void MoveDoraTiles()
    {
        Dora[0].transform.position = DoraPlacement1.transform.position;
        Dora[1].transform.position = DoraPlacement2.transform.position;
        Dora[2].transform.position = DoraPlacement3.transform.position;
        Dora[3].transform.position = DoraPlacement4.transform.position;
        Dora[4].transform.position = DoraPlacement5.transform.position;
    }

    void MakeHand() //And Set hands at table
    {
        int y = 0;
        //Player 1's Hand
        P1Hand = new List<Tile>();
        for (int x = 0; x < 13; x++)
        {
            P1Hand.Add(DrawTiles(TileWall));
        }
        SortHand(P1Hand);

        for(int x = 0; x < P1Hand.Count; x++)
        {
            P1Hand[x].transform.rotation = handLocation.transform.rotation;
            P1Hand[x].transform.position = handLocation.transform.position + new Vector3(y, 0, 0);
            y += 9;
        }
        y = 0;
        //Player 2's Hand
        P2Hand = new List<Tile>();
        for (int x = 0; x < 13; x++)
        {
            P2Hand.Add(DrawTiles(TileWall));
        }
        SortHand(P2Hand);

        //Player 3's Hand
        P3Hand = new List<Tile>();
        for (int x = 0; x < 13; x++)
        {
            P3Hand.Add(DrawTiles(TileWall));
        }
        SortHand(P3Hand);

        //Player 4's Hand
        P4Hand = new List<Tile>();
        for (int x = 0; x < 13; x++)
        {
            P4Hand.Add(DrawTiles(TileWall));
        }
        SortHand(P4Hand);
    }

    public void SortHand(List<Tile> Hands)
    {
        var sortedHand = Hands.OrderBy(Tile => Tile.Suit).ThenBy(Tile => Tile.Value).ToList();
        Hands.Clear();
        //Manually refilling the hand
        for (int x = 0; x < sortedHand.Count; x++)
        {
            Hands.Add(sortedHand[x]);
        }
    }//Sorts Hand.... Initializing this at start but add as a button later to re-sort if player manually sorts and changes mind.

    public Tile DrawTiles(List<Tile> wall) //This method will be reused for both player and A.I. interactions, to be implemented in Phase 2.
    {
        if(wall.Count == 0) 
        { 
            return null;    
        }
        Tile drawnTile = wall[0];
        wall.RemoveAt(0);
        return drawnTile;
    }

    public static void DiscardTiles(Tile tile, List<Tile> hand, List<Tile> discardPile) //tile will be the selected tile to discard... hand will be the Hand to discard from... discardPile is the DiscardPile List.
    {
        if(hand.Contains(tile))
        {
            hand.Remove(tile);
            discardPile.Add(tile);
        }
    }
   
    /*
    void DrawAndDiscardDebug() //The Code Works :)
    {
        Hand.Add(DrawTiles(TileWall));
        DiscardTiles(Hand[3], Hand, DiscardPile);
        UnityEngine.Debug.Log("Removed from hand: " + Hand[3]);
    }
    */ //Draw and Discard examples
    void DebuggingTest()
    {
        /*   for(int x = 0; x < TileWall.Count; x++)
           {
               UnityEngine.Debug.Log("Tile Wall: " + TileWall[x]);
           }
           for (int x = 0; x < DeadWall.Count; x++)
           {
               UnityEngine.Debug.Log("Dead Wall: " + DeadWall[x]);
           }
        */
    }


}