using UnityEngine;
using System.Collections;

public class MainScreen : UIScreen {

	public override void Init()
	{
		SetStage(new FStage("MAIN"));
		SetPixelOffset(0,0, Futile.screen.width, Futile.screen.height);
		FSprite tIcon = new FSprite("Brazil");
		AddSprite(tIcon);
		tIcon.width = 128.0f*Futile.displayScale;
		tIcon.height = 128.0f*Futile.displayScale;
		tIcon.SetPosition(64.0f*Futile.displayScale,64.0f*Futile.displayScale);
		FSprite tIcon2 = new FSprite("England");
		AddSprite(tIcon2);
		tIcon2.width = 128.0f*Futile.displayScale;
		tIcon2.height = 128.0f*Futile.displayScale;
		tIcon2.SetPosition(-64.0f*Futile.displayScale,-64.0f*Futile.displayScale);
	}
}
