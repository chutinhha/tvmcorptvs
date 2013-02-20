using System;

namespace TVMCORP.TVS.UTIL.MODELS
{
    [Serializable]
    public class ApprovalWorkflowParameter
    {
        public string LastApprover { get; set; }
        public string Approvers { get; set; }
        public string TaskInstructions { get; set; }
        public byte[] HashFile { get; set; }
    }
}
