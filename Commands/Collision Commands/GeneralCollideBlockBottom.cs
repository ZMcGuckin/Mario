using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKoopaTroopas
{
    public class GeneralCollideBlockBottom : AbstractCollisionCommand<IGameObject, IGameObject>
    {
        public GeneralCollideBlockBottom(Collision c) : base(c) { }
        public override void Execute()
        {
            if (receiver1.Velocity.Y < 0)
            {
                Rectangle intersection = Rectangle.Intersect(receiver2.LocationRect, receiver1.LocationRect);
                Vector2 newPos = new Vector2(receiver1.Location.X, receiver1.Location.Y + intersection.Height);
                receiver1.Location = newPos;
                receiver1.Velocity = new Vector2(receiver1.Velocity.X, 0);
            }
        }
    }
}
