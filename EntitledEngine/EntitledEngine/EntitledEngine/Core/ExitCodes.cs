using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntitledEngine.EntitledEngine
{
    public class ExitCodes
    {
        public static ExitCodes QuitWindow()
        {
            Application.Exit();
            //Environment.Exit(1);
            return null;
        }

        public static ExitCodes QuitConsole()
        {
            //Application.Exit();
            Environment.Exit(1);
            return null;
        }

        public static ExitCodes QuitApplication()
        {
            Application.Exit();
            Environment.Exit(1);
            return null;
        }
        public static ExitCodes RestartApplication()
        {
            Application.Restart();
			QuitConsole();
            return null;
        }
    }
}
