using UnityEngine;
using System.Collections;

/* Helper class that is literally just a 2-tuple */

public class Tuple {

	private int x;
	private int y;
	
	public Tuple (int x, int y) {
		setX(x);
		setY(y);
	}
	
	
	public void setX (int newX) {
		x = newX;
	}
	public void setY (int newY) {
		y = newY;
	}
	
	public int getX () {
		return x;
	}
	public int getY () {
		return y;
	}
}
