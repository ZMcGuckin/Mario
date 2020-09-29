using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TheKoopaTroopas
{
    public class PushShell : AbstractCollisionCommand<IMario, Koopa>
    {
        CollisionSide Side;
        public PushShell(Collision c) : base(c)
        {
            Side = c.Side;
        }

        public override void Execute()
        {
            new SoundEffects().PlayKick();
            receiver2.PushShell();
            receiver2.Pusher = receiver1;
            receiver2.Velocity = Side == CollisionSide.Right ? new Vector2(receiver2.Velocity.X, receiver2.Velocity.Y) : new Vector2(receiver2.Velocity.X * -1, receiver2.Velocity.Y);
        }
    }
}
