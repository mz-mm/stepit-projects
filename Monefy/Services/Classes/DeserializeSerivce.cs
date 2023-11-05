using Monefy.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Monefy.Services.Classes;

public class DeserializeSerivce : IDeserializeService
{
    public ObservableCollection<T> Deserialize<T>(string path)
    {
        try
        {
            using var fileStream = new FileStream(path, FileMode.OpenOrCreate);
            using var streamReader = new StreamReader(fileStream);

            var json = streamReader.ReadToEnd();
            var result = JsonSerializer.Deserialize<ObservableCollection<T>>(json);

            if (result != null)
            {
                return result;
            }
            else
            {
                return new ObservableCollection<T>();
            }
        }
        catch (JsonException ex)
        {
            return new ObservableCollection<T>();
        }
    }
}
