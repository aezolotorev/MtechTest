using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CustomEventBus.Signals;
using UnityEngine;

namespace CustomEventBus {
    [Serializable]
    public class EventBus { 
       
        private Dictionary<string, List<EventBusCallback>> _signalCallbacks = new Dictionary<string, List<EventBusCallback>>();
        public void Subscribe <T> (Action<T> callback, int priority = 0)
        {
            string key = typeof(T).Name;
            if (_signalCallbacks.ContainsKey(key))
            {
                _signalCallbacks[key].Add(new EventBusCallback(priority, callback));
            }
            else
            {
                _signalCallbacks.Add(key, new List<EventBusCallback>() { new(priority, callback) });
            }

            _signalCallbacks[key] = _signalCallbacks[key].OrderByDescending(x => x.Priority).ToList();            
        }

        public void Publish<T>(T signal)
        {
            string key = typeof(T).Name;
            if (_signalCallbacks.ContainsKey(key))
            {
                foreach (var obj in _signalCallbacks[key])
                {
                    var callback = obj.Callback as Action<T>;
                    callback?.Invoke(signal);
                }
            }            
        }

        public void Unsubscribe<T>(Action<T> callback)
        {
            string key = typeof(T).Name;
            if (_signalCallbacks.ContainsKey(key))
            {
                var callbackToDelete = _signalCallbacks[key].FirstOrDefault(x => x.Callback.Equals(callback));
                if (callbackToDelete != null)
                {
                    _signalCallbacks[key].Remove(callbackToDelete);
                }
            }
            else
            {
                Debug.LogErrorFormat("Trying to unsubscribe for not existing key! {0} ", key);
            }
        }
       
    }
}

