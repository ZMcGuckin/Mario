using System;


namespace TheKoopaTroopas
{
    public class ExitCommand : ICommand
    {
        public void Execute()
        {
            Game1.Instance.Exit();
        }
    }
}
