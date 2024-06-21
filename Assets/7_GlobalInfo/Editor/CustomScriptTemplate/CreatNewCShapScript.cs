
#if UNITY_EDITOR
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

public static class CreatNewCShapScript
{


    /// <summary>
    /// �����̳�MonoBehaviour�ű�
    /// </summary>
    [MenuItem("Assets/NF_Create/C# Scripts/C# MonoBehaviourScript", false, 1)]
    public static void CreatMonoBehaviourScript()
    {
        //����Ϊ���ݸ�CreateEventCSScriptAsset��action�����Ĳ���
        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0,
        ScriptableObject.CreateInstance<CreateNewCShapScriptAsset>(),
        GetSelectPathOrFallback() + "/NewNoMonoBehaviourScript.cs", null,
        "Assets/7_GlobalInfo/Editor/CustomScriptTemplate/C# Script-NewMonoBehaviourScript.txt");
    }


    /// <summary>
    /// �����̳�MonoBehaviour�ֲ��ű�
    /// </summary>
    [MenuItem("Assets/NF_Create/C# Scripts/C# MonoBehaviourScriptPartial", false, 2)]
    public static void CreatMonoBehaviourScriptPartial()
    {
        //����Ϊ���ݸ�CreateEventCSScriptAsset��action�����Ĳ���
        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0,
        ScriptableObject.CreateInstance<CreateNewCShapScriptAsset>(),
        GetSelectPathOrFallback() + "/NewNoMonoBehaviourScript.cs", null,
        "Assets/7_GlobalInfo/Editor/CustomScriptTemplate/C# Script-NewMonoBehaviourScript-partial.txt");
    }

    /// <summary>
    /// �������̳�MonoBehaviour�ű�
    /// </summary>
    [MenuItem("Assets/NF_Create/C# Scripts/C# NoMonoBehaviourScript", false, 10)]
    public static void CreatNoMonoBehaviourScript()
    {
        //����Ϊ���ݸ�CreateEventCSScriptAsset��action�����Ĳ���
        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0,
        ScriptableObject.CreateInstance<CreateNewCShapScriptAsset>(),
        GetSelectPathOrFallback() + "/NewNoMonoBehaviourScript.cs", null,
        "Assets/7_GlobalInfo/Editor/CustomScriptTemplate/C# Script-NewNoMonoBehaviourScript.txt");
    }


    /// <summary>
    /// �������̳�MonoBehaviour�ֲ��ű�
    /// </summary>
    [MenuItem("Assets/NF_Create/C# Scripts/C# NoMonoBehaviourScriptPartial", false, 11)]
    public static void CreatNoMonoBehaviourScripPartial()
    {
        //����Ϊ���ݸ�CreateEventCSScriptAsset��action�����Ĳ���
        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0,
        ScriptableObject.CreateInstance<CreateNewCShapScriptAsset>(),
        GetSelectPathOrFallback() + "/NewNoMonoBehaviourScript.cs", null,
        "Assets/7_GlobalInfo/Editor/CustomScriptTemplate/C# Script-NewNoMonoBehaviourScript-partial.txt");
    }


    /// <summary>
    /// �����̳�GF���̽ű�
    /// </summary>
    [MenuItem("Assets/NF_Create/C# Scripts/C# GFProcedure", false, 50)]
    public static void CreatGFProcedure()
    {
        //����Ϊ���ݸ�CreateEventCSScriptAsset��action�����Ĳ���
        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0,
        ScriptableObject.CreateInstance<CreateNewCShapScriptAsset>(),
        GetSelectPathOrFallback() + "/NewNoMonoBehaviourScript.cs", null,
        "Assets/7_GlobalInfo/Editor/CustomScriptTemplate/C# Script-NewGFProcedure.txt");
    }

    /// <summary>
    /// �����̳�GF����ű�
    /// </summary>
    [MenuItem("Assets/NF_Create/C# Scripts/C# GFComponent", false, 51)]
    public static void CreatGFComponent()
    {
        //����Ϊ���ݸ�CreateEventCSScriptAsset��action�����Ĳ���
        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0,
        ScriptableObject.CreateInstance<CreateNewCShapScriptAsset>(),
        GetSelectPathOrFallback() + "/NewNoMonoBehaviourScript.cs", null,
        "Assets/7_GlobalInfo/Editor/CustomScriptTemplate/C# Script-NewGFComponent.txt");
    }



    /// <summary>
    /// ȡ��Ҫ�����ļ���·��
    /// </summary>
    /// <returns></returns>
    public static string GetSelectPathOrFallback()
    {
        string path = "Assets";
        //����ѡ�е���Դ�Ի��·��
        //Selection.GetFiltered�ǹ���ѡ���ļ����ļ����µ����壬assets��ʾֻ����ѡ�������
        foreach (UnityEngine.Object obj in Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets))
        {
            path = AssetDatabase.GetAssetPath(obj);
            if (!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                path = Path.GetDirectoryName(path);
                break;
            }
        }
        return path;
    }

    /// <summary>
    /// �����ű��ļ���ί����
    /// </summary>
    class CreateNewCShapScriptAsset : EndNameEditAction
    {
        public override void Action(int instanceId, string pathName, string resourceFile)
        {
            UnityEngine.Object obj = CreateScriptAssetFromTemplate(pathName, resourceFile); //������Դ
            ProjectWindowUtil.ShowCreatedAsset(obj); //������ʾ��Դ
        }

        internal static UnityEngine.Object CreateScriptAssetFromTemplate(string pathName, string resourceFile)
        {
            string fullPath = Path.GetFullPath(pathName); //��ȡҪ������Դ�ľ���·��
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(pathName); //��ȡ�ļ�����������չ��

            var textAsset = EditorGUIUtility.Load(resourceFile) as TextAsset;
            string resourceFileText = textAsset.text;

            //����ʵ���Զ����һЩ����  
            resourceFileText = resourceFileText.Replace("#COMPANY#", PlayerSettings.companyName);
            resourceFileText = resourceFileText.Replace("#AUTHOR#", "Passion");
            resourceFileText = resourceFileText.Replace("#VERSION#", "1.0");
            resourceFileText = resourceFileText.Replace("#UNITYVERSION#", Application.unityVersion);
            resourceFileText = resourceFileText.Replace("#CREATETIME#", System.DateTime.Now.ToString("F"));

            resourceFileText = Regex.Replace(resourceFileText, "#SCRIPTNAME#", fileNameWithoutExtension.Split('.')[0]); //��ģ�����е������滻���㴴�����ļ���
            bool encoderShouldEmitUTF8Identifier = true; //����ָ���Ƿ��ṩ Unicode �ֽ�˳����
            bool throwOnInvalidBytes = false; //�Ƿ��ڼ�⵽��Ч�ı���ʱ�����쳣
            UTF8Encoding encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier, throwOnInvalidBytes);
            bool append = false;
            StreamWriter streamWriter = new StreamWriter(fullPath, append, encoding); //д���ļ�
            streamWriter.Write(resourceFileText);
            streamWriter.Close();
            AssetDatabase.ImportAsset(pathName); //ˢ����Դ������
            AssetDatabase.Refresh();
            return AssetDatabase.LoadAssetAtPath(pathName, typeof(UnityEngine.Object));
        }
    }
}

#endif