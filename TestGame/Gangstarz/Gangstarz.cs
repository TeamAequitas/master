using Gangstarz.Managers;
using System;

namespace Gangstarz
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Gangstarz
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new StateManager())
                game.Run();
        }
    }
#endif
}
