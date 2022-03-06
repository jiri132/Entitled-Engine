using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitledEngine.EntitledEngine.Physics
{
	/// <summary>
	/// This has been the new boxcollider for use
	/// </summary>
	public class BoxCollider2D
	{

		public Shape2D Shape;
		public bool Collided(Shape2D Other)
		{
			if (Other.Position.X < Shape.Position.X + Shape.Scale.X &&
					Other.Position.X + Other.Scale.X > Shape.Position.X &&
					Other.Position.Y < Shape.Position.Y + Shape.Scale.Y &&
					Other.Position.Y + Other.Scale.Y > Shape.Position.Y)
			{
				return true;
			}else
			{
				//Log.Info($"No collission with {Other} & {Shape}");
				return false;
			}
		}
		public bool InsideWorldSpace(WorldSpace2D World)
		{
			if (World.Pivot.X < Shape.Position.X + Shape.Scale.X &&
					World.Pivot.X + World.Scale.X > Shape.Position.X &&
					World.Pivot.Y < Shape.Position.Y + Shape.Scale.Y &&
					World.Pivot.Y + World.Scale.Y > Shape.Position.Y)
			{
				return true;
			}
			else
			{
				//Log.Info($"No collission with {Other} & {Shape}");
				return false;
			}
		}

		public BoxCollider2D(Shape2D Shape)
		{
			this.Shape = Shape;

			EntitledEngine.RegisterBoxColliders(this);
		}
		public BoxCollider2D()
		{
			this.Shape = null;
		}

		public void DestroySelf()
		{
			//Log.Warning($"[SHAPE2D]({Tag}) - has been unregisterd");

			EntitledEngine.UnRegisterBoxColliders(this);
		}
	}
}
