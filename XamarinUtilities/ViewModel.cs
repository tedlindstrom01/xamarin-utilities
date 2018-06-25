using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace XamarinUtilities
{
	public class NotifyPropertyChanged : INotifyPropertyChanged
	{
		private Dictionary<string, object> _properties = new Dictionary<string, object>();

		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		protected void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression)
		{
			MemberExpression memberExpression = propertyExpression?.Body as MemberExpression;
			if (!string.IsNullOrEmpty(memberExpression?.Member?.Name))
			{
				OnPropertyChanged(memberExpression.Member.Name);
			}
		}

		protected T GetValue<T>([CallerMemberName] string propertyName = null)
		{
			if (!_properties.ContainsKey(propertyName))
				_properties.Add(propertyName, default(T));

			return (T)_properties[propertyName];
		}

		protected void SetValue<T>(T value, [CallerMemberName] string propertyName = null)
		{
			_properties[propertyName] = value;
			OnPropertyChanged(propertyName);
		}
	}
}
