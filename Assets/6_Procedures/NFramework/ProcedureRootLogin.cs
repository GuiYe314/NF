// **********************************************************************
// 文件信息
// 文件名(File Name):                ProcedureRootLogin.cs
// 作者(Author):                     王云超
// 创建时间(CreateTime):             2024年5月29日 19:13:54
// Unity版本(UnityVersion)：         2021.3.38f1c1
// 脚本描述(Module description):     登录流程
// 脚本修改(Script modification):
// **********************************************************************


using NF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace Procedures.NFramework
{
    public class ProcedureRootLogin : ProcedureBase
    {
        public override bool UseNativeDialog { get { return true; } }

        protected override void OnInit(ProcedureOwner procedureOwner)
        {

            HttpNetController.Init();

            Debug.Log("进行登录");

            HttpNetController.PostDataStream("http://127.0.0.1:2080");


        }


    }

}
