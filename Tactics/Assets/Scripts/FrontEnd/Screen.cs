using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIScreen : UIElement {

	public virtual void AddElement(UIElement tElement)
	{
		AddChild(tElement);
		tElement.Init();
	}
}

public class UIElement {

	Rect m_tRect;
	UIElement m_tParent;
	FStage m_tStage;
	List<UIElement> m_tChildren;

	Rect m_tParentOffsetRect;
	Rect m_tPixelOffsetRect;

	public List<FNode> m_tSprites;

	public virtual UIElement GetParent()
	{
		return m_tParent;
	}

	public virtual void SetParent(UIElement tElement)
	{
		m_tParent = tElement;
		if(m_tParent != null)
			SetStage(m_tParent.GetStage());
	}

	public virtual void AddChild(UIElement tElement)
	{
		if(m_tChildren == null)
			m_tChildren = new List<UIElement>();
		m_tChildren.Add(tElement);
		tElement.SetParent(this);
	}

	public virtual Rect GetRect()
	{
		return m_tRect;
	}

	public virtual Vector2 GetPosition()
	{
		return new Vector2(m_tRect.x, m_tRect.y);
	}

	public virtual float GetWidth()
	{
		return m_tRect.width;
	}

	public virtual float GetHeight()
	{
		return m_tRect.height;
	}

	public virtual void SetParentOffset(float fX, float fY, float fW, float fH)
	{
		m_tParentOffsetRect.x = fX;
		m_tParentOffsetRect.y = fY;
		m_tParentOffsetRect.width = fW;
		m_tParentOffsetRect.height = fH;
		CalculateRect();
	}

	public virtual void SetPixelOffset(float fX, float fY, float fW, float fH)
	{
		m_tPixelOffsetRect.x = fX;
		m_tPixelOffsetRect.y = fY;
		m_tPixelOffsetRect.width = fW;
		m_tPixelOffsetRect.height = fH;
		CalculateRect();
	}

	public virtual FStage GetStage()
	{
		return m_tStage;
	}

	public virtual void SetStage(FStage tStage)
	{
		if(m_tSprites != null)
		{
			foreach(FNode tNode in m_tSprites)
			{
				tNode.RemoveFromContainer();
				tStage.AddChild(tNode);
			}
		}
		m_tStage = tStage;

		if(m_tChildren != null)
		{
			foreach(UIElement tChild in m_tChildren)
			{
				tChild.SetStage(tStage);
			}
		}
	}

	void CalculateRect()
	{
		if(m_tParent != null) //Offset from the center of the parent
		{
			m_tRect.x = (m_tParent.GetRect().x - m_tParent.GetRect().width/2) + m_tParent.GetRect().width * m_tParentOffsetRect.x;
			m_tRect.y = (m_tParent.GetRect().y - m_tParent.GetRect().height/2) + m_tParent.GetRect().height * m_tParentOffsetRect.y;
			m_tRect.width = m_tParent.GetRect().width * m_tParentOffsetRect.width;
			m_tRect.height = m_tParent.GetRect().height * m_tParentOffsetRect.height;
		}
		m_tRect.x += m_tPixelOffsetRect.x;
		m_tRect.y += m_tPixelOffsetRect.y;
		m_tRect.width += m_tPixelOffsetRect.width;
		m_tRect.height += m_tPixelOffsetRect.height;
	}

	public virtual void Init()
	{
		
	}

	public virtual void UpdateChildren()
	{
		if(m_tChildren != null)
		{
			int iElementCount = m_tChildren.Count;
			if(iElementCount > 0)
			{
				for(int iElement = 0; iElement < iElementCount; ++iElement)
				{
					m_tChildren[iElement].Update();
					m_tChildren[iElement].UpdateChildren();
				}
			}
		}
	}

	public virtual void Update()
	{

	}

	public virtual void AddSprite(FSprite tSprite)
	{
		if(tSprite != null)
		{
			if(m_tSprites == null)
				m_tSprites = new List<FNode>();
			m_tSprites.Add(tSprite);
			Futile.stage.AddChild(tSprite);
		}
	}

	public virtual void AddLabel(FLabel tLabel)
	{
		if(tLabel != null)
		{
			if(m_tSprites == null)
				m_tSprites = new List<FNode>();
			m_tSprites.Add(tLabel);
			ScreenStack.tTextStage.AddChild(tLabel);
		}
	}

//	public virtual void InitSprites()
//	{
//		int iSpriteCount = m_tSprites.Count;
//		for(int iSprite = 0; iSprite < iSpriteCount; ++iSprite)
//		{
//			Futile.stage.AddChild(m_tSprites[iSprite]);
//		}
//	}


}
