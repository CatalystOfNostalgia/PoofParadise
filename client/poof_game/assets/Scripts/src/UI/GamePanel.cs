using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;

/**
 * An abstract class used to handle basic 
 * UI panel operation - All game panels should 
 * extend this
 */
public abstract class GamePanel : MonoBehaviour {

    public bool windowState { get; set; }

    abstract public void Start();
    abstract public void GeneratePanel();

    /**
     * Searches for a button in a list
     * Removes the listeners on that button
     * Adds a listener to that button
     */
    public void FindAndModifyButton(string name, Button[] list, UnityAction method)
    {
        // Runs a search for a button by name
        int index = FindUIElement(name, list);

        // Lets the user know that their button doesn't exist
        if (index == -1)
        {
            Debug.LogError("FindAndModifyButton failed to find " + name + " in button list");
            return;
        }

        // Removes all listeners and adds functionality
        list[index].onClick.RemoveAllListeners();
        list[index].onClick.AddListener(method);
    }

    /**
     * Returns a button by name
     */
    public int FindUIElement(string name, Button[] list)
    {
        for (int i = 0; i < list.Length; i++)
        {
            if (list[i].name == name)
            {
                return i;
            }
        }
        return -1;
    }

    /**
     * Returns a slider by name
     */
    public int FindUIElement(string name, Slider[] list)
    {
        for (int i = 0; i < list.Length; i++)
        {
            if (list[i].name == name)
            {
                return i;
            }
        }
        return -1;
    }

    /**
     * Provides the list of buttons for this panel
     */
    public Button[] RetrieveButtonList(string path)
    {
        List<Button> list = new List<Button>();
        foreach (Transform t in this.transform.Find(path))
        {
            list.Add(t.GetComponent<Button>());
        }
        return list.ToArray();
    }

    /**
     * Proves the list of sliders for this panel
     */
     public Slider[] RetrieveSliderList(string path)
    {
        List<Slider> list = new List<Slider>();
        foreach(Transform t in this.transform.Find(path))
        {
            list.Add(t.GetComponent<Slider>());
        }
        return list.ToArray();
    }

    /**
     * Toggles the window on and off
     */
    public void TogglePanel()
    {
        windowState = !windowState;
        this.gameObject.SetActive(windowState);
    }
}
