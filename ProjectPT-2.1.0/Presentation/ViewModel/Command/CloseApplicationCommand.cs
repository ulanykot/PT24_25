﻿using System;
using System.Windows;
using System.Windows.Input;

namespace PresentationViewModel;

internal class CloseApplicationCommand : ICommand
{
    public event EventHandler CanExecuteChanged;

    public CloseApplicationCommand()
    {

    }

    public bool CanExecute(object parameter)
    {
        return true;
    }

    public void Execute(object parameter)
    {
        Application.Current.Shutdown();
    }
}
