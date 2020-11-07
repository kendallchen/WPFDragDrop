using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetLead.DragDrop.UI.Behavior;
using DotNetLead.DragDrop.UI.Model;

namespace DotNetLead.DragDrop.UI.ViewModel
{
    public class CandidateViewModel : ElementViewModel, IDragable
    {
        internal CandidateViewModel(Node i)
            : base(i)
        {
        }

        #region IDragable Members

        Type IDragable.DataType
        {
            get { return typeof(ElementViewModel); }
        }

        void IDragable.Remove(object i)
        {
            CandidateListViewModel.Instance().Remove(this);
        }

        #endregion
    }
}
