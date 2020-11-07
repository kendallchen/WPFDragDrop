using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetLead.DragDrop.UI.Model;
using DotNetLead.DragDrop.UI.Common;
using System.Windows.Input;

namespace DotNetLead.DragDrop.UI.ViewModel
{
    /// <summary>
    /// ViewModel of the Organization Chart
    /// </summary>
    public class OrgTreeViewModel : ViewModelBase
    {
        private static OrgTreeViewModel self;

        private List<OrgElementViewModel> root;
        private OrgElementViewModel rootSingle;

        //the root for binding to TreeView
        public List<OrgElementViewModel> Root
        {
            get 
            {
                if (root == null)
                {
                    root = new List<OrgElementViewModel>();
                    root.Add(this.RootSingle);
                }
                return root;            
            }
        }

        //the root for binding to DetailView
        public OrgElementViewModel RootSingle
        {
            get
            {
                if (rootSingle == null)
                {
                    rootSingle = new OrgElementViewModel(OrgChartManager.Instance().GetRoot());
                }
                return rootSingle;
            }
        }

        private OrgTreeViewModel(){}

        public static OrgTreeViewModel Instance()
        {
            if (self == null)
                self = new OrgTreeViewModel();
            return self;
        }

    }
}
