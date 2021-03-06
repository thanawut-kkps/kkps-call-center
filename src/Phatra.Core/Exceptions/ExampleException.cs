using System.IO;

namespace Phatra.Core.Exceptions
{
    public class ExampleException : BaseMessageException
    {
        public ExampleException()
            : base()
        {

        }

        public ExampleException(string[] args)
            : base(args)
        {

        }

        protected override string XmlPath
        {
            get
            {
                return Path.Combine(this.ExecutingPath, @"Exceptions\Example Business Exception.xml");
            }
        }
    }
}
