using UnityEngine;
using System.Collections;

public class LogoEditorScreen : UIScreen {

	LogoTemplate tTemplate = new LogoTemplate();
	public override void Init()
	{
		SetStage(new FStage("LOGO"));
		SetPixelOffset(0,0, Futile.screen.width, Futile.screen.height);

		tTemplate.Init();
		tTemplate.AddLogoToScreen(this, 260, 100);
	}

	public override void Update()
	{
		if(tTemplate.bUpdate)
		{
			GetStage().RemoveAllChildren();
			tTemplate.AddLogoToScreen(this, 260, 100);

			tTemplate.bUpdate = false;
		}
	}

	public override void GUIDisplay ()
	{
		tTemplate.DisplayLayer(0,0);
	}

}
