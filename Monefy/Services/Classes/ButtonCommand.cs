using System;
using System.Windows.Input;

namespace Monefy.Services.Classes;

public class ButtonCommand : ICommand
{
    private readonly Action _funcToExecute;
    private readonly Func<bool> _funcToCheck = () => true;

    public event EventHandler? CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    public ButtonCommand(Action funcToExecute)
    {
        _funcToExecute = funcToExecute;
    }


    public ButtonCommand(Action funcToExecute, Func<bool> funcToCheck)
    {
        _funcToExecute = funcToExecute;
        _funcToCheck = funcToCheck;
    }

    public bool CanExecute(object? parameter)
    {
        return _funcToCheck();
    }

    public void Execute(object? parameter)
    {
        _funcToExecute();
    }
}
