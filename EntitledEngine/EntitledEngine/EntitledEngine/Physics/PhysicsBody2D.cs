using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitledEngine.EntitledEngine.Physics
{
	/// <summary>
	/// This is used for th engine to register Colisions with BoxColliders
	/// </summary>
	public class PhysicsBody2D
	{
		public Vector2 Velocity { get; private set; }
		public Material2D material;

		public Shape2D shape { get; }

		public bool Gravity = true;
		public bool OnGround;

		private float GravityConstant= 1f;


		public bool up() {
			if (Velocity.Y < 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		public bool down() {
			if (Velocity.Y > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		public bool left() {
			if (Velocity.X < 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		public bool right() {
			if (Velocity.X > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public void UpdateShapePos(Shape2D other)
		{
			shape.Position += Velocity;
			CollisionLoop(other);

			//Log.Info(shape.BoxCollider.Shape.ToString());
			
			if (OnGround)
			{
				FloorFriction();
			}
		}

		void CollisionLoop(Shape2D other)
		{
			if (!shape.BoxCollider.Collided(other) && Gravity)
			{
				Velocity.Y += GravityConstant * material.GetDensity();
				OnGround = false;
				//Log.Info(Velocity.ToString());

			}
			else if (shape.BoxCollider.Collided(other))
			{
				//Log.Info($" entity:{shape.Tag} collided with: {other.Tag}");
				//Velocity.Y -= GravityConstant;
				//get bounce velocity
				
				Bounce();
				//FloorFriction();
				OnGround = true;
			}
		}

		void Bounce()
		{
			Velocity.Y = material.GetFriction() * -1 * Velocity.Y;
		}
		public void BounceX(Shape2D other)
		{
			other.PhysicsBody.Velocity.X = Velocity.X/other.PhysicsBody.material.GetDensity();
			Velocity.X = other.PhysicsBody.Velocity.X / material.GetDensity();
		}

		//friction that the player uses on the floor when walking
		void FloorFriction()
		{
			if (Velocity.X > 0 && right())
			{
				Velocity.X -= material.GetSmoothness();
			}
			if (Velocity.X < 0 && left())
			{
				Velocity.X += material.GetSmoothness();
			}

			if (Velocity.X > -0.2 && Velocity.X < 0.2f)
			{
				Velocity.X = 0;
			}
			
			
		}

		public void AddVelocity(Vector2 Directon, float speed)
		{
			Velocity += Directon * speed;
		}
		public PhysicsBody2D(Shape2D shape)
		{
			this.shape = shape;
			this.material = new Material2D();
			Velocity = Vector2.Zero();
		}

		public PhysicsBody2D(Shape2D shape, Material2D material, float velX = 0, float velY = 0)
		{
			this.shape = shape;
			this.material = material;
			Velocity = new Vector2(velX, velY);

			EntitledEngine.RegisterPhysicBody(this);
		}

		public void DestroySelf()
		{
			//Log.Warning($"[SHAPE2D]({Tag}) - has been unregisterd");

			EntitledEngine.UnRegisterPhysicsBody(this);
		}
	}
}
