using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

using EntitledEngine.EntitledEngine;



/* 
 * 
 * --------------------------Snake--------------------------
 * made in 2 days with version 2 of the engine
 * 
 * 
 * new wit version 2 ofthe engine 
 * Beter drawing system
 * Exit codes
 * error debugging
 * faster overall rendering
 * new maths
 * delta time
 * 
 * made by Jiri
 * 
 * ---------------------------------------------------------
 * 
 */







namespace EntitledEngine
{
	enum Direction { UP, DOWN, LEFT, RIGHT };

    class DemoGame2 : EntitledEngine.EntitledEngine
    {
		public int Width = 20, Height = 20;
		public Vector2 Scale = new Vector2(20,20);
		public int ScreenW, ScreenH;
		public Shape2D Screen;

		public DemoGame2() : base(new EntitledEngine.Vector2(400+16, 400+39), "Entitled Engine Demo", "2D") { }


		//snake data
		public List<Shape2D> Grid = new List<Shape2D>();
		public List<Shape2D> SnakeBody = new List<Shape2D>();
		public Color SnakeColor = Color.LimeGreen;
		public int lenght = 5;
		public int oldLenght;
		public int StepsAfterEating;

		public Vector2 FoodPosition = new Vector2(0,0);
		public Shape2D Food = new Shape2D(new Vector2(20,20), new Vector2(20,20), Vector2.Zero(), Color.Red, new Collider(), "Food");
		//direction data
		public Direction Direction = Direction.RIGHT;

		public Collider collider = new Collider(false);

		public override void OnLoad()
		{
			ScreenW = Width * (int)Scale.X;
			ScreenH = Height * (int)Scale.Y;
			Screen = new Shape2D(Vector2.Zero(), new Vector2(ScreenW, ScreenH), Vector2.Zero(), Color.Gray, new Collider(), "Screen");
			for (int y = 0; y < Height; y++)
			{
				for (int x = 0; x < Width; x++)
				{
					Color color = Color.Gray;
					if ((y % 2 == 0) ^ (x % 2 == 0)) { color = Color.Black; }
					Grid.Add(new Shape2D(new Vector2(x * Width, y * Height), Scale, Vector2.Zero(), color, new Collider(), "Grid"));
					Log.Info($"[{x} , {y}]");
				}
			}
            for (int i = 0; i < lenght; i++)
            {
				SnakeBody.Add(new Shape2D(new Vector2(Width, Height), Scale, Vector2.Zero(), SnakeColor, new Collider(), "Body"));
			}
			oldLenght = lenght;
			FoodNewPosition();
			SpawnFood();
			Log.Warning("Press enter to start");
		}

		public bool time = false;
		int _time;
		
		/// <summary>
		/// I HAVE NO CLUE WHY I DID THIS
		/// I but I think this was for the UI
		/// </summary>
		public override void OnDraw()
		{
			
		}


		//This Function is being Used EVERY FRAME
		public override void OnUpdate()
		{

			if (time)
			{
				_time++;
				if (_time % 3 == 0)
				{

					Console.Clear();
					//all code for logic

					//for (int y = 0; y < Height; y++)
					//{
					//	for (int x = 0; x < Width; x++)
					//	{
					//		//Log.Info((x + 20*y).ToString());
					//		Grid[x + 20 * y].DestroySelf();
					//		Color color = Color.Gray;
					//		if ((y % 2 == 0) ^ (x % 2 == 0)) { color = Color.Black; }
					//		Grid.Add(new Shape2D(new Vector2(x * Width, y * Height), Scale, Vector2.Zero(), color, new Collider(), "Grid"));
					//		//Log.Info($"[{x} , {y}]");
					//	}
					//}
					//Grid.RemoveRange(0, 400);

					//foreach (Shape2D shape in SnakeBody)
					//{
					//	if (shape == SnakeBody[0])
					//	{
					//		AddBodys(,SnakeBody);
					//	}else
					//	{
					//		AddBodys(shape.Position,SnakeBody);
					//	}
					//}
					OriginalSnake();
					//addbodys();
					//DeleteBodys();
					oldLenght = lenght;

					CheckBodyColision();
					CollisionDetectiongAfterEating();

					//SpawnFood();
					CheckFood();
					StepsAfterEating++;
				}

			}
		}
		#region Mode: Tail Runner
		void addbodys()
		{
			for (int i = 0; i < lenght; i++)
			{
				Log.Info($"{SnakeBody[i].Position}");
				SnakeBody.Add(new Shape2D(new Vector2(SnakeBody[i].Position.X, SnakeBody[i].Position.Y), Scale, Vector2.Zero(), SnakeColor, new Collider(), "Body"));
			}
			Vector2 Pos = new Vector2(SnakeBody[lenght - 1].Position.X, SnakeBody[lenght - 1].Position.Y);
			switch (Direction)
			{
				case Direction.UP:
					Pos.Y -= Scale.Y;
					break;
				case Direction.DOWN:
					Pos.Y += Scale.Y;
					break;
				case Direction.LEFT:
					Pos.X -= Scale.X;
					break;
				case Direction.RIGHT:
					Pos.X += Scale.X;
					break;
				default:
					Log.Info(" ");
					break;
			}
			SnakeBody.Add(new Shape2D(Pos, Scale, Vector2.Zero(), SnakeColor, new Collider(), "Head"));
			//Log.Info($"{SnakeBody[lenght-1].Position}");
		}

		void DeleteBodys()
		{
			if (SnakeBody.Count > lenght)
			{
				SnakeBody[0].DestroySelf();
				SnakeBody.RemoveAt(0);
			}

			for (int i = 0; i < oldLenght; i++)
			{
				SnakeBody[i].DestroySelf();
				//SnakeBody.RemoveAt(0);
			}
			
			SnakeBody.RemoveRange(0, oldLenght);
			Log.Info($"{SnakeBody.Count}");
		}
		#endregion
		#region Mode: Original

		void OriginalSnake()
		{
			AddHead(lenght-1);
			DeleteLastBody();
		}

		void AddHead(int index)
		{
			//Log.Info(lenght.ToString());
			//Log.Info(SnakeBody[0].Position.ToString());
			Log.Info(SnakeBody[index].Position.ToString());
			Vector2 Pos = new Vector2(SnakeBody[index].Position.X, SnakeBody[index].Position.Y);
			
			switch (Direction)
			{
				case Direction.UP:
					Pos.Y -= Scale.Y;
					break;
				case Direction.DOWN:
					Pos.Y += Scale.Y;
					break;
				case Direction.LEFT:
					Pos.X -= Scale.X;
					break;
				case Direction.RIGHT:
					Pos.X += Scale.X;
					break;
				default:
					Log.Info(" ");
					break;
			}
			SnakeBody.Add(new Shape2D(Pos, Scale, Vector2.Zero(), SnakeColor, new Collider(), "Body"));
		}
		
		void DeleteLastBody()
		{
			if (SnakeBody.Count > lenght)
			{
				SnakeBody[0].DestroySelf();
				SnakeBody.Remove(SnakeBody[0]);
			}
		}
		


		#endregion
		void CheckFood()
        {
			bool hasColided = false;
            foreach (Shape2D shape2D in SnakeBody)
            {
				if (shape2D.Position.X == Food.Position.X && shape2D.Position.Y == Food.Position.Y)
                {
					Log.Info("Collided with food");
					lenght++;
					hasColided = true;
					FoodNewPosition();
					SpawnFood();
					StepsAfterEating = 0;
                }
            }
			if (hasColided)
			{
				AddHead(lenght - 2);
			}
        }

		void FoodNewPosition()
        {
			Random Random = new Random();

			FoodPosition = new Vector2(Random.Next(1,Width) * 20,Random.Next(1,Height)*20);
        }
		void SpawnFood()
        {
			Food.DestroySelf();
			Food = new Shape2D(FoodPosition,Scale,Vector2.Zero(),Color.Red,new Collider(), "Food");
        }

		void CollisionDetectiongAfterEating()
		{
			for (int i = 0; i < SnakeBody.Count; i++)
			{
				if (StepsAfterEating > 0 && SnakeBody[lenght -1].Position.X == SnakeBody[i].Position.X && SnakeBody[lenght - 1].Position.Y == SnakeBody[i].Position.Y && SnakeBody[lenght-1] != SnakeBody[i])
				{
					ExitCodes.RestartApplication();
				}
			}
		}

		void CheckBodyColision()
        {
			if (SnakeBody[lenght-1].Position.X > Width * Scale.X || SnakeBody[lenght-1].Position.Y > Height * Scale.Y || SnakeBody[lenght-1].Position.X < 0 || SnakeBody[lenght-1].Position.Y < 0)
            {
				ExitCodes.RestartApplication();
            }
        }

        //input for keysDown
        public override void GetKeyDown(KeyEventArgs e)
        {
			if (e.KeyCode == Keys.Enter)
            {
				time = true;
            }
			if (e.KeyCode == Keys.Escape)
            {
				ExitCodes.QuitApplication();
            }
			if (e.KeyCode == Keys.R)
			{
				ExitCodes.RestartApplication();
			}
			if (Direction != Direction.RIGHT && e.KeyCode == Keys.A /*|| e.KeyCode == Keys.Left*/)
            {
				Direction = Direction.LEFT;
            }
			if (Direction != Direction.DOWN && e.KeyCode == Keys.W /*|| e.KeyCode == Keys.Up*/)
			{
				Direction = Direction.UP;
			}
			if (Direction != Direction.UP && e.KeyCode == Keys.S /*|| e.KeyCode == Keys.Down*/)
			{
				Direction = Direction.DOWN;
			}
			if (Direction != Direction.LEFT && e.KeyCode == Keys.D /*|| e.KeyCode == Keys.Right*/)
			{
				Direction = Direction.RIGHT;
			}
			
		}
		//inpuy for keysUp
        public override void GetKeyUp(KeyEventArgs e)
        {
			
		}
    }
}
