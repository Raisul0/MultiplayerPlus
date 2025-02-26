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
                GameNetwork.WriteMessage(new GetPlayerTaunts(GameNetwork.MyPeer.UserName));
                GameNetwork.EndModuleEventAsClient();
            }
        }
        public void PopulateTauntSlots(List<MPTaunt> taunts)
        {
            var _tauntSlots = new MBBindingList<MPTauntSlotVM>();
            foreach (var taunt in taunts)
            {
                _tauntSlots.Add(new MPTauntSlotVM(taunt.TauntId, taunt.TauntName,taunt.TauntAction, OnSlotFocused));
            }
            Populated = true;
            TauntSlots = _tauntSlots;
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
                    GameNetwork.WriteMessage(new StartTaunt(tauntId));
                    GameNetwork.EndModuleEventAsClient();
                }
            }

        }

        public bool Populated { get; set; }
        private MBBindingList<MPTauntSlotVM> _tauntSlot;
        private MPTauntSlotVM _selectedSlot;
    }
}
