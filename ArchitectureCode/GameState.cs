using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Elementaria
{
    public class GameState
    {
        public const int ElementSize = 32;
        public List<CreatureAnimation> Animations = new List<CreatureAnimation>();
        public static Game Game;
        public HashSet<Keys> PressedKeys = new HashSet<Keys>();
        public void FindAnimations()
        {
            Animations.Clear();
            for (var x = 0; x < Game.MapWidth; x++)
                for (var y = 0; y < Game.MapHeight; y++)
                {
                    var creature = Game.Map[x, y];
                    if (creature == null) continue;
                    if (creature is Player) GetPlayerAnim(creature);
                    var command = creature.Delta;
                    var target = new Point(x + command.DeltaX, y + command.DeltaY);
                    Animations.Add(
                        new CreatureAnimation
                        {
                            Action = command,
                            Creature = creature,
                            Location = new Point(x * ElementSize, y * ElementSize),
                            Target = target
                        });
                    creature.Delta = new CreatureAction();
                }
        }

        public void MakeAnimations()
        {
            var pos = new ICreature[Game.MapWidth, Game.MapHeight];
            foreach (var e in Animations)
            {
                var x = e.Target.X;
                var y = e.Target.Y;
                pos[x, y] = e.Creature;
            }
            for (var x = 0; x < Game.MapWidth; x++)
                for (var y = 0; y < Game.MapHeight; y++)
                    Game.Map[x, y] = pos[x, y];
        }

        public void Act()
        {
            for (var x = 0; x < Game.MapWidth; x++)
                for (var y = 0; y < Game.MapHeight; y++)
                {
                    var creature = Game.Map[x, y];
                    if (creature == null || !(creature is Player || creature is Stone)) continue;
                    if (creature is Player)
                        creature.Delta = Move();
                    var command = creature.Delta;
                    var nextPos = new Point(x + command.DeltaX, y + command.DeltaY);
                    if (!InMap(nextPos) || Game.Map[nextPos.X, nextPos.Y] is Stone
                        || Game.Map[nextPos.X, nextPos.Y] is Box && (!InMap(new Point(nextPos.X + command.DeltaX, nextPos.Y + command.DeltaY))
                        || Game.Map[nextPos.X + command.DeltaX, nextPos.Y + command.DeltaY] != null))
                        creature.Delta = new CreatureAction();
                    else if (creature is Player)
                    {
                        command = creature.Delta;
                        if (Game.Map[nextPos.X, nextPos.Y] is Box)
                            Game.Map[nextPos.X, nextPos.Y].Delta = command;
                        if (Game.Map[nextPos.X, nextPos.Y] is Chest)
                        {
                            Game.KeyPressed = Keys.None;
                            Game.Map[nextPos.X, nextPos.Y].Picture = (Bitmap)System.Drawing.Image.FromFile("Images/OpenChest.png");
                            if (Game.CurrentLVL < Game.LVLs.Length - 1)
                            {
                                var dialog = MessageBox.Show("Хотите перейти на следующий уровень?", "Уровень пройден",
                                MessageBoxButtons.YesNo, MessageBoxIcon.None);
                                AnswerDialog(dialog);
                            }
                            else
                            {
                                var end = MessageBox.Show("Последний уровень пройден!", "Уровень пройден", MessageBoxButtons.OKCancel);
                                AnswerDialog(end);
                            }
                        }
                    }
                }
        }

        public void AnswerDialog(DialogResult dialog)
        {
            if (dialog == DialogResult.No || dialog == DialogResult.OK)
                System.Windows.Forms.Application.Exit();
            if (dialog == DialogResult.Yes)
            {
                PressedKeys = new HashSet<Keys>();
                Game.CurrentLVL++;
                Game.CreateMap(Game.LVLs[Game.CurrentLVL]);
            }
            if (dialog == DialogResult.Cancel)
            {
                PressedKeys = new HashSet<Keys>();
                Game.CurrentLVL = Game.LVLs.Length - 1;
                Game.CreateMap(Game.LVLs[Game.CurrentLVL]);
            }
        }

        public bool InMap(Point position)
        {
            return (position.Y <= Game.MapHeight - 1 && position.X <= Game.MapWidth - 1 && position.X >= 0 && position.Y >= 0);
        }

        public static CreatureAction Move()
        {
            var key = Game.KeyPressed;
            if (key == Keys.Down)
                return new CreatureAction { DeltaX = 0, DeltaY = 1 };
            if (key == Keys.Up)
                return new CreatureAction { DeltaX = 0, DeltaY = -1 };
            if (key == Keys.Right)
                return new CreatureAction { DeltaX = 1, DeltaY = 0 };
            if (key == Keys.Left)
                return new CreatureAction { DeltaX = -1, DeltaY = 0 };
            return new CreatureAction { };
        }

        public static void GetPlayerAnim(ICreature player)
        {
            if (player.Delta.DeltaX == -1)
                player.Picture = (Bitmap)System.Drawing.Image.FromFile("Images/PlayerL.png");
            if (player.Delta.DeltaX == 1)
                player.Picture = (Bitmap)System.Drawing.Image.FromFile("Images/PlayerR.png");
            if (player.Delta.DeltaY == -1)
                player.Picture = (Bitmap)System.Drawing.Image.FromFile("Images/PlayerUp.png");
            if (player.Delta.DeltaY == 1)
                player.Picture = (Bitmap)System.Drawing.Image.FromFile("Images/PlayerDown.png");
            if (player.Delta.DeltaX == 0 && player.Delta.DeltaY == 0) player.Picture = (Bitmap)System.Drawing.Image.FromFile(System.IO.Path.Combine("Images", "Player.png"));
        }
    }
}