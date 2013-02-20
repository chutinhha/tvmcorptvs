using System;
using System.Workflow.ComponentModel;

namespace TVMCORP.TVS.WORKFLOWS.Activities
{
    [PersistOnClose]
    [Serializable]
    public class PersistOnClose : System.Workflow.ComponentModel.Activity
    {
        public PersistOnClose()
        {
            this.Name = typeof(PersistOnClose).Name;
        }

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            return ActivityExecutionStatus.Closed;
        }
    }
}
