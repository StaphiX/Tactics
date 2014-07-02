using UnityEngine;
using System.Collections;

public class Decision 
{
	int m_iWeight = 0;
	bool m_bForceDecision = false;

	public bool bForce
	{
		get{return m_bForceDecision;}
		set{m_bForceDecision = value;}
	}

	public int iWeight
	{
		get{return m_iWeight;}
		set{m_iWeight = value;}
	}

	public virtual void ForceDecision(bool bForce)
	{
		m_bForceDecision = bForce;
	}

	public virtual void CalculateWeight(float fMultiplier)//When you want to use the same function with different weighting
	{

	}

	public virtual void Activate()
	{

	}

}
