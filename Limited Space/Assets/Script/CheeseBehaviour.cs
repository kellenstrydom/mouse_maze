using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseBehaviour : MonoBehaviour
{
    private Grid _grid;

    private void Awake()
    {
        _grid = GameObject.Find("Grid").GetComponent<Grid>();

        transform.position = Grid.SnapToGrid(transform.position);
    }

}
