using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    public enum ControlDownNumericType
    {
        _Double,
        _Int
    }
      public static class ConfigCommand
      {
          static RoutedUICommand configOpen = new RoutedUICommand("Configurações", "config", typeof(ConfigCommand));
          public static RoutedUICommand ConfigOpen { get { return configOpen; } }
      }
    
    [Serializable]
    public partial class Data : Window
    {
        
        public ObservableCollection<BaseTopoItem> ListBase { get; set; }
        [NonSerialized]
        CustomControlsForData.controlDown controlDown, controlTop, controlLeft, controlRight, controlAzymuti, controlInclination, controlDistance;
        [NonSerialized]
        DataManager dataManager;
        [NonSerialized]
        public Cave cave;
        [NonSerialized]
        private BaseTopo BaseTopo;
        private event EventHandler baseTopoChange;
        private bool Saved = true;
        public BaseTopo baseTopo{
            get {return this.BaseTopo; }
            set{ this.BaseTopo=value;
            if (baseTopoChange != null)
                baseTopoChange(null, null);
            }}
        //public Data()
        //{
        //    InitializeComponent();
           
        //    buildControls();
        //    this.cave = new Cave();
        //    dataManager = new DataManager(this.cave);
        //    this.DataContext = this;
        //}
        public Data(Cave cave)
        {
           
            InitializeComponent();
            
            buildControls();
            this.DataContext = this;

            if (cave == null)
            {
                this.cave = new Cave();
                dataManager = new DataManager(this.cave);
                datepicker_dateTopo.SelectedDate = DateTime.Now;

            }
            else
            {
                this.cave = cave;
                if (!string.IsNullOrEmpty(this.cave.CaveName))
                {
                    txt_caveName.Text = this.cave.CaveName;

                    this.Title = this.cave.CaveName;
                }
                else
                    this.Title = "Sem nome";
                if (this.cave.DateTopo.Equals(new DateTime(0)))
                    datepicker_dateTopo.SelectedDate = DateTime.Now;
                else
                    datepicker_dateTopo.SelectedDate = this.cave.DateTopo;



                this.baseTopo = null;
                dataManager = new DataManager(this.cave); 
                //datagrid_base.ItemsSource = dataManager.DataGridDataSource(this.cave);
                ListBase = dataManager.DataGridDataSource(this.cave);
                foreach(BaseTopo baseTopo in this.cave.BaseList)
                {
                    cb_baseAnt.Items.Add(baseTopo.BaseName);
                }
            }
            
          
            this.baseTopoChange += Data_baseTopoChange;
            this.cave.BaseRemove += cave_BaseRemove;
            this.cave.BaseAdd += cave_BaseAdd;
            this.cave.BaseModify += cave_BaseModify;
            this.cave.CaveModify += cave_CaveModify;
            
        }

        void cave_CaveModify(object sender, EventArgs e)
        {
            string title;
            if (!string.IsNullOrEmpty(this.cave.CaveName))
                title = this.cave.CaveName;
            else
                title = this.Title;
            if (title.Contains(" *"))
                this.Title = title;
            else
                this.Title = string.Format("{0} *", title);
        }
        private void buildControls()
        {
            this.stackPanel_teste.Children.Clear();
            Visibility vis = System.Windows.Visibility.Collapsed;
            if (!(bool)cb_entrace.IsChecked)
                vis = System.Windows.Visibility.Visible;

            for (int i = 0; i < 7; i++)
            {

                if (i == Properties.Settings.Default.txt_down)
                {
                    controlDown = new CustomControlsForData.controlDown("Baixo", "m",ControlDownNumericType._Double);
                    this.stackPanel_teste.Children.Add(controlDown);
                }
                if (i == Properties.Settings.Default.txt_up)
                {
                    controlTop = new CustomControlsForData.controlDown("Cima", "m", ControlDownNumericType._Double);
                    this.stackPanel_teste.Children.Add(controlTop);
                }
                if (i == Properties.Settings.Default.txt_left)
                {
                    controlLeft = new CustomControlsForData.controlDown("Esquerda", "m", ControlDownNumericType._Double);
                     this.stackPanel_teste.Children.Add(controlLeft);
                }
                if (i == Properties.Settings.Default.txt_right)
                {
                    controlRight = new CustomControlsForData.controlDown("Direita", "m", ControlDownNumericType._Double);
                    this.stackPanel_teste.Children.Add(controlRight);
                }
                if (i == Properties.Settings.Default.txt_azymuti)
                {
                    controlAzymuti = new CustomControlsForData.controlDown("Azymuti", "º", ControlDownNumericType._Int);
                    this.stackPanel_teste.Children.Add(controlAzymuti);
                    controlAzymuti.SetVisible = vis;
                }
                if (i == Properties.Settings.Default.txt_inclination)
                {
                    controlInclination = new CustomControlsForData.controlDown("Inclinação", "º", ControlDownNumericType._Int);
                    this.stackPanel_teste.Children.Add(controlInclination);
                    controlInclination.SetVisible = vis;
                }
                if (i == Properties.Settings.Default.txt_distance)
                {
                    controlDistance = new CustomControlsForData.controlDown("Distância", "m", ControlDownNumericType._Double);
                    this.stackPanel_teste.Children.Add(controlDistance);
                    controlDistance.SetVisible = vis;
                }
            }
            Button btn_comments = new Button();
            btn_comments.Content = "Comentário";
            btn_comments.Height = 22;
            this.stackPanel_teste.Children.Add(btn_comments);
            btn_comments.Click += (s, e) => { if (box_comments.Visibility == Visibility.Collapsed)box_comments.Visibility = Visibility.Visible; else box_comments.Visibility = Visibility.Collapsed; };
        }
        void Data_baseTopoChange(object sender, EventArgs e)
        {
            if(this.baseTopo==null)
            {
                this.lba_baseTopoName.Content = "";
                this.btn_edit.IsEnabled = false;
                this.btn_remove.IsEnabled = false;
            }
            else
            {
                this.lba_baseTopoName.Content = String.Format("Base {0}", this.baseTopo.BaseName);
                this.btn_edit.IsEnabled = true;
                this.btn_remove.IsEnabled = true;
            }
        }

        void cave_BaseModify(object sender, EventArgs e)
        {
            //BaseTopo item = sender as BaseTopo;
            datagrid_base.ItemsSource = dataManager.DataGridDataSource(this.cave);
            //ListBase = dataManager.DataGridDataSource(this.cave);
        }

        void cave_BaseAdd(object sender, EventArgs e)
        {
            //BaseTopo item = sender as BaseTopo;
            datagrid_base.ItemsSource = dataManager.DataGridDataSource(this.cave);
            //ListBase = dataManager.DataGridDataSource(this.cave);
        }

        void cave_BaseRemove(object sender, EventArgs e)
        {
            //BaseTopo item = sender as BaseTopo;
            datagrid_base.ItemsSource = dataManager.DataGridDataSource(this.cave);
            //ListBase = dataManager.DataGridDataSource(this.cave);
        }
        private BaseTopo createBaseTopo()
        {
             BaseTopo _base = new BaseTopo();
           
            _base.BaseName = txt_baseName.Text;
          
            _base.BottomSide = Double.Parse(controlDown.UserInputText);
            _base.TopSide = Double.Parse(controlTop.UserInputText);
            _base.LeftSide = Double.Parse(controlLeft.UserInputText);
            _base.RightSide = Double.Parse(controlRight.UserInputText);
            _base.IsStart = (bool)cb_entrace.IsChecked;
            _base.Comments = txt_comments.Text;
            if (!_base.IsStart)
            {
                _base.Azymuti = Int32.Parse(controlAzymuti.UserInputText);
                _base.Incrination = Double.Parse(controlInclination.UserInputText);
                _base.Distance = Double.Parse(controlDistance.UserInputText);
                _base.RefBase = this.cave.GetBaseForName(cb_baseAnt.Text);
            }
            
            return _base;
                     
        }
       

        private void cb_entrace_Click(object sender, RoutedEventArgs e)
        {
            //if ((bool)cb_entrace.IsChecked)
            //{
            //    othersDatas.Visibility = System.Windows.Visibility.Collapsed;
            //}
            //else
            //{
            //    othersDatas.Visibility = System.Windows.Visibility.Visible;
                
            //}
        }

    

        private void cb_entrace_Checked(object sender, RoutedEventArgs e)
        {
           
            if(controlAzymuti != null)
                controlAzymuti.SetVisible = System.Windows.Visibility.Collapsed;
            if(controlInclination != null)
                controlInclination.SetVisible = System.Windows.Visibility.Collapsed;
            if(controlDistance != null)
                controlDistance.SetVisible = System.Windows.Visibility.Collapsed;
            if (baseAnt != null)
                baseAnt.Visibility = System.Windows.Visibility.Collapsed;
            

        }

        private void cb_entrace_Unchecked(object sender, RoutedEventArgs e)
        {
            
            if (controlAzymuti != null)
                controlAzymuti.SetVisible = System.Windows.Visibility.Visible;
            if (controlInclination != null)
                controlInclination.SetVisible = System.Windows.Visibility.Visible;
            if (controlDistance != null)
                controlDistance.SetVisible = System.Windows.Visibility.Visible;
            if(baseAnt!= null)
                baseAnt.Visibility = System.Windows.Visibility.Visible;
           
        }
        #region Ações de botões
        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
           // this.Title = String.Format("{0} *", this.cave.CaveName);
            BaseTopo _base = createBaseTopo();
            if (!this.cave.BaseNameCheck(_base.BaseName))
            {
                this.cave.AddBase(_base);
                cb_baseAnt.Items.Add(_base.BaseName);
                cb_baseAnt.SelectedIndex = this.cave.BaseList.Count - 1;
                btn_clear_Click(null, null);


            }
            else
                global::System.Windows.MessageBox.Show("Esse nome de base já foi adicionado, escolha outro.", "Aviso!!!");
        }
        private void btn_edit_Click(object sender, RoutedEventArgs e)
        {
           
            if(this.baseTopo != null)
            {
                //this.cave.BaseList.Remove(this.baseTopo);

                int index = this.cave.BaseList.IndexOf(this.baseTopo);
                
                //cb_baseAnt.Items.Remove(this.baseTopo.BaseName);
                
                this.cave.EditBase(createBaseTopo(), index);
                this.baseTopo = null;
               
            }
        }

        private void btn_clear_Click(object sender, RoutedEventArgs e)
        {
            this.baseTopo = null;
            txt_baseName.Text = "";
            
            controlAzymuti.restoreDefault();
            controlDistance.restoreDefault();
            controlDown.restoreDefault();
            controlInclination.restoreDefault();
            controlLeft.restoreDefault();
            controlRight.restoreDefault();
            controlTop.restoreDefault();
            txt_comments.Text = "";
            
        }
    
       

        private void btn_remove_Click(object sender, RoutedEventArgs e)
        {
            //this.Title = String.Format("{0} *", this.cave.CaveName);
            this.cave.RemoveBase(this.baseTopo);
            cb_baseAnt.SelectedIndex = this.cave.BaseList.Count - 1;
            this.baseTopo = null;
            
        }
#endregion
        private void datagrid_base_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BaseTopoItem bi = (sender as DataGrid).CurrentItem as BaseTopoItem;
            txt_baseName.Text = bi.BaseName;
        
            cb_entrace.IsChecked = bi.IsStart;

            controlAzymuti.UserInputText = bi.Azymuti.ToString();
            controlDistance.UserInputText = bi.Distance.ToString();
            controlDown.UserInputText = bi.BottomSide.ToString();
            controlInclination.UserInputText = bi.Incrination.ToString();
            controlLeft.UserInputText = bi.LeftSide.ToString();
            controlRight.UserInputText = bi.RightSide.ToString();
            controlTop.UserInputText = bi.TopSide.ToString();
            cb_baseAnt.SelectedItem = bi.RefBaseName;
            txt_comments.Text = bi.Comments;
            this.baseTopo = this.cave.GetBaseForName(bi.BaseName);
        }
        private void datagrid_base_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            datagrid_base_MouseLeftButtonUp(sender, e);
        }


        #region Command Binding
        private void OpenCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Cave tempCave = null;
            //this.cave = dataManager.Load();
            if (dataManager.Load(out tempCave))
                this.cave = tempCave;
            else
                return;
            dataManager.cave = this.cave;
            datagrid_base.ItemsSource = dataManager.DataGridDataSource(this.cave);
            cb_baseAnt.Items.Clear();
            foreach (BaseTopo baseTopo in this.cave.BaseList)
            {
                cb_baseAnt.Items.Add(baseTopo.BaseName);
            }
            this.Title = this.cave.CaveName;
            txt_caveName.Text = this.cave.CaveName;
            datepicker_dateTopo.SelectedDate = this.cave.DateTopo;
            this.cave.BaseRemove += cave_BaseRemove;
            this.cave.BaseAdd += cave_BaseAdd;
            this.cave.BaseModify += cave_BaseModify;
        }
        private void SaveCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.cave.CaveName = txt_caveName.Text;
            if (datepicker_dateTopo.SelectedDate != null)
                this.cave.DateTopo = (DateTime)datepicker_dateTopo.SelectedDate;
            else
                this.cave.DateTopo = new DateTime(0);
            if (dataManager.Save())
                this.Title = this.cave.CaveName;
        }
        private void SaveAsCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if(dataManager.SaveAs())
                this.Title = this.cave.CaveName;
        }
        private void NewCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.cave.NewCave();
            this.Title = "";
            datagrid_base.ItemsSource = null;
            datagrid_base.Items.Clear();
            cb_baseAnt.Items.Clear();
        }

        private void ConfigCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DataConfigWindow dt = new DataConfigWindow();
            dt.Show();
            dt.ApplyChanges += () => { buildControls(); };//this.Close(); (new Data(this.cave)).Show(); };
            
        }

        private void ConfigCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

       

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }
#endregion
        private void datepicker_dateTopo_CalendarClosed(object sender, RoutedEventArgs e)
        {
            DateTime date = (DateTime)datepicker_dateTopo.SelectedDate;
            this.cave.DateTopo = new DateTime(date.Ticks);
           // global::System.Windows.MessageBox.Show(date.Ticks.ToString());
           // global::System.Windows.MessageBox.Show(this.cave.DateTopo.Ticks.ToString());
        }

        private void txt_baseName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_baseName.Text))
            {
                btn_add.IsEnabled = false;
                btn_edit.IsEnabled = false;
            }
            else
            {
                btn_add.IsEnabled = true;
                btn_edit.IsEnabled = true;
            }
        }

       
           
    }
    
}
