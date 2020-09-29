using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TheKoopaTroopas
{
    public class LittleMarioCommand : ICommand
    {
        IMario Mario;

        public LittleMarioCommand(IMario Mario)
        {
            this.Mario = Mario;
        }

        public void Execute()
        {
            Game1.Instance.DamageMario(Mario, new LittleMarioState());
        }
    }
}
