using System;
using System.Collections.Generic;
using System.Text;
using System.Workflow.ComponentModel.Compiler;

namespace TVMCORP.TVS.WORKFLOWS.Activities.DP
{
	public class CopyListItemActivityValidator:ActivityValidator
	{
        public override ValidationErrorCollection Validate(ValidationManager manager, object obj)
        {
            ValidationErrorCollection myCollection =  base.Validate(manager, obj);

            //CopyListItemExtended myActivity = (CopyListItemExtended)obj;

            //if (EnsureListExists(myActivity.DestinationListUrl))
            //    myCollection.Add(new ValidationError(string.Format("No List was found at following location {0}",myActivity.DestinationListUrl)));


            return myCollection;

        }
	}
}
