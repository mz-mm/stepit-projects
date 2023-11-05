using Monefy.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Monefy.Services.Classes;

public class DeserializeSerivce : IDeserializeService
{
    public List<T> Deserialize<T>(string path)
    {
        try
        {
            using var fileStream = new FileStream(path, FileMode.OpenOrCreate);
            using var streamReader = new StreamReader(fileStream);

            var json = streamReader.ReadToEnd();
            var result = JsonSerializer.Deserialize<List<T>>(json);

            if (result != null)
            {
                return result;
            }
            else
            {
                return new List<T>();
            }
        }
        catch (JsonException ex)
        {
            return new List<T>();
        }
    }
}
