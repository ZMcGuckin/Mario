using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace TheKoopaTroopas
{
    public class PauseCommand : ICommand
    {
        public void Execute()
        {
            Game1.Instance.PauseUnpauseGame();
        }
    }
}