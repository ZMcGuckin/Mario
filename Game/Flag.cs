using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKoopaTroopas
{
    public class Flag : IGameObject
    {
        public Vector2 Location { get; set; }
        int travelDistance = 330;
        int travelledDistance;
        UniversalSprite Sprite { get; set; }
        public Vector2 Velocity { get; set; }
        public bool Grounded { get; set; }
        public string CollisionType { get; set; }
        public string SpecificCollisionType { get; set; }

        public Flag(Vector2 location)
        {
            Location = location;
            Sprite = UniversalSpriteFactory.Instance.CreateSprite("Flag", Location);
        }

        public void Update(GameTime gameTime)
        {
            if (Game1.Instance.CurrentState == Game1.GameState.End && travelDistance > travelledDistance)
            {
                Location = new Vector2(Location.X, Location.Y + 2);
                Sprite.Update(gameTime, Location);
                travelledDistance += 2;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, false);
        }


        public Rectangle LocationRect
        {
            get { return Sprite.HitBox; }
        }
    }
}
