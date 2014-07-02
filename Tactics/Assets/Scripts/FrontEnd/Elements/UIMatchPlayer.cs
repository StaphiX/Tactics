using UnityEngine;
using System.Collections;
using SaveData;

public class UIMatchPlayer : UIElement
{
	PlayerAI m_tAI = null;
	float fW = 0.02f;
	FSprite tPlayer;

	public override void Init()
	{
		tPlayer = new FSprite("blank");
		tPlayer.color = new Color(0.6f, 0.07f, 0.07f);
		AddSprite(tPlayer);
	}

	public void SetPlayerAI(PlayerAI tAI)
	{
		m_tAI = tAI;
		if(m_tAI != null)
			m_tAI.SetPosition(Formation.GetPosition(m_tAI.GetFormationPos().ePos, m_tAI.IsHome()));
	}

	public override void Update()
	{
		if(m_tAI != null)
		{
			Vector2 tPosition = m_tAI.GetPosition();
			SetParentOffset(tPosition.x, tPosition.y, fW, 0.0f);
			SetPixelOffset(0.0f, 0.0f, 0.0f, GetRect().width);
			tPlayer.SetPosition(GetRect().x, GetRect().y);
			tPlayer.SetDimensions(GetRect().width, GetRect().height);
		}
	}
}
