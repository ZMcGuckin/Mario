using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKoopaTroopas
{
    public class BumpBlock : AbstractCollisionCommand<IMario,IBlock>
    {
        
        public BumpBlock(Collision c) : base(c) { }


        public override void Execute()
        {

            if (receiver1.Velocity.Y <= 0)
            {
                ((AbstractBlock)receiver2).Bump(receiver1);
            }
        }
    }
}
