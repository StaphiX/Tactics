using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class InputManager
{
	static List<InputTouch> m_tPressedTouch = new List<InputTouch>();
	static List<InputTouch> m_tReleasedTouch = new List<InputTouch>();
	static List<InputTouch> m_tHeldTouch = new List<InputTouch>();
	public static float m_fTouchTolerance = 0;
	int m_iFramesPressed = 0;

	public static void Init () 
	{
		
	}

	public static void Update () 
	{
		ClearPressTouches();
		m_tReleasedTouch.Clear(); //Release touches only exist for a frame
		if(Input.GetMouseButtonUp(0))
		{
			AddRelease(Input.mousePosition, 0);
		}

		if(Input.GetMouseButtonDown(0))
		{
			AddPress(Input.mousePosition, 0);
		}
	}

	static void ClearPressTouches() //Remove press touches that have been released
	{
		foreach(InputTouch tTouch in m_tReleasedTouch)
		{
			for(int iPressTouch = 0; iPressTouch < m_tPressedTouch.Count; ++iPressTouch)
			{
				if(m_tPressedTouch[iPressTouch].iTouchIndex == tTouch.iTouchIndex)
				{
					m_tPressedTouch.RemoveAt(iPressTouch);
					iPressTouch--;
				}
			}
		}
	}

	public static void AddRelease(Vector2 vPosition, int iTouch)
	{
		foreach(InputTouch tTouch in m_tReleasedTouch)
		{
			if(tTouch.iTouchIndex == iTouch)
			{
				tTouch.vPosition = vPosition;
				return;
			}
		}
		m_tReleasedTouch.Add(new InputTouch(vPosition, iTouch));
	}

	public static void AddPress(Vector2 vPosition, int iTouch)
	{
		foreach(InputTouch tTouch in m_tPressedTouch)
		{
			if(tTouch.iTouchIndex == iTouch)
			{
				tTouch.vPosition = vPosition;
				return;
			}
		}
		m_tPressedTouch.Add(new InputTouch(vPosition, iTouch));
	}

	public static bool IsPressInRect(float fX, float fY, float fW, float fH)
	{
		foreach(InputTouch tTouch in m_tPressedTouch)
		{
			if(IsTouchInRect(tTouch, fX, fY, fW, fH))
				return true;
		}
		return false;
	}

	public static bool IsReleaseInRectF(float fX, float fY, float fW, float fH)
	{
		ConvertFutilePosition(new Vector2(fX, fY), out fX, out fY);
		ConvertFutileDimensions(new Vector2(fW, fH), out fW, out fH);

		return IsReleaseInRect(fX, fY, fW, fH);
	}

	public static bool IsReleaseInRect(float fX, float fY, float fW, float fH)
	{
		foreach(InputTouch tTouch in m_tReleasedTouch)
		{
			if(IsTouchInRect(tTouch, fX, fY, fW, fH))
				return true;
		}
		return false;
	}

	static bool IsTouchInRect(InputTouch tTouch, float fX, float fY, float fW, float fH)
	{
		if(tTouch.vPosition.x >= 0 && tTouch.vPosition.y >= 0)
		{
			if(tTouch.vPosition.x > fX - m_fTouchTolerance)
			{
				if(tTouch.vPosition.y > fY - m_fTouchTolerance)
				{
					if(tTouch.vPosition.x < fX + fW + m_fTouchTolerance)
					{
						if(tTouch.vPosition.y < fY + fH + m_fTouchTolerance)
						{
							return true;
						}	
					}	
				}
			}
		}
		return false;
	}

	public static bool IsKeyReleased(KeyCode eKey)
	{
		return Input.GetKeyUp(eKey);
	}

	public static void ConvertFutilePosition(Vector2 vPos, out float fX,out float fY)
	{
		vPos = Futile.stage.LocalToScreen(vPos);
		fX = vPos.x;
		fY = vPos.y;
	}

	public static void ConvertFutileDimensions(Vector2 vDim, out float fX,out float fY)
	{
		fX = vDim.x;
		fY = vDim.y;
	}
	
}

public class InputTouch
{
	public Vector2 vPosition;
	public int iTouchIndex = 0;

	public InputTouch(Vector2 _vPosition, int _iTouch)
	{
		vPosition = _vPosition;
		iTouchIndex = _iTouch;
	}

	public static InputTouch zero
	{
		get
		{
			return new InputTouch(-Vector2.one, 0);
		}
	}

}

