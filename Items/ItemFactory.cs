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
    public static class ItemFactory
    {
        public static IItem CreateFireFlower(Vector2 location)
        {
            return new FireFlower(location);
        }

        public static IItem CreateStar(Vector2 location)
        {
            return new Star(location);
        }

        public static IItem CreateCoin(Vector2 location)
        {
            return new Coin(location);
        }

        public static IItem CreateHealthMushroom(Vector2 location)
        {
            return new HealthMushroom(location);
        }

        public static IItem CreateBigMushroom(Vector2 location)
        {
            return new BigMushroom(location);
        }
    }
}
