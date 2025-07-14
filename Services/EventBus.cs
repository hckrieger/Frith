using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;
using HandlerList = System.Collections.Generic.List<Frith.Services.IEventCallback>;


namespace Frith.Services
{
	public interface IEvent
	{

	}
	public interface IEventCallback
	{
		public void Call(IEvent e);

		public void Execute(IEvent e) => Call(e);
		
	};


	public class EventCallback<TOwner, TEvent> : IEventCallback where TEvent : IEvent
	{

		private TOwner ownerInstance;
		public TOwner Owner => ownerInstance;
		private Action<TEvent> callbackFunction;
		public void Call(IEvent e)
		{
			callbackFunction.Invoke((TEvent)e);
		}

		public EventCallback(TOwner ownerInstance, Action<TEvent> callbackFunction)
		{
			this.ownerInstance = ownerInstance;
			this.callbackFunction = callbackFunction;
		}
	}
	public class EventBus
	{
		private Dictionary<Type, HandlerList> subscribers = [];

		public void SubscribeToEvent<TEvent, TOwner>(TOwner ownerInstance, Action<TEvent> callbackFunction) where TEvent : IEvent 
		{
			var subscriber = new EventCallback<TOwner, TEvent>(ownerInstance, callbackFunction);
			var type = typeof(TEvent);
			if (!subscribers.TryGetValue(type, out HandlerList? value))
			{
				value = new HandlerList();
				subscribers[type] = value;
			}

			subscribers[type].Add(subscriber);
			
		}


		public void EmitEvent<TEvent>(TEvent e) where TEvent : IEvent
		{
			if (subscribers.TryGetValue(typeof(TEvent), out HandlerList? handlers))
			{
				foreach (var handler in handlers)
				{
					handler.Execute(e);
				}
			} 
		}
	}


}
