using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DecisionMaker
{
	int m_iSeed = Random.Range(0,int.MaxValue);
	System.Random m_tRandom;
	List<Decision> m_tDecisions;
	int m_iTotalWeight = 0;
	bool m_bForceDecision = false;

	public System.Random tRandom
	{
		get 
		{
			if(m_tRandom == null)
			{
				m_tRandom = new System.Random(m_iSeed);
			}
			return m_tRandom;
		}
	}

	public int iSeed
	{
		get {return m_iSeed;}
		set {m_iSeed = value; m_tRandom = new System.Random(m_iSeed);}
	}

	public int iDecisionCount
	{
		get{return m_tDecisions == null ? 0 : m_tDecisions.Count; }
	}

	public void ClearDecisions()
	{
		if(m_tDecisions != null)
			m_tDecisions.Clear();

		m_iTotalWeight = 0;
		m_bForceDecision = false;
	}

	public void AddDecision(Decision tDecision, float fMultipler)
	{
		if(m_tDecisions == null)
			m_tDecisions = new List<Decision>();

		m_tDecisions.Add(tDecision);
		tDecision.CalculateWeight(fMultipler);
		m_iTotalWeight += tDecision.iWeight;

		if(tDecision.bForce)
			m_bForceDecision = true;
	}

	public void RecalculateWeight(Decision tDecision, float fMultipler)
	{
		m_iTotalWeight -= tDecision.iWeight;
		tDecision.CalculateWeight(fMultipler);
		m_iTotalWeight += tDecision.iWeight;

		if(tDecision.bForce)
			m_bForceDecision = true;
	}

	public void ResolveDecision()
	{
		int iValue = tRandom.Next(0, m_iTotalWeight+1);
		int iTotal = 0;
		foreach(Decision tDecision in m_tDecisions)
		{
			if(!m_bForceDecision) //There is no forced decision find random one
			{
				iTotal += tDecision.iWeight;
				if(iValue <= iTotal)
				{
					tDecision.Activate();
					return;
				}
			}
			else
			{
				if(tDecision.bForce)
				{
					tDecision.Activate();
					m_bForceDecision = false;
					return;
				}
				m_bForceDecision = false;
			}
		}
		if(m_tDecisions != null && m_tDecisions.Count > 0) //DEFAULT TO VERY LAST DESICION IF WE HAVE FAILED TO SELECT ONE
		{
			m_tDecisions[m_tDecisions.Count-1].Activate();
		}

	}
}
