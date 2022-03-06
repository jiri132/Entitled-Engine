using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitledEngine.EntitledEngine
{
    /// <summary>
    /// This Class is used for doing math returning floating values
    /// </summary>
    class Mathf
    {
        public static float[,] MatMul(float[,] a, float[,] b)
        {
            int rowsA = a.GetLength(0);
            int colsA = a.GetLength(1);

            int rowsB = b.GetLength(0);
            int colsB = b.GetLength(1);

            float[,] results = new float[rowsA, colsB];

            if (colsA != rowsB)
            {
                Log.Error($"Collum A: {colsA} must be the same length as rows B: {rowsB}");

                return null;
            }

            for (int i = 0; i < rowsA; i++)
            {
                for (int j = 0; j < colsB; j++)
                {
                    float sum = 0;
                    for (int k = 0; k < colsA; k++)
                    {
                        sum += a[i, k] * b[k, j]; 
                    }
                    results[i, j] = sum;
                }
            }
            return results;

        }

        public static Vector2 Divide(Vector2 a)
        {
            float A = a.X, B = a.Y, C = Mathf.sqrt(A*A + B*B);
            A = C / A;
            B = C / B;
            C = C / C;

            return new Vector2(A, B);
        }
        /// <summary>
        /// Returns the length of the vector
        /// </summary>
        /// <param name="a">the vector used for calculating its magnitude</param>
        /// <returns>the magnitude of the given vector</returns>
        public static float magnitude(Vector2 a)
        {
            float x = a.X, y= a.Y;
            return Mathf.sqrt(x*x + y*y);
        }

        /// <summary>
        /// multiplies x to the power of given number
        /// </summary>
        /// <param name="x">number to multiply</param>
        /// <param name="n">number how many times</param>
        /// <returns>the output of x^n</returns>
        public static float pow(float x, int n)
        {
            return (float)Math.Pow(x, n);
        }
        public static float sqrt(float x)
        {
            return (float)Math.Sqrt(x);
        }

    }
}
