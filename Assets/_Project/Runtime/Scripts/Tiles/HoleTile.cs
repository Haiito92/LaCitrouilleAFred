using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleTile : Tile
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Maybe add here a condition to check that its the player that is coming in the collider and not something else
        //OnTriggerEnteredEvent?.Invoke();
    }
}
