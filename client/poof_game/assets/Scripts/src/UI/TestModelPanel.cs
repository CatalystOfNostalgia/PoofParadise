using UnityEngine;

public class TestModelPanel : MonoBehaviour {

	private DisplayManager displayManager;

	void Awake(){
        displayManager = DisplayManager.Instance ();
	}

	public void Test123(){
        if (ModelPanel.modelPanel == null) { Debug.Log("NULL"); }
		ModelPanel.modelPanel.Choice ("Choose a Button and brace yourself");
	}

	void TestButton1Function(){
		displayManager.DisplayMessage ("You are a superstar for clicking Button 1");
	}
	void TestButton2Function(){
		displayManager.DisplayMessage ("You clicked Button 2");
	}
	void TestButton3Function(){
		displayManager.DisplayMessage ("Button 3 is the best button");
	}
    void TestCancelFunction()
    {
        displayManager.DisplayMessage("Cancel button");
    }
}
