using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitledEngine.EntitledEngine.Core
{
	public class Units
	{
		public int PixPerUnit { get; private set; }

		public Units(int PixUnit)
		{
			PixPerUnit = PixUnit;
		}
		public void ChangePixPerUnit(int PixUnit)
		{
			PixPerUnit = PixUnit;
		}

		//pixels to units
		public float PixToUnit(int pix)
		{
			return pix / PixPerUnit;
		}
		public Vector2 PixToUnit(Vector2 pix)
		{
			return pix / PixPerUnit;
		}

		public Vector3 PixToUnit(Vector3 pix)
		{
			return pix / PixPerUnit;
		}
		

		//units to pixels
		public float UnitToPix(float unit)
		{
			return unit * PixPerUnit;
		}
		public Vector2 UnitToPix(Vector2 unit)
		{
			return unit * PixPerUnit;
		}
		public Vector3 UnitToPix(Vector3 unit)
		{
			return unit * PixPerUnit;
		}
		
	}
}
