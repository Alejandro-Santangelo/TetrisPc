using System;
using System.Drawing;
using System.Windows.Forms;

namespace TetrisWinForms
{
    public class MainForm : Form
    {
        private Timer timer;
        private TetrisGame game;
        private Button btnPause;
        private Button btnRestart;
        private Label lblStatus;
        private Panel panelJuego;

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (game != null && !game.IsGameOver && (!game.IsPaused || keyData == Keys.P))
            {
                switch (keyData)
                {
                    case Keys.A:
                        game.MoveLeft();
                        break;
                    case Keys.D:
                        game.MoveRight();
                        break;
                    case Keys.Up:
                        game.RotateCW();
                        break;
                    case Keys.Down:
                        game.RotateCCW();
                        break;
                    case Keys.Left:
                        game.RotateCCW();
                        break;
                    case Keys.Right:
                        game.RotateCW();
                        break;
                    case Keys.P:
                        game.TogglePause();
                        break;
                    case Keys.R:
                        game.Restart();
                        break;
                    default:
                        return base.ProcessCmdKey(ref msg, keyData);
                }
                UpdateStatus();
                panelJuego.Invalidate();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public MainForm()
        {
            this.Load += (s, e) => this.Focus();
            this.DoubleBuffered = true;
            int blockSize = 28;
            int cols = 10;
            int rows = 20;
            int panelWidth = blockSize * cols; // 280
            int panelHeight = blockSize * rows; // 560
            this.Width = panelWidth + 140;
            this.Height = panelHeight + 40;
            this.Text = "Tetris";
            this.KeyPreview = true;

            panelJuego = new Panel();
            panelJuego.Location = new Point(0, 0);
            panelJuego.Size = new Size(panelWidth, panelHeight);
            panelJuego.BackColor = Color.Black;
            panelJuego.Paint += (s, e) => game.Draw(e.Graphics);
            panelJuego.TabStop = false;
            panelJuego.MouseClick += (s, e) => this.Focus();
            panelJuego.MouseEnter += (s, e) => this.Focus();
            this.Controls.Add(panelJuego);
            game = new TetrisGame();
            timer = new Timer();
            timer.Interval = 400;
            timer.Tick += (s, e) => { game.Tick(); UpdateStatus(); panelJuego.Invalidate(); };
            timer.Start();
            this.KeyDown += MainForm_KeyDown;

            int controlsLeft = panelWidth + 10;
            int controlsTop = 30;
            btnPause = new Button { Text = "Pausar", Location = new Point(controlsLeft, controlsTop), Size = new Size(100, 40) };
            btnPause.Click += (s, e) => { game.TogglePause(); UpdateStatus(); panelJuego.Invalidate(); };
            this.Controls.Add(btnPause);

            btnRestart = new Button { Text = "Nueva Partida", Location = new Point(controlsLeft, controlsTop + 60), Size = new Size(100, 40) };
            btnRestart.Click += (s, e) => { game.Restart(); UpdateStatus(); panelJuego.Invalidate(); };
            this.Controls.Add(btnRestart);

            lblStatus = new Label { Text = "", Location = new Point(controlsLeft, controlsTop + 120), Size = new Size(100, 30), ForeColor = Color.White, BackColor = Color.Black, TextAlign = ContentAlignment.MiddleCenter };
            this.Controls.Add(lblStatus);
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (game.IsGameOver) return;
            if (game.IsPaused && e.KeyCode != Keys.P) return;
            switch (e.KeyCode)
            {
                case Keys.A:
                    game.MoveLeft();
                    break;
                case Keys.D:
                    game.MoveRight();
                    break;
                case Keys.Up:
                    game.RotateCW();
                    break;
                case Keys.Down:
                    game.RotateCCW();
                    break;
                case Keys.Left:
                    game.RotateCCW();
                    break;
                case Keys.Right:
                    game.RotateCW();
                    break;
                case Keys.P:
                    game.TogglePause();
                    break;
                case Keys.R:
                    game.Restart();
                    break;
            }
            UpdateStatus();
            panelJuego.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // El dibujo del juego ahora está en el panelJuego
        }

        private void UpdateStatus()
        {
            if (game.IsGameOver)
            {
                lblStatus.Text = "Juego terminado";
                btnPause.Enabled = false;
            }
            else if (game.IsPaused)
            {
                lblStatus.Text = "Pausado";
                btnPause.Text = "Reanudar";
                btnPause.Enabled = true;
            }
            else
            {
                lblStatus.Text = "En juego";
                btnPause.Text = "Pausar";
                btnPause.Enabled = true;
            }
        }
    }
}
