using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Reflection;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;
using Hypertek.IOffice.Workflow.Activities;

namespace Hypertek.IOffice.Workflow.Actions
{
    public partial class ExtractIPAttachments
    {
        #region Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCode]
        [System.CodeDom.Compiler.GeneratedCode("", "")]
        private void InitializeComponent()
        {
            this.CanModifyActivities = true;
            this.ExtractAttachments = new System.Workflow.Activities.CodeActivity();
            Persist = new PersistOnClose();
            // 
            // Persit
            // 
            this.Persist.Name = "Persist";
            // 
            // ExtractAttachments
            // 
            this.ExtractAttachments.Name = "ExtractAttachments";
            this.ExtractAttachments.ExecuteCode += new System.EventHandler(this.ExtractAttachmentsExecuteCode);
            // 
            // ExtractIPAttachments
            // 
            this.Activities.Add(Persist);
            this.Activities.Add(this.ExtractAttachments);
            this.Name = "ExtractIPAttachments";
            this.CanModifyActivities = false;

        }

        #endregion
        private PersistOnClose Persist;
        private CodeActivity ExtractAttachments;
    }
}
