using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LogoEditorScreen : UIScreen {

	LogoEditor tEditor;

	public override void Init()
	{
		SetStage(new FStage("LOGO"));
		Futile.AddStage(GetStage());
		SetPixelOffset(0,0, Futile.screen.width, Futile.screen.height);
		tEditor = new LogoEditor(this);
	}

	public override void Update()
	{
		if(InputManager.IsKeyReleased(KeyCode.LeftArrow))
		{
			tEditor.IncPage(-1);
			tEditor.bUpdate = true;
		}
		if(InputManager.IsKeyReleased(KeyCode.RightArrow))
		{
			tEditor.IncPage(1);
			tEditor.bUpdate = true;
		}

		tEditor.Update();
	}

	public override void GUIDisplay ()
	{
		tEditor.DisplayLayer(0,0);
	}
}

public class LogoEditor
{
	public LogoImage tLogoImage;
	public bool bUpdate = false;
	int iPage = 0;
	int iMaxLogos = 10;
	UIScreen tScreen;

	int iCurrentLayer = -1;
	int iCurrentSelection = -1;
	bool bSelectMask = false;

	string[] sPrimary = new string[] { "255", "255", "0" };
	string[] sSecondary = new string[] { "255", "0", "255" };
	Color tPrimary = Color.yellow;
	Color tSecondary = Color.magenta;

	public LogoEditor(UIScreen _tScreen)
	{
		tLogoImage = new LogoImage();
		tLogoImage.Init();
		tScreen = _tScreen;
		AddLogoToScreen(260, 160);
		AddLogoListToScreen();
		AddLogoLayersToScreen();
	}

	public void SetPage(int _iPage)
	{
		iPage = _iPage;

		List<LogoPart> tParts = LogoPart.GetPartList(LogoPart.ePartFilter);
		int iPartCount = tParts.Count;

		if(iPage * iMaxLogos >= iPartCount)
			iPage = 0;
		else if(iPage < 0)
			iPage = iPartCount / iMaxLogos;
	}

	public void IncPage(int iInc)
	{
		SetPage(iPage + iInc);
	}

	public void Update()
	{
		UpdateSelectedImages();
		if(bUpdate)
		{
			tScreen.GetStage().RemoveAllChildren();
			AddLogoToScreen(260, 160);
			AddLogoListToScreen();
			AddLogoLayersToScreen();
			
			bUpdate = false;
		}
	}

	public void AddLogoToScreen(float fX, float fY)
	{
		tLogoImage.tColorPrimary = tPrimary;
		tLogoImage.tColorSecondary = tSecondary;
		
		for(int iLayer = 0; iLayer < LogoImage.MAXLAYERS; ++iLayer)
		{
			LogoLayer tLayer = tLogoImage.tLayers[iLayer];
			
			bool bFirst = iLayer == 0;
			bool bLast = iLayer == LogoImage.MAXLAYERS-1;

			FMaskedSprite tSprite = new FMaskedSprite(tLayer.GetImage(), tLayer.GetMask());
			tScreen.AddSprite(tSprite);
			float fWidth = tLogoImage.fWidth * tLayer.GetScale();
			tSprite.width = fWidth;
			tSprite.height = fWidth;
			float fXOffset = tLogoImage.fWidth * tLayer.GetPosition().x;
			float fYOffset = tLogoImage.fWidth * tLayer.GetPosition().y;
			tSprite.SetPosition(fX + fXOffset,fY + fYOffset);
			tSprite.color = tLogoImage.GetColour(tLayer.eColour);
		}
	}

	public void AddLogoListToScreen()
	{
		List<LogoPart> tParts = LogoPart.GetPartList(LogoPart.ePartFilter);

		int iCount = Mathf.Min(tParts.Count, iMaxLogos+iPage*iMaxLogos);
		for(int iPart = iPage*iMaxLogos; iPart < iCount; ++iPart)
		{
			Rect tPartRect = GetLayerPartRect(iPart - iPage*iMaxLogos);
			FSprite tSprite = new FSprite(tParts[iPart].sImageName);
			tScreen.AddSprite(tSprite);
			tSprite.width = tPartRect.width;
			tSprite.height = tPartRect.height;
			tSprite.SetPosition(tPartRect.x,tPartRect.y);
			tSprite.color = iCurrentSelection == iPart ? Color.cyan : Color.white;
		}
	}

	public void AddLogoLayersToScreen()
	{
		for(int iLayer = 0; iLayer < LogoImage.MAXLAYERS; ++iLayer)
		{
			AddLogoLayerToScreen(iLayer);
		}
	}

	public void AddLogoLayerToScreen(int iLayer)
	{
		Rect tImageRect = GetLayerRect(iLayer, false);
		Rect tMaskRect = GetLayerRect(iLayer, true);
		LogoLayer tLayer = tLogoImage.tLayers[iLayer];
		string sImageName = tLayer.GetImage();
		string sMaskName = tLayer.GetMask();
		Color tImageCol = iLayer == iCurrentLayer && !bSelectMask ? Color.cyan : Color.white;
		Color tMaskCol = iLayer == iCurrentLayer && bSelectMask ? Color.cyan : Color.white;
		if(sImageName.Length < 1)
		{
			sImageName = "LOGOcircle";
			if(bSelectMask || iCurrentLayer != iLayer)
				tImageCol = Color.black;
		}
		if(sMaskName.Length < 1)
		{
			sMaskName = "LOGOcircle";
			if(!bSelectMask || iCurrentLayer != iLayer)
				tMaskCol = Color.black;
		}
		FSprite tImageSprite = new FSprite(sImageName);
		FSprite tMaskSprite = new FSprite(sMaskName);
		tScreen.AddSprite(tImageSprite);
		tScreen.AddSprite(tMaskSprite);
		tImageSprite.width = tImageRect.width;
		tImageSprite.height = tImageRect.height;
		tImageSprite.SetPosition(tImageRect.x,tImageRect.y);
		tImageSprite.color = tImageCol;
		tMaskSprite.width = tMaskRect.width;
		tMaskSprite.height = tMaskRect.width;
		tMaskSprite.SetPosition(tMaskRect.x,tMaskRect.y);
		tMaskSprite.color = tMaskCol;
	}

	public Rect GetLayerRect(int iLayer, bool bMask)
	{
		float fLayerW = 64;
		float fYOffset = 150 * iLayer;
		float fXOffset = bMask ? 80 : 0;
		return new Rect((-Screen.width/2) + 40 + fXOffset, Screen.height/2 - 40 - fYOffset, fLayerW, fLayerW);
	}

	public Rect GetLayerPartRect(int iIndex)
	{
		float fWidth = tLogoImage.fWidth/4;
		return new Rect(-250 + iIndex * (fWidth+5), -200, fWidth, fWidth);
	}

	public void DisplayLayer(float fX, float fY)
	{
		string[] sFilters = new string[LogoImage.FILTERCOUNT] { "NONE", "FirstLayer", "Background", "LastLayer", "Circular", "Mask" };
		float fH = 80;
		float fW = 200;

		Rect tButtonRect = new Rect(300, Screen.height - 65, Screen.width, 20);
		for(int iFilter = 0; iFilter < sFilters.Length; ++iFilter)
		{
			bool bToggle = true;
			if(iFilter > 0)
				bToggle = ((int)LogoPart.ePartFilter & 1 << (iFilter-1)) != (int)ELogoType.NONE;
			if(bToggle != GUI.Toggle(new Rect(tButtonRect.x+80*iFilter, tButtonRect.y, 50, 20), bToggle, sFilters[iFilter]))
			{
				bToggle = !bToggle;
				int iType = 0;
				if(iFilter > 0)
					iType = 1 << (iFilter-1);
				if(bToggle)
					LogoPart.ePartFilter |= (ELogoType)iType;
				else
					LogoPart.ePartFilter &= ~(ELogoType)iType;

				iPage = 0;
				bUpdate = true;
			}
		}
		if(GUI.Button(new Rect(tButtonRect.x, tButtonRect.y+20, 150, 20), "Set Random Layer"))
		{
			if(iCurrentLayer > -1)
			{
				if(!bSelectMask)
				{
					tLogoImage.tLayers[iCurrentLayer].sImage = "";
					tLogoImage.tLayers[iCurrentLayer].eType = LogoPart.ePartFilter;
				}
				else
				{
					tLogoImage.tLayers[iCurrentLayer].sMask = "";
					tLogoImage.tLayers[iCurrentLayer].eMaskType = LogoPart.ePartFilter;
				}
				bUpdate = true;
			}
		}

		if(iCurrentLayer > -1)
		{
			Rect tRect = GetLayerRect(0, true);
			float fRectX, fRectY;
			InputManager.ConvertFutilePosition(new Vector2(tRect.x, tRect.y), out fRectX, out fRectY);
			tRect.x = fRectX + 60;
			tRect.y = fRectY;
			tRect.width = 100;
			tRect.height = 15;
			LogoLayer tLayer = tLogoImage.tLayers[iCurrentLayer];
			float fXMin = tLayer.fXMin;
			fXMin = GUI.HorizontalSlider(tRect, fXMin, -1.0f, 1.0f);
			float fXMax = tLayer.fXMax;
			tRect.y += 15;
			fXMax = GUI.HorizontalSlider(tRect, fXMax, -1.0f, 1.0f);
			tRect.y += 30;
			float fYMin = tLayer.fYMin;
			fYMin = GUI.HorizontalSlider(tRect, fYMin, -1.0f, 1.0f);
			tRect.y += 15;
			float fYMax = tLayer.fYMax;
			fYMax = GUI.HorizontalSlider(tRect, fYMax, -1.0f, 1.0f);
			tRect.y += 30;
			float fSizeMin = tLayer.fSizeMin;
			fSizeMin = GUI.HorizontalSlider(tRect, fSizeMin, 0.0f, 2.0f);
			tRect.y += 15;
			float fSizeMax = tLayer.fSizeMax;
			fSizeMax = GUI.HorizontalSlider(tRect, fSizeMax, 0.0f, 2.0f);

			if(tLayer.fXMin != fXMin || tLayer.fXMax != fXMax)
			{
				tLayer.SetX(fXMin, fXMax);
				bUpdate = true;
			}
			if(tLayer.fYMin != fYMin || tLayer.fYMax != fYMax)
			{
				tLayer.SetY(fYMin, fYMax);
				bUpdate = true;
			}
			if(tLayer.fSizeMin != fSizeMin || tLayer.fSizeMax != fSizeMax)
			{
				tLayer.SetSize(fSizeMin, fSizeMax);
				bUpdate = true;
			}


		}

		Rect tTextRect = new Rect(300, Screen.height-25, 50, 20);
		sPrimary[0] = GUI.TextField(tTextRect, sPrimary[0]);
		tTextRect.x += 50;
		sPrimary[1] = GUI.TextField(tTextRect, sPrimary[1]);
		tTextRect.x += 50;
		sPrimary[2] = GUI.TextField(tTextRect, sPrimary[2]);
		tTextRect.x += 80;
		sSecondary[0] = GUI.TextField(tTextRect, sSecondary[0]);
		tTextRect.x += 50;
		sSecondary[1] = GUI.TextField(tTextRect, sSecondary[1]);
		tTextRect.x += 50;
		sSecondary[2] = GUI.TextField(tTextRect, sSecondary[2]);

		if(float.TryParse(sPrimary[0], out tPrimary.r))
			tPrimary.r /= 255.0f;
		if(float.TryParse(sPrimary[1], out tPrimary.g))
			tPrimary.g /= 255.0f;
		if(float.TryParse(sPrimary[2], out tPrimary.b))
			tPrimary.b /= 255.0f;
		if(float.TryParse(sSecondary[0], out tSecondary.r))
			tSecondary.r /= 255.0f;
		if(float.TryParse(sSecondary[1], out tSecondary.g))
			tSecondary.g /= 255.0f;
		if(float.TryParse(sSecondary[2], out tSecondary.b))
			tSecondary.b /= 255.0f;

		tTextRect.x += 60;
		tTextRect.width = 80;
		if(GUI.Button(tTextRect, "UPDATE"))
			bUpdate = true;
	}

	public void UpdateSelectedImages()
	{
		for(int iLayer = 0; iLayer < LogoImage.MAXLAYERS; ++iLayer)
		{
			Rect tImageRect = GetLayerRect(iLayer, false);
			Rect tMaskRect = GetLayerRect(iLayer, true);
			if(InputManager.IsReleaseInRectF(tImageRect))
			{
				iCurrentLayer = iLayer;
				bSelectMask = false;
				bUpdate = true;
			}
			else if(InputManager.IsReleaseInRectF(tMaskRect))
			{
				iCurrentLayer = iLayer;
				bSelectMask = true;
				bUpdate = true;
			}
		}

		List<LogoPart> tParts = LogoPart.GetPartList(LogoPart.ePartFilter);
		
		int iCount = Mathf.Min(tParts.Count, iMaxLogos+iPage*iMaxLogos);
		for(int iPart = iPage*iMaxLogos; iPart < iCount; ++iPart)
		{
			Rect tPartRect = GetLayerPartRect(iPart-iPage*iMaxLogos);
			if(InputManager.IsReleaseInRectF(tPartRect))
			{
				iCurrentSelection = iPart;
				bUpdate = true;
			}
		}

		if(bUpdate)
			SwapImage();
	}

	public void SwapImage()
	{
		if(iCurrentSelection > -1 && iCurrentLayer > -1)
		{
			List<LogoPart> tParts = LogoPart.GetPartList(LogoPart.ePartFilter);
			string sPart = tParts[iCurrentSelection].sImageName;
			if(!bSelectMask)
			{
				tLogoImage.tLayers[iCurrentLayer].sImage = sPart;
			}
			else
			{
				tLogoImage.tLayers[iCurrentLayer].sMask = sPart;
			}

			iCurrentSelection = -1;
		}
	}
}
