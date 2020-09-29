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
    public class KeyController : IController
    {
        private Dictionary<Keys, ICommand> keyMappings;

        public KeyController()
        {
            keyMappings = new Dictionary<Keys, ICommand>();
        }

        public void RegisterCommand(Keys key, ICommand command)
        {
            keyMappings.Add(key, command);
        }

        public void Update()
        {
            Keys[] pressedKeys = Keyboard.GetState().GetPressedKeys();

            foreach (Keys key in pressedKeys)
            {
                //If game is paused or in finishing state, only accept quit, unpause, reset
                if(Game1.Instance.CurrentState == Game1.GameState.Paused || Game1.Instance.CurrentState == Game1.GameState.End)
                {
                    if(key == Keys.P || key == Keys.R || key == Keys.Q)
                    {
                        keyMappings[key].Execute();
                    }
                }
                else
                {
                    if (keyMappings.ContainsKey(key))
                        keyMappings[key].Execute();
                }
            }
        }
    }
}
