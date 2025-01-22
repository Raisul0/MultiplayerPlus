using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class MPPTeamDeathMatchSpawningBehavior : TeamDeathmatchSpawningBehavior
    {
        public MPPTeamDeathMatchSpawningBehavior()
        {
            IsSpawningEnabled = true;
        }

        public override void Initialize(SpawnComponent spawnComponent)
        {
            base.Initialize(spawnComponent);

            base.OnAllAgentsFromPeerSpawnedFromVisuals += OnAllAgentsFromPeerSpawnedFromVisuals;
        }

        public override void Clear()
        {
            base.Clear();

            base.OnAllAgentsFromPeerSpawnedFromVisuals -= OnAllAgentsFromPeerSpawnedFromVisuals;
        }

        public override void OnTick(float dt)
        {
            if (IsSpawningEnabled && _spawnCheckTimer.Check(Mission.CurrentTime))
            {
                SpawnAgents();
            }

            base.OnTick(dt);
        }

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
                            AgentBuildData agentBuildData = new AgentBuildData(heroCharacter).MissionPeer(component).Equipment(equipment).Team(component.Team).TroopOrigin(new BasicBattleAgentOrigin(heroCharacter)).IsFemale(component.Peer.IsFemale).BodyProperties(base.GetBodyProperties(component, (component.Team == base.Mission.AttackerTeam) ? @object : object2)).VisualsIndex(0).ClothingColor1((component.Team == base.Mission.AttackerTeam) ? basicCultureObject.Color : basicCultureObject.ClothAlternativeColor).ClothingColor2((component.Team == base.Mission.AttackerTeam) ? basicCultureObject.Color2 : basicCultureObject.ClothAlternativeColor2);
                            if (this.GameMode.ShouldSpawnVisualsForServer(networkCommunicator))
                            {
                                //base.AgentVisualSpawnComponent.SpawnAgentVisualsForPeer(component, agentBuildData, component.SelectedTroopIndex, false, 0);
                                if (agentBuildData.AgentVisualsIndex == 0)
                                {
                                    component.HasSpawnedAgentVisuals = true;
                                    component.EquipmentUpdatingExpired = false;
                                }
                            }
                            this.GameMode.HandleAgentVisualSpawning(networkCommunicator, agentBuildData, 0, true);
                        }
                    }
                }
            }
        }

        public override bool AllowEarlyAgentVisualsDespawning(MissionPeer lobbyPeer)
        {
            return true;
        }

        // TODO_KORNEEL GetRespawnPeriod will never be used, even in Duel, TDM, FFA, because there is always at least an attacker team
        public override int GetMaximumReSpawnPeriodForPeer(MissionPeer peer)
        {
            if (GameMode.WarmupComponent != null && GameMode.WarmupComponent.IsInWarmup)
            {
                return MultiplayerWarmupComponent.RespawnPeriodInWarmup;
            }
            else
            {
                if (peer.Team != null)
                {
                    if (peer.Team.Side == BattleSideEnum.Attacker)
                    {
                        return MultiplayerOptions.OptionType.RespawnPeriodTeam1.GetIntValue();
                    }
                    else if (peer.Team.Side == BattleSideEnum.Defender)
                    {
                        return MultiplayerOptions.OptionType.RespawnPeriodTeam2.GetIntValue();
                    }
                }

                return -1;
            }
        }

        protected override bool IsRoundInProgress()
        {
            return Mission.Current.CurrentState == Mission.State.Continuing;
        }

        private new void OnAllAgentsFromPeerSpawnedFromVisuals(MissionPeer peer)
        {
            Team team = peer.Team;
            bool isTeamOne = team == Mission.AttackerTeam;
            bool isTeamTwo = team == Mission.DefenderTeam;

            var teamCulture = MBObjectManager.Instance.GetObject<BasicCultureObject>(isTeamOne
                ? (MultiplayerOptions.OptionType.CultureTeam1.GetStrValue())
                : (MultiplayerOptions.OptionType.CultureTeam2.GetStrValue()));

            var mpClass = MultiplayerClassDivisions.GetMPHeroClasses(teamCulture).ElementAt(peer.SelectedTroopIndex);

            GameMode.ChangeCurrentGoldForPeer(peer, GameMode.GetCurrentGoldForPeer(peer) - mpClass.TroopCasualCost);
        }
    }
}
