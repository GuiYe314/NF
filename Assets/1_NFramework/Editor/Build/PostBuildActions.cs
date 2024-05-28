// **********************************************************************
// �ļ���Ϣ
// �ļ���(File Name):                 PostBuildActions.cs
// ����(Author):                     ���Ƴ�
// ����ʱ��(CreateTime):             2024��5��23�� 11:03:36
// Unity�汾(UnityVersion)��         2021.3.38f1c1
// �ű�����(Module description):     �����ִ�нű�
// �ű��޸�(Script modification):
// **********************************************************************

using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;

public class PostBuildActions
{
    /// <summary>
    /// ������WebGl�����滻������ֻ�����ʾ����
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