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
using br.corp.bonus630.topologia;

namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataManager data;
        public MainWindow()
        {
            InitializeComponent();
            Cave cave = new Cave();
            cave.CaveName = "Cavidade 01";
            cave.FilePath = "\\";

            List<BaseTopo> bs = new List<BaseTopo>();

            BaseTopo baseTopografica = new BaseTopo();
            baseTopografica.BaseName = "0";
            baseTopografica.BottomSide = 0;
            baseTopografica.TopSide = 1;
            baseTopografica.LeftSide = 1;
            baseTopografica.RightSide = 2;


            BaseTopo baseTopografica1 = new BaseTopo();
            baseTopografica1.IsStart = false;
            baseTopografica1.BaseName = "1";
            baseTopografica1.RefBase = baseTopografica;
            baseTopografica1.Distance = 5;
            baseTopografica1.Incrination = 45;
            baseTopografica1.Azymuti = 20;
            baseTopografica1.BottomSide = 0;
            baseTopografica1.TopSide = 1;
            baseTopografica1.LeftSide = 2;
            baseTopografica1.RightSide = 2;


            BaseTopo baseTopografica2 = new BaseTopo();
            baseTopografica2.IsStart = false;
            baseTopografica2.BaseName = "2";
            baseTopografica2.RefBase = baseTopografica1;
            baseTopografica2.Distance = 7;
            baseTopografica2.Incrination = 45;
            baseTopografica2.Azymuti = 45;
            baseTopografica2.BottomSide = 0;
            baseTopografica2.TopSide = 1;
            baseTopografica2.LeftSide = 0.5;
            baseTopografica2.RightSide = 1;

            BaseTopo baseTopografica3 = new BaseTopo();
            baseTopografica3.IsStart = false;
            baseTopografica3.BaseName = "3";
            baseTopografica3.RefBase = baseTopografica1;
            baseTopografica3.Distance = 3;
            baseTopografica3.Incrination = 45;
            baseTopografica3.Azymuti = 90;
            baseTopografica3.BottomSide = 0;
            baseTopografica3.TopSide = 1;
            baseTopografica3.LeftSide = 1.5;
            baseTopografica3.RightSide = 1;

            bs.Add(baseTopografica);
            bs.Add(baseTopografica1);
            bs.Add(baseTopografica2);
            bs.Add(baseTopografica3);
            cave.BaseList = bs;
            data = new DataManager(cave);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           Cave cave = data.Load();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            data.Save();
        }
    }
}
