using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Placement : MonoBehaviour
{
    [SerializeField] 
    private bool isHolding;
    [SerializeField]
    private Transform objInHand;

    private Grid _grid;

    private void Start()
    {
        _grid = GetComponent<Grid>();
    }

    void Update()
    {
        if (isHolding && objInHand)
        {
            FollowCursor();
            
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                PlaceObject();
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                CancelPlacement();
            }
        }
    }

    void FollowCursor()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        objInHand.position = Grid.SnapToGrid(mousePos);    

        if (Input.GetKeyDown(KeyCode.Comma))
            Rotate(-1);
        if (Input.GetKeyDown(KeyCode.Period))
            Rotate(1);
        if (Input.GetKeyDown(KeyCode.R))
            Rotate(-1);
    }

    void CancelPlacement()
    {
        if (objInHand)
            Destroy(objInHand.gameObject);
        
        isHolding = false;
        objInHand = null;
    }
    
    void PlaceObject()
    {
        // check if can place
        PlaceableObject _placeableObject = objInHand.GetComponent<PlaceableObject>();

        if (!_placeableObject.isPlaceable)
        {
            // cant place
            return;
        }
        _placeableObject.Placed();
        
        isHolding = false;
        objInHand = null;
        
        
        if (_placeableObject.isWall)
        {
            gameObject.GetComponent<ObjectSelection>().Regrab();
        }
        _placeableObject.enabled = false;
    }

    public void PickUpObject(Transform obj)
    {
        CancelPlacement();
        
        isHolding = true;
        objInHand = obj;
    }

    void Rotate(int direction)
    {
        objInHand.Rotate(Vector3.back,90 * math.sign(direction));
    }
}
