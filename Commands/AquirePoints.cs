using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKoopaTroopas
{
    public class AquirePoints :ICommand
    {
        IMario Mario;
        int Pts;
        public AquirePoints(IMario mario,int ptsToAdd)
        {
            Mario = mario;
            Pts = ptsToAdd;
        }
        public void Execute()
        {
            Game1.Instance.GameVariables.Score += Pts;
            Game1.Instance.GameLists.IndicatorText.Add(new IndicatorText(Pts+"", new Vector2(Mario.Location.X + Mario.LocationRect.Width / 2, Mario.Location.Y)));
        }
    }
}
