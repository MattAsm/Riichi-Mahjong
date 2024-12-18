using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using JetBrains.Annotations;

public class BrainDeadForDevTest : MonoBehaviour
{
    public GameObject tileManager;

    public int playerNumber;
    public List<Tile> hand;
    public bool handMade = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var theScript = tileManager.GetComponent<TileManager>();
        hand = theScript.gameObject.GetComponent<TileManager>().MakeHand();
        Debug.Log(hand.Count);
    }

    // Update is called once per frame
    void Update()
    {
    /*    
         if(GetComponent<GameManager>().isPlayerTurn == true)
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
