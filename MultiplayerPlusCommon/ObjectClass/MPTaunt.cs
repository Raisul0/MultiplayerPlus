using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplayerPlusCommon.ObjectClass
{
    public class MPTaunt
    {
        public MPTaunt()
        {
            TauntId = "";
            TauntName = "None";
            TauntAction = "";
            SoundEventName = "";
        }
        public MPTaunt(string tauntId, string tauntName,string tauntAction, string soundEventName= null)
        {
            TauntId = tauntId;
            TauntName = tauntName;
            TauntAction = tauntAction;
            SoundEventName = soundEventName;
        }

        public string TauntId { get; set; }
        public string TauntName { get; set; }
        public string TauntAction { get; set; }
        public string SoundEventName { get; set; }
    }
}
