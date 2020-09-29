using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TheKoopaTroopas
{
    public class BigMarioCommand : AbstractCollisionCommand<IMario,IGameObject>
    {
        public BigMarioCommand(Collision c) : base(c) { }

        public override void Execute()
        {
            Game1.Instance.UpgradeMario(receiver1, new BigMarioState());
            new AquirePoints(receiver1, GameConstants.BigMushroomPoints).Execute();
        }

    }
}
