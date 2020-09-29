using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKoopaTroopas
{
    public interface IBlock : IGameObject
    {
        Boolean IsBumped { get; }
        IMario Bumper { get; set; }
        Items Item { get; set; }
        void Bump(IMario Mario);
    }
}
