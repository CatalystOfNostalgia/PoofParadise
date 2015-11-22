using UnityEngine;
using UnityEngine.UI;
using System;

public class LoginPanel : GamePanel {

    private Button[] buttons;
    public InputField usernameField;
    public InputField passwordField;
    private InputField[] inputFields;
    
    /**
     * Initialize
     */
    override public void Start () {
        buttons = RetrieveButtonList("Buttons");
        inputFields = RetrieveInputFieldList("TextFields");
        GeneratePanel ();
    }

    /**
     * add functionality to the panel
     */
    override public void GeneratePanel () {

        FindAndModifyUIElement("Log In Button", buttons, () => LogIn());

    }

    /**
     * Logs the user into the server and changes the scene
     */
    public void LogIn() {

        String password = inputFields[0].textComponent.text;
        String username = inputFields[1].textComponent.text;
        String userInfo = GetHTTP.login(username, password);
        Debug.Log(userInfo);
        Application.LoadLevel("Demo_scene");
        //SaveState.state.PullFromServer("ted1", "password");

    }

    /**
     * Creates an accont for the user
     */
    public void CreateAccount() {

    }

}
