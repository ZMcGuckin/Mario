using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKoopaTroopas
{
    public class MarioBounce : AbstractCollisionCommand<IMario,IGameObject>
    {
        public MarioBounce(Collision c) : base(c) { }
        public override void Execute()
        {
            receiver1.EnemyJump();
        }
    }
}
