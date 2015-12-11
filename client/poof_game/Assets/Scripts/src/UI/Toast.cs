using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Toast : GamePanel {

    public static Toast toast;
    public string title;
    public string message;
    public bool isActive;
    private int seconds;
    private Rect windowRect = new Rect(Screen.width/2 - 120, Screen.height/2 - 50, 120, 50);

    public override void Start()
    {
        //makeToast("Not enough resources");
    }

    public void makeToast(string title, string message)
    {
        this.title = title;
        this.message = message;
        isActive = true;
        this.seconds = 3;
    }

    public void makeToast (string message)
    {
        makeToast("Warning", message);
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

    IEnumerator waitForSeconds()
    {
        yield return new WaitForSeconds(seconds);
        endMessage();
    }

    public override void GeneratePanel()
    {
        throw new NotImplementedException();
    }

    void OnGUI()
    {
        if (isActive)
        {
            windowRect = new Rect(Screen.width / 2 - 120, Screen.height / 2 - 50, 10+message.Length*8, 50);//8pixels per char
            windowRect = GUI.Window(0, windowRect, WindowFunction, title);
            StartCoroutine(waitForSeconds());
        }

    }
    void WindowFunction (int windowID) {
        // Draw any Controls inside the window here
        float y = 20;
        GUI.Label(new Rect(5, y, windowRect.width, 20), message);
    }
    }
