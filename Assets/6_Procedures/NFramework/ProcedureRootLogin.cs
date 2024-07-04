// **********************************************************************
// �ļ���Ϣ
// �ļ���(File Name):                ProcedureRootLogin.cs
// ����(Author):                     ���Ƴ�
// ����ʱ��(CreateTime):             2024��5��29�� 19:13:54
// Unity�汾(UnityVersion)��         2021.3.38f1c1
// �ű�����(Module description):     ��¼����
// �ű��޸�(Script modification):
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

            Debug.Log("���е�¼");

            HttpNetController.PostDataStream("http://127.0.0.1:2080");


        }


    }

}
