using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKoopaTroopas
{
    public class MarioCollideBlockLeft : AbstractCollisionCommand<IMario, IGameObject>
    {
        CollisionSide Side;
        public MarioCollideBlockLeft(Collision c) : base(c)
        {
            Side = c.Side;
        }
        public override void Execute()
        {
            (new GeneralCollideBlockLeft(new Collision(receiver1, receiver2, Side))).Execute();
            receiver1.Velocity = new Vector2(0, receiver1.Velocity.Y);
        }
    }
}
