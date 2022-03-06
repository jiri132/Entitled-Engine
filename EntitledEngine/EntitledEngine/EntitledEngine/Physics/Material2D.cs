using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitledEngine.EntitledEngine.Physics
{
	/// <summary>
	/// This is used for the physicsBody so it can register all the info of physics
	/// </summary>
	public class Material2D
	{
		private float Friction { get; set; }
		private float Density { get; set; }
		private float Smoothness { get; set; }

		public Material2D(float Fr = 0.6f, float De = 1f, float Sm = 0.2f)
		{
			this.Friction = Fr;
			this.Density = De;
			this.Smoothness = Sm;
		}

		public override string ToString()
		{
			return $"[ Friction: {Friction} | Density: {Density} | Smoothness: {Smoothness} ]";
		}

		#region Get the info
		public float GetFriction()
		{
			return Friction;
		}
		public float GetDensity()
		{
			return Density;
		}
		public float GetSmoothness()
		{
			return Smoothness;
		}
		#endregion

		#region Change Funcs
		public void ChangeFriction(float newFr)
		{
			this.Friction = newFr;
		}
		public void ChangeDensity(float newDe)
		{
			this.Density = newDe;
		}
		public void ChangeSmoothness(float newSm)
		{
			this.Smoothness = newSm;
		}
		#endregion



	}
}
