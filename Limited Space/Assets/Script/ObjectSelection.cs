using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelection : MonoBehaviour
{
    public GameObject Option;
    private Placement _placement;

    private void Awake()
    {
        _placement = GetComponent<Placement>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GrabObject(Option);
        }
    }

    void GrabObject(GameObject selectedObject)
    {
        GameObject obj = Instantiate(selectedObject, Vector3.zero, Quaternion.identity);
        
        _placement.PickUpObject(obj.transform);
    }
    
    
}
