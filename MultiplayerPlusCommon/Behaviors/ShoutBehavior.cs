using System.Collections.Generic;
using MultiplayerPlusCommon.Constants;
using MultiplayerPlusCommon.ObjectClass;
using TaleWorlds.MountAndBlade;

namespace MultiplayerPlusCommon.Behaviors
{
    public class ShoutBehavior : MissionNetwork
    {
        public override MissionBehaviorType BehaviorType => MissionBehaviorType.Other;
        public List<MPShout> Shouts = new List<MPShout>();
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
            if (GameNetwork.IsClient)
            {
                Read();
            }
        }

        public void Read()
        {
            //Shouts = MPShoutWheel.Shouts;
        }


    }
}
