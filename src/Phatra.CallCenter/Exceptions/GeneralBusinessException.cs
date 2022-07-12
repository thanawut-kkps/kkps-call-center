using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phatra.CallCenter.Exceptions
{
    public class GeneralBusinessException : BaseCashierBusinessException
    {
        public GeneralBusinessException(string message)
            : base(new string[] { message })
        {

        }

        public GeneralBusinessException(Exception innerException, string message)
            : base(innerException, new string[] { message })
        {

        }
    }
}
