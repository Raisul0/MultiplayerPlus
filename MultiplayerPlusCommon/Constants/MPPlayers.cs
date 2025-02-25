using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TaleWorlds.Core.ItemObject;
using TaleWorlds.Core;
using MultiplayerPlusCommon.ObjectClass;
using JetBrains.Annotations;

namespace MultiplayerPlusCommon.Constants
{
    public static class MPPlayers
    {
        public static List<MPAgent> Players { get; set; } = new List<MPAgent>();
        public static void AddPlayer(MPAgent player)
        {
            Players.Add(player);
        }

        public static void RemovePlayer(MPAgent player)
        {
            Players.Remove(player);
        }

        public static MPAgent GetMPAgentFromPlayerId(string playerId)
        {
            return Players.FirstOrDefault(x=>x.PlayerId == playerId);   
        }

        public static MPAgent GetMPAgentFromUserName(string userName)
        {
            return Players.FirstOrDefault(x => x.UserName == userName);
        }

        public static Equipment EquipPlayerCustomEquipment(string playerId,string characterClass,Equipment equipment)
        {
            if (equipment != null)
            {
                var player = GetMPAgentFromPlayerId(playerId);

                if (player!=null)
                {
                    var classCosmetic = player.ClassCosmetics.FirstOrDefault(x => x.Class == characterClass);

                    if (classCosmetic!=null)
                    {
                        if (!string.IsNullOrEmpty(classCosmetic.Head)) equipment[EquipmentIndex.Head] = classCosmetic.HeadItem;
                        if (!string.IsNullOrEmpty(classCosmetic.Shoulder)) equipment[EquipmentIndex.Cape] = classCosmetic.ShoulderItem;
                        if (!string.IsNullOrEmpty(classCosmetic.Body)) equipment[EquipmentIndex.Body] = classCosmetic.BodyItem;
                        if (!string.IsNullOrEmpty(classCosmetic.Arms)) equipment[EquipmentIndex.Gloves] = classCosmetic.ArmsItem;
                        if (!string.IsNullOrEmpty(classCosmetic.Legs)) equipment[EquipmentIndex.Leg] = classCosmetic.LegsItems;
                    }
                }
            }

            return equipment;
        }

    }
    
}
