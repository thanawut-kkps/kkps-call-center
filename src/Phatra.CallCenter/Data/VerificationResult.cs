using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phatra.CallCenter.Data
{
    public class VerificationResult
    {
        public int QuestionId { get; set; }
        public int PurposeId { get; set; }
        public string CallSession { get; set; }
        public decimal? ClientId { get; set; }
        public string CustomerPhone { get; set; }
        public string AgentId { get; set; }
        public bool IsPassed { get; set; }
    }
}
