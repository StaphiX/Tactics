using UnityEngine;
using System.Collections;

public class MathUtil {
	
	public static float GetAngleBetween(float fBaseX, float fBaseY, float fNewX, float fNewY)
	{
		float fDeltaY = fNewY - fBaseY;
		float fDeltaX = fNewX - fBaseX;
		
		return Mathf.Atan2(fDeltaY, fDeltaX);
	}
}
