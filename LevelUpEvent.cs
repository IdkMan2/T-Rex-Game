using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T_Rex_Game
{
    delegate void LevelUpEventHandler(object sender, LevelUpEventArgs e);
    class LevelUpEventArgs
    {
        internal int level;
        internal LevelUpEventArgs(int level)
        {
            this.level = level;
        }
    }
}
