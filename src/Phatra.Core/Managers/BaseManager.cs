using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phatra.Core.Logging;

namespace Phatra.Core.Managers
{
    public abstract class BaseManager
    {
        private ILog _logger;

        protected ILog Logger
        {
            get 
            {
                if (_logger == null)
                {
                    _logger = new Log4NetLogger(this.GetType());
                }
                return _logger;
            }
        }        
    }
}
