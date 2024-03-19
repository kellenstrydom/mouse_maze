using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MouseBehaviour : MonoBehaviour
{
    [Serializable]
    public struct BlocksToCheese
    {
        [SerializeField] public int x;
        [SerializeField] public int y;
    }
    
    
    public Transform cheese;
    public BlocksToCheese toCheese;
    private Grid _grid;
    private float timer;
    private float stepTime = 1;

    private void Awake()
    {
        _grid = GameObject.Find("Grid").GetComponent<Grid>();

        transform.position = Grid.SnapToGrid(transform.position);
    }

    private void Update()
    {
        if (timer < stepTime)
            timer += Time.deltaTime;
        else
        {
            timer = 0;
            MoveToCheese();
        }
    }

    void MoveToCheese()
    {
        toCheese.x = (int)(cheese.position.x - transform.position.x);
        toCheese.y = (int)(cheese.position.y - transform.position.y);

        if (math.abs(toCheese.x) > math.abs(toCheese.y))
        {
            transform.up = Vector3.right * math.sign(toCheese.x);
            // Cast a ray forward.
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 1f);
            if (hit.collider != null)
            {
                Debug.Log(hit.collider);
                WallReached();
            }
            else
            {
                Vector2 newPos = transform.position + (transform.up * Grid.rowLength);
                transform.position = Grid.SnapToGrid(newPos);
            }
        }
        else
        {
            transform.up = Vector3.up * math.sign(toCheese.y);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 1f);
            if (hit.collider != null)
            {
                Debug.Log(hit.collider);
                WallReached();
            }
            else
            {
                Vector2 newPos = transform.position + (transform.up * Grid.columnLength);
                transform.position = Grid.SnapToGrid(newPos);
            }
        }
    }

    void WallReached()
    {
        if (math.abs(toCheese.x) <= math.abs(toCheese.y))
        {
            transform.up = Vector3.right * math.sign(toCheese.x);
            // Cast a ray forward.
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 1f);
            if (hit.collider != null)
            {
                Debug.Log(hit.collider);
            }
            else
            {
                Vector2 newPos = transform.position + (transform.up * Grid.rowLength);
                transform.position = Grid.SnapToGrid(newPos);
            }
        }
        else
        {
            transform.up = Vector3.up * math.sign(toCheese.y);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 1f);
            if (hit.collider != null)
            {
                Debug.Log(hit.collider);
            }
            else
            {
                Vector2 newPos = transform.position + (transform.up * Grid.columnLength);
                transform.position = Grid.SnapToGrid(newPos);
            }
        }
    }
}
