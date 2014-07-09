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

[System.Flags]
public enum ELogoColour
{ 	
	White = 0,
	Black =	1 << 0,
	Primary = 	1 << 1,
	Secondary = 1 << 2,
	Gold = 	1 << 3,
	Grey = 	1 << 4,
}

public class LogoImage {

	public const int FILTERCOUNT = 6;
	public const int MAXLAYERS = 5;
	public Color tColorPrimary = Color.white;
	public Color tColorSecondary = Color.white;

	public float fWidth = 256.0f;
	public LogoLayer[] tLayers = new LogoLayer[MAXLAYERS];

	public void Init()
	{
		for(int iLayer = 0; iLayer < MAXLAYERS; ++iLayer)
		{
			tLayers[iLayer] = new LogoLayer();
			tLayers[iLayer].eColour = iLayer % 2 == 0 ? ELogoColour.Primary : ELogoColour.Secondary;

			if(iLayer <= 0)
				tLayers[iLayer].eType |= ELogoType.FirstLayer;
			else
				tLayers[iLayer].tParent = tLayers[iLayer-1];
			if(iLayer == MAXLAYERS-1)
				tLayers[iLayer].eType |= ELogoType.LastLayer;

			if(iLayer > 0 && iLayer < MAXLAYERS-1)
				tLayers[iLayer].eType |= ELogoType.Background;
		}

		SetSeed(11);
	}

	public Color GetColour(ELogoColour eColour)
	{
		switch(eColour)
		{
		case ELogoColour.White:
			return Color.white;
		case ELogoColour.Black:
			return Color.black;
		case ELogoColour.Primary:
			return tColorPrimary;
		case ELogoColour.Secondary:
			return tColorSecondary;
		case ELogoColour.Gold:
			return new Color(0.94f, 0.8f, 0.0f);
		case ELogoColour.Grey:
			return Color.gray;
		default:
			return Color.white;
		}
		return Color.white;
	}

	public void SetSeed(int iSeed)
	{
		for(int iLayer = 0; iLayer < MAXLAYERS; ++iLayer)
		{
			tLayers[iLayer].iSeed = iSeed + iLayer;
		}
	}
}

public class LogoLayer
{
	public int iSeed = 11;
	public LogoLayer tParent = null;
	public ELogoColour eColour = ELogoColour.White;
	public ELogoType eType = ELogoType.NONE;
	public ELogoType eMaskType = ELogoType.NONE;
	public string sImage = "";
	public string sMask = "";
	public float fSizeMin = 1.0f;
	public float fSizeMax = 1.0f;
	public float fYMin = 0.0f;
	public float fYMax = 0.0f;
	public float fXMin = 0.0f;
	public float fXMax = 0.0f;

	public string GetImage()
	{
		if(sImage.Length > 0)
			return sImage;
		
		return GetRandomPartName();
	}

	public string GetRandomPartName()
	{
		Random.seed = iSeed;
		
		return LogoPart.FindPartOfType(eType).sImageName;
	}

	public string GetMask()
	{
		if(sMask.Length > 0)
			return sMask;
		
		if(eMaskType == ELogoType.NONE || (eMaskType & ELogoType.Mask) == ELogoType.NONE)
		{
			return "LOGOblank";
		}

		Random.seed = iSeed;
		return LogoPart.FindPartOfType(eMaskType).sImageName;
	}

	public Vector2 GetPosition()
	{
		Random.seed = iSeed;
		Vector2 vPos = new Vector2(Random.Range(fXMin, fXMax), Random.Range(fYMin, fYMax));
		
		return vPos;
	}

	public float GetScale()
	{
		Random.seed = iSeed;
		float fSize = Random.Range(fSizeMin, fSizeMax);
		if(tParent != null)
			fSize *= tParent.GetScale();
		return fSize;
	}

	public void SetX(float fMin, float fMax)
	{
		if(fMin > fXMax)
		{
			fMax = fMin;
		}
		if(fMax < fXMin)
		{
			fMin = fMax;
		}

		fXMin = fMin;
		fXMax = fMax;

	}

	public void SetY(float fMin, float fMax)
	{
		if(fMin > fYMax)
		{
			fMax = fMin;
		}
		if(fMax < fYMin)
		{
			fMin = fMax;
		}

		fYMin = fMin;
		fYMax = fMax;
	}

	public void SetSize(float fMin, float fMax)
	{
		if(fMin > fSizeMax)
		{
			fMax = fMin;
		}
		if(fMax < fSizeMin)
		{
			fMin = fMax;
		}

		fSizeMin = fMin;
		fSizeMax = fMax;
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
		new LogoPart("LOGOblank", ELogoType.NONE),
		new LogoPart("LOGOcircle", ELogoType.Background | ELogoType.FirstLayer | ELogoType.Circular | ELogoType.Mask),
		new LogoPart("LOGOcircle2", ELogoType.Background | ELogoType.Circular | ELogoType.LastLayer),
		new LogoPart("LOGOloop", ELogoType.Background | ELogoType.Circular),
		new LogoPart("LOGOdiag", ELogoType.Background | ELogoType.Circular),
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
