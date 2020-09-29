using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKoopaTroopas
{
    public class ItemTurnAroundLeft : AbstractCollisionCommand<IItem, IBlock>
    {
        CollisionSide Side;
        public ItemTurnAroundLeft(Collision c) : base(c)
        {
            Side = c.Side;
        }
        public override void Execute()
        {
            (new GeneralCollideBlockLeft(new Collision(receiver1,receiver2,Side))).Execute();
            receiver1.TurnAround();
        }
    }
}
