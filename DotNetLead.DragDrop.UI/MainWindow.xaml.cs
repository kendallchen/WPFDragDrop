using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DotNetLead.DragDrop.UI.Model;
using DotNetLead.DragDrop.UI.ViewModel;

namespace DotNetLead.DragDrop.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.pnlOrgChart.DataContext = OrgTreeViewModel.Instance();
            this.cclCandidate.DataContext = CandidateListViewModel.Instance();
        }
    }
}
