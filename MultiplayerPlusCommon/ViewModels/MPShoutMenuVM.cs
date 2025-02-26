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

        public void PopulateShoutSlots(List<MPShout> shouts)
        {
            var _shoutSlots = new MBBindingList<MPShoutSlotVM>();
            foreach (var shout in shouts)
            {
                _shoutSlots.Add(new MPShoutSlotVM(shout.ShoutId, shout.ShoutName, shout.VoiceType, OnSlotFocused));
            }
            Populated = true;
            ShoutSlots = _shoutSlots;
        }

        private void OnSlotFocused(MPShoutSlotVM shoutSlot)
        {
            _selectedSlot = shoutSlot;
        }

        public void ExecuteShout()
        {
            if (_selectedSlot != null)
            {
                var shoutId = _selectedSlot.ShoutId;

                if (GameNetwork.IsClient)
                {
                    GameNetwork.BeginModuleEventAsClient();
                    GameNetwork.WriteMessage(new StartShout(shoutId));
                    GameNetwork.EndModuleEventAsClient();
                }
            }

        }

        public bool Populated { get; set; }
        private MBBindingList<MPShoutSlotVM> _shoutSlots;
        private MPShoutSlotVM _selectedSlot;
    }
}
