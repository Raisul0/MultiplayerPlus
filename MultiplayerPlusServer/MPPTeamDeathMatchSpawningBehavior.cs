﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MultiplayerPlusCommon.Constants;
using NetworkMessages.FromServer;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.ObjectSystem;
using static TaleWorlds.MountAndBlade.Agent;

namespace MultiplayerPlusServer
{
    /// <summary>
    /// Simple spawn behavior for the Battle Royale.    
    /// </summary>
    public class MPPTeamDeathMatchSpawningBehavior : SpawningBehaviorBase
    {
        public MPPTeamDeathMatchSpawningBehavior()
        {
            IsSpawningEnabled = true;
        }

        public override void Initialize(SpawnComponent spawnComponent)
        {
            base.Initialize(spawnComponent);
            base.OnAllAgentsFromPeerSpawnedFromVisuals += this.OnAllAgentsFromPeerSpawnedFromVisuals;
            if (this.GameMode.WarmupComponent == null)
            {
                this.RequestStartSpawnSession();
            }
        }

        // Token: 0x06000188 RID: 392 RVA: 0x000070A9 File Offset: 0x000052A9
        public override void Clear()
        {
            base.Clear();
            base.OnAllAgentsFromPeerSpawnedFromVisuals -= this.OnAllAgentsFromPeerSpawnedFromVisuals;
        }

        // Token: 0x06000189 RID: 393 RVA: 0x000070C3 File Offset: 0x000052C3
        public override void OnTick(float dt)
        {
            if (this.IsSpawningEnabled && this._spawnCheckTimer.Check(base.Mission.CurrentTime))
            {
                this.SpawnAgents();
            }
            base.OnTick(dt);
        }

        // Token: 0x0600018A RID: 394 RVA: 0x000070F4 File Offset: 0x000052F4
        protected override void SpawnAgents()
        {
            
            BasicCultureObject @object = MBObjectManager.Instance.GetObject<BasicCultureObject>(MultiplayerOptions.OptionType.CultureTeam1.GetStrValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions));
            BasicCultureObject object2 = MBObjectManager.Instance.GetObject<BasicCultureObject>(MultiplayerOptions.OptionType.CultureTeam2.GetStrValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions));
            foreach (NetworkCommunicator networkCommunicator in GameNetwork.NetworkPeers)
            {
                if (networkCommunicator.IsSynchronized)
                {
                    MissionPeer component = networkCommunicator.GetComponent<MissionPeer>();
                    if (component != null && component.ControlledAgent == null && !component.HasSpawnedAgentVisuals && component.Team != null && component.Team != base.Mission.SpectatorTeam && component.TeamInitialPerkInfoReady && component.SpawnTimer.Check(base.Mission.CurrentTime))
                    {
                        BasicCultureObject basicCultureObject = (component.Team.Side == BattleSideEnum.Attacker) ? @object : object2;
                        MultiplayerClassDivisions.MPHeroClass mpheroClassForPeer = MultiplayerClassDivisions.GetMPHeroClassForPeer(component, false);
                        if (mpheroClassForPeer == null || mpheroClassForPeer.TroopCasualCost > this.GameMode.GetCurrentGoldForPeer(component))
                        {
                            if (component.SelectedTroopIndex != 0)
                            {
                                component.SelectedTroopIndex = 0;
                                GameNetwork.BeginBroadcastModuleEvent();
                                GameNetwork.WriteMessage(new UpdateSelectedTroopIndex(networkCommunicator, 0));
                                GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.ExcludeOtherTeamPlayers, networkCommunicator);
                            }
                        }
                        else
                        {
                            BasicCharacterObject heroCharacter = mpheroClassForPeer.HeroCharacter;
                            Equipment equipment = heroCharacter.Equipment.Clone(false);
                            MPPerkObject.MPOnSpawnPerkHandler onSpawnPerkHandler = MPPerkObject.GetOnSpawnPerkHandler(component);
                            IEnumerable<ValueTuple<EquipmentIndex, EquipmentElement>> enumerable = (onSpawnPerkHandler != null) ? onSpawnPerkHandler.GetAlternativeEquipments(true) : null;
                            if (enumerable != null)
                            {
                                foreach (ValueTuple<EquipmentIndex, EquipmentElement> valueTuple in enumerable)
                                {
                                    equipment[valueTuple.Item1] = valueTuple.Item2;
                                }
                            }
                            this.GameMode.ChangeCurrentGoldForPeer(component, 100000000);
                            MPPlayers.EquipPlayerCustomEquipment(networkCommunicator.PlayerConnectionInfo.PlayerID.ToString(), mpheroClassForPeer.StringId,equipment);

                            AgentBuildData agentBuildData = new AgentBuildData(heroCharacter).
                                MissionPeer(component).
                                Equipment(equipment).
                                Team(component.Team).
                                TroopOrigin(new BasicBattleAgentOrigin(heroCharacter)).
                                IsFemale(component.Peer.IsFemale).
                                BodyProperties(base.GetBodyProperties(component, (component.Team == base.Mission.AttackerTeam) ? @object : object2)).
                                VisualsIndex(0).
                                ClothingColor1((component.Team == base.Mission.AttackerTeam) ? basicCultureObject.Color : basicCultureObject.ClothAlternativeColor).
                                ClothingColor2((component.Team == base.Mission.AttackerTeam) ? basicCultureObject.Color2 : basicCultureObject.ClothAlternativeColor2);
                            if (this.GameMode.ShouldSpawnVisualsForServer(networkCommunicator) && agentBuildData.AgentVisualsIndex == 0)
                            {
                                component.HasSpawnedAgentVisuals = true;
                                component.EquipmentUpdatingExpired = false;
                            }
                            this.GameMode.HandleAgentVisualSpawning(networkCommunicator, agentBuildData, 0, true);
                        }
                    }
                }
            }
        }

        // Token: 0x0600018B RID: 395 RVA: 0x000073E0 File Offset: 0x000055E0
        public override bool AllowEarlyAgentVisualsDespawning(MissionPeer lobbyPeer)
        {
            return true;
        }

        // Token: 0x0600018C RID: 396 RVA: 0x000073E4 File Offset: 0x000055E4
        public override int GetMaximumReSpawnPeriodForPeer(MissionPeer peer)
        {
            if (this.GameMode.WarmupComponent != null && this.GameMode.WarmupComponent.IsInWarmup)
            {
                return 3;
            }
            if (peer.Team != null)
            {
                if (peer.Team.Side == BattleSideEnum.Attacker)
                {
                    return MultiplayerOptions.OptionType.RespawnPeriodTeam1.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions);
                }
                if (peer.Team.Side == BattleSideEnum.Defender)
                {
                    return MultiplayerOptions.OptionType.RespawnPeriodTeam2.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions);
                }
            }
            return -1;
        }

        // Token: 0x0600018D RID: 397 RVA: 0x00007448 File Offset: 0x00005648
        protected override bool IsRoundInProgress()
        {
            return Mission.Current.CurrentState == Mission.State.Continuing;
        }

        // Token: 0x0600018E RID: 398 RVA: 0x00007458 File Offset: 0x00005658
        private new void OnAllAgentsFromPeerSpawnedFromVisuals(MissionPeer peer)
        {
            bool flag = peer.Team == base.Mission.AttackerTeam;
            Team defenderTeam = base.Mission.DefenderTeam;
            MultiplayerClassDivisions.MPHeroClass mpheroClass = MultiplayerClassDivisions.GetMPHeroClasses(MBObjectManager.Instance.GetObject<BasicCultureObject>(flag ? MultiplayerOptions.OptionType.CultureTeam1.GetStrValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions) : MultiplayerOptions.OptionType.CultureTeam2.GetStrValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions))).ElementAt(peer.SelectedTroopIndex);
            this.GameMode.ChangeCurrentGoldForPeer(peer, this.GameMode.GetCurrentGoldForPeer(peer) - mpheroClass.TroopCasualCost);

            var equipment = peer.ControlledAgent.SpawnEquipment;

            MPPlayers.EquipPlayerCustomEquipment(peer.GetNetworkPeer().PlayerConnectionInfo.PlayerID.ToString(), mpheroClass.StringId, equipment);

            peer.ControlledAgent.UpdateSpawnEquipmentAndRefreshVisuals(equipment);
        }
    }
}
