using CSFCreate;
using System;
using System.Collections;
using System.Collections.Generic;
 

static public class CsfConvertExcel
{


    /// <summary>
    ///  
    /// </summary>
    /// <param name="csfPath">路径要完整名 并且包括 对应文件 例如  xxx/xxx/xxxx.csf </param>
    /// <param name="excelPath"></param>
    static public void Csf2Excel(string csfPath, string excelPath)
    {

        try
        {
            CSFData cSFData = CSFDataTool.GetCSFDataFromCsfFile(csfPath);
            if (cSFData == null) throw new System.Exception();

            ExcelCsfDataTool.CreatExcel(excelPath, cSFData);  // 失败会抛出错误

            DebugLog.Log("csf 转表格成功  (*^▽^*) 以后可以打表生成文本了");
        }
        catch (Exception ex)
        {
            DebugLog.Log("=================报错信息 【debug反馈用】 可以不管==========");
            DebugLog.Log(ex.ToString());
            DebugLog.Log("============================================================\n\n");


            DebugLog.Log("csf 转换 excel失败，请查阅以上输出进行解决");
        }
    }


    /// <summary>
    ///  
    /// </summary>
    /// <param name="csfPath">路径要完整名 并且包括 对应文件 例如  xxx/xxx/xxxx.csf </param>
    /// <param name="excelPath"></param>
    static public void Excel2Csf(string csfPath, string excelPath)
    {
        try
        {
            CSFData cSFData = ExcelCsfDataTool.GetCsfDataFromExcel(excelPath);
            if (cSFData == null) throw new System.Exception();

            CSFDataTool.CreateCSFFile(csfPath, cSFData);
            DebugLog.Log("表格转csf文件成功   ");

        }
        catch (Exception ex)
        {
            DebugLog.Log("\n\n=================报错信息 【debug反馈用】 可以不管==========");
            DebugLog.Log(ex.ToString());
            DebugLog.Log("============================================================\n\n");
            DebugLog.Log(" excel 转换 csf 失败(⊙o⊙)…，请查阅以上输出进行解决");
        }
    }


}
