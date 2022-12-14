using ESBtest.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESBtest.Common
{
    public class FileControl
    {
        /// <summary>
        /// 识别文件类型
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        private static string GetFileType(string filepath)
        {
            string[] s = filepath.Split('.');
            return string.IsNullOrEmpty(s[1]) ? null : s[1];
        }
        /// <summary>
        /// 读取文件内容并存到样品信息列表中
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static List<SampleModel> ReadFile(string filepath)
        {
            List<SampleModel> sList = new List<SampleModel>();
            StreamReader strFile = new StreamReader(filepath);
            if(strFile!=null)
            {
                while (!strFile.EndOfStream)
                {
                    string str = strFile.ReadLine();
                    string type = GetFileType(filepath);
                    string[] strsplit = null;
                    switch (type)
                    {
                        case "txt":
                            strsplit = str.Split(' ');
                            break;
                        case "csv":
                            strsplit = str.Split(',');
                            break;
                        default:
                            break;
                    }
                    Console.WriteLine(strsplit.Count());
                    SampleModel sample = new SampleModel
                    {
                        SampleName = strsplit[0],
                        Category = strsplit[1],
                        SamplingDate = strsplit[2],
                        Longitude = strsplit[3],
                        Latitude = strsplit[4],
                        State = int.Parse(strsplit[5]),
                        Comment = strsplit.Count() > 6 ? strsplit[6] : "",
                    };
                    switch (sample.State)
                    {
                        case 0:
                            sample.StateStr = "unknown";
                            break;
                        case 1:
                            sample.StateStr = "in stock";
                            break;
                        case 2:
                            sample.StateStr = "locked";
                            break;
                        case 3:
                            sample.StateStr = "out on loan";
                            break;
                        default:
                            break;
                    }
                    sList.Add(sample);
                }
            }
            strFile.Close();
            return sList;
        }
        /// <summary>
        /// 将选中的样品信息写入到目标文件路径中
        /// </summary>
        /// <param name="filepath">目标文件路径</param>
        /// <param name="sList">选中的样品信息</param>
        /// <returns></returns>
        public static bool WriteFile(string filepath, ObservableCollection<SampleModel> sList)
        {
            if(sList != null && !string.IsNullOrEmpty(filepath))
            {
                try
                {
                    string spliter = "";
                    switch (GetFileType(filepath))
                    {
                        case "txt":
                            spliter = " ";
                            break;
                        case "csv":
                            spliter = ",";
                            break;
                        default:
                            break;
                    }
                    StreamWriter sw = new StreamWriter(filepath);
                    string write = "";
                    foreach (SampleModel sample in sList)
                    {
                        write += sample.SampleName + spliter + sample.Category + spliter + sample.SamplingDate + spliter + sample.Longitude
                            + spliter + sample.Latitude + spliter + sample.State + "\n";
                    }
                    Console.WriteLine(write);
                    sw.Write(write);
                    sw.Flush();
                    sw.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            return false;
        }
    }
}
