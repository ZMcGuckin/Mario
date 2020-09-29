using System;


namespace TheKoopaTroopas
{
    public class CrouchCommand : ICommand
    {
        IMario Mario;

        public CrouchCommand(IMario Mario)
        {
            this.Mario = Mario;
        }

        public void Execute()
        {
            Mario.Crouch();
        }
    }
}