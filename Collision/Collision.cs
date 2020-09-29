using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKoopaTroopas
{
    public class Collision
    {
        public IGameObject ObjectCollidedWith { get; set; }
        public IGameObject ObjectColliding { get; set; }
        public CollisionSide Side { get; set; }
        public Collision(IGameObject colliding, IGameObject collidedWith, CollisionSide side)
        {
            ObjectCollidedWith = collidedWith;
            ObjectColliding = colliding;
            this.Side = side;
        }
        public Collision()
        {
            ObjectCollidedWith = null;
            this.Side = CollisionSide.Default;
        }
    }
}
