// **********************************************************************
// 文件信息
// 文件名(File Name):                 PostBuildActions.cs
// 作者(Author):                     王云超
// 创建时间(CreateTime):             2024年5月23日 11:03:36
// Unity版本(UnityVersion)：         2021.3.38f1c1
// 脚本描述(Module description):     打包后执行脚本
// 脚本修改(Script modification):
// **********************************************************************

using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;

public class PostBuildActions
{
    /// <summary>
    /// 打包后对WebGl进行替换，解决手机端显示问题
    /// </summary>
    /// <param name="target"></param>
    /// <param name="targetPath"></param>
    [PostProcessBuild]
    public static void OnPostProcessBuild(BuildTarget target, string targetPath)
    {
        if (target != BuildTarget.WebGL) return;

        var path = Path.Combine(targetPath, "Build/UnityLoader.js");
        var text = File.ReadAllText(path);
        text = text.Replace("UnityLoader.SystemInfo.mobile", "false");
        text = text.Replace("[\"Edge\", \"Firefox\", \"Chrome\", \"Safari\"].indexOf(UnityLoader.SystemInfo.browser) == -1", "false");
        File.WriteAllText(path, text);
    }
}