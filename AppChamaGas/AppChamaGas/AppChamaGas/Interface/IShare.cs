using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppChamaGas.Interface
{
    public interface IShare
    {
        Task Share(string path, string title);
    }
}
