using System.Collections.Generic;

namespace Phatra.Core.Utilities
{
    /// <summary>
    /// Class to store one CSV row
    /// </summary>
    public class CsvRow : List<string>
    {
        public string LineText { get; set; }
    }
}
