using MultiplayerPlusCommon.Constants;
using NetworkMessages.FromServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.ObjectSystem;

namespace MultiplayerPlusServer.GameModes.Warmup
{
    public class MPPWarmupSpawningBehavior : SpawningBehaviorBase
    {
        public MPPWarmupSpawningBehavior()
        {
            IsSpawningEnabled = true;
        }

        public override void Initialize(SpawnComponent spawnComponent)
        {
            base.Initialize(spawnComponent);

            base.OnAllAgentsFromPeerSpawnedFromVisuals += OnAllAgentsFromPeerSpawnedFromVisuals;
        }
        public override void OnTick(float dt)
        {
            if (IsSpawningEnabled && _spawnCheckTimer.Check(base.Mission.CurrentTime))
            {
                SpawnAgents();
            }

            base.OnTick(dt);
        }

        protected override void SpawnAgents()
        {
            BasicCultureObject @object = MBObjectManager.Instance.GetObject<BasicCultureObject>(MultiplayerOptions.OptionType.CultureTeam1.GetStrValue());
            BasicCultureObject object2 = MBObjectManager.Instance.GetObject<BasicCultureObject>(MultiplayerOptions.OptionType.CultureTeam2.GetStrValue());
            foreach (NetworkCommunicator networkPeer in GameNetwork.NetworkPeers)
            {
                if (!networkPeer.IsSynchronized)
                {
                    continue;
                }

                MissionPeer component = networkPeer.GetComponent<MissionPeer>();
                if (component == null || component.ControlledAgent != null || component.HasSpawnedAgentVisuals || component.Team == null || component.Team == base.Mission.SpectatorTeam || !component.TeamInitialPerkInfoReady || !component.SpawnTimer.Check(base.Mission.CurrentTime))
                {
                    continue;
                }

                IAgentVisual agentVisual = null;
                BasicCultureObject basicCultureObject = ((component.Team.Side == BattleSideEnum.Attacker) ? @object : object2);
                int selectedTroopIndex = component.SelectedTroopIndex;
                IEnumerable<MultiplayerClassDivisions.MPHeroClass> mPHeroClasses = MultiplayerClassDivisions.GetMPHeroClasses(basicCultureObject);
                MultiplayerClassDivisions.MPHeroClass mPHeroClass = ((selectedTroopIndex < 0) ? null : mPHeroClasses.ElementAt(selectedTroopIndex));
                if (mPHeroClass == null && selectedTroopIndex < 0)
                {
                    mPHeroClass = mPHeroClasses.First();
                    selectedTroopIndex = 0;
                }

                BasicCharacterObject heroCharacter = mPHeroClass.HeroCharacter;
                Equipment equipment = heroCharacter.Equipment.Clone();
                IEnumerable<(EquipmentIndex, EquipmentElement)> enumerable = MPPerkObject.GetOnSpawnPerkHandler(component)?.GetAlternativeEquipments(isPlayer: true);
                if (enumerable != null)
                {
                    foreach (var item in enumerable)
                    {
                        equipment[item.Item1] = item.Item2;
                    }
                }

                MatrixFrame matrixFrame;
                if (agentVisual == null)
                {
                    matrixFrame = SpawnComponent.GetSpawnFrame(component.Team, heroCharacter.Equipment.Horse.Item != null);
                }
                else
                {
                    matrixFrame = agentVisual.GetFrame();
                    matrixFrame.rotation.MakeUnit();
                }

                MPPlayers.EquipPlayerCustomEquipment(networkPeer.PlayerConnectionInfo.PlayerID.ToString(), mPHeroClass.StringId, equipment);
                AgentBuildData agentBuildData = new AgentBuildData(heroCharacter).MissionPeer(component).Equipment(equipment).Team(component.Team)
                    .TroopOrigin(new BasicBattleAgentOrigin(heroCharacter))
                    .InitialPosition(in matrixFrame.origin);
                Vec2 direction = matrixFrame.rotation.f.AsVec2.Normalized();
                AgentBuildData agentBuildData2 = agentBuildData.InitialDirection(in direction).IsFemale(component.Peer.IsFemale).BodyProperties(GetBodyProperties(component, basicCultureObject))
                    .VisualsIndex(0)
                    .ClothingColor1((component.Team == base.Mission.AttackerTeam) ? basicCultureObject.Color : basicCultureObject.ClothAlternativeColor)
                    .ClothingColor2((component.Team == base.Mission.AttackerTeam) ? basicCultureObject.Color2 : basicCultureObject.ClothAlternativeColor2);
                if (GameMode.ShouldSpawnVisualsForServer(networkPeer) && agentBuildData2.AgentVisualsIndex == 0)
                {
                    component.HasSpawnedAgentVisuals = true;
                    component.EquipmentUpdatingExpired = false;
                }

                GameMode.HandleAgentVisualSpawning(networkPeer, agentBuildData2);
            }
        }

        public override bool AllowEarlyAgentVisualsDespawning(MissionPeer lobbyPeer)
        {
            return true;
        }

        public override int GetMaximumReSpawnPeriodForPeer(MissionPeer peer)
        {
            return 3;
        }

        protected override bool IsRoundInProgress()
        {
            return Mission.Current.CurrentState == Mission.State.Continuing;
        }

        public override void Clear()
        {
            base.Clear();
            RequestStopSpawnSession();
            base.OnAllAgentsFromPeerSpawnedFromVisuals -= OnAllAgentsFromPeerSpawnedFromVisuals;
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
    }
}
