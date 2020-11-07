using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace DotNetLead.DragDrop.UI.Model
{
    class OrgChartManager
    {
        private static OrgChartManager self;

        //orgchart stored in dictionary
        private Dictionary<int, Node> list = new Dictionary<int, Node>();

        private OrgChartManager()
        {
            //populate data
            list.Add(1, new Node { Id = 1, FirstName = "Joe", LastName = "Smith", ParentId = 1 });
            list.Add(2, new Node { Id = 2, FirstName = "Rich", LastName = "Angel", ParentId = 1 });
            list.Add(3, new Node { Id = 3, FirstName = "Mary", LastName = "Peach", ParentId = 1 });
            list.Add(4, new Node { Id = 4, FirstName = "Mike", LastName = "Door", ParentId = 2 });
            list.Add(5, new Node { Id = 5, FirstName = "Jimmy", LastName = "Fond", ParentId = 2 });
            list.Add(6, new Node { Id = 6, FirstName = "Ann", LastName = "Brown", ParentId = 4 });
            list.Add(7, new Node { Id = 7, FirstName = "George", LastName = "Farm", ParentId = 4 });
            list.Add(8, new Node { Id = 8, FirstName = "Able", LastName = "Jump", ParentId = 4 });
            list.Add(9, new Node { Id = 9, FirstName = "Corn", LastName = "Shaw", ParentId = 1 });
            list.Add(10, new Node { Id = 10, FirstName = "Grey", LastName = "Peech", ParentId = 9 });
            list.Add(11, new Node { Id = 11, FirstName = "Manny", LastName = "Travel", ParentId = 9 });
        }

        internal static OrgChartManager Instance()
        {
            if (self == null)
                self = new OrgChartManager();
            return self;
        }

        //get the root
        internal Node GetRoot()
        {
            return list[1];  //return the top root node
        }

        //get the children of a node
        internal IEnumerable<Node> GetChildren(int parentId)
        {
            return from a in list
                   where a.Value.ParentId == parentId
                        && a.Value.Id != parentId   //don't include the root, which has the same Id and ParentId
                   select a.Value;
        }

        /// <summary>
        /// Add a node to be under a parent
        /// </summary>
        /// <param name="i"></param>
        /// <param name="parentId"></param>
        internal void AddNode(Node i, int parentId)
        {
            if (list.ContainsKey(i.Id))  //if node already exists
                list[i.Id].ParentId = parentId;
            else // add new node
            {
                i.ParentId = parentId;
                list.Add(i.Id, i);
            }
        }

        internal void RemoveNode(int nodeId)
        {
            //assign all the child of the node
            //to be the child of the parent node
            if (list.ContainsKey(nodeId))
            {
                Node n = list[nodeId];
                if (nodeId != 1)  //if not root
                {
                    Reassign(n, n.ParentId);
                }
                list.Remove(nodeId);
            }
        }

        /// <summary>
        /// Reassign the children under the node 
        /// to be under the new parent
        /// </summary>
        /// <param name="i"></param>
        /// <param name="parentId"></param>
        internal void Reassign(Node i, int parentId)
        {
            foreach (Node child in GetChildren(i.Id))
                child.ParentId = parentId;
        }
    }
}
