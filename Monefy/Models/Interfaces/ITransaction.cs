using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monefy.Models.Interfaces;
public interface ITransaction
{
    public string Description { get; set; }
    public double Amount { get; set; }
    public DateTime Date { get; set; }
}
