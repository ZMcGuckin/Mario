using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKoopaTroopas
{
    public abstract class AbstractCollisionCommand<T, U> : ICommand
    {
        protected T receiver1 { get; private set; }
        protected U receiver2 { get; private set; }
        protected AbstractCollisionCommand(Collision c)
        {
            receiver1 = (T)c.ObjectColliding;
            receiver2 = (U)c.ObjectCollidedWith;
        }
        public virtual void Execute()
        {
        }
    }
}
