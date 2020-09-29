using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKoopaTroopas
{
    public class NullCommand : AbstractCollisionCommand<object, object>
    {
        public NullCommand(Collision c) : base(c) { }
    }
}
