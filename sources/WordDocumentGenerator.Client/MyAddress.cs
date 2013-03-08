using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordDocumentGenerator.Client
{
    public class Phone {
    
        public string Label { get; set; }
        public string Number { get; set; }

    }
    public class MyAddress
    {
        public DateTime Date { get; set; }

        public string Name { get; set; }
        public List<Phone> Phones{ get; set; }
    }
}
