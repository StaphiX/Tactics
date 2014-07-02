using UnityEngine;
using System.Collections;

public enum EMatchState
{
	NONE,
	Kickoff,
	ESETPLAY,
	InPlay,
}

public class MatchSetup {

	public static MatchSetup tMatchInPlay = null;
	EMatchState m_tState = EMatchState.NONE;

	public EMatchState tState
	{
		get { return m_tState; }
		set { m_tState = value; }
	}

	public void ResetMatch()
	{
		m_tState = EMatchState.NONE;
		tMatchInPlay = this;
	}

	public void Init() 
	{
		ResetMatch();
	}

	public void Update () 
	{
	
	}
}
