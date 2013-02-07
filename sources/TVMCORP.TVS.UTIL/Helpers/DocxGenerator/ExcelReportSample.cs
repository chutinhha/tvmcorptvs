using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TVMCORP.TVS.Util.Helper.DocXGenerator
{
    public class ExcelReportSample : IReportBase
    {
        [Cell(Index="A2")]
        public string MyProperty { get; set; }
    }
}
