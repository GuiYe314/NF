// **********************************************************************
// 文件信息
// 文件名(File Name):                FrameworkLog.ILogHelper.cs
// 作者(Author):                     王云超
// 创建时间(CreateTime):             2024年5月27日 15:05:38
// Unity版本(UnityVersion)：         2021.3.38f1c1
// 脚本描述(Module description):     框架日志
// 脚本修改(Script modification):
// **********************************************************************



namespace F.Librarie
{
    public static partial class FrameworkLog
    {
        /// <summary>
        /// 游戏框架日志辅助器接口。
        /// </summary>
        public interface ILogHelper
        {
            /// <summary>
            /// 记录日志。
            /// </summary>
            /// <param name="level">游戏框架日志等级。</param>
            /// <param name="message">日志内容。</param>
            void Log(FrameworkLogLevel level, object message);
        }
    }
}