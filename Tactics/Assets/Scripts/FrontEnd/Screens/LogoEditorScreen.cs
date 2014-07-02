using UnityEngine;
using System.Collections;

public class LogoEditorScreen : UIScreen {
	
	public override void Init()
	{
		SetStage(new FStage("LOGO"));
		SetPixelOffset(0,0, Futile.screen.width, Futile.screen.height);

		FSprite tIconShield = new FMaskedSprite("LOGOcircle", "LOGOcircle");
		tIconShield.color = new Color(0.3f, 0.5f, 0.8f);
		AddSprite(tIconShield);
		tIconShield.width = 128.0f*Futile.displayScale;
		tIconShield.height = 128.0f*Futile.displayScale;
		tIconShield.SetPosition(64.0f*Futile.displayScale,64.0f*Futile.displayScale);

		FSprite tIconWings = new FMaskedSprite("LOGOloop", "LOGOcircle");
		tIconWings.color = new Color(1.0f, 1.0f, 1.0f);
		AddSprite(tIconWings);
		tIconWings.width = 128.0f*Futile.displayScale;
		tIconWings.height = 128.0f*Futile.displayScale;
		tIconWings.SetPosition(64.0f*Futile.displayScale,64.0f*Futile.displayScale);

		FSprite tIconDiag = new FMaskedSprite("LOGOthreestripe", "LOGOcircle");
		tIconDiag.color = new Color(1.0f, 1.0f, 1.0f);
		AddSprite(tIconDiag);
		tIconDiag.width = 80.0f*Futile.displayScale;
		tIconDiag.height = 80.0f*Futile.displayScale;
		tIconDiag.SetPosition(64.0f*Futile.displayScale,64.0f*Futile.displayScale);

		FSprite tIconStar = new FMaskedSprite("LOGOhorse", "LOGOcircle");
		tIconStar.color = new Color(0.1f, 0.1f, 0.1f);
		AddSprite(tIconStar);
		tIconStar.width = 64.0f*Futile.displayScale;
		tIconStar.height = 64.0f*Futile.displayScale;
		tIconStar.SetPosition(64.0f*Futile.displayScale,64.0f*Futile.displayScale);

	}

	public override void Update()
	{

	}

}
