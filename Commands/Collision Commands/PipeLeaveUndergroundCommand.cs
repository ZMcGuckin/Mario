using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TheKoopaTroopas
{
    public class PipeLeaveUndergroundCommand : AbstractCollisionCommand<IMario, Pipe>
    {
        public PipeLeaveUndergroundCommand(Collision c) : base(c) { }
        public override void Execute()
        {
            receiver1.Velocity = new Vector2(0, 0);
            Game1.Instance.GoAboveGround(receiver2.TeleportLocation);
        }
    }
}
