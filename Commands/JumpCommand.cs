using System;


namespace TheKoopaTroopas
{
    public class JumpCommand : ICommand
    {
        IMario Mario;

        public JumpCommand(IMario Mario)
        {
            this.Mario = Mario;
        }

        public void Execute()
        {
            Mario.Jump();
        }
    }
}