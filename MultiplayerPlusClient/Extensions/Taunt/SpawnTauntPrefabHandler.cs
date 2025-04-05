using Helpers;
using MultiplayerPlusClient.CustomViews;
using MultiplayerPlusCommon.Helpers;
using MultiplayerPlusCommon.NetworkMessages.FromServer;
using NetworkMessages.FromServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Diamond;

namespace MultiplayerPlusClient.Extensions.Taunt
{
    public class SpawnTauntPrefabHandler : IHandlerRegister
    {
        public void Register(GameNetwork.NetworkMessageHandlerRegisterer reg)
        {
            reg.Register<SpawnTauntPrefab>(SpawnTauntPrefabWithSound);
        }

        public void SpawnTauntPrefabWithSound(SpawnTauntPrefab baseMessage)
        {
            var prefabName = baseMessage.PrefabName; //"musical_instrument_flute";
            var soundEventName = baseMessage.SoundEventName; //"taunt/sound/violin";
            var frame = baseMessage.SpawnLocation;

            if (!string.IsNullOrEmpty(prefabName))
            {
                GameEntity tauntPrefab = GameEntity.Instantiate(Mission.Current.Scene, prefabName, frame);
                SetPrefabPosition(tauntPrefab);
                var result = RemovePrefab(tauntPrefab, 10);
            }

            if (!string.IsNullOrEmpty(soundEventName))
            {
                var _soundIndex = SoundEvent.GetEventIdFromString(soundEventName);
                var soundEvent = SoundEvent.CreateEvent(_soundIndex, Mission.Current.Scene);
                soundEvent.SetPosition(frame.origin);
                soundEvent.Play();
            }
        }

        public async Task RemovePrefab(GameEntity tauntPrefab, int tauntDuration)
        {
            await Task.Delay(tauntDuration*1000);
            tauntPrefab.Remove(0);
        }

        public void SetPrefabPosition(GameEntity tauntPrefab)
        {
            var vec = Agent.Main.GetEyeGlobalPosition();
            //var mat = new Mat3();
            //mat.s = new Vec3(0.5f);
            var newFrame = new MatrixFrame(Mat3.Identity, vec); /*tauntPrefab.GetGlobalFrame();*/
            var agentBodyProperties = Agent.Main.BodyPropertiesValue;


            

            
            float height = (float)Convert.ToDouble((agentBodyProperties.KeyPart8 >> 19) & 0x3F);
            var elevateTo = height * 0.065f;
            InformationManager.DisplayMessage( new InformationMessage("Player height : " + height.ToString()));
            InformationManager.DisplayMessage(new InformationMessage("Elevation Height : " + elevateTo.ToString()));
            //newFrame.Elevate(elevateTo);
            //newFrame.Strafe(-0.03f);
            //newFrame.Advance(0.6f);
            //newFrame.rotation.RotateAboutUp(1f);
            //newFrame.rotation.RotateAboutForward(1.6f);
            //newFrame.rotation.RotateAboutSide(1.6f);

            //newFrame
            //newFrame.rotation.

            //newFrame.rotation.ro

            tauntPrefab.SetGlobalFrame(newFrame);
        }

    }
}
