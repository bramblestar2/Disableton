using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Disableton.ViewModels;
using System.Diagnostics;
using System.Linq;

namespace Disableton.Views;

public partial class MainView : UserControl
{
    private MainViewModel viewModel;

    public MainView()
    {
        InitializeComponent();

        viewModel = new MainViewModel();
        this.DataContext = viewModel;
    }

    protected override void OnUnloaded(RoutedEventArgs e)
    {
        base.OnUnloaded(e);

        viewModel.Dispose();
    }
}
