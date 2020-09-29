using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKoopaTroopas {
    public class TurnBothLeft : AbstractCollisionCommand<IEnemy,IEnemy>
    {
        public TurnBothLeft(Collision c) : base(c)
        {
        }
        public override void Execute()
        {
            Rectangle intersection = Rectangle.Intersect(receiver2.LocationRect, receiver1.LocationRect);
            receiver1.Location = new Vector2(receiver1.Location.X - intersection.Width/2, receiver1.Location.Y);
            receiver2.Location = new Vector2(receiver2.Location.X + intersection.Width/2, receiver2.Location.Y);
            receiver1.TurnAround();
            receiver2.TurnAround();
        }
    }
}
