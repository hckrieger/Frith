using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;


namespace Frith.Events
{

	

	public abstract class Event
    {
        public Guid Guid { get; set; }

        protected Event() 
        {
            Guid = Guid.NewGuid();
        }
    }


    public interface IEventCallback
    {
        public void Call(Event e);
    }

    public class EventCallback<TEvent> : IEventCallback where TEvent : Event
    {
        private readonly Action<TEvent> action; // OnCollision(CollisionEvent e);

        public EventCallback(Action<TEvent> action)
        {
            this.action = action;
        }

        public void Call(Event e)
        {
            if (e is TEvent typedEvent)
            {
                action.Invoke(typedEvent);
            }
        }
    }

    public class EventBus
    {
        private readonly Dictionary<Type, List<IEventCallback>> subscribers = [];
		public Dictionary<string, Event> EventCache = [];



		public void SubscribeToEvent<TEvent>(Action<TEvent> callback) where TEvent : Event
        {
            var eventType = typeof(TEvent);
            if (!subscribers.ContainsKey(eventType))
            {
                subscribers[eventType] = new List<IEventCallback>();
            }

            subscribers[eventType].Add(new EventCallback<TEvent>(callback));    
        }

        public void UnsubscribeFromEvent<TEvent>(Action<TEvent> callback) where TEvent: Event
        {
            var eventType = typeof(TEvent);
            if (subscribers.TryGetValue(eventType, out var handlers))
            {
                handlers.RemoveAll(h => h is EventCallback<TEvent> eventCallback && eventCallback.Equals(callback));
            }
        }

        public void EmitEvent<TEvent>(TEvent e) where TEvent : Event
        {
            var eventType = e.GetType();
            if (subscribers.TryGetValue(eventType, out var handlers))
            {
                foreach (var handler in handlers)
                {
                    handler.Call(e);
                }
            }
        }

        public void Reset()
        {
            subscribers.Clear();
        }
    }

}
