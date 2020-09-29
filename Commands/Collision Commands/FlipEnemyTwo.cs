using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKoopaTroopas
{
    public class FlipEnemyTwo : ICommand
    {
        IEnemy Enemy;
        IMario Mario;
        public FlipEnemyTwo(Collision c)
        {
            Enemy = (IEnemy)c.ObjectCollidedWith;
            if (c.ObjectColliding is IMario mario)
            {
                Mario = mario;
            }
            else if (c.ObjectColliding is Koopa koopa)
            {
                Mario = koopa.Pusher;
            }
            else if (c.ObjectColliding is FireBall fireBall)
            {
                Mario = fireBall.Shooter;
            }
        }

        public void Execute()
        {
            Enemy.BeFlipped(Mario);
        }
    }
}
