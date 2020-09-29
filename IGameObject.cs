using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;



namespace TheKoopaTroopas
{
    public interface IGameObject : IUpdateable
    {
        Rectangle LocationRect { get; }
        Vector2 Location{ get; set; }
        Vector2 Velocity { get; set; }
        Boolean Grounded { get; set; }
        String CollisionType { get; }

        String SpecificCollisionType { get; }
    }
}
