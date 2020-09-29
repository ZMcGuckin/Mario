using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace TheKoopaTroopas
{
    public class DestroyBlockCommand : ICommand
    {
        private IBlock block;

        public DestroyBlockCommand(IBlock block)
        {
            this.block = block;
        }

        public void Execute()
        {
            Game1.Instance.GameLists.PurgeList.Add(block);
        }
    }
}
