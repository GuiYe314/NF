// **********************************************************************
// 文件信息
// 文件名(File Name):                Version.IVersionHelper.cs
// 作者(Author):                     王云超
// 创建时间(CreateTime):             2024年5月27日 15:44:23
// Unity版本(UnityVersion)：         2021.3.38f1c1
// 脚本描述(Module description):     版本辅助器
// 脚本修改(Script modification):
// **********************************************************************

namespace F.Librarie
{
    public static partial class Version
    {
        /// <summary>
        /// 版本号辅助器接口。
        /// </summary>
        public interface IVersionHelper
        {
            /// <summary>
            /// 获取游戏版本号。
            /// </summary>
            string GameVersion
            {
                get;
            }

            /// <summary>
            /// 获取内部游戏版本号。
            /// </summary>
            int InternalGameVersion
            {
                get;
            }
        }
    }
}