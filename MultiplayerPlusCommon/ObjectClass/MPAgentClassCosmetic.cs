using MultiplayerPlusCommon.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Core;

namespace MultiplayerPlusCommon.ObjectClass
{
    public class MPAgentClassCosmetic
    {
        public string Class { get; set; }
        public string Head { get; set; }
        public string Shoulder { get; set; }
        public string Body { get; set; }
        public string Arms { get; set; }
        public string Legs { get; set; }

        public EquipmentElement GetItemObjectFromString(string classCosmeticPart)
        {
            return new EquipmentElement(MultiplayerPlusHelper.GetItemObjectfromString(classCosmeticPart));
        }
        public EquipmentElement HeadItem => new EquipmentElement(MultiplayerPlusHelper.GetItemObjectfromString(Head));
        public EquipmentElement ShoulderItem => new EquipmentElement(MultiplayerPlusHelper.GetItemObjectfromString(Shoulder));
        public EquipmentElement BodyItem => new EquipmentElement(MultiplayerPlusHelper.GetItemObjectfromString(Body));
        public EquipmentElement ArmsItem => new EquipmentElement(MultiplayerPlusHelper.GetItemObjectfromString(Arms));
        public EquipmentElement LegsItems => new EquipmentElement(MultiplayerPlusHelper.GetItemObjectfromString(Legs));
    }
}
