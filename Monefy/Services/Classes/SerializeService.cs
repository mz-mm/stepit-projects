using Monefy.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Monefy.Services.Classes;

public class SerializeService : ISerializeService
{
    public void Serialize<T>(string path, ObservableCollection<T> list)
    {
        using var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
        using var streamWriter = new StreamWriter(fileStream);
        string json = JsonConvert.SerializeObject(list, Formatting.Indented);
        streamWriter.Write(json);
    }
}
