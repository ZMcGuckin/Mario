using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKoopaTroopas
{
    public class BumpEnemy :AbstractCollisionCommand<IEnemy,IBlock>
    {
        CollisionSide Side;

        public BumpEnemy(Collision c) :base(c)
        {
            Side = c.Side;
        }
        public override void Execute()
        {

            if (receiver2.IsBumped)
            {
                receiver1.BeFlipped(receiver2.Bumper);
            }
            else
            {
                (new GeneralCollideBlockTop(new Collision(receiver1, receiver2, Side))).Execute();
            }
        }
    }
}
