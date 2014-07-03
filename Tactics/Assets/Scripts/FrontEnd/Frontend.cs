using UnityEngine;
using System.Collections;

public class Frontend : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		ScreenStack.Init();
		ScreenStack.Add(new LogoEditorScreen());
		//PlayerDatabase.LoadPlayerDatabase();
	}
	
	// Update is called once per frame
	void Update () {
	
		InputManager.Update(); //Gets all the inputs for this frame
		ScreenStack.Update();

	}

	void OnGUI()
	{
		ScreenStack.GUIDisplay();
	}
}
