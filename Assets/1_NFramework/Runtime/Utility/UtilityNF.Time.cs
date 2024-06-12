// **********************************************************************
// 文件信息
// 文件名(File Name):                UtilityNF.cs
// 作者(Author):                     填写你的名字
// 创建时间(CreateTime):             2024年5月30日 11:22:20
// Unity版本(UnityVersion)：         2021.3.38f1c1
// 脚本描述(Module description):
// 脚本修改(Script modification):
// **********************************************************************
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NF
{
    public static partial class UtilityNF
    {
        
        public static partial class Time
        {
            public static string Get_Timestamp()
            {
                // 获取当前UTC时间
                DateTime utcNow = DateTime.UtcNow;

                // 将当前UTC时间转换为Unix时间戳（毫秒）
                long unixTimeMilliseconds = new DateTimeOffset(utcNow).ToUnixTimeMilliseconds();

                // 将时间戳转换为字符串
                string timestampString = unixTimeMilliseconds.ToString();

                return timestampString;
            }
        }


    }
}