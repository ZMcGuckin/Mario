using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKoopaTroopas
{
    public class RemoveObj2Command: AbstractCollisionCommand<IGameObject,IGameObject>
    {
        public RemoveObj2Command(Collision c) : base(c) { }
        public override void Execute()
        {
            Game1.Instance.GameLists.PurgeList.Add(receiver2);
        }
    }
}
