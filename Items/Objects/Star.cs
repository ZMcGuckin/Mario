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
    public class Star : AbstractItem
    {
        const int MaxYVelocity = 5;
        public Star(Vector2 location)
        {
            Velocity = new Vector2(3, 0);
            Grounded = true;
            Location = location;
            Sprite = UniversalSpriteFactory.Instance.CreateSprite("Star", Location);
        }

        public override void Update(GameTime gameTime)
        {
            if (Velocity.Y < MaxYVelocity)
            {
                Velocity = new Vector2(Velocity.X, Velocity.Y + GameConstants.GeneralGravity);
            }

            if (Grounded)
            {
                Velocity = new Vector2(Velocity.X, Velocity.Y - 7);
            }
            Location = new Vector2(Location.X + Velocity.X, Location.Y + Velocity.Y);
            Sprite.Update(gameTime, Location);
        }


        public override string SpecificCollisionType => "Star";
    }
}
