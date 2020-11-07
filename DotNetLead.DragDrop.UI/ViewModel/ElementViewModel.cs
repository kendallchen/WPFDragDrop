using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetLead.DragDrop.UI.Common;
using DotNetLead.DragDrop.UI.Model;
using System.IO;


namespace DotNetLead.DragDrop.UI.ViewModel
{
    /// <summary>
    /// ViewModel of an Element
    /// </summary>
    public class ElementViewModel : ViewModelBase
    {
        private int Id;
        private string firstName;
        private string lastName;
        private string imagePath;

        private bool isSelected;

        public int ID
        {
            get { return Id; }
            set { Id = value; }
        }

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public string ImagePath
        {
            get { return imagePath; }
            set { imagePath = value; }
        }

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        internal ElementViewModel(Node i)
        {
            this.ID = i.Id;
            this.FirstName = i.FirstName;
            this.LastName = i.LastName;
            this.ImagePath = Path.GetFullPath("Images/" + this.ID.ToString() + ".png");
        }

        /// <summary>
        /// Copy constructor 
        /// </summary>
        internal ElementViewModel(ElementViewModel i)
        {
            this.ID = i.ID;
            this.FirstName = i.FirstName;
            this.LastName = i.LastName;
            this.ImagePath = i.ImagePath;
        }


    }
}
