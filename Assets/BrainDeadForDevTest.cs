using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using JetBrains.Annotations;

public class BrainDeadForDevTest : MonoBehaviour
{

    TileManager tm;
    public int playerNumber;

    public List<Tile> hand;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    { 
        tm = GetComponent<TileManager>();
        hand = new List<Tile>();
        tm.MakeHand();
    }

    // Update is called once per frame
    void Update()
    {
    /*    
     *    if(GetComponent<GameManager>().isPlayerTurn == true)
        {
            turn();
        }
    */
    }

    public void turn()
    {
       // GetComponent<TileManager>().DrawTiles();
    }
}
