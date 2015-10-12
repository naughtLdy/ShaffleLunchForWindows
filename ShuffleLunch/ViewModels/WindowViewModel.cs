using lunch_proto.Utils;
using ShuffleLunch.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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

		#region DeskList 変更通知プロパティ

		private ObservableCollection<Desk> _deskList = new ObservableCollection<Desk>();
		public ObservableCollection<Desk> DeskList
		{
			get { return _deskList; }
			set
			{
				SetProperty(ref _deskList, value);
			}
		}

		#endregion

		#region PersonList 変更通知プロパティ

		private ObservableCollection<Person> _personList = new ObservableCollection<Person>();
		public ObservableCollection<Person> PersonList
		{
			get { return _personList; }
			set
			{
				SetProperty(ref _personList, value);
			}
		}

		#endregion

		/// <summary>
		/// ファイルオープン
		/// </summary>
		public ICommand FileOpen { get; private set; }

		public WindowViewModel()
		{
			Title = "ShuffleLunch";

			FileOpen = new DelegateCommand(_ =>
			{
				var jsonData = Models.LunchInfo.Get();
				PersonList = new ObservableCollection<Person>(jsonData.persons);
				DeskList = new ObservableCollection<Desk>(jsonData.desks);

			});
		}

	}
}
