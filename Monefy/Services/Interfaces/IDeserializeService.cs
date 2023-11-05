using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Monefy.Services.Interfaces;

public interface IDeserializeService
{
    public ObservableCollection<T> Deserialize<T>(string path); 

}
