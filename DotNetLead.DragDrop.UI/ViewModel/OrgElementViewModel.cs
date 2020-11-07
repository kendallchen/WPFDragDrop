using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetLead.DragDrop.UI.Model;
using DotNetLead.DragDrop.UI.Common;
using System.Windows.Input;
using System.IO;
using System.Collections.ObjectModel;
using DotNetLead.DragDrop.UI.Behavior;

namespace DotNetLead.DragDrop.UI.ViewModel
{
    /// <summary>
    /// ViewModel of the Element in the Organization Chart
    /// </summary>
    public class OrgElementViewModel : ElementViewModel, IDropable, IDragable
    {
        private ObservableCollection<OrgElementViewModel> children;
        private OrgElementViewModel parent;

        private bool isMoveWithinOrganization = false;  

        public ObservableCollection<OrgElementViewModel> Children
        {
            get 
            {
                if (children == null) //not yet initialized
                    return GetChildren();
                return children;
            }
            set 
            { 
                children = value;
                OnPropertyChanged("Children");
            }
        }

        internal OrgElementViewModel(Node i, OrgElementViewModel parent = null) 
            : base(i)
        {
            //assign reference to the parent OrgElementViewModel
            this.parent = parent;  
        }

        private ObservableCollection<OrgElementViewModel> GetChildren()
        {
            children = new ObservableCollection<OrgElementViewModel>();
            //get the list of children from Model
            foreach (Node i in OrgChartManager.Instance().GetChildren(this.ID))
            {
                children.Add(new OrgElementViewModel(i, this));                     
            }
            return children;
        }

        #region IDropable Members

        Type IDropable.DataType
        {
            get { return typeof(ElementViewModel); }
        }

        /// <summary>
        /// Drop data into this OrgElementViewModel
        /// </summary>
        void IDropable.Drop(object data, int index)
        {
            //if moving within organization, reassign the children to the 
            //level above first
            OrgElementViewModel org = data as OrgElementViewModel;
            ElementViewModel elem = data as ElementViewModel;

            if (org != null) 
            {
                org.isMoveWithinOrganization = true; 
                if (org.ID == this.ID) //if dragged and dropped yourself, don't need to do anything
                    return;
                OrgChartManager.Instance().Reassign(new Node { Id = org.ID }, org.parent.ID);
                OrgChartManager.Instance().AddNode(new Node { Id = org.ID, 
                                                    ParentId = this.ID, 
                                                    FirstName = org.FirstName, 
                                                    LastName = org.LastName },
                                                this.ID);                
            }
            else if (elem != null)
            {
                OrgChartManager.Instance().AddNode(new Node { Id = elem.ID, 
                                                        ParentId = this.ID, 
                                                        FirstName = elem.FirstName, 
                                                        LastName = elem.LastName },
                                                    this.ID);
            }
            this.Children = this.GetChildren();  //refresh view
        }

        #endregion

        #region IDragable Members

        Type IDragable.DataType
        {
            get 
            {
                //only allow drag if not root
                if (parent != null)
                    return typeof(ElementViewModel);
                else
                    return typeof(UInt16);
            }
        }

        /// <summary>
        /// Remove this OrgElementViewModel from the 
        /// OrgTreeViewModel
        /// </summary>
        /// <param name="i"></param>
        void IDragable.Remove(object data)
        {
            //if moving within organization we don't want to 
            //delete the data from the tree
            OrgElementViewModel org = data as OrgElementViewModel;
            if (org != null)
            {
                //if not a move within organization(moving outside of orgchart)
                if (!org.isMoveWithinOrganization)
                {
                    OrgChartManager.Instance().RemoveNode(this.ID);
                }
            }
            //refresh the view of parent's children
            if (parent != null)
                parent.Children = parent.GetChildren(); 
        }

        #endregion

    }
}
