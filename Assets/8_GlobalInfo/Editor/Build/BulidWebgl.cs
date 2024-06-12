// **********************************************************************
// 文件信息
// 文件名(File Name):                BulidWebgl.cs
// 作者(Author):                     王云超
// 创建时间(CreateTime):             2024年6月3日 9:50:42
// Unity版本(UnityVersion)：         2021.3.38f1c1
// 脚本描述(Module description):     webgl 发布脚本
// 脚本修改(Script modification):
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR && UNITY_WEBGL
namespace NF
{
    public class BulidWebgl
    {
        [MenuItem("NF/Webgl/CompilationWebGL")]
        static public void AddWebGL()
        {
            //PlayerSettings.WebGL.emscriptenArgs = "-s \"BINARYEN_TRAP_MODE = 'clamp'\"";
        }

    }
}
#endif