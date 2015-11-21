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
    }

    /**
     * add functionality to the panel
     */
    override public void GeneratePanel () {

        FindAndModifyUIElement("Log In Button", buttons, () => LogIn());
        // FindAndModifyUIElement("Create Account Button", inputFields, () => CreateAccount());

    }

    /**
     * Logs the user into the server and changes the scene
     */
    public void LogIn() {

    }

    /**
     * Creates an accont for the user
     */
    public void CreateAccount() {

    }

}
