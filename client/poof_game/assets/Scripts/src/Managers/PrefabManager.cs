using UnityEngine;
using System.Linq;

public class PrefabManager : Manager {

    // Static reference to this gameobject
    public static PrefabManager prefabManager;

    // Contains the list of resource buildings as generated from the folder
    public ResourceBuilding[] resourceBuildings { get; set; }

    // Use this for initialization
    override public void Start () {
        if (prefabManager == null)
        {
            DontDestroyOnLoad(gameObject);
            prefabManager = this;
        }
        else if (prefabManager != this)
        {
            Destroy(gameObject);
        }

        GeneratePrefabLists();
    }
	
    private void GeneratePrefabLists()
    {
        resourceBuildings = Resources.LoadAll("Prefabs/Buildings", typeof(ResourceBuilding)).Cast<ResourceBuilding>().ToArray();
    }

    /**
     * Generates building object
     *
     * Deprecated -> Still using prefabs but generating list dynamically
     * instead of storing references
     */
    private Building GenerateBuildingObject(int size, string name, int fireCost, int waterCost, int earthCost, int airCost)
    {
        // This line creates an object -> We just want to generate a prefab
        GameObject obj = new GameObject();
        obj.name = name;

        obj.AddComponent<SpriteRenderer>();
        //obj.GetComponent<SpriteRenderer>().sprite = resourceBuildingSprites[1];

        obj.AddComponent<ResourceBuilding>();
        Building ret = obj.transform.GetComponent<ResourceBuilding>();
        ret.size = size;
        ret.buildingName = name;
        ret.fireCost = fireCost;
        ret.waterCost = waterCost;
        ret.earthCost = earthCost;
        ret.airCost = airCost;

        Destroy(obj);

        return ret;
    }

    /**
     * Builds a sprite list
     *
     * Deprecated -> Use if you wish to generate your own prefabs
     */
    private void GenerateSpriteList(Object[] objs, Sprite[] outList)
    {
        for (int i = 0; i < objs.Length; i++)
        {
            System.Type type = objs[i].GetType();

            if (type == typeof(UnityEngine.Texture2D))
            {

                Texture2D tex = objs[i] as Texture2D;

                Sprite newSprite = Sprite.Create(objs[i] as Texture2D, new Rect(0f, 0f, tex.width, tex.height), Vector2.zero);

                outList[i] = newSprite;
            }
        }
    }
}
