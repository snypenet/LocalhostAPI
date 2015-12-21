using LocalhostAPI.Behaviors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocalhostAPI
{
    public partial class LocalWindow : Form
    {
        private static ServiceHost _ServiceHost = null;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string mutexName = string.Format("{0}_{1}", assembly.GetType().GUID, assembly.GetType().Name);
            bool mutexCreated = true;
            using(var mutex = new Mutex(true, mutexName, out mutexCreated))
            {
                _ServiceHost = new ServiceHost(typeof(Local), new Uri(ConfigurationManager.AppSettings["ServiceUrl"]));
                ServiceEndpoint endpoint = _ServiceHost.AddServiceEndpoint(typeof(ILocal), new WebHttpBinding { MaxReceivedMessageSize = int.MaxValue, CrossDomainScriptAccessEnabled = true }, "");
                endpoint.EndpointBehaviors.Add(new EnableCorsEndpointBehavior());
                endpoint.EndpointBehaviors.Add(new WebHttpBehavior());
                _ServiceHost.Open();

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                var window = new LocalWindow();
                Application.Run();
            }
        }

        public LocalWindow()
        {
            InitializeComponent();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ServiceHost.Close();
            Close();
            Application.Exit();
        }
    }
}
