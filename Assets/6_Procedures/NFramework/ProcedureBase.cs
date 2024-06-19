// **********************************************************************
// 文件信息
// 文件名(File Name):                ProcedureBase.cs
// 作者(Author):                     王云超
// 创建时间(CreateTime):             2024年5月29日 19:22:02
// Unity版本(UnityVersion)：         2021.3.38f1c1
// 脚本描述(Module description):     框架流程基类
// 脚本修改(Script modification):
// **********************************************************************


namespace Procedures.NFramework
{
    public abstract class ProcedureBase : GameFramework.Procedure.ProcedureBase
    {
        // 获取流程是否使用原生对话框
        // 在一些特殊的流程（如游戏逻辑对话框资源更新完成前的流程）中，可以考虑调用原生对话框进行消息提示行为
        public abstract bool UseNativeDialog
        {
            get;
        }
    }
}