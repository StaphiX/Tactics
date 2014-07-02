using UnityEngine;
using System.Collections;

public class DrawUtil 
{
	public static FSprite RectSprite(float fX, float fY, float fW, float fH, Color tCol)
	{
		FSprite tSprite = new FSprite("blank");
		tSprite.SetDimensions(fX, fY, fW, fH);
		tSprite.color = tCol;

		return tSprite;
	}
}
