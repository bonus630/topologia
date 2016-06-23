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
using System.Windows.Shapes;

namespace br.corp.bonus630.topologia
{
    /// <summary>
    /// Interaction logic for BaseDetails.xaml
    /// </summary>
    public partial class BaseDetails : Window
    {
        public BaseDetails(BaseTopo baseTopo,POINT p)
        {
            InitializeComponent();
           // System.Windows.Point point = new System.Windows.Point();
           // point.X = (double)p.x;
            //point.Y = (double)p.y;
           // var location = PointToScreen(point);
            this.Left = p.x;
            this.Top = p.y;
            this.baseName.Content = baseTopo.BaseName;
        }
    }
}
