using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [Serializable]
    public struct Bounds
    {
        public int left;
        public int right;
        public int up;
        public int down;
    }
    
    public static int maxScent = 0;
    public static float columnLength = 1;
    public static float rowLength = 1;
    public Bounds bounds;

    public static bool canPLay = false;

    public GameObject scent;

    public bool needsUpdate;

    public List<Pheromone> allPheremones = new List<Pheromone>();

    public GameObject SelectPanel;

    private void Awake()
    {
        CreateScentGrid();
    }

    private void Update()
    {
        if (needsUpdate)
        {
            //UpdateMax();
            updatePheromones();
            Debug.Log(Grid.maxScent);
            needsUpdate = false;
        }
    }

    public void UpdateMax()
    {
        foreach (var pheromone in allPheremones)
        {
            if (pheromone.scentValue > maxScent) maxScent = pheromone.scentValue;
        }
    }

    void CreateScentGrid()
    {
        
        for (int i = bounds.left; i < bounds.right; i++)
        {
            for (int j = bounds.down; j < bounds.up; j++)
            {
                GameObject temp = Instantiate(scent, SnapToGrid(new Vector2(i, j)),Quaternion.identity);
                allPheremones.Add(temp.GetComponent<Pheromone>());
            }
        }
    }
    
    static public Vector2 SnapToGrid(Vector2 pos)
    {
        Vector2 gridPos;
        gridPos.x = math.round(pos.x / columnLength) * columnLength;
        gridPos.y = math.round(pos.y / rowLength) * rowLength;

        return gridPos;
    }

    public void updatePheromones()
    {
        List<Pheromone> queue = new List<Pheromone>();

        maxScent = 0;
        
        foreach (Pheromone pheromone in allPheremones)
        {
            pheromone.newVal = -1;
            if (pheromone.isOnFood)
            {
                //Debug.Log(pheromone);
                queue.Add(pheromone);
            }
        }

        while (queue.Count > 0)
        {
            //Debug.Log("do");
            queue.AddRange(queue[0].TrailUpdate());
            queue.RemoveAt(0);
        }

        
    }
    public void PlayPause()
    {
        canPLay = !canPLay;
        SelectPanel.SetActive(!canPLay);
        if (canPLay)
            updatePheromones();
        
    }

    public void Quit()
    {
        Application.Quit();
    }
    
}
