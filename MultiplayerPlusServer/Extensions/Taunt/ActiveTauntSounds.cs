using MultiplayerPlusCommon.ObjectClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace MultiplayerPlusServer.Extensions.Taunt
{
    public static class ActiveTauntSounds
    {
        public static List<SoundLocation> ActiveSounds = new List<SoundLocation>  {
        };

        public static void AddSound(SoundEvent soundEvent,Vec3 position)
        {
            ActiveSounds.Add(new SoundLocation(position,soundEvent));
        }

        public static void RemoveSound(SoundEvent soundEvent)
        {
            var sound = ActiveSounds.FirstOrDefault(x=>x.SoundEvent == soundEvent);
            if(sound != null)
            {
                ActiveSounds.Remove(sound);
            }
            
        }

        public static void StopSoundsCloseBy(Vec3 position)
        {
            foreach (var item in ActiveSounds)
            {
                var distance = item.Position.Distance(position);
                if(distance > 0)
                {
                    item.SoundEvent.Stop();
                }
            }
        }
    }

    public class SoundLocation
    {

        public Vec3 Position;
        public SoundEvent SoundEvent;

        public SoundLocation(Vec3 position, SoundEvent soundEvent)
        {
            Position = position;
            SoundEvent = soundEvent;
        }
    }
}
