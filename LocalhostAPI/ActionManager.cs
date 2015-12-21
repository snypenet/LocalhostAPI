using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LocalhostAPI
{
    public class ActionManager
    {
        private static readonly object _LockManager = new object();
        private static ActionManager _Manager;
        public static ActionManager Instance
        {
            get
            {
                if(_Manager == null)
                {
                    lock (_LockManager)
                    {
                        if(_Manager == null)
                        {
                            _Manager = new ActionManager();
                        }
                    }
                }

                return _Manager;
            }
        }
        private readonly ConcurrentDictionary<string, Action> _Actions;
        private ActionManager()
        {
            _Actions = new ConcurrentDictionary<string, Action>();
            Load();
        }

        public void Refresh()
        {
            _Actions.Clear();
            Load();
        }

        public void Load()
        {
            XmlDocument document = new XmlDocument();
            document.Load(Directory.GetCurrentDirectory() + "/actions.xml");
            XmlNodeList actionNodes = document.SelectNodes("/actions/action");
            if(actionNodes != null)
            {
                foreach(XmlNode actionNode in actionNodes)
                {
                    _Actions[actionNode.Attributes["name"].Value] = new Action
                    {
                        Name = actionNode.Attributes["name"].Value,
                        Location = actionNode.Attributes["location"].Value,
                        TypeName = actionNode.Attributes["type"].Value
                    };
                }
            }
        }

        public Action GetAction(string name)
        {
            return _Actions.ContainsKey(name) ? _Actions[name] : null;
        }
    }

    public class Action
    {
        public string TypeName { get; set; }
        public string Location { get; set; }
        public string Name { get; set; }

        private Assembly _Binary;
        public Assembly Binary
        {
            get
            {
                if(_Binary == null)
                {
                    _Binary = Assembly.LoadFile(Location);
                }

                return _Binary;
            }
        }
    }
}
