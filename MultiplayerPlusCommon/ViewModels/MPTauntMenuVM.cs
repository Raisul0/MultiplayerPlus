using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiplayerPlusCommon.Behaviors;
using MultiplayerPlusCommon.Constants;
using MultiplayerPlusCommon.NetworkMessages.FromClient;
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
            TauntSlots = PopulateTauntSlots();
            RefreshValues();
        }
        private MBBindingList<MPTauntSlotVM> PopulateTauntSlots()
        {
            var _tauntSlots = new MBBindingList<MPTauntSlotVM>();

            var tauntBehaviour = Mission.Current.GetMissionBehavior<TauntBehavior>();

            foreach (var item in tauntBehaviour.Taunts)
            {
                _tauntSlots.Add(new MPTauntSlotVM(item.TauntId, item.TauntName,item.TauntAction, OnSlotFocused));
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

                var tauntIndex = _selectedSlot.TauntSlot;

                if (GameNetwork.IsClient)
                {
                    GameNetwork.BeginModuleEventAsClient();
                    GameNetwork.WriteMessage(new StartTaunt(tauntIndex, GameNetwork.MyPeer));
                    GameNetwork.EndModuleEventAsClient();
                }
            }

        }

        private MBBindingList<MPTauntSlotVM> _tauntSlot;
        private MPTauntSlotVM _selectedSlot;
    }
}
