using System;
using System.Collections.Generic;
using System.Linq;
using Phatra.Core.Exceptions;
using System.IO;

namespace Phatra.CallCenter.Exceptions
{
    public class BaseCashierBusinessException : BaseBusinessException
    {
        public BaseCashierBusinessException()
            : base()
        {

        }

        public BaseCashierBusinessException(string message)
            : base(new string[] { message })
        {

        }

        public BaseCashierBusinessException(params string[] args)
            : base(args)
        {

        }

        public BaseCashierBusinessException(Exception innerException, params string[] args)
            : base(innerException, args)
        {

        }

        public BaseCashierBusinessException(Exception innerException, string message)
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
