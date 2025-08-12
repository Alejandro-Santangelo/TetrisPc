using System;
using System.Drawing;

namespace TetrisWinForms
{
    public class TetrisGame
    {
        public bool IsGameOver { get; private set; } = false;
        public bool IsPaused { get; private set; } = false;
        private const int Rows = 20;
        private const int Cols = 10;
        private const int BlockSize = 28;
        private int[,] board = new int[Rows, Cols];
        private Tetromino current;
        private Random rand = new Random();
        private int curRow, curCol;

        public TetrisGame()
        {
            SpawnTetromino();
        }

        public void Tick()
        {
            if (IsPaused || IsGameOver) return;
            if (!Move(1, 0))
            {
                Place();
                ClearLines();
                if (!CanSpawn())
                {
                    IsGameOver = true;
                }
                else
                {
                    SpawnTetromino();
                }
            }
        }

        public void MoveLeft() => Move(0, -1);
        public void Pause() => IsPaused = true;
        public void Resume() => IsPaused = false;
        public void TogglePause() => IsPaused = !IsPaused;
        public void Restart()
        {
            board = new int[Rows, Cols];
            IsGameOver = false;
            IsPaused = false;
            SpawnTetromino();
        }
        public void MoveRight() => Move(0, 1);
        public void RotateCW() => Rotate(1);
        public void RotateCCW() => Rotate(-1);
        public void RotateLeft() => Rotate(-1);
        public void RotateRight() => Rotate(1);

        private void SpawnTetromino()
        {
            current = Tetromino.Random(rand);
            curRow = 0;
            curCol = Cols / 2 - 2;
        }

        private bool Move(int dr, int dc)
        {
            if (IsPaused || IsGameOver) return false;
            if (Valid(curRow + dr, curCol + dc, current.Shape))
            {
                curRow += dr;
                curCol += dc;
                return true;
            }
            return false;
        }

        private bool CanSpawn()
        {
            return Valid(0, Cols / 2 - 2, current.Shape);
        }

        private void Rotate(int dir)
        {
            var rotated = current.GetRotated(dir);
            if (Valid(curRow, curCol, rotated))
                current.Shape = rotated;
        }

        private bool Valid(int r, int c, int[,] shape)
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (shape[i, j] != 0)
                    {
                        int nr = r + i, nc = c + j;
                        if (nr < 0 || nr >= Rows || nc < 0 || nc >= Cols || board[nr, nc] != 0)
                            return false;
                    }
            return true;
        }

        private void Place()
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (current.Shape[i, j] != 0)
                        board[curRow + i, curCol + j] = current.Color;
        }

        private void ClearLines()
        {
            for (int i = Rows - 1; i >= 0; i--)
            {
                bool full = true;
                for (int j = 0; j < Cols; j++)
                    if (board[i, j] == 0) full = false;
                if (full)
                {
                    for (int k = i; k > 0; k--)
                        for (int j = 0; j < Cols; j++)
                            board[k, j] = board[k - 1, j];
                    for (int j = 0; j < Cols; j++)
                        board[0, j] = 0;
                    i++;
                }
            }
        }

        public void Draw(Graphics g)
        {
            g.Clear(Color.Black);
            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Cols; j++)
                    if (board[i, j] != 0)
                        g.FillRectangle(new SolidBrush(Color.FromArgb(board[i, j])), j * BlockSize, i * BlockSize, BlockSize - 1, BlockSize - 1);
            if (!IsGameOver)
            {
                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 4; j++)
                        if (current.Shape[i, j] != 0)
                            g.FillRectangle(new SolidBrush(Color.FromArgb(current.Color)), (curCol + j) * BlockSize, (curRow + i) * BlockSize, BlockSize - 1, BlockSize - 1);
            }
            if (IsGameOver)
            {
                using (var sf = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                using (var font = new Font("Arial", 24, FontStyle.Bold))
                using (var brush = new SolidBrush(Color.White))
                {
                    g.DrawString("GAME OVER", font, brush, new RectangleF(0, 0, Cols * BlockSize, Rows * BlockSize), sf);
                }
            }
        }
    }
}
