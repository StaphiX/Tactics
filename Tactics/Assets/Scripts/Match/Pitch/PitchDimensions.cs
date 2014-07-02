using UnityEngine;
using System.Collections;

public class PitchDimensions {
	
		public float m_fMeterSize = 10.0f;
		
		public float m_fLineW = 0;
		//Pitch Orientation is Landscape
		public float m_fPitchW = 0;
		public float m_fPitchH = 0;
		public float m_fSidelineOffset = 0;
		public float m_fBacklineOffset = 0;
		public float m_fCenterCircleW = 0;
		public float m_fSixYardW = 0;
		public float m_fSixYardH = 0;
		public float m_fEighteenYardW = 0;
		public float m_fEighteenYardH = 0;
		public float m_fGoalW = 0;
		public float m_fGoalH = 0;
		public float m_fCornerW = 0;
		public float m_fPlayerW = 0;
		public float m_fBallW = 0;
		
		public PitchDimensions(float fMeterSize)
		{
			InitialisePitchDim(fMeterSize);
		}
		
		public void InitialisePitchDim(float fMeterSize)
		{
			m_fMeterSize = fMeterSize;
			//Pitch Orientation is Landscape
			m_fLineW = m_fMeterSize / 4.0f;
			m_fPitchW = 100 * m_fMeterSize;
			m_fPitchH = 70 * m_fMeterSize;
			m_fSidelineOffset = 5 * m_fMeterSize;
			m_fBacklineOffset = 10 * m_fMeterSize;
			m_fCenterCircleW = 18.3f * m_fMeterSize;
			m_fSixYardW = 5.5f * m_fMeterSize;
			m_fSixYardH = 18.2f * m_fMeterSize;
			m_fEighteenYardW = 16.5f * m_fMeterSize;
			m_fEighteenYardH = 40.3f * m_fMeterSize;
			m_fGoalW = 1.8f * m_fMeterSize;
			m_fGoalH = 7.3f * m_fMeterSize;
			m_fCornerW = 0.9f * m_fMeterSize;
			m_fPlayerW = 5*m_fMeterSize;
			m_fBallW = 2*m_fMeterSize;
		}
}
