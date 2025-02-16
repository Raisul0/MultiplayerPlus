using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiplayerPlusCommon.Behaviors;
using MultiplayerPlusCommon.NetworkMessages.FromClient;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace MultiplayerPlusCommon.ViewModels
{
    public class MPShoutMenuVM : ViewModel
    {
        [DataSourceProperty]
        public MBBindingList<MPShoutSlotVM> ShoutSlots
        {
            get
            {
                return _shoutSlots;
            }
            set
            {
                if (value != _shoutSlots)
                {
                    _shoutSlots = value;
                    OnPropertyChangedWithValue(value, "ShoutSlots");
                }
            }
        }


        public MPShoutMenuVM()
        {
            ShoutSlots = PopulateShoutSlots();
            RefreshValues();
        }
        private MBBindingList<MPShoutSlotVM> PopulateShoutSlots()
        {
            var _shoutSlots = new MBBindingList<MPShoutSlotVM>();

            var shoutBehavior = Mission.Current.GetMissionBehavior<ShoutBehavior>();

            foreach (var item in shoutBehavior.Shouts)
            {
                _shoutSlots.Add(new MPShoutSlotVM(item.ShoutIndex, item.ShoutName, OnSlotFocused));
            }

            return _shoutSlots;
        }

        private void OnSlotFocused(MPShoutSlotVM shoutSlot)
        {
            _selectedSlot = shoutSlot;
        }

        public void ExecuteShout()
        {
            if (_selectedSlot != null)
            {

                var shoutIndex = _selectedSlot.ShoutIndex;

                if (GameNetwork.IsClient)
                {
                    GameNetwork.BeginModuleEventAsClient();
                    GameNetwork.WriteMessage(new StartShout(shoutIndex,Agent.Main.Index, GameNetwork.MyPeer));
                    GameNetwork.EndModuleEventAsClient();
                }
            }

        }

        private MBBindingList<MPShoutSlotVM> _shoutSlots;
        private MPShoutSlotVM _selectedSlot;
    }
}
