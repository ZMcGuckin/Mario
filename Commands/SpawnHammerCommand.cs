using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKoopaTroopas
{
    public class SpawnHammerCommand : ICommand
    {
        Vector2 location;

        public SpawnHammerCommand(Vector2 location)
        {
            this.location = location;
        }

        public void Execute()
        {
            Game1.Instance.GameLists.DynamicMasterList.Add(new Hammer(location));
        }
    }
}
