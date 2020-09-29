using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace TheKoopaTroopas
{
    public class HiddenBlock : AbstractBlock
    {
        public HiddenBlock(Vector2 location, Items item)
        {
            Item = item;
            Location = location;
            Sprite = UniversalSpriteFactory.Instance.CreateSprite("HiddenBlock", location);
            IsBumped = false;
        }

        public override void Update(GameTime gameTime)
        {
            Sprite.Update(gameTime,Location);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        public override string SpecificCollisionType => "HiddenBlock";
        public override string CollisionType => "HiddenBlock";

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
