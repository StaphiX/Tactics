using UnityEngine;
using System.Collections;
using SaveData;

public class Formation
{
	EFormation m_eFormation = EFormation.E442;
	FormationPos[] m_tFormationPositions = new FormationPos[11];
	
	void SetFormationPositions(EPosition[] ePos)
	{
		int iPosCount = 0;
		foreach(EPosition thisPos in ePos)
		{
			if(iPosCount < m_tFormationPositions.Length)
			{
				SetFormationPosition(iPosCount, thisPos);
			}
			else
			{
				return;
			}
			iPosCount++;
		}
	}
	
	public void SetFormationPosition(int iIndex, EPosition tPos)
	{
		m_tFormationPositions[iIndex] = new FormationPos(tPos);
	}

	public FormationPos GetFormationPosition(int iIndex)
	{
		return m_tFormationPositions[iIndex];
	}

	public static Vector2 GetPosition(EPosition ePosition, bool bHome)
	{
		Vector2 vPos = GetFormationPosition(ePosition);
		
		if(!bHome)
		{
			vPos.x *= -1;
			vPos.x += 1.0f;
			vPos.y *= -1;
			vPos.y += 1.0f;
		}
		
		return vPos;
	}

	public static Vector2 GetKickoffPosition(EPosition ePosition, bool bHome)
	{
		Vector2 vPos = GetPosition(ePosition, bHome);

		if(ePosition < EPosition.EDEF)
		{
			return vPos;
		}
		else if(ePosition < EPosition.EMID) //Defender
		{
			vPos.x = vPos.x * 1.0f;
			vPos.y = vPos.y;
		}
		else if(ePosition < EPosition.EATT) //Midfielder
		{
			vPos.x = vPos.x * 0.7f;
			vPos.y = vPos.y;
		}
		else //Attacker
		{
			vPos.x = vPos.x * 0.7f;
			vPos.y = vPos.y;
		}

		vPos.y = Mathf.Clamp(vPos.y, 0.01f, 0.99f);

		if(bHome)
			vPos.x = Mathf.Clamp(vPos.x, 0.0f, 0.49f);
		else
			vPos.x = Mathf.Clamp(vPos.x, 0.51f, 1.0f);

		return vPos;
	}

	static Vector2 GetFormationPosition(EPosition ePosition)
	{
		return GetPositionInfo(ePosition).vPosition;
	}
	
	public static PostionInfo GetPositionInfo(EPosition ePosition)
	{
		PostionInfo tInfo = new PostionInfo();
		tInfo.vPosition = Vector2.zero;
		tInfo.sName = "N/A";
		
		switch(ePosition)
		{
		case EPosition.GK:
			tInfo.sName = "GK";
			tInfo.vPosition = new Vector2(0, 0.5f);
			break;
		case EPosition.LB:
			tInfo.sName = "LB";
			tInfo.vPosition = new Vector2(0.2f, 0.2f);
			break;
		case EPosition.LCB:
			tInfo.sName = "LCB";
			tInfo.vPosition = new Vector2(0.15f, 0.4f);
			break;
		case EPosition.CB:
			tInfo.sName = "CB";
			tInfo.vPosition = new Vector2(0.15f, 0.5f);
			break;
		case EPosition.RCB:
			tInfo.sName = "RCB";
			tInfo.vPosition = new Vector2(0.15f, 0.6f);
			break;
		case EPosition.RB:
			tInfo.sName = "RB";
			tInfo.vPosition = new Vector2(0.2f, 0.8f);
			break;
		case EPosition.LM:
			tInfo.sName = "LM";
			tInfo.vPosition = new Vector2(0.5f, 0.2f);
			break;
		case EPosition.DM:
			tInfo.sName = "DM";
			tInfo.vPosition = new Vector2(0.3f, 0.5f);
			break;
		case EPosition.DCM:
			tInfo.sName = "DCM";
			tInfo.vPosition = new Vector2(0.35f, 0.5f);
			break;
		case EPosition.LWM:
			tInfo.sName = "LWM";
			tInfo.vPosition = new Vector2(0.6f, 0.2f);
			break;
		case EPosition.LCM:
			tInfo.sName = "LCM";
			tInfo.vPosition = new Vector2(0.5f, 0.4f);
			break;
		case EPosition.CM:
			tInfo.sName = "CM";
			tInfo.vPosition = new Vector2(0.5f, 0.5f);
			break;
		case EPosition.RCM:
			tInfo.sName = "RCM";
			tInfo.vPosition = new Vector2(0.5f, 0.6f);
			break;
		case EPosition.RWM:
			tInfo.sName = "RWM";
			tInfo.vPosition = new Vector2(0.6f, 0.8f);
			break;
		case EPosition.ACM:
			tInfo.sName = "ACM";
			tInfo.vPosition = new Vector2(0.65f, 0.5f);
			break;
		case EPosition.AM:
			tInfo.sName = "AM";
			tInfo.vPosition = new Vector2(0.7f, 0.5f);
			break;
		case EPosition.RM:
			tInfo.sName = "RM";
			tInfo.vPosition = new Vector2(0.5f, 0.8f);
			break;
		case EPosition.LST:
			tInfo.sName = "LST";
			tInfo.vPosition = new Vector2(0.8f, 0.4f);
			break;
		case EPosition.CST:
			tInfo.sName = "CST";
			tInfo.vPosition = new Vector2(0.8f, 0.5f);
			break;
		case EPosition.RST:
			tInfo.sName = "RST";
			tInfo.vPosition = new Vector2(0.8f, 0.6f);
			break;
		case EPosition.LW:
			tInfo.sName = "LW";
			tInfo.vPosition = new Vector2(0.75f, 0.15f);
			break;
		case EPosition.RW:
			tInfo.sName = "RW";
			tInfo.vPosition = new Vector2(0.75f, 0.85f);
			break;
		default:
			tInfo.vPosition = new Vector2(0, 0.5f);
			break;
		}
		
		return tInfo;
	}

	public void SetFormation(EFormation eFormation)
	{
		m_eFormation = eFormation;
		SetDefaultPositions();
	}

	public void SetDefaultPositions()
	{
		switch(m_eFormation)
		{
		case EFormation.E442:
			Set442();
			break;
		case EFormation.E433:
			Set433();
			break;
		case EFormation.E424:
			Set424();
			break;
		case EFormation.E343:
			Set343();
			break;
		case EFormation.E442Dia:
			Set442Diamond();
			break;
		case EFormation.E451:
			Set451();
			break;
		case EFormation.E361:
			Set361();
			break;
		case EFormation.E451Def:
			Set451Defensive();
			break;
		case EFormation.E433Cen:
			Set433Central();
			break;
		case EFormation.E433Att:
			Set433Attacking();
			break;
		}
	}

	void Set442()
	{
		SetFormationPositions(new EPosition[]{EPosition.GK,EPosition.LB,EPosition.LCB,EPosition.RCB,EPosition.RB, EPosition.LWM, 
			EPosition.LCM, EPosition.RCM, EPosition.RWM, EPosition.LST, EPosition.RST});
	}
	
	void Set433()
	{
		SetFormationPositions(new EPosition[]{EPosition.GK,EPosition.LB,EPosition.LCB,EPosition.RCB,EPosition.RB, EPosition.LM, 
			EPosition.CM, EPosition.RM, EPosition.LW, EPosition.CST, EPosition.RW});
	}
	
	void Set424()
	{
		SetFormationPositions(new EPosition[]{EPosition.GK,EPosition.LB,EPosition.LCB,EPosition.RCB,EPosition.RB, EPosition.LCM, 
			EPosition.RCM, EPosition.LW, EPosition.LST, EPosition.RST, EPosition.RW});
	}
	
	void Set343()
	{
		SetFormationPositions(new EPosition[]{EPosition.GK,EPosition.LB,EPosition.CB,EPosition.RB,EPosition.LWM, EPosition.LCM, 
			EPosition.RCM, EPosition.RWM, EPosition.LW, EPosition.CST, EPosition.RW});
	}
	
	void Set442Diamond()
	{
		SetFormationPositions(new EPosition[]{EPosition.GK,EPosition.LB,EPosition.LCB,EPosition.RCB,EPosition.RB, EPosition.DCM, 
			EPosition.LM, EPosition.RM, EPosition.ACM, EPosition.LST, EPosition.RST});
	}
	
	void Set451()
	{
		SetFormationPositions(new EPosition[]{EPosition.GK,EPosition.LB,EPosition.LCB,EPosition.RCB,EPosition.RB, EPosition.LWM, 
			EPosition.LCM, EPosition.RCM, EPosition.RWM, EPosition.AM, EPosition.CST});
	}
	
	void Set361()
	{
		SetFormationPositions(new EPosition[]{EPosition.GK,EPosition.LB,EPosition.CB,EPosition.RB,EPosition.LWM, EPosition.LCM, 
			EPosition.CM, EPosition.RCM, EPosition.RWM, EPosition.AM, EPosition.CST});
	}
	
	void Set451Defensive()
	{
		SetFormationPositions(new EPosition[]{EPosition.GK,EPosition.LB,EPosition.LCB,EPosition.RCB,EPosition.RB, EPosition.LM, 
			EPosition.LCM, EPosition.DCM, EPosition.RCM, EPosition.RM, EPosition.CST});
	}
	
	void Set433Central()
	{
		SetFormationPositions(new EPosition[]{EPosition.GK,EPosition.LB,EPosition.LCB,EPosition.RCB,EPosition.RB, EPosition.LCM, 
			EPosition.DCM, EPosition.RCM, EPosition.LW, EPosition.CST, EPosition.RW});
	}
	
	void Set433Attacking()
	{
		SetFormationPositions(new EPosition[]{EPosition.GK,EPosition.LB,EPosition.LCB,EPosition.RCB,EPosition.RB, EPosition.LCM, 
			EPosition.AM, EPosition.RCM, EPosition.LW, EPosition.CST, EPosition.RW});
	}
}

public class PostionInfo
{
	public string sName;
	public Vector2 vPosition;
}
