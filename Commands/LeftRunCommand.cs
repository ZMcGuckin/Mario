using System;


namespace TheKoopaTroopas
{
    public class LeftRunCommand : ICommand
    {
        IMario Mario;

        public LeftRunCommand(IMario Mario)
        {
            this.Mario = Mario;
        }

        public void Execute()
        {
            Mario.Move(Game1.Instance.GameTime, false);
        }
    }
}
