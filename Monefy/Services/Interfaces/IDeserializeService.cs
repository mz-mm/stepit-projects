using System.Collections.ObjectModel;

namespace Monefy.Services.Interfaces;

public interface IDeserializeService
{
    public ObservableCollection<T> Deserialize<T>(string fileName); 
}
