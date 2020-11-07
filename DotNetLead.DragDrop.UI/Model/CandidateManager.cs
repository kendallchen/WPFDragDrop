using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetLead.DragDrop.UI.Model
{
    class CandidateManager
    {
        private static CandidateManager self;

        //candidates stored in dictionary
        private Dictionary<int, Node> list = new Dictionary<int, Node>();

        private CandidateManager()
        {
            //populate data
            list.Add(20, new Node { Id = 20, FirstName = "Norra", LastName = "Page"});
            list.Add(21, new Node { Id = 21, FirstName = "Austin", LastName = "Mart"});
            list.Add(22, new Node { Id = 22, FirstName = "Gauge", LastName = "Sharp" });
            list.Add(23, new Node { Id = 23, FirstName = "Lawry", LastName = "Mansion"});
        }

        internal static CandidateManager Instance()
        {
            if (self == null)
                self = new CandidateManager();
            return self;
        }


        //get the list of candidates
        internal IEnumerable<Node> GetList()
        {
            return list.Values.ToList();
        }
    }
}
