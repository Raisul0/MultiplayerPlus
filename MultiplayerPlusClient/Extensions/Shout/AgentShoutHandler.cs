using MultiplayerPlusCommon.Helpers;
using MultiplayerPlusCommon.NetworkMessages.FromServer;
using NetworkMessages.FromServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.GauntletUI;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using static TaleWorlds.MountAndBlade.SkinVoiceManager;

namespace MultiplayerPlusClient.Extensions.Shout
{
    public class AgentShoutHandler : IHandlerRegister
    {
        public void Register(GameNetwork.NetworkMessageHandlerRegisterer reg)
        {
            reg.Register<AgentShout>(Shout);
        }

        public void Shout(AgentShout baseMessage)
        {
            Agent agentFromIndex = Mission.MissionNetworkHelper.GetAgentFromIndex(baseMessage.AgentIndex, false);
            var voiceType = "CustomShout";


            SoundManager.Reset();

            var frame = SoundManager.GetListenerFrame();

            


        }
       
    }
}
