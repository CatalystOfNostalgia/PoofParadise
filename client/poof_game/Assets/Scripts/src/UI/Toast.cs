using UnityEngine;
using System.Collections;
using System;

public class Toast : GamePanel {

    public static Toast toast;
    public Texture2D backPanel;
    public string message;
    private bool isActive;
    private int seconds;

    public override void Start()
    {
        isActive = true;
        Debug.Log("lonely toast");
    }

    void OnGUI()
    {
        if (!isActive)
        {
            return;
        }
        GUILayout.Label("HELLLLOOOOO");
        int x = Screen.width / 2;
        int y = Screen.height / 2;
        GUI.BeginGroup(new Rect(x, y, 200, 100));
        GUI.Box(new Rect(x, y, 200, 100), backPanel);
        GUI.BeginGroup(new Rect(x, y, 200, 100));
        GUI.Label(new Rect(x, y, 200, 100), message);
        GUI.EndGroup();
        GUI.EndGroup();
        Debug.Log("Toast made");
        StartCoroutine(wait5());
        //endMessage();
    }

    public void makeToast (string message)
    {
        this.message = message;
        isActive = true;
        this.seconds = 5;
    }
    
    public void makeToast (string message, int seconds)
    {
        makeToast(message);
        this.seconds = seconds;
    }

    private void endMessage()
    {
        isActive = false;
    }

    IEnumerator wait5()
    {
        yield return new WaitForSeconds(seconds);
        endMessage();
    }

    public override void GeneratePanel()
    {
        throw new NotImplementedException();
    }
}
