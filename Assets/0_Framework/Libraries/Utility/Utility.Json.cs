// **********************************************************************
// �ļ���Ϣ
// �ļ���(File Name):                Utility.Json.cs
// ����(Author):                     ���Ƴ�
// ����ʱ��(CreateTime):             2024��5��27�� 16:45:08
// Unity�汾(UnityVersion)��         2021.3.38f1c1
// �ű�����(Module description):     Json������
// �ű��޸�(Script modification):
// **********************************************************************

using System;

namespace F.Librarie
{

    public static partial class Utility
    {
        /// <summary>
        /// JSON ��ص�ʵ�ú�����
        /// </summary>
        public static partial class Json
        {
            private static IJsonHelper s_JsonHelper = null;

            /// <summary>
            /// ���� JSON ��������
            /// </summary>
            /// <param name="jsonHelper">Ҫ���õ� JSON ��������</param>
            public static void SetJsonHelper(IJsonHelper jsonHelper)
            {
                s_JsonHelper = jsonHelper;
            }

            /// <summary>
            /// ���������л�Ϊ JSON �ַ�����
            /// </summary>
            /// <param name="obj">Ҫ���л��Ķ���</param>
            /// <returns>���л���� JSON �ַ�����</returns>
            public static string ToJson(object obj)
            {
                if (s_JsonHelper == null)
                {
                    throw new FrameworkException("JSON helper is invalid.");
                }

                try
                {
                    return s_JsonHelper.ToJson(obj);
                }
                catch (Exception exception)
                {
                    if (exception is FrameworkException)
                    {
                        throw;
                    }

                    throw new FrameworkException(Text.Format("Can not convert to JSON with exception '{0}'.", exception), exception);
                }
            }

            /// <summary>
            /// �� JSON �ַ��������л�Ϊ����
            /// </summary>
            /// <typeparam name="T">�������͡�</typeparam>
            /// <param name="json">Ҫ�����л��� JSON �ַ�����</param>
            /// <returns>�����л���Ķ���</returns>
            public static T ToObject<T>(string json)
            {
                if (s_JsonHelper == null)
                {
                    throw new FrameworkException("JSON helper is invalid.");
                }

                try
                {
                    return s_JsonHelper.ToObject<T>(json);
                }
                catch (Exception exception)
                {
                    if (exception is FrameworkException)
                    {
                        throw;
                    }

                    throw new FrameworkException(Text.Format("Can not convert to object with exception '{0}'.", exception), exception);
                }
            }

            /// <summary>
            /// �� JSON �ַ��������л�Ϊ����
            /// </summary>
            /// <param name="objectType">�������͡�</param>
            /// <param name="json">Ҫ�����л��� JSON �ַ�����</param>
            /// <returns>�����л���Ķ���</returns>
            public static object ToObject(Type objectType, string json)
            {
                if (s_JsonHelper == null)
                {
                    throw new FrameworkException("JSON helper is invalid.");
                }

                if (objectType == null)
                {
                    throw new FrameworkException("Object type is invalid.");
                }

                try
                {
                    return s_JsonHelper.ToObject(objectType, json);
                }
                catch (Exception exception)
                {
                    if (exception is FrameworkException)
                    {
                        throw;
                    }

                    throw new FrameworkException(Text.Format("Can not convert to object with exception '{0}'.", exception), exception);
                }
            }
        }
    }

}
