// **********************************************************************
// 文件信息
// 文件名(File Name):                GameFrameworkConfigs.cs
// 作者(Author):                     王云超
// 创建时间(CreateTime):             2024年6月17日 10:00:36
// Unity版本(UnityVersion)：         2021.3.39f1
// 脚本描述(Module description):     配置文件地址
// 脚本修改(Script modification):
// **********************************************************************

using UnityEngine;
using UnityGameFramework.Editor.ResourceTools;
using UnityGameFramework.Editor;
using GameFramework;
using System.IO;
using Unity.VisualScripting.FullSerializer;

namespace NF.Editor
{
    public static class GameFrameworkConfigs
    {
        [BuildSettingsConfigPath]
        public static string BuildSettingsConfig = Utility.Path.GetRegularPath(Path.Combine(Application.dataPath, "5_AllResources/FrameworkResources/Configs/BuildSettings.xml"));

        [ResourceCollectionConfigPath]
        public static string ResourceCollectionConfig = Utility.Path.GetRegularPath(Path.Combine(Application.dataPath, "5_AllResources/FrameworkResources/Configs/ResourceCollection.xml"));

        [ResourceEditorConfigPath]
        public static string ResourceEditorConfig = Utility.Path.GetRegularPath(Path.Combine(Application.dataPath, "5_AllResources/FrameworkResources/Configs/ResourceEditor.xml"));

        [ResourceBuilderConfigPath]
        public static string ResourceBuilderConfig = Utility.Path.GetRegularPath(Path.Combine(Application.dataPath, "5_AllResources/FrameworkResources/Configs/ResourceBuilder.xml"));
    }
}