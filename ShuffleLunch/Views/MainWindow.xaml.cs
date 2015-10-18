using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ShuffleLunch.Common.Extensions;
using ShuffleLunch.Models;
using ShuffleLunch.ViewModels;

namespace ShuffleLunch
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void UIElement_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var lb = sender as ListBox;
            if (lb?.SelectedItem != null)
            {
                DragDrop.DoDragDrop(lb, lb.ItemContainerGenerator.ContainerFromItem(lb.SelectedItem), DragDropEffects.Move);
            }
        }

        private void UIElement_OnDragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ListBoxItem)))
            {
                e.Effects = DragDropEffects.Move;
            }
        }

        private void UIElement_OnDrop(object sender, DragEventArgs e)
        {
            var lb = sender as ListBox;

            var item = (ListBoxItem)e.Data.GetData(typeof(ListBoxItem));
            var parent = item.FindAncestor<ListBox>();
            if (lb == parent) { return; }
            var content = parent.ItemContainerGenerator.ItemFromContainer(item);
            ((ObservableCollection<Person>)parent.ItemsSource)?.Remove((Person)content);
            ((ObservableCollection<Person>)lb?.ItemsSource)?.Add((Person)content);
        }

		private void Window_Drop(object sender, DragEventArgs e)
		{
			var dropfiles = (string[])(e.Data.GetData(DataFormats.FileDrop));
			if (dropfiles == null)
			{
				return;
			}

			((WindowViewModel)DataContext).SetList(dropfiles[0]);
		}

		private void Window_PreviewDragOver(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop, true))
			{
				e.Effects = DragDropEffects.Copy;
			}
			else
			{
				e.Effects = DragDropEffects.None;
			}
			e.Handled = true;
		}
	}
}
