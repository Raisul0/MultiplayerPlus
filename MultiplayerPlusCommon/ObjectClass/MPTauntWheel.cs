using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplayerPlusCommon.ObjectClass
{
    public class MPTauntWheel
    {
        public List<MPTaunt> Taunts = new List<MPTaunt>  {
            new MPTaunt(),
            new MPTaunt(),
            new MPTaunt(),
            new MPTaunt(),
            new MPTaunt(),
            new MPTaunt(),
        };

        public void UpdateTauntSlot(int slotId,string tauntId,string tauntAction,string tauntName,string soundEventName="")
        {
            var index = slotId - 1;
            var taunt = Taunts.ElementAt(index);
            taunt.TauntId = tauntId;
            taunt.TauntAction = tauntAction;
            taunt.TauntName = tauntName;
            taunt.SoundEventName = soundEventName;
        }

        public (string, string) GetTauntIdNameSlot(int slotId)
        {
            var index = slotId - 1;
            var taunt = Taunts.ElementAt(index);

            return (taunt.TauntId, taunt.TauntName);
        }

        public string FindTauntAction(string tauntId)
        {
            return Taunts.FirstOrDefault(x => x.TauntId == tauntId)?.TauntAction ?? "";
        }

    }
}
