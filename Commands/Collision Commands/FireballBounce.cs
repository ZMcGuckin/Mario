using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKoopaTroopas
{
    public class FireballBounce :AbstractCollisionCommand<FireBall,IGameObject>
    {
        public FireballBounce(Collision c) : base(c) { }
        public override void Execute()
        {
            receiver1.Bounce();
        }
    }
}
