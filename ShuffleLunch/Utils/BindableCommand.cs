using System;
using System.Windows;
using System.Windows.Input;

namespace ShuffleLunch.Utils
{
    public class BindableCommand : FrameworkElement, ICommand
    {
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            "Command", typeof(ICommand), typeof(BindableCommand), new PropertyMetadata(default(ICommand), CommandChanged));

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty ParameterProperty = DependencyProperty.Register(
            "Parameter", typeof (object), typeof (BindableCommand), new PropertyMetadata(default(object)));

        public object Parameter
        {
            get { return GetValue(ParameterProperty); }
            set { SetValue(ParameterProperty, value); }
        }

        private static void CommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((BindableCommand)d).CommandChanged(e);
        }

        private void CommandChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue != null)
            {
                ((ICommand)e.OldValue).CanExecuteChanged -= OnCanExecuteChanged;
            }
            if (e.NewValue != null)
            {
                ((ICommand)e.NewValue).CanExecuteChanged += OnCanExecuteChanged;
            }
            RaiseCanExecuteChanged();
        }

        private void OnCanExecuteChanged(object sender, EventArgs eventArgs)
        {
            RaiseCanExecuteChanged();
        }

        private void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(object parameter)
        {
            //return Command?.CanExecute(Parameter) ?? false;
            return true;
        }

        public void Execute(object parameter)
        {
            Command?.Execute(Parameter);
        }

        public event EventHandler CanExecuteChanged;
    }
}
