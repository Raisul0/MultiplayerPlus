using MultiplayerPlusCommon.Constants;
using MultiplayerPlusCommon.NetworkMessages.FromServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Core;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.MissionRepresentatives;

namespace MultiplayerPlusServer.GameModes.Common
{
    public class MPPLobbyComponent : MissionLobbyComponent
    {
        private MissionMultiplayerGameModeBase _gameMode;
        public override void OnBehaviorInitialize()
        {
            this._gameMode = Mission.Current.GetMissionBehavior<MissionMultiplayerGameModeBase>();
            base.OnBehaviorInitialize();
            base.OnPostMatchEnded += OnPostMatchEnd;
        }
        private void OnPostMatchEnd()
        {
            var firstPlayerTaunt = "";
            var secondPlayerTaunt = "";
            var thirdPlayerTaunt = "";

            MissionScoreboardComponent missionBehavior = Mission.Current.GetMissionBehavior<MissionScoreboardComponent>();
            List<MissionPeerWithUpdatedScore> list = new List<MissionPeerWithUpdatedScore>();
            foreach (MissionScoreboardComponent.MissionScoreboardSide missionScoreboardSide in from s in missionBehavior.Sides
                                                                                               where s != null && s.Side != BattleSideEnum.None
                                                                                               select s)
            {
                foreach (MissionPeer item in missionScoreboardSide.Players)
                {
                    list.Add(new MissionPeerWithUpdatedScore() { Peer=item,Score=GetPeerScore(item)});
                }
            }
            list = list.OrderByDescending(x => x.Score).ToList();
                
            if (list.Count > 0)
            {
                MissionPeer peer = list.ElementAt(0).Peer;
                var playerId = peer.GetNetworkPeer().PlayerConnectionInfo.PlayerID.ToString();
                firstPlayerTaunt = MPPlayers.GetMatchMVPTauntByPlayerId(playerId);
            }
            if (list.Count > 1)
            {
                MissionPeer peer2 = list.ElementAt(1).Peer;
                var playerId = peer2.GetNetworkPeer().PlayerConnectionInfo.PlayerID.ToString();
                secondPlayerTaunt = MPPlayers.GetMatchMVPTauntByPlayerId(playerId);
            }
            if (list.Count > 2)
            {
                MissionPeer peer3 = list.ElementAt(2).Peer;
                var playerId = peer3.GetNetworkPeer().PlayerConnectionInfo.PlayerID.ToString();
                thirdPlayerTaunt = MPPlayers.GetMatchMVPTauntByPlayerId(playerId);
            }

            if (GameNetwork.IsServer)
            {
                GameNetwork.BeginBroadcastModuleEvent();
                GameNetwork.WriteMessage(new SetMatchMVPTaunt(firstPlayerTaunt, secondPlayerTaunt, thirdPlayerTaunt));
                GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.None);
            }
        }

        private int GetPeerScore(MissionPeer peer)
        {
            if (peer == null)
            {
                return 0;
            }
            if (this._gameMode.GetMissionType() != MultiplayerGameType.Duel)
            {
                return peer.Score;
            }
            DuelMissionRepresentative component = peer.GetComponent<DuelMissionRepresentative>();
            if (component == null)
            {
                return 0;
            }
            return component.Score;
        }

        public class MissionPeerWithUpdatedScore
        {
            public MissionPeer Peer { get; set; }
            public int Score { get; set; }
        }
    }
}
