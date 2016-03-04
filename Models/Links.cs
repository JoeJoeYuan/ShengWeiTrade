using System;
using System.Data;

namespace Models
{
    /// <summary>
    /// 本类由软件生成生成
    /// 禁止修改
    /// 若需要扩展，请另建一个partial class完成。
    /// </summary>
    [Serializable]
    public partial class Links
    {
        public Links(){}
        public Links(int id,string lName,string lPath)
        {
            this.ID=id;
            this.LName=lName;
            this.LPath=lPath;
        }
        public Links(IDataReader reader)
        {
            if(ModelHelper.HasColumn(reader,"ID"))
            {
                if (reader["ID"]!=DBNull.Value)
                {
                    this.ID=(int)reader["ID"];
                }
            }
            if(ModelHelper.HasColumn(reader,"LName"))
            {
                if (reader["LName"]!=DBNull.Value)
                {
                    this.LName=(string)reader["LName"];
                }
            }
            if(ModelHelper.HasColumn(reader,"LPath"))
            {
                if (reader["LPath"]!=DBNull.Value)
                {
                    this.LPath=(string)reader["LPath"];
                }
            }
        }
        
        public int? ID{get;set;}
        public string LName{get;set;}
        public string LPath{get;set;}
    }
}
