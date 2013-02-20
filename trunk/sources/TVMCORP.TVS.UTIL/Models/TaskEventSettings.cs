using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TVMCORP.TVS.UTIL.MODELS
{
    [Serializable]
    [XmlInclude(typeof(TaskEventTypes))]
    public class TaskEventSetting
    {
        public TaskEventTypes Type { get; set; }
        public List<TaskActionSettings> Actions { get; set; }
        public TaskEventSetting()
        {
            Actions = new List<TaskActionSettings>();
        }
    }

    [Serializable]
    public class TaskEventSettings
    {
        public List<TaskEventSetting> Events { get; set; }
        public TaskEventSettings()
        {
            Events = new List<TaskEventSetting>();
        }
    }

    [Serializable]
    public enum TaskEventTypes
    {
        TaskCreated,
        TaskApproved,
        TaskRejected,
        WorkflowTerminated,
        TaskReassigned,
        TaskInformationRequested,
        TaskInformationSent,
        ReminderDateReached,
        EscalationDateReached,
        TaskCompleted,
        TaskOnHold,
        DocumentSigned,
        RecipientSigned,
        RecipientRejected,
        DocumentRejected,
        DocumentExpired,
        ByPassTask,
        DocumentRetrieved,
        DocumentSent,
        WFApproved,
        WFRejected,
        WFEnd,
        WFStarted,
        TaskLevelCompleted,
        TaskLevelStarted
    }

    [Serializable]
    public enum EventOwners
    {
        Workflow,
        DocuSignProcess,
        ApprovalWF
    }

}
