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
    public class PadController : IController
    {
        private Dictionary<Buttons, ICommand> controllerMappings;
        private const int arrayLength = 25;

        public PadController()
        {
            controllerMappings = new Dictionary<Buttons, ICommand>();
        }

        public void RegisterCommand(Buttons button, ICommand command)
        {
            controllerMappings.Add(button, command);
        }

        public void Update()
        {
            //Checks to see which buttons are pressed and then adds them to the execute list. It's not as easy as the keyboard
            var pressedButtons = new Buttons[arrayLength];

            foreach (Buttons button in Enum.GetValues(typeof(Buttons)))
            {
                if (GamePad.GetState(PlayerIndex.One).IsButtonDown(button))
                {
                    pressedButtons[pressedButtons.Count()] = button;
                }
            }

            foreach (Buttons button in pressedButtons)
            {
                //If game is paused or in finishing state, only accept unpause
                if (Game1.Instance.CurrentState == Game1.GameState.Paused || Game1.Instance.CurrentState == Game1.GameState.End)
                {
                    if (button == Buttons.Start)
                    {
                        controllerMappings[button].Execute();
                    }
                }
                else
                {
                    if (controllerMappings.ContainsKey(button))
                        controllerMappings[button].Execute();
                }
            }
        }
    }
}
