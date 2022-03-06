using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitledEngine.EntitledEngine
{
	/// <summary>
	/// This is OUT OF USE since Version 3(Physics update)
	/// </summary>
    public class Collider
    {
        public bool Collided;

        public Collider()
        {

        }
        public Collider(Shape2D shape2D)
        {
            
        }
        public Collider( bool Collided)
        {
            this.Collided = Collided;
        }
        public static Collider OnCollisionEnter(Shape2D Self, Shape2D Other)
        {
            if (Other.Position.X < Self.Position.X + Self.Scale.X &&
                    Other.Position.X + Other.Scale.X > Self.Position.X &&
                    Other.Position.Y < Self.Position.Y + Self.Scale.Y &&
                    Other.Position.Y + Other.Scale.Y > Self.Position.Y)
            {
                return new Collider(true);
            }
            else
            {
                return new Collider(false);
            }
        }
        public static Collider OnCollisionExit(Shape2D Self, Shape2D Other)
        {
            if (Other.Position.X < Self.Position.X + Self.Scale.X &&
                    Other.Position.X + Other.Scale.X > Self.Position.X &&
                    Other.Position.Y < Self.Position.Y + Self.Scale.Y &&
                    Other.Position.Y + Other.Scale.Y > Self.Position.Y)
            {
                return new Collider(false);
            }
            else
            {
                return new Collider(true);
            }
        }
        /*
        public static bool OnCollisionStay(Shape2D Self, Shape2D Other)
        {
            if (Other.Position.X < Self.Position.X + Self.Scale.X &&
                    Other.Position.X + Other.Scale.X > Self.Position.X &&
                    Other.Position.Y < Self.Position.Y + Self.Scale.Y &&
                    Other.Position.Y + Other.Scale.Y > Self.Position.Y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        */
        //public static bool OnCollisionExit()
        //{

        //}
    }
}
