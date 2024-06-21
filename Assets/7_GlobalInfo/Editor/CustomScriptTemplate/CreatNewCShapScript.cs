
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
    /// 创建继承MonoBehaviour脚本
    /// </summary>
    [MenuItem("Assets/NF_Create/C# Scripts/C# MonoBehaviourScript", false, 1)]
    public static void CreatMonoBehaviourScript()
    {
        //参数为传递给CreateEventCSScriptAsset类action方法的参数
        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0,
        ScriptableObject.CreateInstance<CreateNewCShapScriptAsset>(),
        GetSelectPathOrFallback() + "/NewNoMonoBehaviourScript.cs", null,
        "Assets/7_GlobalInfo/Editor/CustomScriptTemplate/C# Script-NewMonoBehaviourScript.txt");
    }


    /// <summary>
    /// 创建继承MonoBehaviour局部脚本
    /// </summary>
    [MenuItem("Assets/NF_Create/C# Scripts/C# MonoBehaviourScriptPartial", false, 2)]
    public static void CreatMonoBehaviourScriptPartial()
    {
        //参数为传递给CreateEventCSScriptAsset类action方法的参数
        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0,
        ScriptableObject.CreateInstance<CreateNewCShapScriptAsset>(),
        GetSelectPathOrFallback() + "/NewNoMonoBehaviourScript.cs", null,
        "Assets/7_GlobalInfo/Editor/CustomScriptTemplate/C# Script-NewMonoBehaviourScript-partial.txt");
    }

    /// <summary>
    /// 创建不继承MonoBehaviour脚本
    /// </summary>
    [MenuItem("Assets/NF_Create/C# Scripts/C# NoMonoBehaviourScript", false, 10)]
    public static void CreatNoMonoBehaviourScript()
    {
        //参数为传递给CreateEventCSScriptAsset类action方法的参数
        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0,
        ScriptableObject.CreateInstance<CreateNewCShapScriptAsset>(),
        GetSelectPathOrFallback() + "/NewNoMonoBehaviourScript.cs", null,
        "Assets/7_GlobalInfo/Editor/CustomScriptTemplate/C# Script-NewNoMonoBehaviourScript.txt");
    }


    /// <summary>
    /// 创建不继承MonoBehaviour局部脚本
    /// </summary>
    [MenuItem("Assets/NF_Create/C# Scripts/C# NoMonoBehaviourScriptPartial", false, 11)]
    public static void CreatNoMonoBehaviourScripPartial()
    {
        //参数为传递给CreateEventCSScriptAsset类action方法的参数
        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0,
        ScriptableObject.CreateInstance<CreateNewCShapScriptAsset>(),
        GetSelectPathOrFallback() + "/NewNoMonoBehaviourScript.cs", null,
        "Assets/7_GlobalInfo/Editor/CustomScriptTemplate/C# Script-NewNoMonoBehaviourScript-partial.txt");
    }


    /// <summary>
    /// 创建继承GF流程脚本
    /// </summary>
    [MenuItem("Assets/NF_Create/C# Scripts/C# GFProcedure", false, 50)]
    public static void CreatGFProcedure()
    {
        //参数为传递给CreateEventCSScriptAsset类action方法的参数
        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0,
        ScriptableObject.CreateInstance<CreateNewCShapScriptAsset>(),
        GetSelectPathOrFallback() + "/NewNoMonoBehaviourScript.cs", null,
        "Assets/7_GlobalInfo/Editor/CustomScriptTemplate/C# Script-NewGFProcedure.txt");
    }

    /// <summary>
    /// 创建继承GF组件脚本
    /// </summary>
    [MenuItem("Assets/NF_Create/C# Scripts/C# GFComponent", false, 51)]
    public static void CreatGFComponent()
    {
        //参数为传递给CreateEventCSScriptAsset类action方法的参数
        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0,
        ScriptableObject.CreateInstance<CreateNewCShapScriptAsset>(),
        GetSelectPathOrFallback() + "/NewNoMonoBehaviourScript.cs", null,
        "Assets/7_GlobalInfo/Editor/CustomScriptTemplate/C# Script-NewGFComponent.txt");
    }



    /// <summary>
    /// 取得要创建文件的路径
    /// </summary>
    /// <returns></returns>
    public static string GetSelectPathOrFallback()
    {
        string path = "Assets";
        //遍历选中的资源以获得路径
        //Selection.GetFiltered是过滤选择文件或文件夹下的物体，assets表示只返回选择对象本身
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
    /// 创建脚本文件的委托类
    /// </summary>
    class CreateNewCShapScriptAsset : EndNameEditAction
    {
        public override void Action(int instanceId, string pathName, string resourceFile)
        {
            UnityEngine.Object obj = CreateScriptAssetFromTemplate(pathName, resourceFile); //创建资源
            ProjectWindowUtil.ShowCreatedAsset(obj); //高亮显示资源
        }

        internal static UnityEngine.Object CreateScriptAssetFromTemplate(string pathName, string resourceFile)
        {
            string fullPath = Path.GetFullPath(pathName); //获取要创建资源的绝对路径
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(pathName); //获取文件名，不含扩展名

            var textAsset = EditorGUIUtility.Load(resourceFile) as TextAsset;
            string resourceFileText = textAsset.text;

            //这里实现自定义的一些规则  
            resourceFileText = resourceFileText.Replace("#COMPANY#", PlayerSettings.companyName);
            resourceFileText = resourceFileText.Replace("#AUTHOR#", "Passion");
            resourceFileText = resourceFileText.Replace("#VERSION#", "1.0");
            resourceFileText = resourceFileText.Replace("#UNITYVERSION#", Application.unityVersion);
            resourceFileText = resourceFileText.Replace("#CREATETIME#", System.DateTime.Now.ToString("F"));

            resourceFileText = Regex.Replace(resourceFileText, "#SCRIPTNAME#", fileNameWithoutExtension.Split('.')[0]); //将模板类中的类名替换成你创建的文件名
            bool encoderShouldEmitUTF8Identifier = true; //参数指定是否提供 Unicode 字节顺序标记
            bool throwOnInvalidBytes = false; //是否在检测到无效的编码时引发异常
            UTF8Encoding encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier, throwOnInvalidBytes);
            bool append = false;
            StreamWriter streamWriter = new StreamWriter(fullPath, append, encoding); //写入文件
            streamWriter.Write(resourceFileText);
            streamWriter.Close();
            AssetDatabase.ImportAsset(pathName); //刷新资源管理器
            AssetDatabase.Refresh();
            return AssetDatabase.LoadAssetAtPath(pathName, typeof(UnityEngine.Object));
        }
    }
}

#endif