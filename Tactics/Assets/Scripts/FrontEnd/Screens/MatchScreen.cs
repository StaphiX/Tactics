using UnityEngine;
using System.Collections;
using SaveData;

public class MatchScreen : UIScreen {

	MatchSetup tMatch = new MatchSetup();
	TeamAI tTeamAI = new TeamAI();
	UIMatchPlayer[] tUIPlayer = new UIMatchPlayer[11];

	public override void Init()
	{
		ScreenStack.SetBackgroundCol(new Color(0.15f, 0.5f, 0.1f));
		SetStage(new FStage("MATCH"));
		SetPixelOffset(0, 0, Screen.width, Screen.height);

		tMatch.Init();
		tMatch.tState = EMatchState.Kickoff;
		tTeamAI.Init();


		EFormation eForm = EFormation.E442;
		Formation tForm = new Formation();
		tForm.SetFormation(eForm);

		for(int iPlayer = 0; iPlayer < tUIPlayer.Length; ++iPlayer)
		{
			tTeamAI.GetPlayerAI(iPlayer).SetFormationPos(tForm.GetFormationPosition(iPlayer));

			tUIPlayer[iPlayer] = new UIMatchPlayer();
			tUIPlayer[iPlayer].SetParent(this);
			tUIPlayer[iPlayer].Init();
			tUIPlayer[iPlayer].SetPlayerAI(tTeamAI.GetPlayerAI(iPlayer));
		}
	}

	public override void Update()
	{
		tTeamAI.Update();

		for(int iPlayer = 0; iPlayer < tUIPlayer.Length; ++iPlayer)
		{
			tUIPlayer[iPlayer].Update();
		}
	}
}
