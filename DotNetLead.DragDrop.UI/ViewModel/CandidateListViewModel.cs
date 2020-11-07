using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetLead.DragDrop.UI.Common;
using System.Windows.Input;
using DotNetLead.DragDrop.UI.Behavior;
using System.Collections.ObjectModel;
using System.Windows;

namespace DotNetLead.DragDrop.UI.ViewModel
{
    /// <summary>
    /// ViewModel of the Candidate List
    /// </summary>
    class CandidateListViewModel : ViewModelBase, IDropable
    {
        private static CandidateListViewModel self;

        private ElementViewModel selected;
        private ICommand selectedCommand;
        private ObservableCollection<CandidateViewModel> list;

        public ObservableCollection<CandidateViewModel> List
        {
            get { return list; }
            set
            {
                list = value;
                OnPropertyChanged("List");
            }
        }

        public ElementViewModel Selected
        {
            get { return selected; }
            set
            {
                selected = value;
                selected.IsSelected = true;
                OnPropertyChanged("Selected");
            }
        }

        public ICommand SelectedCommand
        {
            get
            {
                if (selectedCommand == null)
                {
                    selectedCommand = new CommandBase(i => this.SetSelected(i), null);
                }
                return selectedCommand;
            }
        }

        private void SetSelected(object element)
        {
            this.Selected = element as CandidateViewModel;
        }

        private CandidateListViewModel()
        {
            //populate initial data
            if (list == null)
            {
                //populate initial data
                list = new ObservableCollection<CandidateViewModel>();
                foreach (Model.Node i in Model.CandidateManager.Instance().GetList())
                    list.Add(new CandidateViewModel(i));
            }        
        }

        public static CandidateListViewModel Instance()
        {
            if (self == null)
                self = new CandidateListViewModel();
            return self;
        }

        internal void Remove(CandidateViewModel i)
        {
            list.Remove(i);
            OnPropertyChanged("List"); //refresh view
        }

        #region IDropable Members

        /// <summary>
        /// Returns the type of the item that can be dropped
        /// </summary>
        Type IDropable.DataType
        {
            get { return typeof(ElementViewModel); }
        }

        void IDropable.Drop(object data, int index)
        {
            ElementViewModel item = data as ElementViewModel;
            if (item != null)
            {
                if (list.Where(i => i.ID == item.ID).Count() == 0)
                {
                    CandidateViewModel dropItem = new CandidateViewModel(new Model.Node
                                                                    {
                                                                        Id = item.ID,
                                                                        FirstName = item.FirstName,
                                                                        LastName = item.LastName
                                                                    });
                    list.Insert(index, dropItem);
                    this.List = list; //refresh view
                }
            }
        }

        #endregion

    }
}
