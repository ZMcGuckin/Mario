using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKoopaTroopas
{
    class SpawnCoinCommand : ICommand
    {
        private Vector2 location;

        public SpawnCoinCommand(Vector2 blockLocation)
        {
            location = blockLocation;
            this.location.Y -= 34;
            this.location.X += 15;
        }

        public void Execute()
        {
            new SoundEffects().PlayCoin();
            Game1.Instance.GameLists.DynamicMasterList.Add(ItemFactory.CreateCoin(location));
        }
    }
}
