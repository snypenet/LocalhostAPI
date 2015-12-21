using LocalhostAPI.Integration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json;

namespace LocalhostAPI
{
    public class Local : ILocal
    {
        public string ExecuteWithParameters(string actionName, Dictionary<string, string> parameters)
        {
            throw new NotImplementedException();
        }

        public string ExecuteWithoutParameters(string actionName)
        {
            Action action = ActionManager.Instance.GetAction(actionName);
            if(action == null)
            {
                throw new Exception("Action does not exist");
            }

            Type executeType = action.Binary.GetTypes().FirstOrDefault(t => t.FullName == action.TypeName);
            if(executeType == null)
            {
                throw new Exception("Execution Type is not found");
            }

            var executor = Activator.CreateInstance(executeType) as IActionWithoutParameters;

            return JsonConvert.SerializeObject(executor.Execute());
        }

        public string Test(string source)
        {
            return source;
        }
    }
}
