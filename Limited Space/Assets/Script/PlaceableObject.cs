using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableObject : MonoBehaviour
{
    public bool isPlaceable = true;

    public void Placed()
    {
        Collider2D[] collider2Ds = GetComponents<Collider2D>();
        foreach (Collider2D col in collider2Ds)
        {
            col.isTrigger = false;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        isPlaceable = false;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isPlaceable = true;
    }
}
