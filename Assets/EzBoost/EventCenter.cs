using System;
using System.Collections.Generic;
using UnityEngine;

namespace EzBoost
{
    public class EventCenter
    {
        // Dictionary storing listeners for each event type
        private static Dictionary<Type, List<Delegate>> _eventListeners = new();
        
        // Cache Dictionary to reduce garbage generation by reusing lists
        private static Dictionary<Type, List<Delegate>> _listenersToProcess = new();
        
        /// <summary>
        /// Register a listener for a specific event type
        /// </summary>
        /// <param name="listener">Object implementing IEventListener</param>
        /// <typeparam name="T">Event type</typeparam>
        public static void AddListener<T>(IEventListener<T> listener) where T : struct
        {
            var eventType = typeof(T);
            
            if (!_eventListeners.ContainsKey(eventType))
            {
                _eventListeners[eventType] = new List<Delegate>();
            }
            
            // Create delegate from the listener's OnEvent method
            Action<T> callback = listener.OnEzEvent;
            
            // Check if the listener is already registered
            if (!_eventListeners[eventType].Contains(callback))
            {
                _eventListeners[eventType].Add(callback);
            }
            else
            {
                Debug.LogWarning($"Listener {listener} is already registered for event {eventType.Name}");
            }
        }
        
        /// <summary>
        /// Register a callback function for a specific event type
        /// </summary>
        /// <param name="callback">Function that processes the event</param>
        /// <typeparam name="T">Event type</typeparam>
        public static void AddListener<T>(Action<T> callback) where T : struct
        {
            var eventType = typeof(T);
            
            if (!_eventListeners.ContainsKey(eventType))
            {
                _eventListeners[eventType] = new List<Delegate>();
            }
            
            if (!_eventListeners[eventType].Contains(callback))
            {
                _eventListeners[eventType].Add(callback);
            }
            else
            {
                Debug.LogWarning($"Callback is already registered for event {eventType.Name}");
            }
        }
        
        /// <summary>
        /// Unregister a listener for a specific event type
        /// </summary>
        /// <param name="listener">Object implementing IEventListener</param>
        /// <typeparam name="T">Event type</typeparam>
        public static void RemoveListener<T>(IEventListener<T> listener) where T : struct
        {
            var eventType = typeof(T);
            
            if (!_eventListeners.ContainsKey(eventType))
            {
                return;
            }
            
            Action<T> callback = listener.OnEzEvent;

            if (!_eventListeners[eventType].Contains(callback)) return;
            _eventListeners[eventType].Remove(callback);
                
            // Remove key if no listeners remain
            if (_eventListeners[eventType].Count == 0)
            {
                _eventListeners.Remove(eventType);
            }
        }
        
        /// <summary>
        /// Unregister a callback function for a specific event type
        /// </summary>
        /// <param name="callback">Function that processes the event</param>
        /// <typeparam name="T">Event type</typeparam>
        public static void RemoveListener<T>(Action<T> callback) where T : struct
        {
            var eventType = typeof(T);
            
            if (!_eventListeners.TryGetValue(eventType, out var listener))
            {
                return;
            }

            if (!listener.Contains(callback)) return;
            _eventListeners[eventType].Remove(callback);
                
            // Remove key if no listeners remain
            if (_eventListeners[eventType].Count == 0)
            {
                _eventListeners.Remove(eventType);
            }
        }
        
        /// <summary>
        /// Trigger an event, notifying all listeners
        /// </summary>
        /// <param name="eventData">Event data</param>
        /// <typeparam name="T">Event type</typeparam>
        public static void TriggerEvent<T>(T eventData) where T : struct
        {
            var eventType = typeof(T);
            
            if (!_eventListeners.ContainsKey(eventType))
            {
                return;
            }
            
            // Copy the list of listeners to avoid errors if listeners are added/removed during processing
            if (!_listenersToProcess.TryGetValue(eventType, out var value))
            {
                _listenersToProcess[eventType] = new();
            }
            else
            {
                value.Clear();
            }
            
            _listenersToProcess[eventType].AddRange(_eventListeners[eventType]);
            
            // Call all listeners
            foreach (var listener in _listenersToProcess[eventType])
            {
                try
                {
                    var callback = listener as Action<T>;
                    callback?.Invoke(eventData);
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Error processing event {eventType.Name}: {ex.Message}\n{ex.StackTrace}");
                }
            }
        }
        
        /// <summary>
        /// Unregister all listeners for a specific event type
        /// </summary>
        /// <typeparam name="T">Event type</typeparam>
        public static void ClearListeners<T>() where T : struct
        {
            var eventType = typeof(T);
            
            if (_eventListeners.ContainsKey(eventType))
            {
                _eventListeners.Remove(eventType);
            }
        }
        
        /// <summary>
        /// Unregister all listeners for all event types
        /// </summary>
        public static void ClearAllListeners()
        {
            _eventListeners.Clear();
        }
    }
}