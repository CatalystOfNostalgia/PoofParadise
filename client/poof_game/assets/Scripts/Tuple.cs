﻿using UnityEngine;
using System.Collections;
using System;

/* Helper class that is literally just a 2-tuple */

[Serializable]
public class Tuple {

    public int x { get; set; }
	public int y { get; set; }
	
	public Tuple (int x, int y) {
        this.x = x;
        this.y = y;
	}
}
