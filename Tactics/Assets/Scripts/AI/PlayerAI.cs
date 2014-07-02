using UnityEngine;
using System;
using System.Collections;
using SaveData;

public class PlayerAI
{
	StateMachine<PlayerAI> m_tStateMachine;
	FormationPos m_tFormationPos;
	bool m_bMakeDescision = false;
	bool m_bHasPossesion = false;

/////////////////////////////////////////////////////////////////////////////
/// Movement
////////////////////////////////////////////////////////////////////////////
/// All values are normalised as a percentage of the pitchX and pitchY
	bool m_bCanMove = true;
	Vector2 m_vPosition = Vector2.zero;
	Vector2 m_vDesiredPosition = Vector2.zero;
	Vector2 m_vVelocity = Vector2.zero;
	Vector2 m_vAcceleration = Vector2.zero;


	Action m_tAction = null;

	public void Init(FormationPos tPos)
	{
		SetFormationPos(tPos);
		SetupStateMachine();
	}

	private void SetupStateMachine()
	{
		m_tStateMachine = new StateMachine<PlayerAI>(this);
		SetState(new PSKickoff());
	}

	public void SetState(StateBehavior<PlayerAI> tState)
	{
		m_tStateMachine.SetState(tState);
	}

	public void SetFormationPos(FormationPos tPos)
	{
		m_tFormationPos = tPos;
	}

	public FormationPos GetFormationPos()
	{
		return m_tFormationPos;
	}

	public void Update()
	{
		m_tStateMachine.Update();
		CompleteAction();
	}

	public void CompleteAction()
	{
		if(m_tAction != null)
			m_tAction();
	}
		
	public bool IsHome()
	{
		return true;
	}

	public void SetPosition(Vector2 vPos)
	{
		m_vPosition = vPos;
	}
	
	public Vector2 GetPosition()
	{
		return m_vPosition;
	}

/////////////////////////////////////////////////////////////////////////////
/// Player Actions
////////////////////////////////////////////////////////////////////////////

	public void IdleMovement()
	{
		if(!m_bCanMove)
		{
			SetMakeDecision(true);
		}
		else
		{
			if(m_vDesiredPosition.x == 0)
				UnityEngine.Random.seed = 11;
			else
				UnityEngine.Random.seed = (int)(m_vDesiredPosition.x*10000);
			m_vDesiredPosition.x = UnityEngine.Random.Range(0.0f, 1.0f);
			m_vDesiredPosition.y = UnityEngine.Random.Range(0.0f, 1.0f);
			m_tAction = MoveToPosition;
		}
	}

	void AccelerateDecelerate()
	{
		float fMaxAccelX = 0.00002f;
		float fMaxAccelY = 0.00002f;
		//Set acceleration
		Vector2 vNormalTarget = (m_vDesiredPosition - m_vPosition).normalized;

		float fCurrentAngle = Mathf.Atan2(m_vVelocity.y,m_vVelocity.x);
		float fNewAngle = Mathf.Atan2(vNormalTarget.y,vNormalTarget.x);

		float fAngleDiff = Mathf.Abs(fCurrentAngle - fNewAngle); 
		fAngleDiff = ((2*Mathf.PI) - fAngleDiff)*fAngleDiff/Mathf.PI;//180 degrees is the sharpest movement
		fMaxAccelX += (fAngleDiff / Mathf.PI) * Mathf.Abs(m_vVelocity.x)/4;
		fMaxAccelY += (fAngleDiff / Mathf.PI) * Mathf.Abs(m_vVelocity.y)/4;

		if(fMaxAccelX > 0.001f || fMaxAccelY > 0.001f)
		{
			fMaxAccelX = 0.001f;
			fMaxAccelY = 0.001f;
		}

		m_vAcceleration.x = vNormalTarget.x * fMaxAccelX;
		m_vAcceleration.y = vNormalTarget.y * fMaxAccelY;

	}

	public void MoveToPosition()
	{
		float fMaxVelocity = 0.002f;

		AccelerateDecelerate();

		m_vVelocity.x = Mathf.Clamp(m_vVelocity.x + m_vAcceleration.x, -fMaxVelocity, fMaxVelocity);
		m_vVelocity.y = Mathf.Clamp(m_vVelocity.y + m_vAcceleration.y, -fMaxVelocity, fMaxVelocity);

		if(Mathf.Abs(m_vVelocity.x) > Mathf.Abs(m_vPosition.x - m_vDesiredPosition.x))
		{
		    m_vPosition.x = m_vDesiredPosition.x;
		}
		else
		   m_vPosition.x += m_vVelocity.x;
		if(Mathf.Abs(m_vVelocity.y) > Mathf.Abs(m_vPosition.y - m_vDesiredPosition.y))
		{
		    m_vPosition.y = m_vDesiredPosition.y;
		}
		else
		   m_vPosition.y += m_vVelocity.y;

		if(Mathf.Approximately(m_vDesiredPosition.x, m_vPosition.x)) //Have reached our target
			if(Mathf.Approximately(m_vDesiredPosition.y, m_vPosition.y))
				SetMakeDecision(true);
	}

	public void SetPossesion(bool bPossesion)
	{
		m_bHasPossesion = bPossesion;
	}

	public bool IsInPossesion()
	{
		return m_bHasPossesion;
	}

	public void SetMakeDecision(bool bDecision)
	{
		m_bMakeDescision = bDecision;
	}

	public bool MakeDecision()
	{
		return m_bMakeDescision;
	}
}

public enum EPlayerState
{
	EKickoff,
	EWithBall,
	EWithoutBall,
}

//READ THIS
//http://playmedusa.com/blog/a-finite-state-machine-in-c-for-unity3d/

public class PSKickoff : StateBehavior<PlayerAI>
{
	public PSKickoff() : base (EPlayerState.EKickoff)
	{

	}

	/// Decisions
	ShouldIdleMove tShouldIdleMove;
	IsBallInPlay tIsBallInPlay;

	public override void OnEnter(PlayerAI tPlayer) 
	{
		Vector2 vKickoffPos = Formation.GetKickoffPosition(tPlayer.GetFormationPos().ePos, tPlayer.IsHome());
		tPlayer.SetPosition(vKickoffPos);
		tPlayer.SetMakeDecision(true);
	}
	public override void Update(PlayerAI tPlayer) 
	{
		if(tPlayer.MakeDecision())
		{
			if(tDecisionMaker.iDecisionCount <= 0)
			{
				tShouldIdleMove = new ShouldIdleMove(tPlayer);
				tIsBallInPlay = new IsBallInPlay(MatchSetup.tMatchInPlay, tPlayer);
				tDecisionMaker.AddDecision(tShouldIdleMove, 1.0f);
				tDecisionMaker.AddDecision(tIsBallInPlay, 1.0f);
			}
			else
			{
				tDecisionMaker.RecalculateWeight(tShouldIdleMove, 1.0f);
				tDecisionMaker.RecalculateWeight(tIsBallInPlay, 1.0f);
			}

			tDecisionMaker.ResolveDecision();
			tPlayer.SetMakeDecision(false);
		}
	}
	public override void OnExit(PlayerAI tPlayer) {}
}

public class PSWithBall : StateBehavior <PlayerAI>
{
	public PSWithBall() : base (EPlayerState.EWithBall)
	{
		
	}
	
	public virtual void OnEnter() 
	{
		int i = 10;
		++i;
	}
	public virtual void Update() {}
	public virtual void OnExit() {}
}

public class PSWithoutBall : StateBehavior<PlayerAI>
{
	public PSWithoutBall() : base (EPlayerState.EWithoutBall)
	{
		
	}
	
	public virtual void OnEnter() 
	{
		int i = 10;
		++i;
	}
	public virtual void Update() {}
	public virtual void OnExit() {}
}