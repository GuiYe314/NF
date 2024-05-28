// **********************************************************************
// 文件信息
// 文件名(File Name):                Version.cs
// 作者(Author):                     王云超
// 创建时间(CreateTime):             2024年5月27日 15:43:09
// Unity版本(UnityVersion)：         2021.3.38f1c1
// 脚本描述(Module description):     版本类
// 脚本修改(Script modification):
// **********************************************************************


namespace F.Librarie
{
    /// <summary>
    /// 版本号类。
    /// </summary>
    public static partial class Version
    {
        private const string FrameworkVersionString = "2024.05.27";

        private static IVersionHelper s_VersionHelper = null;

        /// <summary>
        /// 获取游戏框架版本号。
        /// </summary>
        public static string FrameworkVersion
        {
            get
            {
                return FrameworkVersionString;
            }
        }

        /// <summary>
        /// 获取游戏版本号。
        /// </summary>
        public static string GameVersion
        {
            get
            {
                if (s_VersionHelper == null)
                {
                    return string.Empty;
                }

                return s_VersionHelper.GameVersion;
            }
        }

        /// <summary>
        /// 获取内部游戏版本号。
        /// </summary>
        public static int InternalGameVersion
        {
            get
            {
                if (s_VersionHelper == null)
                {
                    return 0;
                }

                return s_VersionHelper.InternalGameVersion;
            }
        }

        /// <summary>
        /// 设置版本号辅助器。
        /// </summary>
        /// <param name="versionHelper">要设置的版本号辅助器。</param>
        public static void SetVersionHelper(IVersionHelper versionHelper)
        {
            s_VersionHelper = versionHelper;
        }
    }
}