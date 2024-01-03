using Avalonia.Controls;
using Disableton.ViewModels;

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
}
