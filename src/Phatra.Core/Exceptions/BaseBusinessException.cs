using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phatra.Core.Exceptions
{
    public abstract class BaseBusinessException : BaseMessageException
    {
        public BaseBusinessException()
            : base()
        {

        }

        public BaseBusinessException(params string[] args)
            : base(args)
        {

        }

        public BaseBusinessException(Exception innerException, params string[] args)
            : base(innerException, args)
        {

        }

    }
}
