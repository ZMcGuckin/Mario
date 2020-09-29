using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKoopaTroopas
{
    class SpawnFireFlowerCommand : ICommand
    {
        private Vector2 location;

        public SpawnFireFlowerCommand(Vector2 blockLocation)
        {
            location = blockLocation;
            this.location.Y -= 28;
            this.location.X += 15;
        }

        public void Execute()
        {
            new SoundEffects().PlayPowerupAppears();
            Game1.Instance.GameLists.DynamicMasterList.Add(ItemFactory.CreateFireFlower(location));
        }
    }
}
