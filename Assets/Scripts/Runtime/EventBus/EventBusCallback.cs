using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomEventBus
{
    public class EventBusCallback
    {
        /// <summary>
        /// Чем выше Priority, тем раньше вызовется ивент
        /// </summary>
        public readonly int Priority;
        public readonly object Callback;

        public EventBusCallback(int priority, object callback)
        {
            Priority = priority;
            Callback = callback;
        }
    }
}