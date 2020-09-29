using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TheKoopaTroopas
{
    public class OneUp : AbstractCollisionCommand<IMario, IGameObject>
    {
        public OneUp(Collision c) : base(c) { }
        public override void Execute()
        {
            new SoundEffects().PlayOneup();
            //only display 1up text if appropriate
            if(receiver1 != null)
                Game1.Instance.GameLists.IndicatorText.Add(new IndicatorText("1Up", new Vector2(receiver1.Location.X + receiver1.LocationRect.Width / 2, receiver1.Location.Y)));
            Game1.Instance.GameVariables.Lives += 1;
        }
    }
}
