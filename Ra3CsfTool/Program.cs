using System;
using System.IO;
using INIProcess;

namespace Ra3CsfTool
{
    using OfficeOpenXml;
    using System.Configuration;
    class Program
    {


       static string tip = @"
【csf excel 转换器 v1.00】 by 奏  如遇到什么bug 请反馈 幻次元制作组成员

使用方式
1  把本工具放置到和csf同目录的文件夹下。
2  输入 【1】 回车进行 excel -> csf转换  (excel 转 csf会直接覆盖)
3  输入 【2】 回车进行 csf->excel的转换  (csf 转 excel 不会覆盖请手动删除)

注意事项
1 本工具只支持 xlsx格式 不支持xls

";
        static void Main(string[] args)
        {


            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

 
            string currentPath = System.IO.Directory.GetCurrentDirectory();



            string INIPath=currentPath+@"\path.INI";   //INI路径
            string DefaultcsfPath = currentPath + @"\GameStrings.csf";
            string DefaultexcelPath = currentPath + @"\csf.xlsx";
            string DefaultdiffPath = currentPath + @"\csf.diff";
            Console.WriteLine(INIPath);
            if(!(File.Exists(INIPath)))//检测INI文件是否存在
            {
                Console.WriteLine("INI File did not exist.Will Generate an INI for you");//不存在则创建
                INIPorcess.INIGenerate(DefaultcsfPath,DefaultexcelPath,DefaultdiffPath,INIPath);
                return;
            }
            if (INIPorcess.SectionCheck("Parameters", INIPath) == false)//检查INI格式
            {
                return;
            }
            string csfPath = INIPorcess.PathReader("csfPath", DefaultcsfPath, INIPath);//读取csf路径
            string excelPath = INIPorcess.PathReader("excelPath", DefaultexcelPath, INIPath);//读取excel路径
            string diffPath = INIPorcess.PathReader("diffPath", DefaultdiffPath, INIPath);
            string csfName = System.IO.Path.GetFileName(csfPath);
            string workDict = csfPath.Remove(csfPath.Length-1-csfName.Length);
            Console.WriteLine("CSF:" + csfPath);
            Console.WriteLine("Excel:"+excelPath);
            Console.WriteLine("Diff:" + diffPath);



            print(tip);
            print("当前的工作路径是:" + workDict + "\n");
            while (true)
            {

                char input = Console.ReadKey().KeyChar;
                Console.ReadKey();


                Console.Clear();
                print(tip);
                print("当前的工作路径是:" + workDict + "\n");

                switch (input)
                {
                    case '1':
                        CsfConvertExcel.Excel2Csf(csfPath, excelPath);
                        break;
                    case '2':
                        CsfConvertExcel.Csf2Excel(csfPath, excelPath);
                        break;
                    default:
                        break;
                }


  
            }
             
        }




        static void print(object str)
        {
            Console.WriteLine(str);
        }
    }
}
