using System;
using System.Collections.Generic;
using System.Reflection;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.PlayerServices;
using static TaleWorlds.MountAndBlade.Agent;

namespace MultiplayerPlusCommon.GameModes.Duel
{
    public static class AdimiHelpers
    {
        public static readonly Dictionary<PlayerId, DuelConfig> PlayersDuelConfig = new Dictionary<PlayerId, DuelConfig>();
        public static object GetField(object instance, string field) => GetFieldInfo(instance, field).GetValue(instance);
        public static void SetField(object instance, string field, object value) => GetFieldInfo(instance, field).SetValue(instance, value);
        public static void SetPublicField(object instance, string field, object value) => GetPublicFieldInfo(instance, field).SetValue(instance, value);
        public static object GetProperty(object instance, string field) => GetPropertyInfo(instance, field).GetValue(instance);
        public static void SetProperty(object instance, string field, object value) => GetPropertyInfo(instance, field).SetValue(instance, value, null);

        public static object InvokeMethod(object instance, string method, object[] parameters)
        {
            return instance
                .GetType()
                .GetMethod(method, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Invoke(instance, parameters);
        }

        public static PropertyInfo GetPropertyInfo(object instance, string prop)
        {
            Type t = instance.GetType();
            while (t != null)
            {
                var f = t.GetProperty(prop, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (f != null)
                {
                    return f.DeclaringType.GetProperty(prop, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                }

                t = t.BaseType;
            }

            throw new ArgumentException($"Property {prop} not found in {instance.GetType()}");
        }

        public static void RaiseEvent(object instance, string evt, object[] parameters)
        {
            var deleg = (MulticastDelegate)GetFieldInfo(instance, evt).GetValue(instance);
            if (deleg == null)
            {
                return;
            }

            foreach (var invocation in deleg.GetInvocationList())
            {
                invocation?.DynamicInvoke(parameters);
            }
        }

        private static FieldInfo GetFieldInfo(object instance, string field)
        {
            Type t = instance.GetType();
            while (t != null)
            {
                var f = t.GetField(field, BindingFlags.Instance | BindingFlags.NonPublic);
                if (f != null)
                {
                    return f;
                }

                t = t.BaseType;
            }

            throw new ArgumentException($"Field {field} not found in {instance.GetType()}");
        }

        private static FieldInfo GetPublicFieldInfo(object instance, string field)
        {
            Type t = instance.GetType();
            while (t != null)
            {
                var f = t.GetField(field, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (f != null)
                {
                    return f;
                }

                t = t.BaseType;
            }

            throw new ArgumentException($"Field {field} not found in {instance.GetType()}");
        }
    }

    public class FactionColor
    {
        public static List<FactionColor> AllColors { get; private set; } = new List<FactionColor>();
        public int PrimaryColor { get; set; } = 0;
        public int IconColor { get; set; } = 0;
        public List<FactionColor> Exclude { get; set; } = new List<FactionColor>();

        public FactionColor()
        {
            AllColors.Add(this);
        }
    }

    public class DuelConfig
    {
        public int FirstToLimit { get; set; }
        public bool FirstToSeven { get; set; }
        public NetworkCommunicator NetworkPeer { get; set; }
        public MissionTime? LastTeleportWarningTimer { get; set; }
        public List<ItemObject> RespawnEquip { get; set; } // RespawnEquip
        public MatrixFrame? DeathFrame { get; set; } // Last Death frame
        public int NoWeaponTreshold { get; set; }

        public DuelConfig(NetworkCommunicator networkPeer)
        {
            NetworkPeer = networkPeer;
            FirstToLimit = 1;
            FirstToSeven = false;
            LastTeleportWarningTimer = null;
            RespawnEquip = null;
            DeathFrame = null;
            NoWeaponTreshold = 0;
        }

        public void OnAgentWieldedItemChange()
        {
            AdimiToolsConsoleLog.Log("Called OnAgentWieldedItemChange");
            MissionPeer missionPeer = NetworkPeer.GetComponent<MissionPeer>();
            Agent agent = missionPeer.ControlledAgent;

            if (AdimiHelpers.PlayersDuelConfig.TryGetValue(NetworkPeer.VirtualPlayer.Id, out DuelConfig duelConfig))
            {
                EquipmentIndex wieldedItemIndex = agent.GetWieldedItemIndex(HandIndex.MainHand);
                if (wieldedItemIndex != EquipmentIndex.None)
                {
                    duelConfig.NoWeaponTreshold = 0;
                    AdimiToolsConsoleLog.Log("Reset NoWeaponTreshold");
                }
            }
        }
    }

}

