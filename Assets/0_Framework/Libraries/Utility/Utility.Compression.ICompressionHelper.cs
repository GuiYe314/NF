// **********************************************************************
// �ļ���Ϣ
// �ļ���(File Name):                Utility.Compression.ICompressionHelper.cs
// ����(Author):                     ���Ƴ�
// ����ʱ��(CreateTime):             2024��5��27�� 16:45:08
// Unity�汾(UnityVersion)��         2021.3.38f1c1
// �ű�����(Module description):     
// �ű��޸�(Script modification):
// **********************************************************************
using System;
using System.IO;


namespace F.Librarie
{
    public static partial class Utility
    {
        /// <summary>
        /// ѹ����ѹ����ص�ʵ�ú�����
        /// </summary>
        public static partial class Compression
        {
            /// <summary>
            /// ѹ����ѹ���������ӿڡ�
            /// </summary>
            public interface ICompressionHelper
            {
                /// <summary>
                /// ѹ�����ݡ�
                /// </summary>
                /// <param name="bytes">Ҫѹ�������ݵĶ���������</param>
                /// <param name="offset">Ҫѹ�������ݵĶ���������ƫ�ơ�</param>
                /// <param name="length">Ҫѹ�������ݵĶ��������ĳ��ȡ�</param>
                /// <param name="compressedStream">ѹ��������ݵĶ���������</param>
                /// <returns>�Ƿ�ѹ�����ݳɹ���</returns>
                bool Compress(byte[] bytes, int offset, int length, Stream compressedStream);

                /// <summary>
                /// ѹ�����ݡ�
                /// </summary>
                /// <param name="stream">Ҫѹ�������ݵĶ���������</param>
                /// <param name="compressedStream">ѹ��������ݵĶ���������</param>
                /// <returns>�Ƿ�ѹ�����ݳɹ���</returns>
                bool Compress(Stream stream, Stream compressedStream);

                /// <summary>
                /// ��ѹ�����ݡ�
                /// </summary>
                /// <param name="bytes">Ҫ��ѹ�������ݵĶ���������</param>
                /// <param name="offset">Ҫ��ѹ�������ݵĶ���������ƫ�ơ�</param>
                /// <param name="length">Ҫ��ѹ�������ݵĶ��������ĳ��ȡ�</param>
                /// <param name="decompressedStream">��ѹ��������ݵĶ���������</param>
                /// <returns>�Ƿ��ѹ�����ݳɹ���</returns>
                bool Decompress(byte[] bytes, int offset, int length, Stream decompressedStream);

                /// <summary>
                /// ��ѹ�����ݡ�
                /// </summary>
                /// <param name="stream">Ҫ��ѹ�������ݵĶ���������</param>
                /// <param name="decompressedStream">��ѹ��������ݵĶ���������</param>
                /// <returns>�Ƿ��ѹ�����ݳɹ���</returns>
                bool Decompress(Stream stream, Stream decompressedStream);
            }
        }
    }
}