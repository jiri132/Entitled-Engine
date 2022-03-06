using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace EntitledEngine.EntitledEngine
{
	public class Shape2D
	{
		public Vector2 Position = null;
		public Vector2 Scale = null;
		//add Vector2 Rotation
		public Vector2 Rotation = null;
		public string Tag = "";
		public Color color;
		public Collider Collider;

		public Physics.BoxCollider2D BoxCollider { get; private set; }
		public Physics.PhysicsBody2D PhysicsBody { get; private set; }
		public Physics.Material2D Material { get; private set; }
		//old method


		/// <summary>
		/// DONT USE THIS this is out of date and cant work with physics or any other objects can only return true or false with collsions!
		/// </summary>
		public Shape2D(Vector2 Position, Vector2 Scale, Vector2 Rotation, Color Color, Collider Collider, string Tag)
		{
			this.Position = Position;
			this.Scale = Scale;
			this.Rotation = Rotation;
			this.Tag = Tag;
			this.color = Color;
			this.Collider = Collider;

			//Log.Info($"[SHAPE2D]({Tag}) - has been registerd");

			EntitledEngine.RegisterShape(this);
		}

		/// <summary>
		/// This is a Shape that registers everything from physics
		/// </summary>
		public Shape2D(Vector2 Position, Vector2 Scale, Vector2 Rotation, Color Color,string Tag)
		{
			//standard stuff
			this.Position = Position;
			this.Scale = Scale;
			this.Rotation = Rotation;
			this.Tag = Tag;
			this.color = Color;
			
			

			//Log.Info($"[SHAPE2D]({Tag}) - has been registerd");

			EntitledEngine.RegisterShape(this);
		}


		public void SetBoxCollider(Physics.BoxCollider2D BoxCollider)
		{
			this.BoxCollider = BoxCollider;
		}
		public void SetPhysicBody(Physics.PhysicsBody2D PhysicsBody)
		{
			this.PhysicsBody = PhysicsBody;
		}
		public void SetMaterial(Physics.Material2D Material)
		{
			this.Material = Material;
		}

		public void DestroySelf()
		{
			//Log.Warning($"[SHAPE2D]({Tag}) - has been unregisterd");

			EntitledEngine.UnRegisterShape(this);
		}
	}
}
