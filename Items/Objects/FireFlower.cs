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
    public class FireFlower : AbstractItem
    {
        public FireFlower(Vector2 location)
        {
            Location = location;
            Sprite = UniversalSpriteFactory.Instance.CreateSprite("FireFlower", Location);
        }

        public override void Update(GameTime gameTime)
        {
            Sprite.Update(gameTime, Location);
        }


        public override string SpecificCollisionType => "FireFlower";
    }  
}
