using System;


namespace TheKoopaTroopas
{
    public static class Program
    {
        private static Game1 gameInstance = Game1.Instance;
        [STAThread]
      
        static void Main()
        {
            using (var game = gameInstance)
                game.Run();
        }
    }
}
