using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Monefy.Services.Classes;

public class EnumService
{
    public static T TryParseString<T>(string value) where T : struct, Enum
    {
        if (Enum.TryParse(value, true, out T result))
        {
            return result;
        }

        throw new ArgumentException($"Invalid value for enum type {typeof(T)}: {value}");
    }
}
