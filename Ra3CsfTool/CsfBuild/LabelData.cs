using System.Collections;
using System.Collections.Generic;
 





namespace CSFCreate
{
    /// <summary>
    ///  一整个labelData
    /// </summary>
    public struct LabelData
    {
        public string lableName;
        public int lableNameLenth => lableName.Length;
        public string labelContext;
        public int labelContextLenth => labelContext.Length;


        public void OutPut()
        {
            DebugLog.Log($" {lableName}:{labelContext}");
        }
    }

}