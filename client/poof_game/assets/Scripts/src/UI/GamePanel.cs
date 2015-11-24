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
    public void FindAndModifyUIElement(string name, Button[] list, UnityAction method)
    {
        // Runs a search for a button by name
        int index = FindUIElement(name, list);

        // Lets the user know that their button doesn't exist
        if (index == -1)
        {
            Debug.LogError("FindAndModifyUIElement failed to find " + name + " in button list");
            return;
        }

        // Removes all listeners and adds functionality
        list[index].onClick.RemoveAllListeners();
        list[index].onClick.AddListener(method);
    }

    /**
     * Searches for a slider in a list
     * Removes the listeners from that silder
     * Adds a listener that that slider
     */
    public void FindAndModifyUIElement(string name, Slider[] list, UnityAction<float> method)
    {
        // Runs a search for a button by name
        int index = FindUIElement(name, list);

        // Lets the user know that their button doesn't exist
        if (index == -1)
        {
            Debug.LogError("FindAndModifyUIElement failed to find " + name + " in button list");
            return;
        }
        list[index].onValueChanged.RemoveAllListeners();
        list[index].onValueChanged.AddListener(method);
    }

    /**
     * Returns a button by name
     */
    public int FindUIElement(string name, Selectable[] list)
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
     * Provides the list of Texts for this panel
     */
    public Text[] RetrieveTextList(string path)
    {
        List<Text> list = new List<Text>();
        foreach (Transform t in this.transform.Find(path))
        {
            list.Add(t.GetComponent<Text>());
        }
        return list.ToArray();
    }

    /**
     * Provides the list of text fields for this panel
     */
    public InputField[] RetrieveInputFieldList(string path)
    {
        List<InputField> list = new List<InputField>();
        foreach (Transform t in this.transform.Find(path))
        {
            list.Add(t.GetComponent<InputField>());
        }
        return list.ToArray();
    }

    /**
     * Provides the list of sliders for this panel
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
     * Provides the list of selectable objects on this panel
     */
    public Selectable[] RetrievesSelectableList(string path)
    {
        List<Selectable> list = new List<Selectable>();
        foreach(Transform t in this.transform.Find(path))
        {
            list.Add(t.GetComponent<Selectable>());
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
