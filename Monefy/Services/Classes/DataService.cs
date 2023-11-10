using GalaSoft.MvvmLight.Messaging;
using Monefy.Messages;
using Monefy.Models;

namespace Monefy.Services.Classes;

public class DataService
{
    private readonly IMessenger _messenger;

    public DataService(IMessenger messenger)
    {
        _messenger = messenger;
    }

    public void SendData<T>(T data) where T : IData
    {
        _messenger.Send(new DataMessage()
        {
            Data = data
        });
    }
}
