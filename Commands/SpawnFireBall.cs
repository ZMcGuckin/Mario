using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKoopaTroopas
{
    public class SpawnFireBallCommand : ICommand
    {
        Vector2 location;
        Boolean facingRight;
        IMario shooter;

        public SpawnFireBallCommand(Vector2 location, Boolean facingRight, IMario shooter)
        {
            this.location = location;
            this.facingRight = facingRight;
            this.shooter = shooter;
        }

        public void Execute()
        {
            Game1.Instance.GameLists.DynamicMasterList.Add(new FireBall(location, facingRight, shooter));
        }
    }
}
