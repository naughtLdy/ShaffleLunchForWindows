using lunch_proto.Utils;
using ShuffleLunch.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
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

		#region PersonAndDeskList 変更通知プロパティ

		private ObservableCollection<PersonAndDesk> _personAndDeskList = new ObservableCollection<PersonAndDesk>();
		public ObservableCollection<PersonAndDesk> PersonAndDeskList
		{
			get { return _personAndDeskList; }
			set
			{
				SetProperty(ref _personAndDeskList, value);
			}
		}

		#endregion

		#region ShuffleResultList 変更通知プロパティ

		private ObservableCollection<ShuffleResult> _shuffleResultList = new ObservableCollection<ShuffleResult>();
		public ObservableCollection<ShuffleResult> ShuffleResultList
		{
			get { return _shuffleResultList; }
			set
			{
				SetProperty(ref _shuffleResultList, value);
			}
		}

		#endregion

		#region AddUserName 変更通知プロパティ

		private string _addUserName;
		public string AddUserName
		{
			get { return _addUserName; }
			set
			{
				SetProperty(ref _addUserName, value);
			}
		}

		#endregion

		#region FontSizeDesk 変更通知プロパティ

		private int _fontSizeDesk;

		public int FontSizeDesk
		{
			get { return _fontSizeDesk; }
			set
			{
				SetProperty(ref _fontSizeDesk, value);
			}
		}

		#endregion

		#region FontSizePerson 変更通知プロパティ

		private int _fontSizePerson;

		public int FontSizePerson
		{
			get { return _fontSizePerson; }
			set
			{
				SetProperty(ref _fontSizePerson, value);
			}
		}

		#endregion

		#region ImageWidth 変更通知プロパティ

		private int _imageWidth;

		public int ImageWidth
		{
			get { return _imageWidth; }
			set
			{
				SetProperty(ref _imageWidth, value);
			}
		}

		#endregion

		#region ImageHeight 変更通知プロパティ

		private int _imageHeight;

		public int ImageHeight
		{
			get { return _imageHeight; }
			set
			{
				SetProperty(ref _imageHeight, value);
			}
		}

		#endregion

		#region ShuffleImageWidth 変更通知プロパティ

		private int _shuffleImageWidth;

		public int ShuffleImageWidth
		{
			get { return _shuffleImageWidth; }
			set
			{
				SetProperty(ref _shuffleImageWidth, value);
			}
		}

		#endregion

		#region ShuffleImageHeight 変更通知プロパティ

		private int _shuffleImageHeight;

		public int ShuffleImageHeight
		{
			get { return _shuffleImageHeight; }
			set
			{
				SetProperty(ref _shuffleImageHeight, value);
			}
		}

		#endregion

		#region TabIndex 変更通知プロパティ

		private bool _shuffleTabSelected;

		public bool ShuffleTabSelected
		{
			get { return _shuffleTabSelected; }
			set
			{
				SetProperty(ref _shuffleTabSelected, value);
			}
		}

		#endregion

		private LunchInfo _lunchInfo;

		/// <summary>
		/// ファイルオープン
		/// </summary>
		public ICommand FileOpen { get; private set; }

		/// <summary>
		/// 参加者をシャッフル
		/// </summary>
		public ICommand ButtonShuffle { get; private set; }

		/// <summary>
		/// 参加者追加
		/// </summary>
		public ICommand ButtonAddUser { get; private set; }

        public ICommand ExportImage { get; private set; }

		public WindowViewModel()
		{
			Title = "ShuffleLunch";

			_lunchInfo = new LunchInfo();

			var setting = Setting.Instance;
			setting.Get();
			FontSizeDesk = setting.FontSizeDesk;
			FontSizePerson = setting.FontSizePerson;
			ImageWidth = setting.ImageWidth;
			ImageHeight = setting.ImageHeight;
			ShuffleImageWidth = setting.ShuffleImageWidth;
			ShuffleImageHeight = setting.ShuffleImageHeight;

			ShuffleTabSelected = false;

			FileOpen = new DelegateCommand(_ =>
			{
				var b = _lunchInfo.Get();
				if (b == false)
				{
					return;
				}

				PersonList = new ObservableCollection<Person>(_lunchInfo.PersonList());
				DeskList = new ObservableCollection<Desk>(_lunchInfo.DeskList());
				PersonAndDeskList = new ObservableCollection<PersonAndDesk>(_lunchInfo.PersonAndDeskList());

			});

			ButtonShuffle = new DelegateCommand(_ =>
			{
				var shuffle = new Shuffle();
				var b = shuffle.shuffle(DeskList.ToList<Desk>(), PersonAndDeskList.ToList<PersonAndDesk>());
				if (b == false)
				{
					return;
				}

				ShuffleResultList = new ObservableCollection<ShuffleResult>(shuffle.Get());

				ShuffleTabSelected = true;

			});

			ButtonAddUser = new DelegateCommand(_ =>
			{
				var deskList = new List<string>();
				for (int i = 0; i < DeskList.Count; i++)
				{
					deskList.Add(DeskList[i].name);
				}

				var myAssembly = Assembly.GetEntryAssembly();
				string path = myAssembly.Location;
				path = path.Replace("ShuffleLunch.exe", "");
				var personAndDesk = new PersonAndDesk
				{
					name = AddUserName,
					desk = deskList,
					selectDesk = 0,
					image = path + @"image\guest.png"
				};
				PersonAndDeskList.Add(personAndDesk);
				AddUserName = "";
			});

			ExportImage = new DelegateCommand(element =>
			{
				PngExporter.Export((FrameworkElement)element);
			});
		}

		public bool SetList(string filename)
		{
			if (string.IsNullOrEmpty(filename) == true || filename.EndsWith("json") == false)
			{
				return false;
			}

			var r = _lunchInfo.SetList(filename);
			if (r == false)
			{
				return false;
			}

			PersonList = new ObservableCollection<Person>(_lunchInfo.PersonList());
			DeskList = new ObservableCollection<Desk>(_lunchInfo.DeskList());
			PersonAndDeskList = new ObservableCollection<PersonAndDesk>(_lunchInfo.PersonAndDeskList());

			return true;
		}
	}
}
