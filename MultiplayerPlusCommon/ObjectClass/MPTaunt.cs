using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplayerPlusCommon.ObjectClasses
{
    public class MPTaunt
    {
        public MPTaunt(int tauntId, string tauntName,string tauntAction, string soundEventName)
        {
            TauntId = tauntId;
            TauntName = tauntName;
            TauntAction = tauntAction;
            SoundEventName = soundEventName;
        }

        public int TauntId { get; set; }
        public string TauntName { get; set; }
        public string TauntAction { get; set; }
        public string SoundEventName { get; set; }
    }
}
