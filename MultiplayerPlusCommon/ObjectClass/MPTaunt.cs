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
            PrefabName = "";
        }
        public MPTaunt(string tauntId, string tauntName,string tauntAction, string soundEventName= null,string prefabName=null)
        {
            TauntId = tauntId;
            TauntName = tauntName;
            TauntAction = tauntAction;
            SoundEventName = soundEventName;
            PrefabName = prefabName;
        }

        public string TauntId { get; set; }
        public string TauntName { get; set; }
        public string TauntAction { get; set; }
        public string SoundEventName { get; set; }
        public string PrefabName { get; set;}
    }
}
