using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Web;

namespace openGPS_Common
{
    public class Filelog
    {

        /// <summary>
        /// 程序运行日志（文本文件）
        /// </summary>
        /// <param name="log">记录的内容，需要保持格式后面带有换行“\r\n”</param>
        //public static void SystemRunLog(string log)
        //{
        //    try
        //    {
        //        string assemblyFilePath = Assembly.GetExecutingAssembly().Location;
        //        string assemblyDirPath = Path.GetDirectoryName(assemblyFilePath);
        //        //string configFile = assemblyDirPath + "\\App.config";

        //        string path = assemblyDirPath + "/LogFiles";
        //        CreateFolder(path);

        //        string result = log + Environment.NewLine;
        //        StreamWriter sw = new StreamWriter(path, true);
        //        sw.Write(result);
        //        sw.Close();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        /// <summary>
        /// 创建文件夹
        /// 参数：path 文件夹路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool CreateFolder(string path)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    return true;
                }
                if (!Directory.Exists(path.Substring(0, path.LastIndexOf("\\"))))
                { //若路径中无“\”则表示路径错误
                    return false;
                }
                else
                {
                    //创建文件夹
                    DirectoryInfo dirInfo = Directory.CreateDirectory(path);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        //创建文件
        //参数：path 文件路径
        public static void CreateFile(string path)
        {
            try
            {
                if (CreateFolder(path.Substring(0, path.LastIndexOf("\\"))))
                {
                    if (!File.Exists(path))
                    {
                        FileStream fs = File.Create(path);
                        fs.Close();
                    }
                }
            }
            catch (Exception)
            {
                return;
            }

        }

        //删除文件
        //参数：path 文件夹路径
        public void DeleteFile(string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    return;
                }
                else
                {
                    File.Delete(path);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        //写文件
        //参数：path 文件夹路径 、content要写的内容
        public static void WriteFile(string path, string content)
        {
            try
            {
                if (!File.Exists(path))
                {
                    CreateFile(path);
                }
                FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(content);
                sw.Close();
            }
            catch (Exception)
            {
                return;
            }
        }

        /// <summary>
        /// 将即时日志保存入日志文件
        /// </summary>
        public static void WriteLogFile(string content)
        {
            string assemblyFilePath = Assembly.GetExecutingAssembly().Location;
            string assemblyDirPath = Path.GetDirectoryName(assemblyFilePath);
            string logPath = assemblyDirPath + "\\FileLog";
            WriteLogFile(logPath, content);
        }

        private static object objLock = "";
        /// <summary>
        /// 将即时日志保存入日志文件
        /// </summary>
        public static void WriteLogFile(string directoryPath, string content)
        {
            if (!Directory.Exists(directoryPath))
                CreateFolder(directoryPath);
            //写入新的文件
            string filePath = directoryPath + "\\" + DateTime.Now.Date.ToString("yyyyMMdd") + ".log";
            lock (objLock)
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write))
                {
                    StreamWriter sw = new StreamWriter(fs);
                    //sw.WriteLine("{0} ,{1}", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),content);
                    sw.WriteLine(content);
                    sw.Close();
                    fs.Close();
                }
            }
        }
    }


    public class FileLog2
    {
        /// <summary>
        /// 创建文件夹
        /// 参数：path 文件夹路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool CreateFolder(string path)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    return true;
                }
                if (!Directory.Exists(path.Substring(0, path.LastIndexOf("\\"))))
                { //若路径中无“\”则表示路径错误
                    return false;
                }
                else
                {
                    //创建文件夹
                    DirectoryInfo dirInfo = Directory.CreateDirectory(path);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //创建文件
        //参数：path 文件路径
        public static void CreateFile(string path)
        {
            try
            {
                if (CreateFolder(path.Substring(0, path.LastIndexOf("\\"))))
                {
                    if (!File.Exists(path))
                    {
                        FileStream fs = File.Create(path);
                        fs.Close();
                    }
                }
            }
            catch (Exception)
            {
                return;
            }

        }

        //删除文件
        //参数：path 文件夹路径
        public void DeleteFile(string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    return;
                }
                else
                {
                    File.Delete(path);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        //写文件
        //参数：path 文件夹路径 、content要写的内容
        public static void WriteFile(string path, string content)
        {
            try
            {
                if (!File.Exists(path))
                {
                    CreateFile(path);
                }
                FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(content);
                sw.Close();
            }
            catch (Exception ex)
            {
                return;
            }
        }

        /// <summary>
        /// 将即时日志保存入日志文件
        /// </summary>
        public static void WriteLogFile(string content)
        {
            string assemblyDirPath = Assembly.GetExecutingAssembly().Location;
            //string assemblyDirPath = Path.GetDirectoryName(assemblyFilePath);
            //string assemblyDirPath = HttpRuntime.AppDomainAppPath.ToString();

            string logPath = assemblyDirPath + "\\App_Data\\Log";
            content = DateTime.Now.ToString("HH:mm:ss") + " ,  " + content;
            WriteLogFile(logPath, content, DateTime.Now.ToString("yyyyMMdd") + "_log.txt");
        }

        private static object objLock = "";
        /// <summary>
        /// 将即时日志保存入日志文件
        /// </summary>
        public static void WriteLogFile(string directoryPath, string content, string fileName)
        {
            //写入新的文件
            string filePath = "";
            if (directoryPath.Trim().Trim('/').Trim('\\') == "")
            {
                filePath = fileName;
            }
            else
            {
                if (!Directory.Exists(directoryPath))
                    CreateFolder(directoryPath);
                filePath = directoryPath + "\\" + fileName;
            }
            lock (objLock)
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write))
                {
                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine(content);
                    sw.Close();
                    fs.Close();
                }
            }
        }
    }
}
