using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScentBehaviour : MonoBehaviour
{
    private float timer;
    [SerializeField]
    private float stepTime = 1;
    [SerializeField]
    private Transform dir;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = Grid.SnapToGrid(transform.position);
        dir = GameObject.Find("Grid").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Grid.canPLay) return;
        
        if (timer < stepTime)
            timer += Time.deltaTime;
        else
        {
            timer = 0;
            MoveTowardsFood();
        }
    }

    void MoveTowardsFood()
    {
        dir.up = Vector3.up;
        int lowest = 256;

        string output = null;

        for (int i = 0; i < 4; i++)
        {
            int pheromoneMask = 1 << 9;
            RaycastHit2D hit = Physics2D.Raycast(transform.position + dir.up*.5f, dir.up, 1f, pheromoneMask);
            if (hit)
            {
                int checkScent = hit.collider.GetComponent<Pheromone>().scentValue;
                output += $"{checkScent}, ";
                if (checkScent < lowest)
                {
                    lowest = checkScent;
                    transform.up = dir.up;
                }
            }
            dir.Rotate(Vector3.back, 90);
        }

        Debug.Log(output);
        transform.position = Grid.SnapToGrid(transform.position + transform.up *1f);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Food"))
        {
            col.GetComponent<CheeseBehaviour>().Eat();
        }
    }
}
