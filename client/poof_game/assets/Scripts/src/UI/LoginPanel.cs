using UnityEngine;
using UnityEngine.UI;

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

        Debug.Log("logIN!!!");
        SaveState.state.PullFromServer("ted1", "password");
        Debug.Log("Logged In");

    }

    /**
     * Creates an accont for the user
     */
    public void CreateAccount() {

    }

}
