using UnityEngine;
using System;
using System.Collections;

public delegate void StateAction<T>(T Owner);

public class StateMachine <T>
{
	T m_tOwner;
	private Enum m_eState;
	//private StateBehavior m_tBehavior;

	public StateAction<T> StateOnEnter;
	public StateAction<T> StateUpdate;
	public StateAction<T> StateOnExit;

	public StateMachine(T Owner)
	{
		m_tOwner = Owner;
	}

	public Enum eState
	{
		get
		{
			return m_eState;
		}
		set
		{
			m_eState = value;          
		}
	}

	public void SetState(StateBehavior<T> tBehavior)
	{
		if(StateOnExit != null)
			StateOnExit(m_tOwner);
		eState = tBehavior.eState;
		tBehavior.SetActions(this);
		StateOnEnter(m_tOwner);
	}

	public virtual void Update()
	{
		if(StateUpdate != null)
			StateUpdate(m_tOwner); //Update behavior
	}
}


public class StateBehavior <T>
{
	public StateBehavior(Enum eState)
	{
		m_eState = eState;
		m_tDecisionMaker = new DecisionMaker();
	}

	DecisionMaker m_tDecisionMaker;
	public DecisionMaker tDecisionMaker
	{
		get {return m_tDecisionMaker;}
		set {m_tDecisionMaker = value;}
	}

	Enum m_eState;

	public Enum eState
	{
		get
		{
			return m_eState;
		}
		set
		{
			m_eState = value;           
		}
	}

	public virtual void SetActions(StateMachine<T> tStateMachine)
	{
		tStateMachine.StateOnEnter = OnEnter;
		tStateMachine.StateUpdate = Update;
		tStateMachine.StateOnExit = OnExit;
	}

	public virtual void OnEnter(T Owner) {}
	public virtual void Update(T Owner) {}
	public virtual void OnExit(T Owner) {}
}


