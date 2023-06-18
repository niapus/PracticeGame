using System;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Windows.Forms;
namespace Elementaria
{
    public class GameWindow : Form
    {
        private readonly GameState gameState;
        private int tickCount;
        private Button button1 = new Button() { Location = new Point(0, 0), Text = "LVL 1" };
        private Button button2 = new Button() { Location = new Point(75, 0), Text = "LVL 2" };
        private Button button3 = new Button() { Location = new Point(150, 0), Text = "LVL 3" };
        private Button button4 = new Button() { Location = new Point(225, 0), Text = "LVL 4" };
        private Button buttonRestart = new Button() { Location = new Point(300, 0), Text = "Restart" };
        private Bitmap picture = (Bitmap)Image.FromFile("Images/Terrain.png");

        public GameWindow()
        {
            Controls.Add(button1);
            Controls.Add(button2);
            Controls.Add(button3);
            Controls.Add(button4);
            Controls.Add(buttonRestart);
            button1.MouseClick += Button1_MouseClick;
            button2.MouseClick += Button2_MouseClick;
            button3.MouseClick += Button3_MouseClick;
            button4.MouseClick += Button4_MouseClick;
            buttonRestart.MouseClick += ButtonRestart_MouseClick;
            gameState = new GameState();
            var timer = new Timer();
            timer.Interval = 5;
            timer.Tick += TimerTick;
            timer.Start();
        }

        private void Button1_MouseClick(object sender, MouseEventArgs e)
        {
            GameState.Game.CurrentLVL = 0;
            GameState.Game.CreateMap(GameState.Game.LVLs[GameState.Game.CurrentLVL]);
        }

        private void Button2_MouseClick(object sender, MouseEventArgs e)
        {
            GameState.Game.CurrentLVL = 1;
            GameState.Game.CreateMap(GameState.Game.LVLs[GameState.Game.CurrentLVL]);
        }

        private void Button3_MouseClick(object sender, MouseEventArgs e)
        {
            GameState.Game.CurrentLVL = 2;
            GameState.Game.CreateMap(GameState.Game.LVLs[GameState.Game.CurrentLVL]);
        }

        private void Button4_MouseClick(object sender, MouseEventArgs e)
        {
            GameState.Game.CurrentLVL = 3;
            GameState.Game.CreateMap(GameState.Game.LVLs[GameState.Game.CurrentLVL]);
        }

        private void ButtonRestart_MouseClick(object sender, MouseEventArgs e)
            => GameState.Game.CreateMap(GameState.Game.LVLs[GameState.Game.CurrentLVL]);

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Text = "Elementaria";
            DoubleBuffered = true;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            gameState.PressedKeys.Add(e.KeyCode);
            GameState.Game.KeyPressed = e.KeyCode;
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            gameState.PressedKeys.Remove(e.KeyCode);
            GameState.Game.KeyPressed = gameState.PressedKeys.Any() ? gameState.PressedKeys.Min() : Keys.None;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.TranslateTransform(0, GameState.ElementSize);
            e.Graphics.Clear(Color.White);
            for (var x = 0; x < GameState.Game.MapWidth; x++)
                for (var y = 0; y < GameState.Game.MapHeight; y++)
                    e.Graphics.DrawImage(picture, new Point(x * GameState.ElementSize, y * GameState.ElementSize));
            foreach (var a in gameState.Animations)
            {
                e.Graphics.DrawImage(a.Creature.Picture, a.Location);
            }
        }

        private void TimerTick(object sender, EventArgs args)
        {
            if (tickCount == 0)
            {
                gameState.Act();
                gameState.FindAnimations();
                gameState.MakeAnimations();
                if (MousePosition.Y - Location.Y > 2 * GameState.ElementSize || MousePosition.Y - Location.Y < SystemInformation.CaptionHeight
                    || MousePosition.X - Location.X < 0 || MousePosition.X - Location.X > Size.Width)
                {
                    button1.Enabled = false;
                    button2.Enabled = false;
                    button3.Enabled = false;
                    button4.Enabled = false;
                    buttonRestart.Enabled = false;
                }
                else
                {
                    button1.Enabled = true;
                    button2.Enabled = true;
                    button3.Enabled = true;
                    button4.Enabled = true;
                    buttonRestart.Enabled = true;
                }
            }
            ClientSize = new Size(
                GameState.ElementSize * GameState.Game.MapWidth,
                GameState.ElementSize * (GameState.Game.MapHeight + 1));
            foreach (var e in gameState.Animations)
                e.Location = new Point(e.Location.X + GameState.Game.PlayerSpeed * e.Action.DeltaX, e.Location.Y
                    + GameState.Game.PlayerSpeed * e.Action.DeltaY);
            tickCount++;
            if (tickCount == GameState.ElementSize / GameState.Game.PlayerSpeed)
            {
                tickCount = 0;
            }
            Invalidate();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // GameWindow
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "GameWindow";
            this.Load += new System.EventHandler(this.GameWindow_Load);
            this.ResumeLayout(false);

        }

        private void GameWindow_Load(object sender, EventArgs e)
        {

        }
    }
}