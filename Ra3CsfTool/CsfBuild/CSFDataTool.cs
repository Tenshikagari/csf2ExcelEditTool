using System.Collections;
using System.Collections.Generic;
using System.IO;
using Tool;
 

namespace CSFCreate
{

    static public class CSFDataTool
    {
        #region 读取生成 CSFData对象
        /// <summary>
        /// csf路径文件   返回一个CSFData
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        static public CSFData GetCSFDataFromCsfFile(string path)
        {

           if (!File.Exists(path))
           {
                DebugLog.Log($"{path} 的csf文件不存在，无法进行读取");
                return null;
           }


            CSFData data = new CSFData();

            FileStream fileStream = new FileStream(path, FileMode.Open);
            BinaryReader br = new BinaryReader(fileStream);


            //cfs魔数读取
            int lableCount = ReadHead(br, ref data);
            ReadLable(br, ref data, lableCount);

            br.Close();
            fileStream.Close();

            return data;
        }

 
        static void print(object str)
        {
            DebugLog.Log(str);
        }



        static private int ReadHead(BinaryReader br, ref CSFData csfData)
        {
            var csf = br.ReadBytes(4);

            var versionCode = br.ReadInt32();
            // 版本号（uint32）
            int lableNumber = br.ReadInt32();
            //label名字数量（uint32）
            int lableContextNumber = br.ReadInt32();
            //label内容数量（uint32）
            var langeType = br.ReadInt64();  // 真不知道是什么 物佬说这里是int32
                                             //语言（uint32）
            //print($"文件头【{ByteToStringRever(csf)}】    版本号:【{versionCode}】     label名字数量:【{lableNumber}】    label内容数量:【{lableContextNumber}】 语言:【{langeType}】");

            return lableContextNumber;  // 读文件头 返回有多少个
        }

        static private void ReadLable(BinaryReader br, ref CSFData csfData, int lableNumber)
        {

            for (int i = 0; i < lableNumber; i++)
            {
                var lableData = LabelDataTool.ByteToLableData(br);
                csfData.labelDataList.Add(lableData);
            }
        }


        /// <summary>
        /// 麻了个炮 怎么有些文本是反的 有些不是反的
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        static private string ByteToStringRever(byte[] data)
        {
            ReverseArray<byte>(data);
            return System.Text.Encoding.Default.GetString(data);
        }

        static private string ByteToString(byte[] data)
        {
            return System.Text.Encoding.Default.GetString(data);
        }
        #endregion



        #region 生成csf文件
        /// <summary>
        /// 提交一个csfData 和路径 生成一个csf文件
        /// </summary>
        /// <param name="targetPath"></param>
        /// <param name="csfData"></param>
        static public void CreateCSFFile(string targetPath, CSFData csfData)
        {
            if (File.Exists(targetPath))
            {
                DebugLog.Log($"{targetPath} 的csf文件已存在，已进行覆盖操作");
            }



            List<LabelData> csfDataList = csfData.labelDataList;

            byte[] csf = ByteTool.EncodingStr(" FSC", EncodingType.ASCII);
            byte[] versionCode = ByteTool.ConvertIntToByteArray(1);
            byte[] lablelableNumber = ByteTool.ConvertIntToByteArray(csfDataList.Count);
            byte[] lableContextNumber = ByteTool.ConvertIntToByteArray(csfDataList.Count);
            byte[] langeType = ByteTool.ConvertIntToByteArray(0);
            byte[] unNoInt32 = ByteTool.ConvertIntToByteArray(0);  // 不知道干嘛的 int32 占位


            int lableDataCount = csfDataList.Count;
            List<byte[]> allLableByteDataList = new List<byte[]>();
            foreach (var item in csfDataList)
            {
                allLableByteDataList.Add(LabelDataTool.LableDataToBytes(item));
            }
            var allLableByteData = ByteTool.MergeByteArray(allLableByteDataList);


            var result = ByteTool.MergeByteArray(new List<byte[]>()
        {
            csf,versionCode,lablelableNumber,lableContextNumber,langeType,unNoInt32,allLableByteData
        });
            File.Delete(targetPath);
            FileStream fileStream = new FileStream(targetPath, FileMode.CreateNew, FileAccess.ReadWrite);

            fileStream.Write(result, 0, result.Length);
            fileStream.Close();
        }
        #endregion
 
 
        private static T[] ReverseArray<T>(T[] array)
        {
            T temp;
            int count = array.Length;
            for (int i = 0; i < count / 2; i++)
            {
                temp = array[count - 1 - i];
                array[count - 1 - i] = array[i];
                array[i] = temp;
            }
            return array;
        }
    }
}
