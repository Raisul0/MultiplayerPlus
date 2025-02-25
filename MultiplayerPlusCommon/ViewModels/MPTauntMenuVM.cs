using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiplayerPlusCommon.Behaviors;
using MultiplayerPlusCommon.Constants;
using MultiplayerPlusCommon.NetworkMessages.FromClient;
using MultiplayerPlusCommon.ObjectClass;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace MultiplayerPlusCommon.ViewModels
{
    public class MPTauntMenuVM : ViewModel
    {
        [DataSourceProperty]
        public MBBindingList<MPTauntSlotVM> TauntSlots
        {
            get
            {
                return _tauntSlot;
            }
            set
            {
                if (value != _tauntSlot)
                {
                    _tauntSlot = value;
                    OnPropertyChangedWithValue(value, "TauntSlots");
                }
            }
        }


        public MPTauntMenuVM()
        {
            RefreshValues();
            Populated = false;
        }

        public void SetSlots()
        {
            if (GameNetwork.IsClient)
            {
                GameNetwork.BeginModuleEventAsClient();
                GameNetwork.WriteMessage(new GetPlayerShouts(GameNetwork.MyPeer.UserName));
                GameNetwork.EndModuleEventAsClient();
            }
        }
        private MBBindingList<MPTauntSlotVM> PopulateTauntSlots()
        {
            var _tauntSlots = new MBBindingList<MPTauntSlotVM>();
            var player = MPPlayers.GetMPAgentFromUserName(GameNetwork.MyPeer.UserName);
            List<MPTaunt> taunts;
            if(player != null)
            {
                taunts = player.TauntWheel.Taunts;
            }
            else
            {
                taunts = new MPTauntWheel().Taunts;
            }
            foreach (var taunt in taunts)
            {
                _tauntSlots.Add(new MPTauntSlotVM(taunt.TauntId, taunt.TauntName,taunt.TauntAction, OnSlotFocused));
            }

            return _tauntSlots;
        }

        private void OnSlotFocused(MPTauntSlotVM tauntSlot)
        {
            _selectedSlot = tauntSlot;
        }

        public void ExecuteTaunt()
        {
            if (_selectedSlot != null) {

                var tauntId = _selectedSlot.TanutId;

                if (GameNetwork.IsClient)
                {
                    GameNetwork.BeginModuleEventAsClient();
                    GameNetwork.WriteMessage(new StartTaunt(tauntId, GameNetwork.MyPeer));
                    GameNetwork.EndModuleEventAsClient();
                }
            }

        }

        public bool Populated { get; set; }
        private MBBindingList<MPTauntSlotVM> _tauntSlot;
        private MPTauntSlotVM _selectedSlot;
    }
}
