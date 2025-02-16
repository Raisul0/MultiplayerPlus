using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using MultiplayerPlusCommon.Constants;
using MultiplayerPlusCommon.ObjectClasses;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using static TaleWorlds.MountAndBlade.Diamond.TauntUsageManager;

namespace MultiplayerPlusCommon.Behaviors
{
    public class TauntBehavior : MissionNetwork
    {
        public override MissionBehaviorType BehaviorType => MissionBehaviorType.Other;
        public List<MPTaunt> Taunts = new List<MPTaunt>();
        public override void OnBehaviorInitialize()
        {
            base.OnBehaviorInitialize();
            if (GameNetwork.IsClient)
            {
                Read();
            }
        }

        protected override void HandleNewClientAfterSynchronized(NetworkCommunicator networkPeer)
        {
            if (GameNetwork.IsClient)
            {
                Read();
            }
        }

        public void RefreshTaunts()
        {
            if(GameNetwork.IsClient)
            {
                Read();
            }
        }

        public void Read()
        {
            Taunts = MPTauntWheel.Taunts;
        }


    }
}
