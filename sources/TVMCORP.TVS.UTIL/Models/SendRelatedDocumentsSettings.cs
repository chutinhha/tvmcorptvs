using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TVMCORP.TVS.UTIL.MODELS
{
    [Serializable]
	public class SendRelatedDocumentsSettings
	{
        public string EmailTemplateListUrl {get;set;}
        public string EmailTemplateName { get; set; }
        public int MaximunAttachments { get; set; }
        public int AttachmentSizeLimit { get; set; }
	}
}
