using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public abstract class GamePanel : MonoBehaviour {

    abstract public void Start();

    /**
     * Searches for a button in a list
     * Removes the listeners on that button
     * Adds a listener to that button
     */
    public void FindAndModifyButton(string name, Button[] list, UnityAction method)
    {
        // Runs a search for a button by name
        int index = FindButton(name, list);

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
    public int FindButton(string button, Button[] list)
    {
        for (int i = 0; i < list.Length; i++)
        {
            if (list[i].name == button)
            {
                return i;
            }
        }
        return -1;
    }
}
