using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace TheKoopaTroopas
{
    public class ActionCommand : ICommand
    {
        IMario Mario;

        public ActionCommand(IMario Mario)
        {
            this.Mario = Mario;
        }

        public void Execute()
        {
            Mario.ActionPressed();
        }
    }
}
