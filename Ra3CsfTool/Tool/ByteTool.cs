using System.Collections;
using System.Collections.Generic;
 

namespace Tool
{


public enum EncodingType
{
    ASCII, Unicode
}


static public class ByteTool
{


    public static byte[] ConvertIntToByteArray(int m)
    {
        byte[] arry = new byte[4];
        arry[0] = (byte)(m & 0xFF);
        arry[1] = (byte)((m & 0xFF00) >> 8);
        arry[2] = (byte)((m & 0xFF0000) >> 16);
        arry[3] = (byte)((m >> 24) & 0xFF);

        return arry;
    }

    public static byte[] MergeByteArray(List<byte[]>  byteArryaList)
    {
        List<byte> byteList = new List<byte>();
        foreach (var item in byteArryaList)
        {
            byteList.AddRange(item);
        }
        return byteList.ToArray();
    }



    public static byte[] EncodingStr(string str, EncodingType encodingType)
    {
        if (encodingType == EncodingType.ASCII)
        {
            return System.Text.Encoding.ASCII.GetBytes(str);
        }
        else if (encodingType == EncodingType.Unicode)
        {
            return System.Text.Encoding.ASCII.GetBytes(str);
        }

        return null;
    }


}

}