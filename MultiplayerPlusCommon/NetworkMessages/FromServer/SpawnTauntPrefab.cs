using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;
using TaleWorlds.ObjectSystem;

namespace MultiplayerPlusCommon.NetworkMessages.FromServer
{
    [DefineGameNetworkMessageTypeForMod(GameNetworkMessageSendType.FromServer)]
    public sealed class SpawnTauntPrefab : GameNetworkMessage
    {
        public string PrefabName { get; set; }
        public string SoundEventName { get; set; }
        
        public MatrixFrame SpawnLocation { get; set; }
        public SpawnTauntPrefab()
        {

        }
        public SpawnTauntPrefab(string prefabName,string soundEventName, MatrixFrame spawnLocation) {
            PrefabName = prefabName;
            SoundEventName = soundEventName;
            SpawnLocation = spawnLocation;
        }
        protected override MultiplayerMessageFilter OnGetLogFilter()
        {
            return MultiplayerMessageFilter.General;
        }

        protected override string OnGetLogFormat()
        {
            return "SpawnTauntPrefab  : PrefabName : "+ PrefabName + " , SoundEventName :" + SoundEventName;
        }

        protected override bool OnRead()
        {
            bool result = true;
            this.PrefabName = ReadStringFromPacket(ref result);
            this.SoundEventName = ReadStringFromPacket(ref result);
            this.SpawnLocation = ReadMatrixFrameFromPacket(ref result);
            return result;
        }

        protected override void OnWrite()
        {
            WriteStringToPacket(this.PrefabName);
            WriteStringToPacket(this.SoundEventName);
            WriteMatrixFrameToPacket(this.SpawnLocation);
        }
    }
}
