using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Libs.Common
{
    public class FileDbHelper : IFileDbHelper
    {
        #region for di extensions

        private static IFileDbHelper _resolve()
        {
            var debugHelper = new FileDbHelper();
            return debugHelper;
        }

        public static Func<IFileDbHelper> Resolve = _resolve;

        #endregion

        public IList<T> Read<T>(string path)
        {
            string jsonValue = ReadFile(path);
            if (string.IsNullOrWhiteSpace(jsonValue))
            {
                return new List<T>();
            }
            var list = jsonValue.FromJson<List<T>>();
            if (list == null)
            {
                return new List<T>();
            }
            return list;
        }

        public void Save<T>(string path, IList<T> list)
        {
            IList<T> listFix = new List<T>();
            if (list != null)
            {
                listFix = list;
            }
            var jsonValue = listFix.ToJson(true);
            SaveFile(path, jsonValue);
        }

        //helpers
        private string ReadFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return null;
            }
            var content = File.ReadAllText(filePath);
            return content;
        }
        private void SaveFile(string filePath, string content)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentNullException("filePath");
            }

            string dirPath = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(dirPath))
            {
                if (string.IsNullOrWhiteSpace(dirPath))
                {
                    throw new ArgumentException("非法的路径:" + filePath);
                }
                Directory.CreateDirectory(dirPath);
            }
            File.WriteAllText(filePath, content, Encoding.UTF8);
        }
    }

    public interface IFileDbHelper
    {
        /// <summary>
        /// 读取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        IList<T> Read<T>(string path);

        /// <summary>
        /// 保存到文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="list"></param>
        void Save<T>(string path, IList<T> list);
    }
}
