using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class SaveState : MonoBehaviour {

    // Allows the scene to access this object without searching for it
    public static SaveState state;

    // player data
    public int userID { get; set; }
    public int userLevel { get; set; }
    public int userExperience { get; set; }
    public int hqLevel { get; set; }
    public int poofCount { get; set; }
    public string name { get; set; }
    public string username { get; set; }
    public string userEmail { get; set; }
    public string password { get; set; }

    // List game state variables here

    // resources
    public int fire { get; set; }
    public int air { get; set; }
    public int water { get; set; }
    public int earth { get; set; }
    public int maxFire { get; set; }
    public int maxAir { get; set; }
    public int maxWater { get; set; }
    public int maxEarth { get; set; }

    // elemari
    public int fireEle { get; set; }
    public int waterEle { get; set; }
    public int earthEle { get; set; }
    public int airEle { get; set; }

    // buildings
    public Dictionary<Tuple, Building> resourceBuildings { get; set; }
    public Dictionary<Tuple, Building> decorativeBuildings { get; set; }
    
    /**
     * Produces a singleton on awake
     */
    public void Awake() {
        
        if (state == null) {
            DontDestroyOnLoad(gameObject);
            state = this;
        } else if (state != this) {
            Destroy(gameObject);
        }
        
        // set the fields until we can load
        state.fire = 0;
        state.resourceBuildings = new Dictionary<Tuple, Building>();
        fireEle = 1;
        earthEle = 1;
        waterEle = 1;
        airEle = 1;
        poofCount = 3;
        
    }

    
    /**
     * Saves all relevant data to a local file
     * NOTE: This does not perform a write back but
     * rather creates a fresh file every time.
     */
    public void Save() {
        
        // Dumps JSON to text file
        System.IO.File.WriteAllText(Application.persistentDataPath + "/save_state.dat", this.jsonify());

        //
    }
    
    /**
     * Loads all relevant data from a file
     */
    public void Load() {
        if (File.Exists (Application.persistentDataPath + "/save_state.dat")) {
            string json = System.IO.File.ReadAllText(Application.persistentDataPath + "/save_state.dat");
            
            //PlayerData data = JSON.Deserialize<PlayerData>(json);
            // Reassign all variables here
            // ie: this.setHealth(data.getHealth());
            //GetPlayerData(data);
        }
    }

    /**
     * Pushes player data to server
     */
    public void PushToServer() {
        
        Debug.Log ("data.ToJSON");
        string buildingJSON = this.jsonify();


        // TODO Send JSON to server

        Debug.Log("buildingJSON = " + buildingJSON);
        GetHTTP.toSave (buildingJSON);

    }
    
    /**
     * Pulls player data from server
     */
    public void PullFromServer() {
        
        // get the JSON from the server
        String userInfo = GetHTTP.login("ted1", "password");

        loadJSON (userInfo);
        
    }

    // turns the save data into a JSON String
    public String jsonify() {
        
        String jsonPlayerData = "{ ";

        jsonPlayerData += "\"name\": \"" + name + "\", ";
        jsonPlayerData += "\"level\": \"" + userLevel + "\", ";
        jsonPlayerData += "\"email\": \"" + userEmail + "\", ";
        jsonPlayerData += "\"user_id\": \"" + userID + "\", ";
        jsonPlayerData += "\"username\": \"" + username + "\", ";
        jsonPlayerData += "\"password\": \"" + password + "\", ";
        jsonPlayerData += "\"experience\": \"" + userExperience + "\", ";
        jsonPlayerData += "\"hq_level\": \"" + hqLevel + "\", ";
        jsonPlayerData += "\"fire\": \"" + fire + "\", ";
        jsonPlayerData += "\"air\": \"" + air + "\", ";
        jsonPlayerData += "\"water\": \"" + water + "\", ";
        jsonPlayerData += "\"earth\": \"" + earth + "\", ";
        jsonPlayerData += "\"maxFire\": \"" + maxFire + "\", ";
        jsonPlayerData += "\"maxWater\": \"" + maxWater + "\", ";
        jsonPlayerData += "\"maxAir\": \"" + maxAir + "\", ";
        jsonPlayerData += "\"maxEarth\": \"" + maxEarth + "\", ";
        jsonPlayerData += "\"fireElements\": \"" + fireEle + "\", ";
        jsonPlayerData += "\"waterElements\": \"" + waterEle + "\", ";
        jsonPlayerData += "\"earthElements\": \"" + earthEle + "\", ";
        jsonPlayerData += "\"airElements\": \"" + airEle + "\", ";
        jsonPlayerData += "\"resource_buildings\": [ ";
        
        foreach ( KeyValuePair<Tuple, Building> entry in resourceBuildings) {
            jsonPlayerData += "{ ";
            jsonPlayerData += "\"x_coordinate\": \"" + entry.Key.x + "\", ";
            jsonPlayerData += "\"y_coordinate\": \"" + entry.Key.y + "\", ";
            jsonPlayerData += "\"size\": \"" + entry.Value.size + "\" ";
            jsonPlayerData += "},";
        }
        
        jsonPlayerData = jsonPlayerData.TrimEnd (',');
        
        jsonPlayerData += "], ";
        jsonPlayerData += "\"decorative_buildings\": []";
        jsonPlayerData += "}";
        
        
        return jsonPlayerData;
        
    }

    // This method populates the save data with data from a json string
    private void loadJSON(String json){

        JSONArray loadedResourceBuildings;
        JSONArray loadedDecorativeBuildings;

        JSONNode data = JSON.Parse (json);

        name            = data ["name"]; 
        username        = data ["username"];
        userEmail       = data ["email"];
        password        = data ["password"];
        userID          = data ["user_id"].AsInt;
        userLevel       = data ["level"].AsInt;
        userExperience  = data ["experience"].AsInt;
        hqLevel         = data ["headquarters_level"].AsInt;
        fire            = data ["fire"].AsInt;
        water           = data ["water"].AsInt;
        earth           = data ["earth"].AsInt;
        air             = data ["air"].AsInt;
        maxFire         = data ["max_fire"].AsInt;
        maxWater        = data ["max_water"].AsInt;
        maxEarth        = data ["max_earth"].AsInt;
        maxAir          = data ["max_air"].AsInt;
        fireEle         = data ["fire_ele"].AsInt;
        waterEle        = data ["water_ele"].AsInt;
        earthEle        = data ["earth_ele"].AsInt;
        airEle          = data ["air_ele"].AsInt;
        poofCount       = data ["poof_count"].AsInt;


        // load the buildings
        loadedResourceBuildings = data ["resource_buildings"].AsArray;
        loadedDecorativeBuildings = data ["decorative_buildings"].AsArray;

        foreach (JSONNode building in loadedResourceBuildings) {
            int x = building["position_x"].AsInt;
            int y = building["position_y"].AsInt;

            Building newBuilding;

            switch (building["building_info_id"].AsInt) {

                case 1:
                    newBuilding = BuildingManager.manager.fireTreeLevel1;
                    break;
                case 2:
                    newBuilding = BuildingManager.manager.fireTreeLevel2;
                    break;
                case 3:
                    newBuilding = BuildingManager.manager.pondLevel1;
                    break;
                case 4:
                    newBuilding = BuildingManager.manager.pondLevel2;
                    break;
                case 5:
                    newBuilding = BuildingManager.manager.windmillLevel1;
                    break;
                case 6:
                    newBuilding = BuildingManager.manager.windmillLevel2;
                    break;
                case 7:
                    newBuilding = BuildingManager.manager.caveLevel1;
                    break;
                case 8:
                    newBuilding = BuildingManager.manager.caveLevel2;
                    break;
                default:
                    newBuilding = null;
                    break;
            }

            resourceBuildings.Add(new Tuple(x, y), newBuilding);
        }

    }

}
