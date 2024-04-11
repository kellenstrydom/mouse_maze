using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[Serializable]
public class Pheromone : MonoBehaviour
{
    
    public int scentValue;
    [SerializeField] private List<Pheromone> adjPheromones = new List<Pheromone>();
    private Grid grid;
    public int newVal;
    public bool isOnFood;
    public bool isOnWall;

    private void Awake()
    {
        transform.position = Grid.SnapToGrid(transform.position);
        grid = GameObject.Find("Grid").GetComponent<Grid>();
        scentValue = -1;
    }

    private void Start()
    {
        GetAdjPheromones();
        //StartCoroutine(LateStart());
        //if (isOnFood) TrailUpdate();
    }

    //waits a frame before executing
    private IEnumerator LateStart()
    {
        yield return null;
        GetAdjPheromones();
    }
    void GetAdjPheromones()
    {
        transform.up = Vector3.up;

        for (int i = 0; i < 4; i++)
        {
            int pheremoneMask = 1 << 9;
            RaycastHit2D hit = Physics2D.Raycast(transform.position + transform.up*.5f, transform.up, .5f, pheremoneMask);
            //111111111111111111111
            //000000000100000000
            if (hit)
            {
               //Debug.Log(hit.collider);
                adjPheromones.Add(hit.collider.GetComponent<Pheromone>());
            }
                
        
            transform.Rotate(Vector3.back, 90);
        }
    }

    public List<Pheromone> TrailUpdate()
    {
        if (newVal < 0)
        {
            scentValue = 0;
            newVal = 1;
        }
        else
            scentValue = newVal;
        List<Pheromone> result = new List<Pheromone>();
        
        if (isOnWall)
        {
            scentValue = 256;
            return result;
        }
        
        if (scentValue > Grid.maxScent) Grid.maxScent = scentValue;

        
        foreach (Pheromone other in adjPheromones)
        {
            if (other.setNewVal(scentValue+1))
                result.Add(other);
        }

        return result;
    }

    public bool setNewVal(int val)
    {
        if (newVal > 0 ) return false;
        newVal = val;
        return true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            Debug.Log("off food");
            isOnFood = false;
            grid.needsUpdate = true;
        }
        
        if (other.CompareTag("Wall"))
        {
            Debug.Log("on wall");
            isOnWall = false;
            //grid.needsUpdate = true; 
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        
        if (other.CompareTag("Wall"))
        {
            Debug.Log("on wall");
            isOnWall = true;
        }
        
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Food"))
        {
            Debug.Log("on food");
            isOnFood = true;
            grid.needsUpdate = true;
        }

        if (col.CompareTag("Wall"))
        {
            Debug.Log("on wall");
            isOnWall = true;
            //grid.needsUpdate = true; 
        }
    }
}
