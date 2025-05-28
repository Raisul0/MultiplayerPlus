﻿using NetworkMessages.FromServer;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace MultiplayerPlusCommon.GameModes.Duel
{
    public class AdimiToolsTeleportDoors : UsableMissionObject
    {
        private GameEntity _targetPoint;

        public override void OnUse(Agent userAgent)
        {
            if (userAgent != null && userAgent.IsActive() && userAgent.IsHuman)
            {
                MissionPeer missionPeer = userAgent.MissionPeer;
                NetworkCommunicator networkPeer = missionPeer.GetNetworkPeer();
                if (missionPeer == null || networkPeer == null)
                {
                    return;
                }

                if (_targetPoint != null)
                {
                    userAgent.TeleportToPosition(_targetPoint.GetGlobalFrame().origin);

                    WorldPosition worldPosition = new WorldPosition(Mission.Current.Scene, _targetPoint.GetGlobalFrame().origin);
                    WorldFrame worldFrame = new WorldFrame(_targetPoint.GetGlobalFrame().rotation, worldPosition);
                    Vec2 vec = worldFrame.Rotation.f.AsVec2.Normalized();
                    GameNetwork.BeginBroadcastModuleEvent();
                    GameNetwork.WriteMessage(new AgentTeleportToFrame(userAgent.Index, worldFrame.Origin.GetGroundVec3(), vec));
                    GameNetwork.EndBroadcastModuleEvent(GameNetwork.EventBroadcastFlags.AddToMissionRecord, null);
                }
            }
        }

        public void SetupDoor(GameEntity targetPoint)
        {
            _targetPoint = targetPoint;
        }

        public override string GetDescriptionText(GameEntity gameEntity)
        {
            return "Teleport";
        }
    }
}


