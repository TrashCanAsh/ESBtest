using ESBtest.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESBtest.Common
{
    public class FileControl
    {
        private static string GetFileType(string filepath)
        {
            string[] s = filepath.Split('.');
            return string.IsNullOrEmpty(s[1]) ? null : s[1];
        }

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
                    SampleModel sample = new SampleModel
                    {
                        SampleName = strsplit[0],
                        Category = strsplit[1],
                        SamplingTime = strsplit[2],
                        Longitude = strsplit[3],
                        Latitude = strsplit[4]
                    };
                    sList.Add(sample);
                }
                strFile.Close();
            }
            return sList;
        }
    }
}
