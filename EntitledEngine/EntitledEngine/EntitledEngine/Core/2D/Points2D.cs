using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitledEngine.EntitledEngine.Core._2D
{
     public class Points2D
    {
        public Vector3 position;
        public float[,] rotationX;
        public float[,] rotationY;
        public float[,] rotationZ;

        public float angle;
        public Vector3 Size;
        public Points2D(Vector3 pos, Vector3 siz)
        {
            position = pos;
            Size = siz;

            EntitledEngine.RegisterPoints(this);
        }

        public void RotationX(float angle)
        {
            this.angle = angle;
            rotationX = new float[,] {
                { 1f,0f,0f},
                { 0f,(float)Math.Cos(angle), (float)-Math.Sin(angle) },
                { 0f,(float)Math.Sin(angle), (float)Math.Cos(angle) }
            };
        }
        public void RotationY(float angle)
        {
            this.angle = angle;
            rotationY = new float[,] {
                { (float)Math.Cos(angle),0f, (float)-Math.Sin(angle) },
                { 0f,1f,0f},
                { (float)Math.Sin(angle),0f, (float)Math.Cos(angle) }
            };
        }
        public void RotationZ(float angle)
        {
            this.angle = angle;
            rotationZ = new float[,] {
                { (float)Math.Cos(angle), (float)-Math.Sin(angle),0f },
                { (float)Math.Sin(angle), (float)Math.Cos(angle),0f },
                { 0f,0f,1f}
            };
        }


        public string GetRotation()
        {
            return $"\n[0,0]: {rotationX[0,0]} | [0,1] {rotationX[0, 1]} | [0,2] {rotationX[0, 2]}\n[1,0]: {rotationX[1, 0]} | [1,1] {rotationX[1, 1]} | [1,2]: {rotationX[1, 2]}";
        }

        public void DestroySelf()
        {
            EntitledEngine.UnRegisterPoints(this);
        }
    }
}
