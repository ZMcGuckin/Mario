using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TheKoopaTroopas
{
    public class Coin : AbstractItem
    {
        public Coin(Vector2 location)
        {
            Velocity = new Vector2(0, -5);
            Grounded = false;
            Location = location;
            Sprite = UniversalSpriteFactory.Instance.CreateSprite("Coin", Location);
        }

        public override void Update(GameTime gameTime)
        {
            Velocity = new Vector2(Velocity.X, Velocity.Y + GameConstants.GeneralGravity * 2);
            if (Grounded)
            {
                Game1.Instance.GameLists.PurgeList.Add(this);

            }
            Location = movingLeft ? new Vector2(Location.X - Velocity.X, Location.Y + Velocity.Y) : new Vector2(Location.X + Velocity.X, Location.Y + Velocity.Y);
            Sprite.Update(gameTime,Location);
        }

        public override string SpecificCollisionType => "Coin";
    }
}
