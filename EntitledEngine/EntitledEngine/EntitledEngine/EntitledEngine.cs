using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

using EntitledEngine.EntitledEngine;
using EntitledEngine.EntitledEngine.Core;
using EntitledEngine.EntitledEngine.Core._2D;

using EntitledEngine.EntitledEngine.Shapes3D;

namespace EntitledEngine.EntitledEngine
{
	class Canvas : Form
	{
		public Canvas()
		{
			this.DoubleBuffered = true;
		}
	}

	public abstract class EntitledEngine	
	{
		#region Publics that are safe to edit
		public Vector2 ScreenSize = new Vector2(512, 512);//base 512x512 screen in pixels
		public Units Units = new Units(64);//base 64pixels per unit
	
		public string Dimension; //use "2D" or "3D"

		//"!!! FALSE is experimental!!!"\\
		public bool orthographic; //use true for orthographic and false for prespective

		public Color BackgroundColor = Color.Beige; //change this color to any color you want advicing black

		public Vector2 CameraZoom = new Vector2(.5f, .5f); //dont really mess with the camera zoom it is not really needed
		public Vector2 CameraPosition = Vector2.Zero(); //position will automaticly go to the middle when the screen starts
		public float CameraAngle = 0; //rotation of the camera in 2D on Z axis R to L

		public Physics.WorldSpace2D worldSpace = new Physics.WorldSpace2D(new Vector2(4096, 4096), new Vector2(-2048, -2048));
		#endregion

		#region Privates to make the engine work like used to
		//normal window behaviours
		private string Title = "Pong";
		private Canvas Window = null;
		private bool normalizedWindow = true;
		private Thread GameLoopThread = null;


		//list for redering 2ds
		private static List<Shape2D> AllShapes = new List<Shape2D>();
		private static List<Sprite2D> AllSprites = new List<Sprite2D>();
		private static List<Line2D> AllLines = new List<Line2D>();
		private static List<Points2D> AllPoints = new List<Points2D>();

		//lists for rendering 3ds
		private static List<Cube3D> AllCubes = new List<Cube3D>();

		//lists for rendering physics objects 2ds
		private static List<Physics.PhysicsBody2D> AllPhysicsBodys = new List<Physics.PhysicsBody2D>();
		private static List<Physics.BoxCollider2D> AllBoxColliders = new List<Physics.BoxCollider2D>();
		#endregion





		//orthographic projection
		protected float[,] Projection = {
			{1,0,0 },
			{0,1,0 },
			{0,0,1 }
		};

		//private TimeStep m_TimeStep;
		
		public EntitledEngine(Vector2 ScreenSize, string Title, string dimension)
		{
			//make the affected type of engine usable
			this.ScreenSize = ScreenSize;
			this.Title = Title;
			this.Dimension = dimension;
			//give all behaviours of the window
			Window = new Canvas();
			Window.Size = new Size((int)this.ScreenSize.X, (int)this.ScreenSize.Y);
			Window.Text = this.Title;
			Window.Paint += Renderer;
			Window.KeyDown += Window_KeyDown;
			Window.KeyUp += Window_KeyUp;
			Window.ResizeEnd += Resize_Window;
			Window.Resize += Maxmize;

			GameLoopThread = new Thread(GameLoop);
			GameLoopThread.Start();

			Application.Run(Window);
		}

		private void Window_KeyUp(object sender, KeyEventArgs e)
		{
			GetKeyUp(e);
		}

		private void Window_KeyDown(object sender, KeyEventArgs e)
		{
			GetKeyDown(e);
		}

		//used to reize the window to the correct size ith units for maximizing and normalizing the window
		private void Maxmize(object sender, EventArgs e)
		{
			
			if (Window.WindowState == FormWindowState.Maximized || Window.WindowState == FormWindowState.Normal && !normalizedWindow )
			{
				int oldPPU = Units.PixPerUnit;

				Log.Error("");
				Log.Normal($"old: {Units.PixPerUnit}");
				Vector2 oldScreenSize = ScreenSize;
				ScreenSize = new Vector2(Window.Size.Width, Window.Size.Height);

				CameraPosition = ScreenSize / 2;

				float multiplier = ScreenSize.X / oldScreenSize.X;
				Units.ChangePixPerUnit((int)(Units.PixPerUnit * multiplier));
				Log.Normal($"multiplier: {multiplier}");
				Log.Info($"new: {Units.PixPerUnit} = {oldPPU} * {multiplier} ");
			}
			if (Window.WindowState == FormWindowState.Maximized) { normalizedWindow = false; }
			if (Window.WindowState == FormWindowState.Normal) { normalizedWindow = true; }
		}
		//used for making the correct units if you drag the window larger
		private void Resize_Window(object sender, EventArgs e)
		{
			Control control = (Control)sender;

			if (Window.WindowState == FormWindowState.Maximized || control.Size.Height + control.Size.Width != ScreenSize.X + ScreenSize.Y )
			{
				int oldPPU = Units.PixPerUnit; 

				Log.Error("");
				Log.Normal($"old: {Units.PixPerUnit}");
				Vector2 oldScreenSize = ScreenSize;
				ScreenSize = new Vector2(control.Size.Width, control.Size.Height);

				CameraPosition = ScreenSize / 2;

				float multiplier = ScreenSize.X / oldScreenSize.X;
				Units.ChangePixPerUnit((int)(Units.PixPerUnit * multiplier));
				Log.Normal($"multiplier: {multiplier}");
				Log.Info($"new: {Units.PixPerUnit} = {oldPPU} * {multiplier} ");

			}
		}

		#region Registers / UnRegisters

		#region Shape | Line | Sprite
		public static void RegisterShape(Shape2D shape)
		{
			AllShapes.Add(shape);
		}
		public static void RegisterSprite(Sprite2D sprite)
		{
			AllSprites.Add(sprite);
		}
		public static void RegisterLine(Line2D line)
		{
			AllLines.Add(line);
		}
		public static void UnRegisterShape(Shape2D shape)
		{
			AllShapes.Remove(shape);
		}
		public static void UnRegisterSprite(Sprite2D sprite)
		{
			AllSprites.Remove(sprite);
		}
		public static void UnRegisterLine(Line2D line)
		{
			AllLines.Remove(line);
		}

		public static void RegisterPoints(Points2D points)
		{
			AllPoints.Add(points);
		}
		public static void UnRegisterPoints(Points2D points)
		{
			AllPoints.Remove(points);
		}
		public static void RegisterCubes(Cube3D cube)
		{
			AllCubes.Add(cube);
		}
		public static void UnRegisterCubes(Cube3D cube)
		{
			AllCubes.Remove(cube);
		}
		#endregion
		#region PhysicsBody | BoxCollider
		public static void RegisterPhysicBody(Physics.PhysicsBody2D body)
		{
			AllPhysicsBodys.Add(body);
		}
		public static void RegisterBoxColliders(Physics.BoxCollider2D boxColl)
		{
			AllBoxColliders.Add(boxColl);
		}
		public static void UnRegisterPhysicsBody(Physics.PhysicsBody2D body)
		{
			AllPhysicsBodys.Remove(body);
		}
		public static void UnRegisterBoxColliders(Physics.BoxCollider2D boxColl)
		{
			AllBoxColliders.Remove(boxColl);
		}
		#endregion
		#endregion

		void GameLoop()
		{

			OnLoad();
			while(GameLoopThread.IsAlive)
			{
				try
				{
					//float time = (int)Window.GetLifetimeService();
					//Log.Info(time.ToString());
					OnDraw();
					Window.BeginInvoke((MethodInvoker)delegate { Window.Refresh(); });

					if (Dimension == "2D")
                    {
						PhysicsUpdate2D();
					}
					else if (Dimension == "3D")
                    {

                    }else
                    {
						Log.Error("NO dimension fo PHYSICS");

                    }
					OnUpdate();

					Thread.Sleep(8);
				}
				catch (Exception e)
				{
					Log.Error(e.ToString());
					Log.Error("Window has not been found... Waiting...");
					if (!Window.Visible)
					{
						ExitCodes.QuitConsole();
					}
					
				}
			}
		}
		private void Renderer(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;

			g.Clear(BackgroundColor);

			g.TranslateTransform(CameraPosition.X, CameraPosition.Y);
			g.RotateTransform(CameraAngle);
			g.ScaleTransform(CameraZoom.X, CameraZoom.Y);

			//drawing of 2D objects
			if (Dimension == "2D")
			{
				foreach (Shape2D shape in AllShapes.ToArray())
				{
					g.FillRectangle(new SolidBrush(shape.color), shape.Position.X, shape.Position.Y, shape.Scale.X, shape.Scale.Y);
				}


				foreach (Sprite2D sprite in AllSprites.ToArray())
				{
					g.DrawImage(sprite.Sprite, sprite.Position.X, sprite.Position.Y, sprite.Scale.X, sprite.Scale.Y);
				}
				foreach (Line2D line in AllLines.ToArray())
				{
					Pen p = new Pen(line.color, line.Width);
					g.DrawLine(p, line.A.X, line.A.Y, line.B.X, line.B.Y);

				}
			}
			//dragin of 3D objects
			else if (Dimension == "3D")
			{
                foreach (Points2D p in AllPoints)
                {
					Vector3 Projection2D = Vector3.Zero();
					Vector3 rotation = Vector3.Zero();


					if (p.rotationX != null)
                    {
						rotation = Vector3.MatMul(p.rotationX, p.position);
					}
					if (p.rotationY != null) 
                    {
						if (rotation == Vector3.Zero())
                        {
							rotation = Vector3.MatMul(p.rotationY, p.position);
						}else
                        {
							rotation = Vector3.MatMul(p.rotationY, rotation);
                        }
					}
					if (p.rotationZ != null)
                    {
						if (rotation == Vector3.Zero())
                        {
							rotation = Vector3.MatMul(p.rotationZ, p.position);
						}else
                        {
							rotation = Vector3.MatMul(p.rotationZ, rotation);
                        }

					}
					if (orthographic) { Projection2D = Vector3.MatMul(Projection, rotation); }
					else {
						float depth = 100f;
						float z = 1/(depth - rotation.Z);
						float[,] Projection =
						{
							{z,0,0},
							{0,z,0 }
						};
						Projection2D = Vector3.MatMul(Projection, rotation);
					}
					
					p.position = Projection2D;
					
					//Log.Warning($"1. [{AllPoints[i].position[0,0]},{AllPoints[i].position[0, 1]}]\n2. [{AllPoints[i].position[1, 0]},{AllPoints[i].position[1, 1]}]\n3. [{AllPoints[i].position[2, 0]},{AllPoints[i].position[2, 1]}]");

					//g.FillEllipse(Brushes.Blue, new RectangleF(p.position.X - p.Size.X / 2F, p.position.Y - p.Size.Y / 2F, p.Size.X, p.Size.X));

				}
				foreach (Line2D line in AllLines.ToArray())
				{
					Vector2 A = Units.UnitToPix(line.A);
					Vector2 B = Units.UnitToPix(line.B);
 					Pen p = new Pen(line.color,Units.UnitToPix(line.Width));
					g.DrawLine(p, A.X, A.Y, B.X, B.Y);
				}
			}
			else
			{
				Log.Error("NO dimension");
			}

		}

		private void PhysicsUpdate2D()
		{
			foreach (Physics.PhysicsBody2D Body in AllPhysicsBodys)
			{
				Shape2D shape = Body.shape;
				Shape2D Line = new Shape2D(shape.Position+shape.Scale/2,new Vector2(1,2500),Vector2.Zero(),Color.Blue,new Collider(),"Collision Reader");
				if (shape.BoxCollider.InsideWorldSpace(worldSpace))
				{
					foreach (Physics.BoxCollider2D collider in AllBoxColliders)
					{
						Shape2D other = collider.Shape;
						Collider col = Collider.OnCollisionEnter(Line, other);
						if (other != shape && col.Collided)
						{
							Log.Warning($"breaked on object {other.Tag}");
							shape.PhysicsBody.UpdateShapePos(other);
							break;
						}
					}
					foreach (Physics.PhysicsBody2D Body2 in AllPhysicsBodys)
					{
						if (Body2 != Body)
						{
							Shape2D other = Body2.shape; 

							Collider col = Collider.OnCollisionEnter(shape, other);
							if (col.Collided)
							{
								Body.BounceX(other);
							}
						}
					}
				}
				Line.DestroySelf();
			}

			//foreach (Shape2D shape in AllShapes)
			//{s
			//	if (shape.PhysicsBody != null && shape.BoxCollider.InsideWorldSpace(worldSpace))
			//	{
			//		//do update
			//		//do collider searching
			//		foreach (Shape2D shape2 in AllShapes)
			//		{
			//			//if collider found then activate
			//			if (shape2.BoxCollider != null && shape2 != shape)
			//			{
			//				//update shape position if not collided
			//				shape.PhysicsBody.UpdateShapePos(shape2);
			//			}
			//		}
			//	}
			//}
		}
		
		/// <summary>
		/// Is used only once when instantiating the game
		/// </summary>
		public abstract void OnLoad();
		/// <summary>
		/// function that updates every frame!
		/// </summary>
		public abstract void OnUpdate();
		/// <summary>
		/// function to draw your GUI
		/// </summary>
		public abstract void OnDraw();

		/// <summary>
		/// input for the keys that get pressed
		/// </summary>
		/// <param name="e">key input onPress</param>
		public abstract void GetKeyDown(KeyEventArgs e);
		/// <summary>
		/// input for the keys that get released
		/// </summary>
		/// <param name="e"></param>
		public abstract void GetKeyUp(KeyEventArgs e);
	}
}
