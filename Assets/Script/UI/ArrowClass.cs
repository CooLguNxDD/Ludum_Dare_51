using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct Arrows
{
    private string Direction { get; set; }
    private GameObject Object { get; set; }

    public Arrows(string dir, GameObject obj)
    {
        Direction = dir;
        Object = obj;
    }
    public string getDirection()
    {
        return Direction;
    }
    public GameObject getObject()
    {
        return Object;
    }
}


