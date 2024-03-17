using System;
using System.IO;

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

 


            string csfPath = currentPath + @"\GameStrings.csf";
            string excelPath = currentPath + @"\csf.xlsx";


            print(tip);
            print("当前的工作路径是:" + currentPath + "\n");
            while (true)
            {

                char input = Console.ReadKey().KeyChar;
                Console.ReadKey();


                Console.Clear();
                print(tip);
                print("当前的工作路径是:" + currentPath + "\n");

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
