﻿using System;

namespace SurviveTheFuture
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new SurviveTheFuture())
                game.Run();
        }
    }
#endif
}
