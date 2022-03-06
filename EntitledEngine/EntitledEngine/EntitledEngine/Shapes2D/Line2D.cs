using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace EntitledEngine.EntitledEngine
{

	/// <summary>
	/// DEOSNT WORK as intetnded
	/// </summary>
    public class Line2D
    {
        public Vector2 A = null;
        public Vector2 B = null;
        //add Vector2 Rotation
        public string Tag = "";
        public float Width;
        public Color color;

        public Line2D(Vector2 A,Vector2 Direction,float Magnitude,float Width, Color Color, string Tag)
        {
            this.A = A;
            this.B = A + Direction * Magnitude;
            this.Width = Width;
            this.Tag = Tag;
            this.color = Color;

            Log.Info($"[LINE2D]({Tag}) - has been registerd");

            EntitledEngine.RegisterLine(this);
        }
        public Line2D(Vector2 A, Vector2 B, float Width, Color Color, string Tag)
        {
            this.A = A;
            this.B = B;
            this.Width = Width;
            this.Tag = Tag;
            this.color = Color;

            Log.Info($"[LINE2D]({Tag}) - has been registerd");

            EntitledEngine.RegisterLine(this);

        }

        public void DestroySelf()
        {
            Log.Info($"[LINE2D]({Tag}) - has been unregisterd");

            EntitledEngine.UnRegisterLine(this);
        }
    }
}
