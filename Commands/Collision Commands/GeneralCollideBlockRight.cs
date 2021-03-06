﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
namespace TheKoopaTroopas
{
    public class GeneralCollideBlockRight : AbstractCollisionCommand<IGameObject, IGameObject>
    {

        public GeneralCollideBlockRight(Collision c) : base(c) { }
        public override void Execute()
        {
            Rectangle intersection = Rectangle.Intersect(receiver2.LocationRect, receiver1.LocationRect);
            Vector2 newPos = new Vector2(receiver1.Location.X + intersection.Width, receiver1.Location.Y);
            receiver1.Location = newPos;
           
        }
    }
}
