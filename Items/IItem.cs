﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKoopaTroopas
{
    public interface IItem : IGameObject
    {
        void ResetYVelocity();
        void TurnAround();
    }
}