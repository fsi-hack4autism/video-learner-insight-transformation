using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VideoAnalysis.DataView
{
    /// <summary>
    /// Interaction logic for VideoDataView.xaml
    /// </summary>
    public partial class VideoDataView : UserControl
    {
        public VideoDataView()
        {
            InitializeComponent();
            this.DataContext = new VideoDataViewModel();
        }
    }
}
