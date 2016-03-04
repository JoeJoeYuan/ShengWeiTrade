using System;
using System.Collections.Generic;
using System.Data;

namespace Models
{
    /// <summary>
    /// Model层辅助类
    /// </summary>
    static class ModelHelper
    {
        /// <summary>
        /// 判断Reader中是否包含特定列
        /// </summary>
        /// <param name="reader">读取器对象</param>
        /// <param name="columnName">列名</param>
        /// <returns></returns>
        public static bool HasColumn(IDataReader reader,string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i) == columnName)
                    return true;
            }
            return false;
        }
    }
}
