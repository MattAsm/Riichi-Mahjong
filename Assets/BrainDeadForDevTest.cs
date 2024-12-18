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
        GetHand();
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

    private void GetHand()
    {
        var theScript = tileManager.GetComponent<TileManager>();
        hand = theScript.gameObject.GetComponent<TileManager>().MakeHand();
        int y = -60;
        for (int x = 0; x < hand.Count; x++)
        {            
            hand[x].transform.parent = this.transform;
            hand[x].transform.localPosition = Vector3.zero;
            hand[x].transform.localRotation = Quaternion.identity;

            hand[x].transform.localRotation *= Quaternion.Euler(-90, 0, 0);
            hand[x].transform.localPosition += new Vector3(y, -3.2f, 70);
            y += 9;
        }
    } //Gets hand from TileManager. Makes tiles Child of object the script is attached to and sets them based on Seat Orientation

    public void turn()
    {
       // GetComponent<TileManager>().DrawTiles();
    }
}
