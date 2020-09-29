using System;


namespace TheKoopaTroopas
{
    public class RightRunCommand : ICommand
    {
        IMario Mario;

        public RightRunCommand(IMario Mario)
        {
            this.Mario = Mario;
        }

        public void Execute()
        {
            Mario.Move(Game1.Instance.GameTime, true);
        }
    }
}