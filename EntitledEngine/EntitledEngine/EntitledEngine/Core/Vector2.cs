using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitledEngine.EntitledEngine
{

	/// <summary>
	/// Vecotr 2 class for storing X and Y data format: [X,Y]
	/// </summary>
	public class Vector2
	{
		public float X { get; set; }
		public float Y { get; set; }

		public Vector2()
		{
			X = Zero().X;
			Y = Zero().Y;
		}
		public Vector2(float X, float Y)
		{
			this.X = X;
			this.Y = Y;
		}

        public override string ToString()
        {
			return $"( {X} , {Y} )";
        }
        /// <summary>
        /// returns X && Y as 0
        /// </summary>
        /// <returns></returns>
        public static Vector2 Zero()
		{
			return new Vector2(0,0);
		}

		/// <summary>
		/// Returns a floating value of the distance between the 2 points 
		/// </summary>
		/// <param name="p1">Starting Point</param>
		/// <param name="p2">Ending Point</param>
		/// <returns></returns>
		public static float Distance(Vector2 p1, Vector2 p2)
        {
			float x1 = p1.X, x2 = p2.X;
			float y1 = p1.Y, y2 = p2.Y;
			return Mathf.sqrt(Mathf.pow((x1 - x2),2) + Mathf.pow((y1 - y2),2));
        }

		public static Vector2 Normalized(Vector2 a)
        {
			if (Mathf.magnitude(a) > 0)
            {
				return Mathf.Divide(a);
			}else
            {
				return null;
			}
			
		}

		#region Vector2 places
		public static Vector2 Up()
		{
			return new Vector2(0, 1);
		}
		public static Vector2 Down()
		{
			return new Vector2(0, -1);
		}
		public static Vector2 Left()
		{
			return new Vector2(-1, 0);
		}
		public static Vector2 Right()
		{
			return new Vector2(1, 0);
		}
		#endregion
		#region Vector2 Operators

		#region Operator == != 
		public static bool operator== (Vector2 w1, Vector2 w2)
		{
			return (w1.X == w2.X && w1.Y == w2.Y);
		}
		public static bool operator !=(Vector2 w1, Vector2 w2)
		{
			return !(w1.X == w2.X && w1.Y == w2.Y);
		}
		#endregion
		#region Operator + 
		public static Vector2 operator +(Vector2 w1, Vector2 w2)
		{
			return new Vector2(w1.X + w2.X, w1.Y + w2.Y);
		}
		public static Vector2 operator +(Vector2 w1, float w2)
		{
			return new Vector2(w1.X + w2, w1.Y + w2);
		}
		#endregion
		#region Operator - 
		public static Vector2 operator -(Vector2 w1, Vector2 w2)
		{
			return new Vector2(w1.X - w2.X, w1.Y - w2.Y);
		}
		public static Vector2 operator -(Vector2 w1, float w2)
		{
			return new Vector2(w1.X - w2, w1.Y - w2);
		}
		#endregion
		#region Operator *
		public static Vector2 operator *(Vector2 w1, Vector2 w2)
		{
			return new Vector2(w1.X * w2.X, w1.Y * w2.Y);
		}
		public static Vector2 operator *(Vector2 w1, float w2)
		{
			return new Vector2(w1.X * w2, w1.Y * w2);
		}
		#endregion
		#region Operator /
		public static Vector2 operator /(Vector2 w1, Vector2 w2)
		{
			return new Vector2(w1.X / w2.X, w1.Y / w2.Y);
		}
		public static Vector2 operator /(Vector2 w1, float w2)
		{
			return new Vector2(w1.X / w2, w1.Y / w2);
		}
		#endregion

		#endregion
	}
}
