using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace XamarinUtilities
{
	public static class Events
	{
		private static Dictionary<string, object> _subscribers = new Dictionary<string, object>();

		public static void Publish<T>(T message,
			[CallerMemberName] string method = "",
			[CallerFilePath] string file = "",
			[CallerLineNumber] int line = 0)
		{
			IMessageHandler<T> handler = null;

			foreach (var subscriber in new Dictionary<string, object>(_subscribers))
			{
				handler = (subscriber.Value as IMessageHandler<T>);
				if (handler != null)
					handler.OnMessage(message);
			}
		}

		public static void Subscribe(object subscriber, string uniqueId)
		{
			_subscribers[uniqueId] = subscriber;
		}

		internal static void Clear()
		{
			_subscribers.Clear();
		}
	}

	public interface IMessageHandler<TMessage>
	{
		void OnMessage(TMessage message);
	}
}
