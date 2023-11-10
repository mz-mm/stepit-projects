using System.Collections.ObjectModel;

namespace Monefy.Services.Interfaces;

public interface ISerializeService
{
    public void Serialize<T>(string path, ObservableCollection<T> List);
}
