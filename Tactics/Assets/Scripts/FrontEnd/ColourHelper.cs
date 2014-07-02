using UnityEngine;
using System.Collections;

public class ColourHelper {


}

public static class ColorExtension
{
	public static Color RGBA(float fRed, float fGreen, float fBlue, float fAlpha)
	{
		return new Color(fRed/255.0f, fGreen/255.0f, fBlue/255.0f, fAlpha/255.0f); 
	}

	public static Color RGBA(float fRed, float fGreen, float fBlue)
	{
		return new Color(fRed/255.0f, fGreen/255.0f, fBlue/255.0f); 
	}
}