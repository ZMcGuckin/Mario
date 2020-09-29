using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKoopaTroopas
{
    class SpawnHealthMushroomCommand : ICommand
    {
        private Vector2 location;

        public SpawnHealthMushroomCommand(Vector2 blockLocation)
        {
            location = blockLocation;
            this.location.Y -= 34;
            this.location.X += 15;
        }

        public void Execute()
        {
            new SoundEffects().PlayPowerupAppears();
            Game1.Instance.GameLists.DynamicMasterList.Add(ItemFactory.CreateHealthMushroom(location));
        }
    }
}
