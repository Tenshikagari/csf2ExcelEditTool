using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Tool;
 


namespace CSFCreate 
{
   public  class LabelDataTool
    {
        public static LabelData ByteToLableData(BinaryReader br)
        {
            var lbl = br.ReadBytes(4);

            int stringCount = br.ReadInt32();

            int lableLenth = br.ReadInt32();
            string labelStr = ByteToString(br.ReadBytes(lableLenth));

            var rts = br.ReadBytes(4);
            int labelContextLenth = br.ReadInt32();
            string labelContext = ParseLabelContext(br.ReadBytes(labelContextLenth * 2));  // unicode 因为每个2字节

            //Debug.Log($"魔数【{ByteToString(lbl)}】  string数量【{stringCount}】   lable名字【{labelStr}】  lable魔数【{ByteToString(rts)}】  " +
            //     $" lable内容长度【{labelContextLenth}】 lable内容【{labelContext}】 ");

            return new LabelData()
            {
                lableName = labelStr,
                labelContext = labelContext,
            };
        }


        private static string ParseLabelContext(byte[] data)
        {
            byte temp = (byte)Convert.ToByte(255);  // 11111111 用来翻转用的

            for (int i = 0; i < data.Length; i++)
            {
                data[i] = (byte)(temp ^ data[i]);
            }

            return System.Text.Encoding.Unicode.GetString(data); ;
        }


        private static string ByteToString(byte[] data)
        {
            return System.Text.Encoding.ASCII.GetString(data);
        }


        public static byte[] LableDataToBytes(LabelData labelData)
        {
            //  LBL  string数目   长度  字符串    RTS 长度 字符串
            byte[] lbl = System.Text.Encoding.ASCII.GetBytes(" LBL");
            byte[] stringCount = ByteTool.ConvertIntToByteArray(1);

            byte[] lableNameCount = ByteTool.ConvertIntToByteArray(labelData.lableNameLenth);
            byte[] labelName = System.Text.Encoding.ASCII.GetBytes(labelData.lableName);

            byte[] rts = System.Text.Encoding.ASCII.GetBytes(" RTS");
            byte[] labelContextCount = ByteTool.ConvertIntToByteArray(labelData.labelContextLenth);
            byte[] labelContext = GetLabelContextByte(labelData.labelContext);
 


            return ByteTool.MergeByteArray(new List<byte[]>()
        {
            lbl,stringCount,lableNameCount,labelName,rts,labelContextCount,labelContext
        });


            //byteList.AddRange(lbl);
            //byteList.AddRange(stringCount);
            //byteList.AddRange(lableNameCount);
            //byteList.AddRange(labelName);
            //byteList.AddRange(rts);
            //byteList.AddRange(labelContextCount);
            //byteList.AddRange(labelContext);
            //return byteList.ToArray();
        }

        static byte[] GetLabelContextByte(string lableContext)
        {
            byte[] labelContextTemp = System.Text.Encoding.Unicode.GetBytes(lableContext);
            byte byteTemp = (byte)Convert.ToByte(255);  // 11111111 用来翻转用的
            for (int i = 0; i < labelContextTemp.Length; i++)
            {
                labelContextTemp[i] = (byte)(byteTemp ^ labelContextTemp[i]);
            }
            return labelContextTemp;
        }
    }
}