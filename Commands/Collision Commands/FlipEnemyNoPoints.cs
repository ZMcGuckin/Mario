using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKoopaTroopas
{
    public class FlipEnemyNoPoints: AbstractCollisionCommand<IEnemy,IEnemy>
    {

        public FlipEnemyNoPoints(Collision c) : base(c) { }

        public override void Execute()
        {
            receiver2.BeFlipped(null);
        }
    }
}
