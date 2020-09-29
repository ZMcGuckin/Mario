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
    public class ItemBlock : AbstractBlock
    {
        public ItemBlock(Vector2 location, Items item)
        {
            Item = item;
            Location = location;
            Sprite = UniversalSpriteFactory.Instance.CreateSprite("ItemBlock", location);
            IsBumped = false;
        }

        public override void Update(GameTime gameTime)
        {
            Sprite.Update(gameTime,Location);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch,false);
        }

        public override string SpecificCollisionType => "ItemBlock";

        public override void Bump(IMario Mario)
        {
            ElapsedTime = 0;
            IsBumped = true;
            Bumper = Mario;
            Location = new Vector2(Location.X, Location.Y - bumpVelocity);
            Sprite.Location = Location;
            OpenedBlock.SpawnItem(Bumper, Item, this);
            new OpenBlockCommand(this).Execute();
        }
    }
}
