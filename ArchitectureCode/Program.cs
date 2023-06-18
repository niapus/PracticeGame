using System;
using System.Windows.Forms;

namespace Elementaria
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            GameState.Game = new Game();
            GameState.Game.CreateMap(GameState.Game.LVLs[0]);
            Application.Run(new GameWindow());
        }
    }
}