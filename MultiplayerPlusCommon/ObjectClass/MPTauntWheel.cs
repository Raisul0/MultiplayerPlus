using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MultiplayerPlusCommon.ObjectClass
{
    public class MPTauntWheel
    {
        public const int MatchMVPSlot = 0;
        public const int RoundMVPSlot = 1;

        public List<MPTaunt> Taunts = new List<MPTaunt>  {
            new MPTaunt(),
            new MPTaunt(),
            new MPTaunt(),
            new MPTaunt(),
            new MPTaunt(),
            new MPTaunt(),
            new MPTaunt(),
            new MPTaunt(),
            new MPTaunt(),
            new MPTaunt(),
        };

        public void UpdateTauntSlot(int slotId, string tauntId, string tauntAction, string tauntName, string soundEventName = "")
        {
            var index = slotId - 1;
            var taunt = Taunts.ElementAt(index);
            taunt.TauntId = tauntId;
            taunt.TauntAction = tauntAction;
            taunt.TauntName = tauntName;
            taunt.SoundEventName = soundEventName;
        }

        public MPTaunt GetTauntFromId(string tauntId)
        {
           return Taunts.FirstOrDefault(x=>x.TauntId == tauntId) ?? new MPTaunt();
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

        public string GetMatchMVPTauntAction()
        {
            return Taunts.ElementAt(MatchMVPSlot)?.TauntAction ?? "";
        }
        public string GetRoundMVPTauntAction()
        {
            return Taunts.ElementAt(RoundMVPSlot)?.TauntAction ?? "";
        }

    }

    public static class MPTauntWheelDummy
    {
        public static MPTauntWheel GetWheel()
        {
            var tauntWheel = new MPTauntWheel();
            tauntWheel.UpdateTauntSlot(1, "1", "act_taunt_breakjestering_freeze", "Breakjestering Freeze", "");
            tauntWheel.UpdateTauntSlot(2, "2", "act_taunt_california_gurls", "California Gurls", "");
            tauntWheel.UpdateTauntSlot(3, "3", "act_taunt_flag_dance_1", "Flag Dance 1", "");
            tauntWheel.UpdateTauntSlot(4, "4", "act_taunt_flag_dance_2", "Flag Dance 2", "");
            tauntWheel.UpdateTauntSlot(5, "5", "act_taunt_look_behind", "Look Behind", "");
            tauntWheel.UpdateTauntSlot(6, "6", "act_taunt_knighted", "Knighted", "");
            tauntWheel.UpdateTauntSlot(7, "7", "act_taunt_glance_behind", "Glance Behind", "");
            tauntWheel.UpdateTauntSlot(8, "8", "act_taunt_roar", "Roar", "");
            tauntWheel.UpdateTauntSlot(9, "9", "act_taunt_t_pose", "T-Pose", "");
            tauntWheel.UpdateTauntSlot(10, "10", "act_taunt_true_heart", "True Heart", "");

            return tauntWheel;
        }
    }
}
