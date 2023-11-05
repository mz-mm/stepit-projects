using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Monefy.Services.Interfaces;

public interface IDeserializeService
{
    public List<T> Deserialize<T>(string path); 

}
