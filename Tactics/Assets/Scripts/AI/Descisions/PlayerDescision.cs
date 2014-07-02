using UnityEngine;
using System.Collections;

public class ShouldIdleMove : Decision
{
	PlayerAI tPlayer;

	public ShouldIdleMove(PlayerAI _tPlayer)
	{
		tPlayer = _tPlayer;
	}

	public override void CalculateWeight (float fMultiplier)
	{
		iWeight = 1;
	}

	public override void Activate ()
	{
		tPlayer.IdleMovement();
	}

}

public class IsBallInPlay : Decision
{
	MatchSetup tMatch;
	PlayerAI tPlayer;
	public IsBallInPlay(MatchSetup _tMatch, PlayerAI _tPlayer)
	{
		tMatch = _tMatch;
		tPlayer = _tPlayer;
	}
	
	public override void CalculateWeight (float fMultiplier)
	{
		iWeight = 0;
		if(tMatch.tState > EMatchState.ESETPLAY)
		{
			ForceDecision(true);
		}
	}
	
	public override void Activate ()
	{
		if(tPlayer.IsInPossesion())
		{
			tPlayer.SetState(new PSWithBall());
		}
		else
		{
			tPlayer.SetState(new PSWithoutBall());
		}
	}
}
