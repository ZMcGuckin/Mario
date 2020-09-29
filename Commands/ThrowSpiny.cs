using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKoopaTroopas
{
    public class ThrowSpiny : ICommand
    {
        Vector2 Location;
        public ThrowSpiny(Vector2 location)
        {
            Location = location;
        }
        public void Execute()
        {
            Spiny spine = new Spiny(Location);
            Game1.Instance.GameLists.DynamicMasterList.Add(spine);
        }
    }
}
