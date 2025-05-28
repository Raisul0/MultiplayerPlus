using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.MountAndBlade;

namespace MultiplayerPlusCommon.ObjectClass
{
    public class MPAgent
    {
        public MPAgent(NetworkCommunicator peer) { 
            PlayerId =peer.PlayerConnectionInfo.PlayerID.ToString();
            SteamId = PlayerId.Replace("2.0.0.", "");
            UserName = peer.UserName;
            ClassCosmetics = new List<MPAgentClassCosmetic> { };
            TauntWheel = new MPTauntWheel();
            ShoutWheel = new MPShoutWheel();
            GameMVPTaunt = new MPTaunt();
            RoundMVPTaunt = new MPTaunt();
        }
        public string SteamId { get; set; }
        public string UserName { get; set; } 
        public string PlayerId { get; set; }
        public List<MPAgentClassCosmetic> ClassCosmetics { get; set; }
        public MPTauntWheel TauntWheel { get; set; }
        public MPShoutWheel ShoutWheel { get; set; }
        public MPTaunt GameMVPTaunt { get; set; }
        public MPTaunt RoundMVPTaunt { get; set; }

        public void AddClassCosmetics(MPAgentClassCosmetic c)
        {
            ClassCosmetics.Add(c);
        }
    }
    
}
