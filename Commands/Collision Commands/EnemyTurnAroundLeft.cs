using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TheKoopaTroopas
{
    public class EnemyTurnAroundLeft : AbstractCollisionCommand<IEnemy,IGameObject>
    {
        CollisionSide Side;
        public EnemyTurnAroundLeft(Collision c) : base(c)
        {          
            Side = c.Side;
        }
        public override void Execute()
        {
            
            (new GeneralCollideBlockLeft(new Collision(receiver1, receiver2, Side))).Execute();
            receiver1.TurnAround();
            receiver1.Location = new Vector2(receiver1.Location.X + receiver1.Velocity.X, receiver1.Location.Y);
          
        }
    }
}
