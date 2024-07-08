using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNhanSu
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
                    
            LoginForm loginForm = new LoginForm();
            Application.Run(loginForm);

            if (loginForm.IsAuthenticated)
            {
                Application.Run(new MenuForm(loginForm.LoggedInUsername));
            }                    
        }
    }   
}
