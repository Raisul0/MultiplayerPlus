using Helpers;
using MultiplayerPlusClient.CustomViews;
using MultiplayerPlusCommon.Helpers;
using MultiplayerPlusCommon.NetworkMessages.FromServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

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
            var prefabName = "musical_instrument_flute"; //baseMessage.PrefabName;
            var soundEventName = "taunt/sound/violin";  //baseMessage.SoundEventName; 
            var frame = baseMessage.SpawnLocation;

            if (!string.IsNullOrEmpty(prefabName))
            {
                GameEntity tauntPrefab = GameEntity.Instantiate(Mission.Current.Scene, prefabName, frame);
                SetPrefabPosition(tauntPrefab);
                var result = RemovePrefab(tauntPrefab,10);
            }
            
            if(!string.IsNullOrEmpty(soundEventName))
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
            var newFrame = tauntPrefab.GetGlobalFrame();
            newFrame.Elevate(1.5f);
            newFrame.Advance(0.3f);
            newFrame.rotation.RotateAboutForward(0.5f);

            tauntPrefab.SetGlobalFrame(newFrame);
        }

    }
}
