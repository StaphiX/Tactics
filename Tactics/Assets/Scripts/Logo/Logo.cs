using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Flags]
public enum ELogoType
{
	NONE = 0,
	FirstLayer =	1 << 0,
	Background = 	1 << 1,
	LastLayer = 	1 << 2,
	Circular = 		1 << 3,
	Mask = 			1 << 4,
}

public class LogoImage {

	public const int FILTERCOUNT = 6;
	public const int MAXLAYERS = 5;
	public float fWidth = 256.0f;
	public LogoLayer[] tLayers = new LogoLayer[MAXLAYERS];
}

public class LogoTemplate
{
	public bool bUpdate = false;
	public float fWidth = 256.0f;
	public LogoLayerTemplate[] tLayers = new LogoLayerTemplate[LogoImage.MAXLAYERS];
	public int iSeed = 11;
	public Color tColorPrimary = Color.white;
	public Color tColorSecondary = Color.white;

	public LogoLayerTemplate GetLayer(int iIndex)
	{
		if(iIndex < LogoImage.MAXLAYERS)
			return tLayers[iIndex];
		else 
			return null;
	}

	public int GetLayerSeed(int iLayer)
	{
		return iSeed = iLayer + 1;
	}

	public void Init()
	{
		for(int iLayer = 0; iLayer < LogoImage.MAXLAYERS; ++iLayer)
		{
			tLayers[iLayer] = new LogoLayerTemplate();
			tLayers[iLayer].bFirst = iLayer == 0;
			tLayers[iLayer].bLast = iLayer == LogoImage.MAXLAYERS-1;
			if(iLayer > 0)
				tLayers[iLayer].tPreviousLayer = tLayers[iLayer-1];
		}
	}

	public void AddLogoToScreen(UIScreen tScreen, float fX, float fY)
	{
		LogoLayerTemplate tPreviousLayer = null;

		for(int iLayer = 0; iLayer < LogoImage.MAXLAYERS; ++iLayer)
		{
			LogoLayerTemplate tLayer = tLayers[iLayer];

			bool bFirst = iLayer == 0;
			bool bLast = iLayer == LogoImage.MAXLAYERS-1;

			tLayer.tLayer.tColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
			FMaskedSprite tSprite = new FMaskedSprite(tLayer.GetImage(GetLayerSeed(iLayer)), tLayer.GetMask(GetLayerSeed(iLayer)));
			tScreen.AddSprite(tSprite);
			tSprite.width = fWidth;
			tSprite.height = fWidth;
			tSprite.SetPosition(fX + tLayer.GetPosition().x,fY + tLayer.GetPosition().y);
			tSprite.color = tLayer.tLayer.tColor;

			tPreviousLayer = tLayer;
		}
	}

	public void DisplayLayer(float fX, float fY)
	{
		string[] sFilters = new string[LogoImage.FILTERCOUNT] { "NONE", "FirstLayer", "Background", "LastLayer", "Circular", "Mask" };
		float fH = 140;
		float fW = 500;

		for(int iLayer = 0; iLayer < LogoImage.MAXLAYERS; ++iLayer)
		{
			Rect tBoxRect = new Rect(fX, fY + iLayer * (fH+5), fW, fH);
			LogoLayerTemplate tLayer = tLayers[iLayer];
			GUI.BeginGroup(tBoxRect);
			GUI.Box(new Rect(0, 0, tBoxRect.width, tBoxRect.height), "");
			GUI.Label(new Rect(0,0,tBoxRect.width, 20), "Image: " + tLayer.GetImage(GetLayerSeed(iLayer)));
			GUI.Label(new Rect(0,20,tBoxRect.width, 20), "Mask: " + tLayer.GetMask(GetLayerSeed(iLayer)));
			GUI.Label(new Rect(tBoxRect.width - 100,20,100, 20), "Layer As Mask");
			GUI.Label(new Rect(tBoxRect.width - 200,0,100, 20), "Parent Scale");
			GUI.TextField(new Rect(tBoxRect.width - 100,0,100, 20), tLayer.tLayer.fParentSize.ToString());

			for(int iFilter = 0; iFilter < LogoImage.FILTERCOUNT; ++iFilter)
			{
				bool bValue = tLayer.iLayerAsMask == iFilter;
				if(bValue != GUI.Toggle(new Rect(tBoxRect.width - 50,40+iFilter*20,50, 20),bValue, (iFilter+1).ToString()))
				{
					tLayer.iLayerAsMask = iFilter;
				}
			}

			for(int iFilter = 0; iFilter < LogoImage.FILTERCOUNT; ++iFilter)
			{
				int iLogoType = 0;
				if(iFilter > 0)
				{
					iLogoType = 1 << (iFilter-1);
				}

				ELogoType eType = tLayer.tPart.eType;
				bool bCurrentValue = (eType & (ELogoType)iLogoType) != ELogoType.NONE;
				if(bCurrentValue != GUI.Toggle(new Rect(0 + 100 * (int)(iFilter/5), 40 + (20 * (iFilter % 5)), 200, 20),bCurrentValue, sFilters[iFilter]))
				{
					if(!bCurrentValue)
						eType |= (ELogoType)iLogoType;
					else
						eType &= ~(ELogoType)iLogoType;

					tLayer.tPart.eType = eType;
				}
			}

			GUI.EndGroup();
			if(GUI.Button(new Rect(Screen.width-100, 5, 100, 50), "UPDATE"))
			{
				bUpdate = true;
			}
			try
			{
				int iR1 = int.Parse(GUI.TextField(new Rect(Screen.width-200, 5, 100, 20), (tColorPrimary.r*255).ToString()));
				int iG1 = int.Parse(GUI.TextField(new Rect(Screen.width-300, 5, 100, 20), (tColorPrimary.g*255).ToString()));
				int iB1 = int.Parse(GUI.TextField(new Rect(Screen.width-400, 5, 100, 20), (tColorPrimary.b*255).ToString()));
				int iR2 = int.Parse(GUI.TextField(new Rect(Screen.width-200, 25, 100, 20), (tColorSecondary.r*255).ToString()));
				int iG2 = int.Parse(GUI.TextField(new Rect(Screen.width-300, 25, 100, 20), (tColorSecondary.g*255).ToString()));
				int iB2 = int.Parse(GUI.TextField(new Rect(Screen.width-400, 25, 100, 20), (tColorSecondary.b*255).ToString()));
			}
			catch
			{
			}
		}
	}
}

public class LogoLayerTemplate
{
	public LogoPart tPart = new LogoPart("", ELogoType.Background);
	public LogoPart tMask = new LogoPart("", ELogoType.NONE);
	public LogoLayer tLayer = new LogoLayer();
	public int iLayerAsMask = -1;
	public bool bFirst = false;
	public bool bLast = false;
	public LogoLayerTemplate tPreviousLayer = null;

	public Vector2 GetPosition()
	{
		return tLayer.GetPosition();
	}

	public string GetRandomPartName(int iSeed)
	{
		Random.seed = iSeed;

		if(bFirst)
			tPart.eType |= ELogoType.FirstLayer;
		else
			tPart.eType &= ~ELogoType.FirstLayer;
		if(bLast)
			tPart.eType |= ELogoType.LastLayer;
		else
			tPart.eType &= ~ELogoType.LastLayer;
		
		if(!bFirst && !bLast)
		{
			tPart.eType |= ELogoType.Background;
		}
		
		if(!bFirst && !bLast && tPreviousLayer != null)
		{
			if((tPreviousLayer.tPart.eType & ELogoType.Circular) != ELogoType.NONE)
				tPart.eType |= ELogoType.Circular;
		}
		
		return LogoPart.FindPartOfType(tPart.eType).sImageName;
	}

	public string GetImage(int iSeed)
	{
		if(tPart.sImageName.Length > 0)
			return tPart.sImageName;

		return GetRandomPartName(iSeed);
	}

	public string GetMask(int iSeed)
	{
		if(tMask.eType == ELogoType.NONE)
		{
			return "";
		}
		if(tMask.sImageName.Length > 0)
			return tMask.sImageName;

		tMask.sImageName = LogoPart.FindPartOfType(tMask.eType).sImageName;
		return tMask.sImageName;
	}
}

public class LogoLayer
{
	public string sImage = "";
	public string sMask = "";
	public float fParentSize = -1.0f;
	public float fSizeMin = 1.0f;
	public float fSizeMax = 1.0f;
	public float fYMin = 0.0f;
	public float fYMax = 0.0f;
	public float fXMin = 0.0f;
	public float fXMax = 0.0f;
	public Color tColor = Color.white;

	public Vector2 GetPosition()
	{
		Vector2 vPos = new Vector2(Random.Range(fXMin, fXMax), Random.Range(fYMin, fYMax));
		
		return vPos;
	}

	public float GetScale()
	{
		float fSize = Random.Range(fSizeMin, fSizeMax);
		if(fParentSize >= 0)
			fSize *= fParentSize;
		return fSize;
	}
}

public class LogoPart
{
	public ELogoType eType;
	public string sImageName;
	public LogoPart(string sName, ELogoType _eType)
	{
		eType = _eType;
		sImageName = sName;
	}

	public static LogoPart[] tLogoPart = new LogoPart[]
	{
		new LogoPart("LOGOcircle", ELogoType.Background | ELogoType.FirstLayer | ELogoType.Circular | ELogoType.Mask),
		new LogoPart("LOGOcircle2", ELogoType.Background | ELogoType.Circular | ELogoType.LastLayer),
		new LogoPart("LOGOloop", ELogoType.Background | ELogoType.Circular),
		new LogoPart("LOGOdiag", ELogoType.Background | ELogoType.LastLayer | ELogoType.Circular),
		new LogoPart("LOGOhorse", ELogoType.LastLayer),
		new LogoPart("LOGOlion", ELogoType.LastLayer),
		new LogoPart("LOGOoval", ELogoType.Background | ELogoType.FirstLayer | ELogoType.Circular | ELogoType.Mask),
		new LogoPart("LOGOshield", ELogoType.Background | ELogoType.FirstLayer | ELogoType.Mask),
		new LogoPart("LOGOshield2", ELogoType.Background | ELogoType.LastLayer),
		new LogoPart("LOGOstar", ELogoType.LastLayer),
		new LogoPart("LOGOthreestripe", ELogoType.Background | ELogoType.LastLayer | ELogoType.Circular),
		new LogoPart("LOGOtrident", ELogoType.LastLayer),
		new LogoPart("LOGOwings", ELogoType.FirstLayer),
	};

	public static void DisplayParts()
	{
		int iY = 5;
		List<LogoPart> tParts = GetPartList(ePartFilter);
		foreach(LogoPart tPart in tParts)
		{
			GUI.Label(new Rect(5, iY, 100, 20), tPart.sImageName);
			iY += 20;
		}

		for(int iFilter = 0; iFilter < LogoImage.FILTERCOUNT; ++iFilter)
		{
			int iLogoType = 0;
			if(iFilter > 0)
			{
				iLogoType = 1 << (iFilter-1);
			}

			bool bCurrentValue = (ePartFilter & (ELogoType)iLogoType) != ELogoType.NONE;
			if(bCurrentValue != GUI.Toggle(new Rect(110, 5 + iFilter*20, 30, 20),bCurrentValue, iFilter.ToString()))
			{
				if(!bCurrentValue)
					ePartFilter |= (ELogoType)iLogoType;
				else
					ePartFilter &= ~(ELogoType)iLogoType;
			}
		}
	}

	public static ELogoType ePartFilter = ELogoType.NONE;

	public static List<LogoPart> GetPartList(ELogoType eFilter)
	{
		List<LogoPart> tParts = new List<LogoPart>();
		foreach(LogoPart tPart in tLogoPart)
		{
			if((tPart.eType & eFilter) == eFilter)
			{
				tParts.Add(tPart);
			}
		}
		return tParts;
	}

	public static LogoPart FindPartOfType(ELogoType eType)
	{
		List<LogoPart> tParts = GetPartList(eType);
		if(tParts.Count > 0)
		{
			return tParts[Random.Range(0, tParts.Count)];
		}

		return null;
	}
}
