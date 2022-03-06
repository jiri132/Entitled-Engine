using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitledEngine.EntitledEngine.Core
{
    public class Vector3
    {
		public float X { get; set; }
		public float Y { get; set; }
		public float Z { get; set; }

		public Vector3()
		{
			X = Zero().X;
			Y = Zero().Y;
			Z = Zero().Z;
		}
		public Vector3(float X, float Y, float z)
		{
			this.X = X;
			this.Y = Y;
			this.Z = z;
		}

		public static float[,] vecToMatrix(Vector3 v)
        {
			float[,] m = new float[3, 1];
			m[0, 0] = v.X;
			m[1, 0] = v.Y;
			m[2, 0] = v.Z;
			return m;
		}
		public static Vector3 MatMul(float[,] m, Vector3 v)
        {
			float[,] b = vecToMatrix(v);
			return matrixToVec(Mathf.MatMul(m,b));
        }
		public static Vector3 matrixToVec(float[,] m)
        {
			Vector3 v = new Vector3();
			v.X = m[0, 0];
			v.Y = m[1, 0];
			if (m.Length > 2)
            {
				v.Z = m[2, 0];
            }
			return v;
		}

		public override string ToString()
		{
			return $"[ {X} , {Y} , {Z} ]";
		}
		/// <summary>
		/// returns X && Y as 0
		/// </summary>
		/// <returns></returns>
		public static Vector3 Zero()
		{
			return new Vector3(0, 0,0);
		}



		#region Vector3 places
		public static Vector3 Up()
		{
			return new Vector3(0, 1,0);
		}
		public static Vector3 Down()
		{
			return new Vector3(0, -1,0);
		}
		public static Vector3 Left()
		{
			return new Vector3(-1, 0,0);
		}
		public static Vector3 Right()
		{
			return new Vector3(1, 0,0);
		}
		public static Vector3 Front()
		{
			return new Vector3(0, 0, 1);
		}
		public static Vector3 Backwards()
		{
			return new Vector3(0, 0, -1);
		}
		#endregion
		#region Vector2 Operators

		#region Operator == != 
		public static bool operator ==(Vector3 w1, Vector3 w2)
		{
			return (w1.X == w2.X && w1.Y == w2.Y && w1.Z == w2.Z);
		}
		public static bool operator !=(Vector3 w1, Vector3 w2)
		{
			return !(w1.X == w2.X && w1.Y == w2.Y && w1.Z == w2.Z);
		}
		#endregion
		#region Operator + 
		public static Vector3 operator +(Vector3 w1, Vector3 w2)
		{
			return new Vector3(w1.X + w2.X, w1.Y + w2.Y, w1.Z + w2.Z);
		}
		public static Vector3 operator +(Vector3 w1, float w2)
		{
			return new Vector3(w1.X + w2, w1.Y + w2, w1.Z + w2);
		}
		#endregion
		#region Operator - 
		public static Vector3 operator -(Vector3 w1, Vector3 w2)
		{
			return new Vector3(w1.X - w2.X, w1.Y - w2.Y, w1.Z - w2.Z);
		}
		public static Vector3 operator -(Vector3 w1, float w2)
		{
			return new Vector3(w1.X - w2, w1.Y - w2, w1.Z - w2);
		}
		#endregion
		#region Operator *
		public static Vector3 operator *(Vector3 w1, Vector3 w2)
		{
			return new Vector3(w1.X * w2.X, w1.Y * w2.Y,w1.Z * w2.Z);
		}
		public static Vector3 operator *(Vector3 w1, float w2)
		{
			return new Vector3(w1.X * w2, w1.Y * w2, w1.Z * w2);
		}
		#endregion
		#region Operator /
		public static Vector3 operator /(Vector3 w1, Vector3 w2)
		{
			return new Vector3(w1.X / w2.X, w1.Y / w2.Y, w1.Z / w2.Z);
		}
		public static Vector3 operator /(Vector3 w1, float w2)
		{
			return new Vector3(w1.X / w2, w1.Y / w2, w1.Z / w2);
		}
		#endregion

		#endregion
	}
}
