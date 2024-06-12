// **********************************************************************
// 文件信息
// 文件名(File Name):                UtilityNF.Path.cs
// 作者(Author):                     王云超
// 创建时间(CreateTime):             2024年5月30日 10:09:30
// Unity版本(UnityVersion)：         2021.3.38f1c1
// 脚本描述(Module description):     路径相关的实用函数
// 脚本修改(Script modification):
// **********************************************************************
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace NF
{
	public static partial class UtilityNF
	{
		public static partial class Path
		{

			/// <summary>
			/// 向上截取路径
			/// </summary>
			/// <param name="pahtStr">路径</param>
			/// <param name="degree">向上截取数量</param>
			/// <returns></returns>
			public static string GetParentDirectory(string pahtStr, int degree)
			{

				for (int i = 0; i < degree; i++)
				{
					DirectoryInfo pathInfo = new DirectoryInfo(pahtStr);
					pahtStr = pathInfo.Parent.FullName;
				}
				return pahtStr;
			}
		}
	}
}