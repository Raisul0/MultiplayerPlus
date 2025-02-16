using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiplayerPlusCommon.ObjectClasses;

namespace MultiplayerPlusCommon.Constants
{
    public static class MPTauntWheel
    {
        public static List<MPTaunt> Taunts = new List<MPTaunt>  {
            new MPTaunt(1,"First","",""),
            new MPTaunt(2,"Second","", ""),
            new MPTaunt(3,"Third","", ""),
            new MPTaunt(4,"Fourth","", ""),
            new MPTaunt(5,"Fifth","act_taunt_howl", "taunt/howl"),
            new MPTaunt(6,"Six","act_taunt_accolades","taunt/test"),
        };
    }
}
