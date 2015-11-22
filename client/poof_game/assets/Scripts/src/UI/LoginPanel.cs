using UnityEngine;
using UnityEngine.UI;
using System;
using SimpleJSON;

public class LoginPanel : GamePanel {

    private Button[] buttons;
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
        JSONNode data = JSON.Parse(userInfo);

        if (data["error"] == null) {
            SceneState.state.userInfo = userInfo;
            Application.LoadLevel("Demo_scene");
        } else {
            Debug.Log(data["error"]);
            // TODO: Create a popup window to display the error
        }

    }

    /**
     * Creates an accont for the user
     */
    public void CreateAccount() {

    }

}
