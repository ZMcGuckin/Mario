using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKoopaTroopas
{
    public class StompEnemy : AbstractCollisionCommand<IMario,IEnemy>
    {
        public StompEnemy(Collision c) : base(c) { }

        public override  void Execute()
        {
            receiver2.BeStomped(receiver1);
        }
    }
}
