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
        
        if (scentValue > Grid.maxScent) Grid.maxScent = scentValue;

        List<Pheromone> result = new List<Pheromone>();
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
        Debug.Log("off food");
        isOnFood = false;
        grid.needsUpdate = true;    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("on food");
        isOnFood = true;
        grid.needsUpdate = true;
        //TrailUpdate();
    }
}
