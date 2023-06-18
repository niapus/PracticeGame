using System.Drawing;

namespace Elementaria
{
    public class Player : ICreature
    {
        public CreatureAction DeltaPos = new CreatureAction();
        public Bitmap picture = (Bitmap)Image.FromFile("Images/Player.png");
        public CreatureAction Delta
        {
            get { return DeltaPos; }
            set { DeltaPos = value; }
        }
        public Bitmap Picture
        {
            get { return picture; }
            set { picture = value; }
        }
    }
    
    public class Stone : ICreature
    {
        public CreatureAction DeltaPos = new CreatureAction();
        public Bitmap picture = (Bitmap)Image.FromFile("Images/Stone.png");
        public CreatureAction Delta
        {
            get { return DeltaPos; }
            set { DeltaPos = value; }
        }
        public Bitmap Picture
        {
            get { return picture; }
            set { picture = value; }
        }
    }

    public class Box : ICreature
    {
        public CreatureAction DeltaPos = new CreatureAction();
        public Bitmap picture = (Bitmap)Image.FromFile("Images/Box.png");
        public CreatureAction Delta
        {
            get { return DeltaPos; }
            set { DeltaPos = value; }
        }
        public Bitmap Picture
        {
            get { return picture; }
            set { picture = value; }
        }
    }
    
    public class Chest : ICreature
    {
        public CreatureAction DeltaPos = new CreatureAction();
        public Bitmap picture = (Bitmap)Image.FromFile("Images/Chest.png");
        public CreatureAction Delta
        {
            get { return DeltaPos; }
            set { DeltaPos = value; }
        }
        public Bitmap Picture
        {
            get { return picture; }
            set { picture = value; }
        }
    }
}