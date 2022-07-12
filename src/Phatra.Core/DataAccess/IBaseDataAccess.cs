using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phatra.Core.DataAccess
{
    public interface IBaseDataAccess
    {

        void AddParameter(string parameterName, object parameterValue);

        void ExecuteNonQuery();

        void ReadAll(Action<DynamicDataReader> readerActionBlock);

        void SetSqlText(string sql);
    }
}
