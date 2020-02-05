using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T_Rex_Game
{

    delegate void GameTickEventHandler(object sender, GameTickEventArgs e);
    class GameTickEventArgs
    {
        internal long ticks;
        internal GameTickEventArgs(long ticks)
        {
            this.ticks = ticks;
        }
    }

}
