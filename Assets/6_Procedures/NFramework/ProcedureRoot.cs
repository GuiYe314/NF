// **********************************************************************
// 文件信息
// 文件名(File Name):                ProcedureRoot.cs
// 作者(Author):                     王云超
// 创建时间(CreateTime):             2024年5月29日 19:13:54
// Unity版本(UnityVersion)：         2021.3.38f1c1
// 脚本描述(Module description):     根流程
// 脚本修改(Script modification):
// **********************************************************************



using GameFramework.Fsm;
using NF;
using UnityEngine;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;


namespace Procedures.NFramework
{
    public class ProcedureRoot : ProcedureBase
    {
        public override bool UseNativeDialog { get { return true; } }


        protected override void OnInit(ProcedureOwner procedureOwner)
        {

            try
            {
                base.OnInit(procedureOwner);
                // 游戏初始化时执行
                Log.Info("进入Root流程");


            }
            catch (System.Exception e)
            {
                Debug.LogException(e);

                throw;
            }

       

        }

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
    
        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            // 每次轮询执行
            ChangeState<ProcedureRootLogin>(procedureOwner);
        }

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);
            // 每次离开这个流程时执行
        }

        protected override void OnDestroy(ProcedureOwner procedureOwner)
        {
            base.OnDestroy(procedureOwner);
            // 游戏退出时执行
        }


    }
}