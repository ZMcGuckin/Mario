using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKoopaTroopas
{
    public class MarioCollideBlockRight : AbstractCollisionCommand<IMario, IGameObject>
    {
        CollisionSide Side;
        public MarioCollideBlockRight(Collision c) : base(c)
        {
            Side = c.Side;
        }
        public override void Execute()
        {
            (new GeneralCollideBlockRight(new Collision(receiver1,receiver2, Side))).Execute();
            receiver1.Velocity = new Vector2(0, receiver1.Velocity.Y);
        }
    }
}
