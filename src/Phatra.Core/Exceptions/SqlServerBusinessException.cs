using System;
using System.IO;
using Phatra.Core.Exceptions;
namespace Phatra.Core.Exceptions
{
    public class SqlServerBusinessException : BaseBusinessException
    {
        public SqlServerBusinessException(string message)
            : base(new string[] { message })
        {

        }


        public SqlServerBusinessException(Exception innerException, string message)
            : base(innerException, new string[] { message })
        {

        }

        protected override string XmlPath
        {
            get
            {
                return Path.Combine(this.ExecutingPath, @"Exceptions\BusinessException.xml");
            }
        }
    }
}
