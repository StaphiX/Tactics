using UnityEngine;
using System.Collections;
using SaveData;

public class TeamAI 
{
	PlayerAI[] tPlayer = new PlayerAI[11];

	public void Init()
	{
		for(int iPlayer = 0; iPlayer < tPlayer.Length; ++iPlayer)
		{
			tPlayer[iPlayer] = new PlayerAI();
			tPlayer[iPlayer].Init(new FormationPos(EPosition.DCM));
		}
	}

	public void Update()
	{
		for(int iPlayer = 0; iPlayer < tPlayer.Length; ++iPlayer)
		{
			tPlayer[iPlayer].Update();
		}
	}

	public PlayerAI GetPlayerAI(int iIndex)
	{
		return tPlayer[iIndex];
	}

}
