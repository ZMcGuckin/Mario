using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKoopaTroopas
{
    public class FlipEnemyOne : ICommand
    {
        IEnemy Enemy;
        IMario Mario;
        public FlipEnemyOne(Collision c)
        {
            Enemy = (IEnemy)c.ObjectColliding;
            if(c.ObjectCollidedWith is IMario mario)
            {
                Mario = mario;
            }
            else if(c.ObjectCollidedWith is Koopa koopa)
            {
                Mario = koopa.Pusher;
            }
            else if(c.ObjectCollidedWith is FireBall fireBall)
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
