using CSFCreate;
using OfficeOpenXml;
using System.Collections;
using System.Collections.Generic;
using System.IO;
 

static public class ExcelCsfDataTool
{
    static public void CreatExcel(string path, CSFData csfData)
    {

        if (File.Exists(path))
        {
            DebugLog.Log($"{path} 的csf表已存在 为了安全起见 不会进行覆盖操作，请把当前的表文件进行删除，再重新从csf下生成");
            throw new System.Exception();
            return;
        }


        FileInfo fileInfo = new FileInfo(path);

        Dictionary<string, ExcelWorksheet> csfTypeDict = new Dictionary<string, ExcelWorksheet>(); // 仅仅用来分类罢了

        using (ExcelPackage excel = new ExcelPackage(fileInfo))
        {

            LabelData item;
            for (int i = 0; i < csfData.LabelDataCount; i++)
            {

                item = csfData.labelDataList[i];
                string[] tempStr = item.lableName.Split(':');

                string csfTypeKey = "_";
                string labelNameSimple = item.lableName;
                if (tempStr.Length <= 1)  //  有人没写类型  也就是没有: 内容会用 _代替
                {
                    csfTypeKey = "_";
                    labelNameSimple = item.lableName;
                }
                else
                {
                    csfTypeKey = tempStr[0];
                    labelNameSimple = tempStr[1];

                }


                if (!csfTypeDict.ContainsKey(csfTypeKey.ToUpper()))  // 未缓存到 进行第一次创建表  大小写不敏感 这里的key均这么处理
                {
                    //Debug.Log(csfTypeKey);
                    ExcelWorksheet workSheel1 = excel.Workbook.Worksheets.Add(csfTypeKey);
                    csfTypeDict.Add(csfTypeKey.ToUpper(), workSheel1);


                    workSheel1.Column(1).Width = 50;//设置1列宽
                    workSheel1.Column(2).Width = 200;//设置2列宽

                }

                ExcelWorksheet currentWorkSheet = csfTypeDict[csfTypeKey.ToUpper()];

                int endIndex = 1;
                if (currentWorkSheet.Dimension != null)
                {
                    endIndex = currentWorkSheet.Dimension.End.Row + 1;
                }



                currentWorkSheet.SetValue(endIndex, 1, labelNameSimple); // 这里截取了 :前的玩意
                currentWorkSheet.SetValue(endIndex, 2, item.labelContext);

                currentWorkSheet.Row(1).CustomHeight = true;
            }
            excel.Save();
        }
    }


    static public CSFData GetCsfDataFromExcel(string excelPath )
    {

        if (!File.Exists(excelPath))
        {
            DebugLog.Log($"{excelPath} 路径下 不存在csf表格，无法生成.csf文件. 请先创建对应的表格");
            return null;
        }


        FileInfo fileInfo = new FileInfo(excelPath);
        Dictionary<string, ExcelWorksheet> csfTypeDict = new Dictionary<string, ExcelWorksheet>(); // 仅仅用来分类罢了
 

        CSFData csfData = new CSFData();


        // 遍历表格 并且获得表格名
        // 遍历表格里面的内容 并且拼接表格名+name 获得那个
        using (ExcelPackage excel = new ExcelPackage(fileInfo))
        {
            foreach (ExcelWorksheet workSheet in excel.Workbook.Worksheets)  // 读取每一个子表
            {
                string tabelName = workSheet.Name;  // 表名
                int maxRow = workSheet.Dimension.End.Row; // 最后一行  

                for (int i = 1; i < maxRow +1; i++)  // 这里是最后一行
                {
                    csfData.labelDataList.Add(new LabelData()
                    {
                        lableName = GetFullLableNameKey(tabelName, workSheet.GetValue(i, 1).ToString()),
                        labelContext = workSheet.GetValue(i, 2).ToString()
                    });
                }
            }
        }
 
        return csfData;
    }

    static string GetFullLableNameKey(string tableName,string keyName)
    {
    
        if (tableName == "_") // 默认用这个表示空
        {
            return keyName;
        }
        else
        {
            return $"{tableName}:{keyName}";
        }

        
    }

}
