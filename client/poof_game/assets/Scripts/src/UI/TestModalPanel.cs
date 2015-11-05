using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class TestModalPanel : MonoBehaviour {

	private DisplayManager displayManager;

	private UnityAction myButton1Action;
	private UnityAction myButton2Action;
	private UnityAction myButton3Action;
	private UnityAction myCancelButtonAction;

	void Awake(){
		displayManager = DisplayManager.Instance ();
		myButton1Action = new UnityAction (TestButton1Function);
		myButton2Action = new UnityAction (TestButton2Function);
		myButton3Action = new UnityAction (TestButton3Function); 
	}
	public void Test123(){
        if (ModelPanel.modelPanel == null) { Debug.Log("NULL"); }
		ModelPanel.modelPanel.Choice ("Choose a Button and brace yourself", myButton1Action, myButton2Action, myButton3Action, myCancelButtonAction);
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
}
