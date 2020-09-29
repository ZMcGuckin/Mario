using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKoopaTroopas
{
    public class MarioTakeDamage : AbstractCollisionCommand<IMario, IGameObject>
    {   
        public MarioTakeDamage(Collision c) : base(c) { }

        public override void Execute()
        {
            receiver1.TakeDamage();
        }
    }
}
