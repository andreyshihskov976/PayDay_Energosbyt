using System;
using System.Windows.Forms;

namespace PayDay_Energosbyt
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            LogIn logIn = new LogIn();
            Application.Run(logIn);
            if (logIn.DialogResult == DialogResult.OK)
            {
                Main main = new Main(logIn.Login,logIn.Password);
                Application.Run(main);
            }
        }
    }
}
