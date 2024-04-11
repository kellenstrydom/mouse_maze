using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableObject : MonoBehaviour
{
    public bool isPlaceable = true;
    public bool isWall;

    public void Placed()
    {
        if (!isWall) return;
        Collider2D[] collider2Ds = GetComponents<Collider2D>();
        foreach (Collider2D col in collider2Ds)
        {
            col.isTrigger = false;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Food") || col.CompareTag("Wall"))
            isPlaceable = false;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Food") || other.CompareTag("Wall"))
            isPlaceable = true;
    }
}
