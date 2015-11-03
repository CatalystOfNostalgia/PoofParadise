using UnityEngine;
using System.Collections;
using System;

public abstract class Building : MonoBehaviour {

    private bool selected { get; set; }
    private bool placed { get; set; }
    public bool created { get; set; }
    public int size { get; set; } // All buildings are square - this is determined by side size; e.g. a 3x3 building is size 3

    // Use this for initialization
    protected virtual void Start()
    {
        created = false;
        selected = true;
        placed = false;
        size = 1;
    }
}
