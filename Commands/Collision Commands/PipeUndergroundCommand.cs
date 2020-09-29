using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TheKoopaTroopas
{
    public class PipeUndergroundCommand : AbstractCollisionCommand<IMario, Pipe>
    {
        public PipeUndergroundCommand(Collision c) : base(c) { }
        public override void Execute()
        {
            Rectangle intersection = Rectangle.Intersect(receiver2.LocationRect, receiver1.LocationRect);
            receiver1.Location = new Vector2(receiver1.Location.X, receiver1.Location.Y - intersection.Height + 1);
            receiver1.Grounded = true;
            receiver1.Velocity = new Vector2(receiver1.Velocity.X, 0);
            if (receiver1.IsCrouching())
            {
                Game1.Instance.GoUnderground(receiver2.TeleportLocation);
            }
        }
    }
}
