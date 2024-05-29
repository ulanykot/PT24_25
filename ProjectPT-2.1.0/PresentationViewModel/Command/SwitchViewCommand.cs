using System;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;

namespace PresentationViewModel;

public class SwitchViewCommand : ICommand
{
    public event EventHandler CanExecuteChanged;

    private string _switchToViewModel;

    public SwitchViewCommand(string viewModel)
    {
        _switchToViewModel = viewModel;
    }

    public bool CanExecute(object parameter)
    {
        return true;
    }

    public void Execute(object parameter)
    {
        UserControl userControl = parameter as UserControl;

        Window parentWindow = Window.GetWindow(userControl);

        if (parentWindow != null)
        {
            if (parentWindow.DataContext is MainWindowViewModel mainViewModel)
            {
                switch (_switchToViewModel)
                {
                    case "ProductMasterView":
                        mainViewModel.SelectedViewModel = new ProductMasterViewModel(); break;
                    case "UserMasterView":
                        mainViewModel.SelectedViewModel = new UserMasterViewModel(); break;
                    case "StateMasterView":
                        mainViewModel.SelectedViewModel = new StateMasterViewModel(); break;
                    case "EventMasterView":
                        mainViewModel.SelectedViewModel = new EventMasterViewModel(); break;
                }
            }
        }
    }
}
