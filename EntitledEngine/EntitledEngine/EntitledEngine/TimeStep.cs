using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitledEngine.EntitledEngine
{
	/// <summary>
	/// WIP timestep for calculating physics
	/// </summary>
	public class TimeStep
	{
		private float m_Time;

		public float GetSeconds() { return m_Time; }
		public float GetMiliseconds() { return m_Time * 1000f; }

		public TimeStep(float time = 0f)
		{
			m_Time = time;
		}


	}
}
