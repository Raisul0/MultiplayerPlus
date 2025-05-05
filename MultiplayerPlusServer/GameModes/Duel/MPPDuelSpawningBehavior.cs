using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MultiplayerPlusCommon.Constants;
using MultiplayerPlusCommon.NetworkMessages.FromServer;
using NetworkMessages.FromServer;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.MissionRepresentatives;
using TaleWorlds.ObjectSystem;

namespace MultiplayerPlusServer.GameModes.Duel
{
    /// <summary>
    /// Simple spawn behavior for the Battle Royale.    
    /// </summary>
    public class MPPDuelSpawningBehavior : SpawningBehaviorBase
    {
        public override void Initialize(SpawnComponent spawnComponent)
        {
            base.Initialize(spawnComponent);
            base.OnPeerSpawnedFromVisuals += OnPeerSpawned;
            if (GameMode.WarmupComponent == null)
            {
                RequestStartSpawnSession();
            }

            base.OnAllAgentsFromPeerSpawnedFromVisuals += OnAllAgentsFromPeerSpawnedFromVisuals;
        }

        public override void Clear()
        {
            base.Clear();
            base.OnPeerSpawnedFromVisuals -= OnPeerSpawned;
            base.OnAllAgentsFromPeerSpawnedFromVisuals -= OnAllAgentsFromPeerSpawnedFromVisuals;
        }

        public override void OnTick(float dt)
        {
            if (IsSpawningEnabled && _spawnCheckTimer.Check(Mission.Current.CurrentTime))
            {
                SpawnAgents();
            }

            base.OnTick(dt);
        }

        protected override void SpawnAgents()
        {
            foreach (NetworkCommunicator networkPeer in GameNetwork.NetworkPeers)
            {
                if (!networkPeer.IsSynchronized)
                {
                    continue;
                }

                MissionPeer component = networkPeer.GetComponent<MissionPeer>();
                if (!(component.Representative is DuelMissionRepresentative) || !networkPeer.IsSynchronized || component.ControlledAgent != null || component.HasSpawnedAgentVisuals || component.Team == null || component.Team == base.Mission.SpectatorTeam || !component.TeamInitialPerkInfoReady || component.Culture == null || !component.SpawnTimer.Check(Mission.Current.CurrentTime))
                {
                    continue;
                }

                MultiplayerClassDivisions.MPHeroClass mPHeroClassForPeer = MultiplayerClassDivisions.GetMPHeroClassForPeer(component);
                if (mPHeroClassForPeer == null)
                {
                    if (component.SelectedTroopIndex != 0)
                    {
                        component.SelectedTroopIndex = 0;
                        GameNetwork.BeginBroadcastModuleEvent();
                        GameNetwork.WriteMessage(new UpdateSelectedTroopIndex(networkPeer, 0));
                        GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.ExcludeOtherTeamPlayers, networkPeer);
                    }

                    continue;
                }

                BasicCharacterObject heroCharacter = mPHeroClassForPeer.HeroCharacter;
                Equipment equipment = heroCharacter.Equipment.Clone();
                IEnumerable<(EquipmentIndex, EquipmentElement)> enumerable = MPPerkObject.GetOnSpawnPerkHandler(component)?.GetAlternativeEquipments(isPlayer: true);
                if (enumerable != null)
                {
                    foreach (var item in enumerable)
                    {
                        equipment[item.Item1] = item.Item2;
                    }
                }


                AgentBuildData agentBuildData = new AgentBuildData(heroCharacter).MissionPeer(component).Equipment(equipment).Team(component.Team)
                    .TroopOrigin(new BasicBattleAgentOrigin(heroCharacter))
                    .IsFemale(component.Peer.IsFemale)
                    .BodyProperties(GetBodyProperties(component, component.Culture))
                    .VisualsIndex(0)
                    .ClothingColor1(component.Culture.Color)
                    .ClothingColor2(component.Culture.Color2);

                MPPlayers.EquipPlayerCustomEquipment(networkPeer.PlayerConnectionInfo.PlayerID.ToString(), mPHeroClassForPeer.StringId, equipment);
                agentBuildData.Equipment(equipment);

                if (GameMode.ShouldSpawnVisualsForServer(networkPeer) && agentBuildData.AgentVisualsIndex == 0)
                {
                    component.HasSpawnedAgentVisuals = true;
                    component.EquipmentUpdatingExpired = false;
                }

                GameMode.HandleAgentVisualSpawning(networkPeer, agentBuildData);
            }
        }

        private new void OnAllAgentsFromPeerSpawnedFromVisuals(MissionPeer peer)
        {
            bool flag = peer.Team == base.Mission.AttackerTeam;
            _ = base.Mission.DefenderTeam;
            MultiplayerClassDivisions.MPHeroClass mPHeroClass = MultiplayerClassDivisions.GetMPHeroClasses(MBObjectManager.Instance.GetObject<BasicCultureObject>(flag ? MultiplayerOptions.OptionType.CultureTeam1.GetStrValue() : MultiplayerOptions.OptionType.CultureTeam2.GetStrValue())).ElementAt(peer.SelectedTroopIndex);
            var equipment = peer.ControlledAgent.SpawnEquipment;
            MPPlayers.EquipPlayerCustomEquipment(peer.GetNetworkPeer().PlayerConnectionInfo.PlayerID.ToString(), mPHeroClass.StringId, equipment);

            peer.ControlledAgent.UpdateSpawnEquipmentAndRefreshVisuals(equipment);
        }

        public override bool AllowEarlyAgentVisualsDespawning(MissionPeer missionPeer)
        {
            return true;
        }

        protected override bool IsRoundInProgress()
        {
            return Mission.Current.CurrentState == Mission.State.Continuing;
        }

        private void OnPeerSpawned(MissionPeer peer)
        {
            _ = peer.Representative;
        }
    }
}
