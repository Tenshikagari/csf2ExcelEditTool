using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Collections;

namespace INIProcess
{
    internal static class INIPorcess
    {
        public static bool SectionCheck(string section,string filePath)
        {
            var buffer = new StringBuilder();
            GetPrivateProfileSectionNames(buffer,512, filePath);
            string temp = buffer.ToString();
            string[] sectionArray=temp.Split('\0',StringSplitOptions.RemoveEmptyEntries);
            foreach (string sectionName in sectionArray){
                if (sectionName == section)
                {
                    return true;
                }
            }
            Console.WriteLine("There is a mistake in Section of the INI file. " +
                "You can delete the current INI file and generate a new one or check the spell of sections");
            return false;
        }
        public static string PathReader(string pathtype, string defaultvalue, string filePath)
        {
            try
            {
                StringBuilder targpath = new StringBuilder("Test");
                string section = "Parameters";
                int size = 200;
                GetPrivateProfileString(section, pathtype, defaultvalue, targpath, size, filePath);
                return targpath.ToString();
            }
            catch(Exception ex)
            {
                Console.WriteLine("An error occured"+ex.Message);
                return "NULL";
            }
        }
        public static void INIGenerate(string DcsfPath,string DexcelPath,string DdiffPath,string FilePath)
        {
            WritePrivateProfileString("Parameters", "csfPath", DcsfPath, FilePath);
            WritePrivateProfileString("Parameters", "excelPath", DexcelPath, FilePath);
            WritePrivateProfileString("Parameters", "diffPath", DdiffPath, FilePath);
        }
        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(string section, string key, string def,
                                                          StringBuilder retVal, int size,
                                                          string filePath);
        [DllImport("kernel32.dll")]
        private static extern void WritePrivateProfileString(string section, string key, string def, string filePath);
        [DllImport("kernel32.dll")]
        private static extern uint GetPrivateProfileSectionNames(StringBuilder lpszReturnBuffer,uint size,string filePath);
    }
}