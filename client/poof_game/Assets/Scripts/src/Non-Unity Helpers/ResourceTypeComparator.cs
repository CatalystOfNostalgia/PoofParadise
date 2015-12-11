using UnityEngine;
using System.Collections;
using System;
//sort this shit to fire, water, air, earth
public class ResourceTypeComparator : IComparer
{
    public int Compare(object x, object y)
    {

        return value((Texture2D)x) - value((Texture2D)y);
    }

    private int value(Texture2D x)
    {
        if (x.name == "Icon_Resoure_Fire")
        {
            return 1;
        }

        if (x.name == "Icon_Resoure_Water")
        {
            return 2;
        }

        if (x.name == "Icon_Resoure_Air")
        {
            return 3;
        }

        if (x.name == "Icon_Resoure_Earth")
        {
            return 4;
        }
        return 0;
    }
}
