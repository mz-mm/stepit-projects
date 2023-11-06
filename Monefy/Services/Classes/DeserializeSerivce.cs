using Monefy.Enums;
using Monefy.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monefy.Services.Classes;

public class DeserializeSerivce : IDeserializeService
{
    public ObservableCollection<T> Deserialize<T>(string fileName)
    {
        using var fileStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Read);
        using var streamReader = new StreamReader(fileStream);

        string json = streamReader.ReadToEnd();

        var deserializedObject = JsonConvert.DeserializeObject<ObservableCollection<T>>(json, new JsonSerializerSettings
        { 
            TypeNameHandling = TypeNameHandling.All}
        );

        return deserializedObject;
    }
}
