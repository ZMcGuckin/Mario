using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKoopaTroopas
{
    public class ModeSelectionCommand : ICommand
    {
        public ModeSelectionCommand()
        {
        }

        public void Execute()
        {
            Game1.Instance.TransitionScreen.SetCursorPos();
        }
    }
}
