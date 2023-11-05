using Monefy.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Monefy.Services.Classes;

public class SerializeService : ISerializeService
{
    public void Serialize<T>(string path, List<T> List)
    {
        using var fileStream = new FileStream(path, FileMode.Truncate);
        JsonSerializer.Serialize(fileStream, List);
    }
}
