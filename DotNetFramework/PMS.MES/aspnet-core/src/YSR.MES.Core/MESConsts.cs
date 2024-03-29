﻿using YSR.MES.Debugging;

namespace YSR.MES
{
    public class MESConsts
    {
        public const string LocalizationSourceName = "MES";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "b1dbfa311c0e4fc885ea2900a33a8865";
    }
}
