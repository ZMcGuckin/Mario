using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TheKoopaTroopas
{
    public class FireMarioCommand : AbstractCollisionCommand<IMario, IGameObject>
    {
        public FireMarioCommand(Collision c) : base(c) { }

        public override void Execute()
        {
            Game1.Instance.UpgradeMario(receiver1, new FireMarioState());
            (new AquirePoints(receiver1, GameConstants.FireFlowerPoints)).Execute();
        }


    }
}
