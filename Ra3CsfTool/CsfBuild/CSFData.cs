using System.Collections;
using System.Collections.Generic;


namespace CSFCreate
{

    public class CSFData
    {
        public int LabelDataCount => labelDataList.Count;
        public List<LabelData> labelDataList = new List<LabelData>();


        /// <summary>
        ///  测试用
        /// </summary>
        public void Debug()
        {

            foreach (var item in labelDataList)
            {
                item.OutPut();
            }
        }

    }
}
