
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

using EntitledEngine.EntitledEngine;

namespace EntitledEngine
{
	class DemoGame : EntitledEngine.EntitledEngine
	{
		//screen size
		public Vector2 ScreenSize = new Vector2(512, 512);

		//all the shapes used
		Shape2D panel;
		Shape2D panelAi;
		Shape2D ball;

		Shape2D LeftLine;
		Shape2D RightLine;
		Shape2D TopLine;
		Shape2D BottomLine;



		//Lines to render paths
		Line2D ballLine;

		//Variables of objects
		float ballSpeedX = 2;
		float ballSpeedY = 2;
		int panelSpeed = 0;
		int botSpeed = 5;
		public DemoGame() : base(new EntitledEngine.Vector2( 528, 550), "Entitled Engine Demo", "2D") { }

		

		public override void OnLoad()
		{
			BackgroundColor = Color.Black;
			panel = new Shape2D(new Vector2(5,512/2 - 50),new Vector2(10,100),Vector2.Zero(), Color.White, new Collider(panel) ,"panel");
			panelAi = new Shape2D(new Vector2(512-15, 512/2 - 50), new Vector2(10, 100), Vector2.Zero(), Color.White, new Collider(panelAi),"AI-panel");
			ball = new Shape2D(new Vector2(512/2 -5,512/2-5), new Vector2(10,10), Vector2.Zero(), Color.White, new Collider(),"Ball");
			//lines to keep track of the play size
			LeftLine = new Shape2D(new Vector2(0, 0), new Vector2(5,512), Vector2.Zero(), Color.Cyan, new Collider(LeftLine),"middle");
			RightLine = new Shape2D(new Vector2(512-5, 0), new Vector2(5, 512), Vector2.Zero(), Color.Cyan, new Collider(LeftLine), "middle");
			TopLine = new Shape2D(new Vector2(0, 0), new Vector2(512,5), Vector2.Zero(), Color.Cyan, new Collider(TopLine), "middle");
			BottomLine = new Shape2D(new Vector2(0, 512-5), new Vector2(512,5), Vector2.Zero(), Color.Cyan,new Collider(TopLine), "middle");

			ballLine = new Line2D(ball.Position, new Vector2(ball.Position.X * 10, ball.Position.Y * 10), 1,1, Color.Green,"BallPath");

			//player = new Sprite2D(new Vector2(20,20), new Vector2(10,10), "Players/player","Player");
		}

		/// <summary>
		/// I HAVE NO CLUE WHY I DID THIS
		/// I but I think this was for the UI
		/// </summary>
		public override void OnDraw()
		{

		}

		//Some variables that are used in Updates neccesary to run
		bool _time = true;
		bool paused = false;
		int time;
		bool ballMovingDown = true;
		bool ballMovingLeft = true;
		bool padelUp = false;
		bool padleDown = false;
		//This Function is being Used EVERY FRAME
		public override void OnUpdate()
		{
			if (_time)
			{
				//Log.Info($"ball Y pos: {ball.Position.Y}\nball X pos: {ball.Position.X}");

				CollisionBall();
				BallMovement();
				if (ball.Position.X < 0 - 100)
				{
					BallToMiddle();
					_time = false;
				}
				else if (ball.Position.X > 512 + 100)
				{
					BallToMiddle();
					_time = false;
				}
				ballLine.A = ball.Position;
				ballLine.B = new Vector2(ball.Position.X + ballSpeedX, ball.Position.Y + ballSpeedY);

				//Log.Info(ballLine.A +"  A | B  "+ ballLine.B);
				panelCollision();
				panelAiFollowsBall();

				//if (panel.Position.Y >= 412)
				//{
				//	movingDown = false;
				//}else if (panel.Position.Y <= 0)
				//{
				//	movingDown = true;
				//}

				//if (movingDown)
				//{
				//	panel.Position.Y += .1f;
				//}else
				//{
				//	panel.Position.Y -= .1f;
				//}

				time++;
			}
			else
			{
				if (paused)
				{
					Log.Info("[GAME-STATE] - The game is paused Release the ESCAPE key to continue");
				}
				else
				{
					Log.Info("[GAME-STATE] - Press ENTER to continue playing");
				}
			}
		}

		//input for keysDown
		public override void GetKeyDown(KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{

				if (!_time)
                {
					ballSpeedX = 2;
					ballSpeedY = 2;
					Log.Info(ballSpeedX.ToString());
					_time = true;

				}
			}
			if (e.KeyCode == Keys.Escape)
			{
				Application.Exit();
				Environment.Exit(1);
				_time = false;
				paused = true;
			}
			if (e.KeyCode == Keys.W)
			{
				panelSpeed = -5;
				padelUp = true;
				padleDown = false;
			}
			if (e.KeyCode == Keys.S)
			{
				panelSpeed = 5;
				padelUp = false;
				padleDown = true;
			}
		}
		//input keysUp
		public override void GetKeyUp(KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				_time = true;
				paused = false;
			}
			if (e.KeyCode == Keys.W)
			{
				panelSpeed = 0;
				padelUp = false ;
			}
			if (e.KeyCode == Keys.S)
			{
				panelSpeed = 0;
				padleDown = false;
			}
		}

		public void BallToMiddle()
		{
			ball.Position = new Vector2(512 / 2 - 5, 512 / 2 - 5);
			panelAi.Position = new Vector2(512 - 15, 512 / 2 - 50);
			if (ballMovingLeft)
			{
				ballMovingLeft = false;
			}
			else
			{
				ballMovingLeft = true;
			}
		}

		public void BallMovement()
		{
			if (ballMovingDown)
			{
				if (ball.Position.Y >= 512)
				{
					ballMovingDown = false;
				}
				ball.Position.Y += ballSpeedY;
			}
			else
			{
				if (ball.Position.Y <= 0)
				{
					ballMovingDown = true;
				}
				ball.Position.Y -= ballSpeedY;
			}
		}
		public void CollisionBall()
		{
			Collider collider = new Collider(false);
			if (ballMovingLeft)
			{

				collider = Collider.OnCollisionEnter(ball, panel);
				if (collider.Collided)
				{
									ballMovingLeft = false;
										ballSpeedX+=0.2f;
										ballSpeedY+=0.2f;
					//botSpeed++;
					//Vector2 pos = new Vector2(ballSpeedX, hitFactor(ball.Position, panelAi.Position, (panelAi.Position.Y - ball.Position.Y)));
					//ballSpeedX = pos.X;
					//ballSpeedY = pos.Y;
				}
				ball.Position.X -= ballSpeedX;
			}
			else
			{
				collider = Collider.OnCollisionEnter(ball, panelAi);
				if (collider.Collided)
				{
					
					 	ballMovingLeft = true;
						ballSpeedX+=0.2f;
						ballSpeedY+=0.2f;
					//botSpeed++;

					//Vector2 pos = new Vector2(-ballSpeedX, hitFactor(ball.Position, panelAi.Position, (panelAi.Position.Y - ball.Position.Y)));
					//ballSpeedX = pos.X;
					//ballSpeedY = pos.Y;

				}
				ball.Position.X += ballSpeedX;
			}
		}
		public void panelCollision()
        {
			//if panel hits colision
			if (padleDown)
			{
				Collider collider = new Collider(false);
				collider = Collider.OnCollisionEnter(panel, BottomLine);
				if (!collider.Collided)
				{
					panel.Position.Y += botSpeed;
				}
			}
			if (padelUp)
			{
				Collider collider = new Collider(false);
				collider = Collider.OnCollisionEnter(panel, TopLine);
				if (!collider.Collided)
				{
					//Log.Warning("going up");
					panel.Position.Y -= botSpeed;
				}
			}
		}

		public void panelAiFollowsBall()
        {
			
			if (panelAi.Position.Y < ball.Position.Y)
            {
				Collider collider = new Collider(false);
				collider = Collider.OnCollisionEnter(panelAi, BottomLine);
				if (!collider.Collided)
                {
					panelAi.Position.Y += botSpeed;
				}
			}
			if (panelAi.Position.Y > ball.Position.Y)
            {
				Collider collider = new Collider(false);
				collider = Collider.OnCollisionEnter(panelAi, TopLine);
				if (!collider.Collided)
				{
					//Log.Warning("going up");
					panelAi.Position.Y -= botSpeed;
				}
			}
        }

		float hitFactor(Vector2 ballPos, Vector2 racketPos, float racketHeight)
        {
			/*
			 * ASCII art:
			 *  1 <- at top racket
			 *  0 <- at middle racket
			 * -1 <- at bottom of racket
			*/

			return (ballPos.Y - racketPos.Y) / racketHeight; 
        }
	}
}
