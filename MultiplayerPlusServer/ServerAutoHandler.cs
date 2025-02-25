﻿using MultiplayerPlusCommon.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TaleWorlds.MountAndBlade;
using static TaleWorlds.MountAndBlade.GameNetwork;

namespace MultiplayerPlusServer
{
    /// <summary>
    /// Manages the registration and deregistration of server-side message handlers.
    /// </summary>
    public class ServerAutoHandler : MissionNetwork
    {
        public delegate void HandlerRegister(NetworkMessageHandlerRegisterer reg);

        public event HandlerRegister HandlerRegisterList;

        public override void OnBehaviorInitialize()
        {
            base.OnBehaviorInitialize();
            AddRemoveMessageHandlers(NetworkMessageHandlerRegisterer.RegisterMode.Add);
        }

        public override void OnRemoveBehavior()
        {
            base.OnRemoveBehavior();
            AddRemoveMessageHandlers(NetworkMessageHandlerRegisterer.RegisterMode.Remove);
        }

        public void AddRemoveMessageHandlers(NetworkMessageHandlerRegisterer.RegisterMode mode)
        {
            NetworkMessageHandlerRegisterer reg = new NetworkMessageHandlerRegisterer(mode);
            HandlerRegisterList?.Invoke(reg);
        }

        public ServerAutoHandler()
        {
            // Automatically discover and register all IHandlerRegister implementations
            try
            {
                int handlerCount = 0;
                IEnumerable<Type> handlerTypes = Assembly.GetExecutingAssembly()
                                           .GetTypes()
                                           .Where(t => t.GetInterfaces().Contains(typeof(IHandlerRegister)) && !t.IsAbstract);

                foreach (Type type in handlerTypes)
                {
                    if (Activator.CreateInstance(type) is IHandlerRegister handler)
                    {
                        HandlerRegisterList += handler.Register;
                        handlerCount++;
                    }
                }


            }
            catch (Exception ex)
            {
            }
        }
    }
}