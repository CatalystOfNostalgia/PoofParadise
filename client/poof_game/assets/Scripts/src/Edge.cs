using System;
using UnityEngine;

/**
 * This is a concept class that could be used
 * to map edges to tiles for poof movement
 */
public class Edge {

    public enum Side { up, down, left, right };

    public Side side;

    // Points or tuples?
    public Edge()
	{
	}
}
