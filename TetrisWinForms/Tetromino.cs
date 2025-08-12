using System;

namespace TetrisWinForms
{
    public class Tetromino
    {
        public int[,] Shape;
        public int Color;
        private static readonly int[] Colors = {
            System.Drawing.Color.Cyan.ToArgb(),
            System.Drawing.Color.Blue.ToArgb(),
            System.Drawing.Color.Orange.ToArgb(),
            System.Drawing.Color.Yellow.ToArgb(),
            System.Drawing.Color.Green.ToArgb(),
            System.Drawing.Color.Purple.ToArgb(),
            System.Drawing.Color.Red.ToArgb()
        };
        private static readonly int[][,] Shapes = {
            // I
            new int[,] { {0,0,0,0}, {1,1,1,1}, {0,0,0,0}, {0,0,0,0} },
            // J
            new int[,] { {1,0,0,0}, {1,1,1,0}, {0,0,0,0}, {0,0,0,0} },
            // L
            new int[,] { {0,0,1,0}, {1,1,1,0}, {0,0,0,0}, {0,0,0,0} },
            // O
            new int[,] { {0,1,1,0}, {0,1,1,0}, {0,0,0,0}, {0,0,0,0} },
            // S
            new int[,] { {0,1,1,0}, {1,1,0,0}, {0,0,0,0}, {0,0,0,0} },
            // T
            new int[,] { {0,1,0,0}, {1,1,1,0}, {0,0,0,0}, {0,0,0,0} },
            // Z
            new int[,] { {1,1,0,0}, {0,1,1,0}, {0,0,0,0}, {0,0,0,0} }
        };

        public Tetromino(int[,] shape, int color)
        {
            Shape = (int[,])shape.Clone();
            Color = color;
        }

        public static Tetromino Random(Random rand)
        {
            int idx = rand.Next(Shapes.Length);
            return new Tetromino(Shapes[idx], Colors[idx]);
        }

        public int[,] GetRotated(int dir)
        {
            int[,] result = new int[4, 4];
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    result[i, j] = dir > 0 ? Shape[3 - j, i] : Shape[j, 3 - i];
            return result;
        }
    }
}
