using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.View;

namespace MultiplayerPlusClient
{
    public class MPPlayerCosmeticsEquip : MissionNetwork
    {
        public override void OnBehaviorInitialize()
        {
            base.OnBehaviorInitialize();
            var _peer = GameNetwork.MyPeer?.GetComponent<MissionPeer>();
            if(_peer != null)
            {
                if(_peer.ControlledAgent != null)
                {
                    var myAgent = _peer.ControlledAgent;

                    Equipment equipment = myAgent.SpawnEquipment;
                    ItemObject item = equipment[10].Item;

                    Monster monster = item.HorseComponent.Monster;
                    AgentVisualsData agentVisualsData = new AgentVisualsData().Equipment(equipment).Scale(item.ScaleFactor).Frame(MatrixFrame.Identity)
                    .ActionSet(MBGlobals.GetActionSet(monster.ActionSetCode))
                    .Scene(Mission.Current.Scene)
                    .Monster(monster)
                    .PrepareImmediately(prepareImmediately: false)
                    .MountCreationKey(MountCreationKey.GetRandomMountKeyString(item, MBRandom.RandomInt()));


                    IAgentVisual agentVisual = Mission.Current.AgentVisualCreator.Create(agentVisualsData, "Agent " + myAgent.Character.StringId + " mount", needBatchedVersionForWeaponMeshes: true, forceUseFaceCache: false);
                    ActionIndexCache actionIndexCache3 = agentVisual.GetVisuals().GetSkeleton().GetActionAtChannel(0);

                    float parameter = 0.1f + MBRandom.RandomFloat * 0.8f;
                    var frame = myAgent.Frame;

                    Monster baseMonsterFromRace = TaleWorlds.Core.FaceGen.GetBaseMonsterFromRace(myAgent.Character.Race);
                    IAgentVisual agentVisual2 = Mission.Current.AgentVisualCreator.Create(new AgentVisualsData().Equipment(equipment).BodyProperties(myAgent.BodyPropertiesValue).Frame(myAgent.Frame)
                        .ActionSet(MBActionSet.GetActionSet(baseMonsterFromRace.ActionSetCode))
                        .Scene(Mission.Current.Scene)
                        .Monster(baseMonsterFromRace)
                        .PrepareImmediately(prepareImmediately: false)
                        .UseMorphAnims(useMorphAnims: true)
                        .SkeletonType(myAgent.IsFemale ? SkeletonType.Female : SkeletonType.Male)
                        .ClothColor1(myAgent.ClothingColor1)
                        .ClothColor2(myAgent.ClothingColor2)
                        .AddColorRandomness(false)
                        .ActionCode(actionIndexCache3), "Mission::SpawnAgentVisuals", needBatchedVersionForWeaponMeshes: true, forceUseFaceCache: false);
                    agentVisual2.SetAction(actionIndexCache3);
                    agentVisual2.GetVisuals().GetSkeleton().SetAnimationParameterAtChannel(0, parameter);
                    agentVisual2.GetVisuals().GetSkeleton().TickAnimationsAndForceUpdate(0.1f, frame, tickAnimsForChildren: true);
                    agentVisual2.GetVisuals().SetFrame(ref frame);
                    agentVisual2.SetCharacterObjectID(myAgent.Character.StringId);
                    equipment.GetInitialWeaponIndicesToEquip(out var mainHandWeaponIndex, out var offHandWeaponIndex, out var isMainHandNotUsableWithOneHand);
                    if (isMainHandNotUsableWithOneHand)
                    {
                        offHandWeaponIndex = EquipmentIndex.None;
                    }

                    agentVisual2.GetVisuals().SetWieldedWeaponIndices((int)mainHandWeaponIndex, (int)offHandWeaponIndex);
                    PeerVisualsHolder peerVisualsHolder = new PeerVisualsHolder(missionPeer, buildData.AgentVisualsIndex, agentVisual2, agentVisual);
                    missionPeer.OnVisualsSpawned(peerVisualsHolder, peerVisualsHolder.VisualsIndex);
                    if (missionPeer.IsMine && buildData.AgentVisualsIndex == 0)
                    {
                        this.OnMyAgentVisualSpawned?.Invoke();
                    }
                }

            }
        }
    }
}
