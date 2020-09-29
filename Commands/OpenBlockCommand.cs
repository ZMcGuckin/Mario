using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheKoopaTroopas
{
    public class OpenBlockCommand : ICommand
    {
        IBlock block;

        public OpenBlockCommand(IBlock block)
        {
            this.block = block;
        }

        public void Execute()
        {
            Vector2 location = block.Location;
            Game1.Instance.GameLists.PurgeList.Add(block);
            Game1.Instance.GameLists.AddList.Add(BlockFactory.CreateOpenedBlock(location));
        }
    }
}