using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

using EntitledEngine.EntitledEngine;
using EntitledEngine.EntitledEngine.Core;
using EntitledEngine.EntitledEngine.Core._2D;


namespace EntitledEngine
{
    class DemoGame4 : EntitledEngine.EntitledEngine
    {

        //Screen data
        static float screenX = 500;
        static float screenY = 500;
        public static Vector2 Screen = new Vector2(screenX + 16, screenY + 39);

        
        //making the game info with title and scren size for the window
        public DemoGame4() : base(Screen, "Entitled Engine Demo", "3D") { }

        
        public override void OnDraw()
        {
        
        }


        Vector3[] cube = new Vector3[8];
        Points2D[] point;
        Vector3[] points = new Vector3[8];

        List<Line2D> lines = new List<Line2D>();

        float angle;

        public override void OnLoad()
        {
			Units = new Units(35);

            cube[0] = new Vector3(-5, -5, -5); 
            cube[1] = new Vector3(5, -5, -5); 
            cube[2] = new Vector3(5, 5, -5); 
            cube[3] = new Vector3(-5, 5, -5);

            cube[4] = new Vector3(-5, -5, 5);
            cube[5] = new Vector3(5, -5, 5);
            cube[6] = new Vector3(5, 5, 5);
            cube[7] = new Vector3(-5, 5, 5);

            lines.Add(new Line2D(Vector2.Zero(), Vector2.Zero(),0.5f, System.Drawing.Color.LimeGreen, "Line"));
            lines.Add(new Line2D(Vector2.Zero(), Vector2.Zero(), 0.5f, System.Drawing.Color.Red, "Line"));
            lines.Add(new Line2D(Vector2.Zero(), Vector2.Zero(), 0.5f, System.Drawing.Color.DeepPink, "Line"));
            lines.Add(new Line2D(Vector2.Zero(), Vector2.Zero(), 0.5f, System.Drawing.Color.Purple, "Line"));
            lines.Add(new Line2D(Vector2.Zero(), Vector2.Zero(), 0.5f, System.Drawing.Color.Magenta, "Line"));
            lines.Add(new Line2D(Vector2.Zero(), Vector2.Zero(), 0.5f, System.Drawing.Color.BlueViolet, "Line"));
            lines.Add(new Line2D(Vector2.Zero(), Vector2.Zero(), 0.5f, System.Drawing.Color.Blue, "Line"));
            lines.Add(new Line2D(Vector2.Zero(), Vector2.Zero(), 0.5f, System.Drawing.Color.Orange, "Line"));
            lines.Add(new Line2D(Vector2.Zero(), Vector2.Zero(), 0.5f, System.Drawing.Color.Yellow, "Line"));
            lines.Add(new Line2D(Vector2.Zero(), Vector2.Zero(), 0.5f, System.Drawing.Color.Turquoise, "Line"));
            lines.Add(new Line2D(Vector2.Zero(), Vector2.Zero(), 0.5f, System.Drawing.Color.Cyan, "Line"));
            lines.Add(new Line2D(Vector2.Zero(), Vector2.Zero(), 0.5f, System.Drawing.Color.White, "Line"));



            point = new Points2D[cube.Length];

            for (int i = 0; i < point.Length; i++)
            {
                point[i] = new Points2D(Vector3.Zero(), new Vector3(20, 0, 0));
            }
            /*//first 3
            lines[0] = new Line2D(new Vector2(cube[0].X, cube[0].Y), new Vector2(cube[1].X, cube[1].Y), 3, System.Drawing.Color.Red, "line0-1");
            lines[1] = new Line2D(new Vector2(cube[0].X, cube[0].Y), new Vector2(cube[2].X, cube[2].Y), 3, System.Drawing.Color.Red, "line0-1");
            lines[2] = new Line2D(new Vector2(cube[0].X, cube[0].Y), new Vector2(cube[4].X, cube[4].Y), 3, System.Drawing.Color.Red, "line0-1");
            //second 2
            lines[3] = new Line2D(new Vector2(cube[1].X, cube[1].Y), new Vector2(cube[3].X, cube[3].Y), 3, System.Drawing.Color.Red, "line0-1");
            lines[4] = new Line2D(new Vector2(cube[1].X, cube[1].Y), new Vector2(cube[5].X, cube[5].Y), 3, System.Drawing.Color.Red, "line0-1");
            //third 2
            lines[5] = new Line2D(new Vector2(cube[2].X, cube[2].Y), new Vector2(cube[3].X, cube[3].Y), 3, System.Drawing.Color.Red, "line0-1");
            lines[6] = new Line2D(new Vector2(cube[2].X, cube[2].Y), new Vector2(cube[6].X, cube[6].Y), 3, System.Drawing.Color.Red, "line0-1");
            //fourth 1
            lines[7] = new Line2D(new Vector2(cube[3].X, cube[3].Y), new Vector2(cube[7].X, cube[7].Y), 3, System.Drawing.Color.Red, "line0-1");
            //fifth 2
            lines[8] = new Line2D(new Vector2(cube[4].X, cube[4].Y), new Vector2(cube[5].X, cube[5].Y), 3, System.Drawing.Color.Red, "line0-1");
            lines[9] = new Line2D(new Vector2(cube[4].X, cube[4].Y), new Vector2(cube[6].X, cube[6].Y), 3, System.Drawing.Color.Red, "line0-1");
            //six 1
            lines[10] = new Line2D(new Vector2(cube[5].X, cube[5].Y), new Vector2(cube[7].X, cube[7].Y), 3, System.Drawing.Color.Red, "line0-1");
            //seven 1
            lines[11] = new Line2D(new Vector2(cube[6].X, cube[6].Y), new Vector2(cube[7].X, cube[7].Y), 3, System.Drawing.Color.Red, "line0-1");*/
            CameraPosition = new Vector2(screenX / 2, screenY / 2);
            BackgroundColor = Color.Black;
			orthographic = true;
        }


        
        public override void OnUpdate()
        {
            angle += 0.025f;
            /*foreach (Line2D l in lines)
            {
                l.DestroySelf();
            }
            lines.Clear();
*/
            //Log.Info(angle.ToString());
            if (angle > 0.1f)
            {
                for (int i = 0; i < 4; i++)
                {
                    Connect(i, (i + 1) % 4, points, i);
                    Connect(i + 4, ((i + 1) % 4) + 4, points, i + 4);
                    Connect(i, i + 4, points , i + 8);
                }
            }
            for (int i = 0; i < cube.Length; i++)
            {
                points[i] = point[i].position; 
                //Log.Info(point[i].position.ToString());
                point[i].position = cube[i];
                point[i].RotationX(angle);
                point[i].RotationY(angle);
                //point[i].RotationZ(angle);

                //Log.Info( cube[i].GetRotation());
                //Log.Info($"[ {i} ] : {cube[i].rotation[1,0]}");
                //Log.Info($"{i.ToString()} | {cube[i].position.ToString()}");
            }

        }

        void Connect(int i, int j, Vector3[] points, int ownIndex)
        {
            Vector2 a = new Vector2(points[i].X ,points[i].Y);
            Vector2 b = new Vector2(points[j].X, points[j].Y);

            //lines.Add(new Line2D(a, b, 5, System.Drawing.Color.White, "Line"));
            //Log.Info(ownIndex.ToString());

            lines[ownIndex].A = a;
            lines[ownIndex].B = b;
        }

        public override void GetKeyDown(KeyEventArgs e)
        {

        }

        public override void GetKeyUp(KeyEventArgs e)
        {

        }

    }
}
