using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TheKoopaTroopas
{
    public class StarMarioCommand : AbstractCollisionCommand<IMario,IGameObject>
    {
        public StarMarioCommand(Collision c) : base(c) { }

        public override void Execute()
        {
            Game1.Instance.StarMario(receiver1);
            (new AquirePoints(receiver1, GameConstants.StarPoints)).Execute();
        }
    }
}
