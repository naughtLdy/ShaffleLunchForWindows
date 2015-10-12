using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ShuffleLunch.ViewModels
{
	class WindowViewModel : INotifyPropertyChanged
	{
		#region プロパティ変更通知

		// INotifyPropertyChanged
		public event PropertyChangedEventHandler PropertyChanged;

		protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] String propertyName = null)
		{
			if (object.Equals(storage, value)) return false;

			storage = value;
			this.OnPropertyChanged(propertyName);
			return true;
		}

		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			var eventHandler = this.PropertyChanged;
			if (eventHandler != null)
			{
				eventHandler(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion

		#region Title 変更通知プロパティ

		private string _title;

		public string Title
		{
			get { return this._title; }
			set
			{
				SetProperty(ref _title, value);
			}
		}

		#endregion

		public WindowViewModel()
		{
			Title = "ShuffleLunch";
		}

	}
}
