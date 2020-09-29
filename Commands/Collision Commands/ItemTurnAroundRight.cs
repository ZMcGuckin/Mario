using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKoopaTroopas
{
    public class ItemTurnAroundRight : AbstractCollisionCommand<IItem,IBlock>
    {
        CollisionSide Side;
        public ItemTurnAroundRight(Collision c) : base(c)
        {
            Side = c.Side;
        }
        public override void Execute()
        {
            (new GeneralCollideBlockRight(new Collision(receiver1,receiver2,Side))).Execute();
            receiver1.TurnAround();
        }
    }
}
