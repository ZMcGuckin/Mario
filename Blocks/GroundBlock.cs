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
    public class GroundBlock : AbstractBlock
    {
        public GroundBlock(Vector2 location)
        {
            Location = location;
            Sprite = UniversalSpriteFactory.Instance.CreateSprite("GroundBlock", location);
        }

        public override void Update(GameTime gameTime)
        {
            Sprite.Update(gameTime,Location);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch,false);
        }

        public override string SpecificCollisionType => "GroundBlock";

        public override void Bump(IMario Mario)
        {
            //Ground blocks do not bump
        }
    }
}
