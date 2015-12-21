using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalhostAPI.Integration
{
    public interface IActionWithParameters
    {
        object Execute(Dictionary<string, string> parameters);
    }
}
