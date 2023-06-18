using System.Drawing;

namespace Elementaria
{
    public interface ICreature
    {
        CreatureAction Delta { get; set; }
        Bitmap Picture { get; set; }
    }
}