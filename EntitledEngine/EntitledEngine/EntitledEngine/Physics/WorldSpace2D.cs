using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitledEngine.EntitledEngine.Physics
{

	/// <summary>
	/// Place where you simulate the physics in the world
	/// </summary>
	public class WorldSpace2D
	{
		public Vector2 SimulatingSpace { get; private set; }
		public Vector2 Scale { get; private set; }
		public Vector2 Pivot { get; private set; }

		public WorldSpace2D(Vector2 Scale)
		{
			Pivot = Vector2.Zero();
			this.Scale = Scale;
			GetSimulationSpace();
		}
		public WorldSpace2D(Vector2 Scale, Vector2 Pivot)
		{
			this.Pivot = Pivot;
			this.Scale = Scale;
			GetSimulationSpace();
		}

		//to change simualtion spaces
		private void GetSimulationSpace()
		{
			SimulatingSpace = Pivot + Scale;
		}
		public void ChangeSimuilationSpaceSize(Vector2 Size)
		{
			this.Scale = Scale;
		}
		public void ChangeSimulpationSpacePivot(Vector2 Position)
		{
			Pivot = Position;
		}

	}
}
