using System;
using System.Windows.Input;

namespace lunch_proto.Utils
{
	class DelegateCommand : ICommand
	{
		private Action<Object> _action;

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public event EventHandler CanExecuteChanged;

		public void Execute(object parameter)
		{
			_action(parameter);
		}

		public DelegateCommand(Action<Object> action)
		{
			_action = action;
		}
	}
}
