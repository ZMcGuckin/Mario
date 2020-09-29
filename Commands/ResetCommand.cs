using System;


namespace TheKoopaTroopas
{
    public class ResetCommand : ICommand
    {
        public void Execute()
        {
            Game1.Instance.GameReset();
        }
    }
}
