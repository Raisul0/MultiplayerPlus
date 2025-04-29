using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.MountAndBlade.Network.Messages;
using TaleWorlds.MountAndBlade;
using MultiplayerPlusCommon.GameModes.Skirmish;
using MultiplayerPlusCommon.Constants;

namespace MultiplayerPlusCommon.NetworkMessages.FromServer
{
    [DefineGameNetworkMessageTypeForMod(GameNetworkMessageSendType.FromServer)]
    public sealed class MPPSkirmishWarmupStateChange : GameNetworkMessage
    {
        public MPPWarmupStates WarmupState { get; private set; }

        public float StateStartTimeInSeconds { get; private set; }

        public MPPSkirmishWarmupStateChange(MPPWarmupStates warmupState, long stateStartTimeInTicks)
        {
            WarmupState = warmupState;
            StateStartTimeInSeconds = (float)stateStartTimeInTicks / 1E+07f;
        }

        public MPPSkirmishWarmupStateChange()
        {
        }

        protected override void OnWrite()
        {
            GameNetworkMessage.WriteIntToPacket((int)WarmupState, CompressionMission.MissionRoundStateCompressionInfo);
            GameNetworkMessage.WriteFloatToPacket(StateStartTimeInSeconds, CompressionMatchmaker.MissionTimeCompressionInfo);
        }

        protected override bool OnRead()
        {
            bool bufferReadValid = true;
            WarmupState = (MPPWarmupStates)GameNetworkMessage.ReadIntFromPacket(CompressionMission.MissionRoundStateCompressionInfo, ref bufferReadValid);
            StateStartTimeInSeconds = GameNetworkMessage.ReadFloatFromPacket(CompressionMatchmaker.MissionTimeCompressionInfo, ref bufferReadValid);
            return bufferReadValid;
        }

        protected override MultiplayerMessageFilter OnGetLogFilter()
        {
            return MultiplayerMessageFilter.MissionDetailed;
        }

        protected override string OnGetLogFormat()
        {
            return "Warmup state set to " + WarmupState;
        }
    }
}
