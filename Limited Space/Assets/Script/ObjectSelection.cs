using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelection : MonoBehaviour
{
    public int Option;
    private Placement _placement;
    [SerializeField] 
    private List<GameObject> availableObjs;

    private void Awake()
    {
        _placement = GetComponent<Placement>();
    }


    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.E))
        // {
        //     GrabObject(Option);
        // }
    }

    public void Regrab()
    {
        GrabObject(Option);
    }

    public void GrabObject(int i)
    {
        GameObject obj = Instantiate(availableObjs[i], Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
        Option = i;
        _placement.PickUpObject(obj.transform);
    }
    
    
}
