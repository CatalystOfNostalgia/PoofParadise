using UnityEngine;
using UnityEngine.UI;
using System;
using SimpleJSON;

public class LoginPanel : GamePanel {

    public static LoginPanel panel;
    private Button[] buttons;
    private InputField[] inputFields;
    
    /**
     * Initialize
     */
    override public void Start () {

        if (panel == null) {
            panel = this;
        } else if (panel != this) {
            Destroy(gameObject);
        }

        windowState = true;
        buttons = RetrieveButtonList("Buttons");
        inputFields = RetrieveInputFieldList("TextFields");
        GeneratePanel ();
    }

    /**
     * add functionality to the panel
     */
    override public void GeneratePanel () {

        FindAndModifyUIElement("Log In Button", buttons, () => LogIn());
        FindAndModifyUIElement("Create Account Button", buttons, () => CreateAccount());

    }

    /**
     * Logs the user into the server and changes the scene
     */
    public void LogIn() {

        string password = inputFields[0].textComponent.text;
        string username = inputFields[1].textComponent.text;

        string userInfo = GetHTTP.login(username, password);
        JSONNode data = JSON.Parse(userInfo);

        if (data["error"] == null) {
            SceneState.state.userInfo = userInfo;
            Application.LoadLevel("Demo_scene");
        } else {
            Debug.Log(data["error"]);
            ErrorPanel.panel.TogglePanel();
            ErrorPanel.panel.texts[0].text = data["error"];
            TogglePanel();
            // TODO: Create a popup window to display the error
        }

    }

    /**
     * Creates an accont for the user
     */
    public void CreateAccount() {
        
        string password = inputFields[0].textComponent.text;
        string username = inputFields[1].textComponent.text;

        StartCoroutine(GetHTTP.createAccount("timothy", username, password, username + "@chi.com"));
    }

}
