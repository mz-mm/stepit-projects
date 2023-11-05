using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Monefy.Services.Interfaces;

public interface ISerializeService
{
    public void Serialize<T>(string path, List<T> List);
}
