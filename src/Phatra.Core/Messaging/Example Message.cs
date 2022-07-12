using System.IO;

namespace Phatra.Core.Messaging
{
    public class ExampleMessage : BaseMessage
    {
        private static ExampleMessage _msg;

        public static ExampleMessage Instance
        {
            get
            {
                if (_msg == null) _msg = new ExampleMessage();
                return _msg;
            }
        }

        protected override string XmlMessageFilePath
        {
            get { return Path.Combine(Path.GetDirectoryName(this.ExecutingPath), @"UIMessages\MessageInfo.xml"); }
        }
    }
}
