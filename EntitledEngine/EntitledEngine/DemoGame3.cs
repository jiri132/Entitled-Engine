using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using EntitledEngine.EntitledEngine;
using EntitledEngine.EntitledEngine.Physics;

/* 
 * 
 * --------------------------Physics--------------------------
 * made in 2 months with version 3 of the engine
 * 
 * 
 * new wit version 2 ofthe engine 
 * Physics
 * improved Boxcollider
 * shape2d class renewing
 * 
 * made by Jiri
 * 
 * ---------------------------------------------------------
 * 
 */


namespace EntitledEngine
{
	enum direction { UP, DOWN, LEFT, RIGHT, NULL};

	class DemoGame3 : EntitledEngine.EntitledEngine
	{
		//Screen data
		static float screenX = 500;
		static float screenY = 500;
		public static Vector2 Screen = new Vector2(screenX + 16, screenY + 39);
		//making the game info with title and scren size for the window
		public DemoGame3() : base(Screen, "Entitled Engine Demo", "2D") { }
			
		
		static Shape2D CollidingObject = new Shape2D(new Vector2(-2500, 400), new Vector2(5000, 1500), Vector2.Zero(), Color.Red, "Floor");
		static Shape2D CollidingObject2 = new Shape2D(new Vector2(200, 200), new Vector2(200, 15), Vector2.Zero(), Color.Red, "Floor2");
		static Shape2D shape = new Shape2D(new Vector2(screenX / 2, 10), new Vector2(30, 50), Vector2.Zero(), Color.Blue, "Test-Subject");
		static Shape2D shape2 = new Shape2D(new Vector2(screenX * 2, 10), new Vector2(30, 50), Vector2.Zero(), Color.Green, "Test-Subject2");
		static Shape2D shape3 = new Shape2D(new Vector2(screenX, 10), new Vector2(30, 50), Vector2.Zero(), Color.Green, "Test-Subject3");
		static Shape2D shape4 = new Shape2D(new Vector2(100, 10), new Vector2(30, 50), Vector2.Zero(), Color.Green, "Test-Subject4");
		static Shape2D shape5 = new Shape2D(new Vector2(screenX *3, 10), new Vector2(30, 50), Vector2.Zero(), Color.Green, "Test-Subject5");
		//material so i can reuse it on more objects
		static Material2D PlayerMaterial = new Material2D(0.5f);
		Material2D ObjMat()
		{
			Random random = new Random();
			Material2D material = new Material2D();

			material.ChangeDensity((float)random.Next(3,20)/10);
			material.ChangeFriction((float)random.Next(4, 10) / 10);
			material.ChangeSmoothness((float)random.Next(7, 15) / 10);

			return material;
		}

		direction direction = direction.NULL;
		float Speed = 1f;
		float JumpForce = 18f;
		public override void OnLoad()
		{

			CameraPosition = new Vector2(256, 256);

			//creaing a point for deltatime (WIP)
			DeltaTime.CreatePoint();

			CollidingObject.SetBoxCollider(new BoxCollider2D(CollidingObject));
			CollidingObject2.SetBoxCollider(new BoxCollider2D(CollidingObject2));

			//makematerial of the player can aso be done when creating shape
			shape.SetMaterial(PlayerMaterial);
			shape2.SetMaterial(PlayerMaterial);
			shape3.SetMaterial(PlayerMaterial);
			shape4.SetMaterial(PlayerMaterial);
			shape5.SetMaterial(PlayerMaterial);

			Log.Warning($"| {shape2.Material.ToString()}\n | {shape3.Material.ToString()}\n | {shape3.Material.ToString()}\n | {shape4.Material.ToString()}\n | {shape5.Material.ToString()}");

			//Giving the boxcolliders and physics bodys their needed information of the shape
			shape.SetBoxCollider(new BoxCollider2D(shape));
			shape.SetPhysicBody(new PhysicsBody2D(shape, shape.Material,0));

			shape2.SetBoxCollider(new BoxCollider2D(shape2));
			shape2.SetPhysicBody(new PhysicsBody2D(shape2, shape2.Material, 2.3f));

			shape3.SetBoxCollider(new BoxCollider2D(shape3));
			shape3.SetPhysicBody(new PhysicsBody2D(shape3, shape3.Material, 0.3f));

			shape4.SetBoxCollider(new BoxCollider2D(shape4));
			shape4.SetPhysicBody(new PhysicsBody2D(shape4, shape4.Material, 4f));

			shape5.SetBoxCollider(new BoxCollider2D(shape5));
			shape5.SetPhysicBody(new PhysicsBody2D(shape5, shape5.Material, -1f));

			
			//shape = new Shape2D(new Vector2(screenX / 2, 10), new Vector2(10, 10), Vector2.Zero(), Color.Green, new BoxCollider(shape), new PhysicsBody(shape), "Test-Subject");
			//CollidingObject = new Shape2D(new Vector2(0, 400), new Vector2(500, 25), Vector2.Zero(), Color.Red, new BoxCollider(CollidingObject), "colliding object");
		}
		int time;
		public override void OnUpdate()
		{
			time++;
			//to track the physics of the PhysicsBody of player for testing
			Log.Info($"{time.ToString()} | {shape.PhysicsBody.Velocity.ToString()} | {shape.Position.ToString()}");
			//Log.Info($"{shape.BoxCollider.InsideWorldSpace(worldSpace).ToString()}");

			Movement();
		}

		void Movement()
		{
			switch (direction)
			{
				case direction.UP:
					//Log.Info(shape.PhysicsBody.OnGround.ToString());
					//Log.Info($" {shape.PhysicsBody.Velocity.ToString()} | {shape.Position.ToString()}");
					if (shape.PhysicsBody.OnGround)
					{
						shape.PhysicsBody.AddVelocity(Vector2.Up(), -JumpForce);
					}

					break;
				case direction.DOWN:
					//shape.PhysicsBody.AddVelocity(Vector2.Down(), Speed);
					break;
				case direction.LEFT:
					shape.PhysicsBody.AddVelocity(Vector2.Left(), Speed);
					break;
				case direction.RIGHT:
					shape.PhysicsBody.AddVelocity(Vector2.Right(), Speed);
					break;
				case direction.NULL:
					//do nothing
					break;
				default:
					break;
			}
		}


		public override void OnDraw()
		{
			
		}
		public override void GetKeyDown(KeyEventArgs e)
		{
			if (e.KeyCode == Keys.A /*|| e.KeyCode == Keys.Left*/)
			{
				direction = direction.LEFT;
			}
			if (e.KeyCode == Keys.W /*|| e.KeyCode == Keys.Up*/)
			{
				direction = direction.UP;
			}
			if (e.KeyCode == Keys.S /*|| e.KeyCode == Keys.Down*/)
			{
				direction = direction.DOWN;
			}
			if (e.KeyCode == Keys.D /*|| e.KeyCode == Keys.Right*/)
			{
				direction = direction.RIGHT;
			}
		}
		public override void GetKeyUp(KeyEventArgs e)
		{
			if (e.KeyCode == Keys.A /*|| e.KeyCode == Keys.Left*/)
			{
				direction = direction.NULL;
			}
			if (e.KeyCode == Keys.W /*|| e.KeyCode == Keys.Up*/)
			{
				direction = direction.NULL;
			}
			if (e.KeyCode == Keys.S /*|| e.KeyCode == Keys.Down*/)
			{
				direction = direction.NULL;
			}
			if (e.KeyCode == Keys.D /*|| e.KeyCode == Keys.Right*/)
			{
				direction = direction.NULL;
			}
		}

		
	}
}
