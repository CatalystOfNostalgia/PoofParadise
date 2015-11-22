using UnityEngine;
using UnityEngine.UI;

public class ErrorPanel : GamePanel {

    public Text[] texts { get; set; }
    private Button[] buttons;
    public static ErrorPanel panel;

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
        TogglePanel();
        buttons = RetrieveButtonList("Buttons");
        texts = RetrieveTextList("Texts");
        GeneratePanel();
    }

    /**
     * add functionality
     */
    override public void GeneratePanel () {

        FindAndModifyUIElement ("Exit Button", buttons, () => Exit());
    }

    private void Exit() {

        TogglePanel();
        LoginPanel.panel.TogglePanel();
    }
}
