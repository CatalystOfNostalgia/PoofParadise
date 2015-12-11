using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Toast : GamePanel {

    public static Toast toast;
    public string title;
    public string message;
    private bool isActive;
    private int seconds;

    public override void Start()
    {
        makeToast("Not enough resources");
    }

    public void makeToast(string title, string message)
    {
        this.title = title;
        this.message = message;
        isActive = true;
        this.seconds = 5;
        TogglePanel();
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

    IEnumerator wait5()
    {
        yield return new WaitForSeconds(seconds);
        endMessage();
    }

    public override void GeneratePanel()
    {
        throw new NotImplementedException();
    }
    private Rect windowRect = new Rect(20, 20, 120, 50);

    void OnGUI()
    {
        windowRect = GUI.Window(0, windowRect, WindowFunction, title);

    }
    void WindowFunction (int windowID) {
        // Draw any Controls inside the window here
        float y = 20;
        GUI.Label(new Rect(5, y, windowRect.width, 20), message);
    }
    }
