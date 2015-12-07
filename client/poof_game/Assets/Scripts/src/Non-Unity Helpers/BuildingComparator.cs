using System;
using System.Collections;

public class BuildingIDComparator : IComparer
{
    public int Compare(object x, object y)
    {
        return ((Building)x).ID - ((Building)y).ID;
    }
}
