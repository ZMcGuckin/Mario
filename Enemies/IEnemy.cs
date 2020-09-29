using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKoopaTroopas
{
    public interface IEnemy : IGameObject
    {
        void BeStomped(IMario mario);
        void BeFlipped(IMario mario);
        void TurnAround();
    }
}
