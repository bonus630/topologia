using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public class SLabel : Label
    {
        private int value;
        private string name;
        private bool selected = false;
        public int Value { get { return this.value; } set { this.value = value; } }
        public string Name { get { return this.name; } set { this.name = value; } }

        public bool Selected { get { return this.selected; } set { this.selected = value; } }
        public SLabel(string name, int value)
        {
            this.value = value;
            this.name = name;
            this.Content = name;
            //this.MouseUp += (s, e) => { this.selected = !selected; selectedChange(s as SLabel); };
        }
        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            selectedChange();
            base.OnMouseUp(e);
        }
        private void selectedChange()
        {
            StackPanel container = this.Parent as StackPanel;
            foreach(SLabel l in container.Children)
            {

                l.selected = false;
                l.Background = new SolidColorBrush(Colors.Transparent);
            }
            this.selected = true;
            this.Background = new SolidColorBrush(Colors.Aquamarine);
            
                
            
        }
        
    }

    /// <summary>
    /// Interaction logic for DataConfigWindow.xaml
    /// </summary>
    public partial class DataConfigWindow : Window
    {
        private Dictionary<string, int> ControlList = new Dictionary<string, int>();
        
        private Cave cave;
        public event Action ApplyChanges;
        public DataConfigWindow()
        {
            InitializeComponent();
            //fillDefaultControlList();
            fillPropertiesControlList();
            fillTxtList();
        }

        private void fillDefaultControlList()
        {
            ControlList.Clear();
            ControlList.Add("Baixo", 0);
            ControlList.Add("Cima", 1);
            ControlList.Add("Esquerda", 2);
            ControlList.Add("Direita", 3);
            ControlList.Add("Inclinação", 4);
            ControlList.Add("Distância", 5);
            ControlList.Add("Azymuti", 6);
        }
        private void fillPropertiesControlList()
        {
            ControlList.Clear();
            ControlList.Add("Baixo", Properties.Settings.Default.txt_down);
            ControlList.Add("Cima", Properties.Settings.Default.txt_up);
            ControlList.Add("Esquerda", Properties.Settings.Default.txt_left);
            ControlList.Add("Direita", Properties.Settings.Default.txt_right);
            ControlList.Add("Inclinação", Properties.Settings.Default.txt_inclination);
            ControlList.Add("Distância", Properties.Settings.Default.txt_distance);
            ControlList.Add("Azymuti", Properties.Settings.Default.txt_azymuti);
        }
        private void changeDefaultControlList()
        {
            int i = 0;
            foreach (string item in ControlList.Keys)
            {
                ControlList[item] = (stack_labels.Children[i] as SLabel).Value;
                i++;
            }
        }
        private void fillTxtList()
        {
            stack_labels.Children.Clear();
            for (int i = 0; i < 7; i++)
            {
                foreach (string item in ControlList.Keys)
                {
                    if (i == ControlList[item])
                    {
                        SLabel l = new SLabel(item, ControlList[item]);


                        stack_labels.Children.Add(l);
                    }
                }
            }
          
            
        }
        private void reorderStack()
        {
            List<SLabel> list = new List<SLabel>();
            foreach (SLabel item in stack_labels.Children)
            {
                list.Add(item);
            }

            stack_labels.Children.Clear();
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0;j<list.Count;j++)
                {
                    if (i == (list[j] as SLabel).Value)
                    {
                        stack_labels.Children.Add(list[j] as SLabel);
                   
                    }
                    
                }
            }
            
            
        
        }
        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            saveConfig();
            this.Close();
        }

       

        private void btn_default_Click(object sender, RoutedEventArgs e)
        {
            fillDefaultControlList();
            fillTxtList();
        }

        private void btn_up_Click(object sender, RoutedEventArgs e)
        {
            
            int nextUp = 0;
            for (int i = 6; i >= 0; i--)
            {

                SLabel item = stack_labels.Children[i] as SLabel;
                if (item.Selected)
                {
                    if (item.Value >0)
                    {
                        if (!CheckPrevField(i))
                        {
                            item.Value--;
                            nextUp++;
                        }
                    }



                }
                else
                {
                    if (item.Value < 6 && nextUp<6)
                    {
                        item.Value = item.Value + nextUp;
                        nextUp = 0;
                        if (item.Value > 6)
                            item.Value = 6;
                    }
                }
                Debug.WriteLine(item.Name + " " + item.Value.ToString());
            }
            reorderStack();
        }
        private bool CheckNextField(int currentIndex)
        {
            if(currentIndex+1<7)
            {
                SLabel l = stack_labels.Children[currentIndex+1] as SLabel;

                if (l.Selected && l.Value == 6)
                    return true;
            }
            return false;
        }
        private bool CheckPrevField(int currentIndex)
        {
            if (currentIndex - 1 > -1)
            {
                SLabel l = stack_labels.Children[currentIndex - 1] as SLabel;

                if (l.Selected && l.Value == 0)
                    return true;
                
            }
            return false;
        }
        private void btn_down_Click(object sender, RoutedEventArgs e)
        {


            int nextUp = 0;
            for (int i = 0; i < 7; i++)
            {

                SLabel item = stack_labels.Children[i] as SLabel;
                if (item.Selected)
                {
                    if (item.Value < 6)
                    {
                        if (!CheckNextField(i))
                        {
                            item.Value++;

                            nextUp++;
                        }
                    }



                }
                else
                {
                    if (item.Value > 0 && nextUp > 0)
                    {
                        item.Value = item.Value - nextUp;
                        nextUp = 0;
                        if (item.Value < 0)
                            item.Value = 0;
                    }
                }
                Debug.WriteLine(item.Name + " " + item.Value.ToString());
            }
                reorderStack();
          
        }
        private void saveConfig()
        {
            
           foreach (SLabel item in stack_labels.Children)
           {
                 if(item.Name=="Baixo") 
                      Properties.Settings.Default.txt_down = item.Value;
                 if (item.Name == "Cima")
                    Properties.Settings.Default.txt_up=item.Value;
                 if (item.Name == "Esquerda")
                    Properties.Settings.Default.txt_left=item.Value;
                 if (item.Name == "Direita")
                    Properties.Settings.Default.txt_right=item.Value;
                 if (item.Name == "Inclinação")
                     Properties.Settings.Default.txt_inclination=item.Value;
                 if (item.Name == "Distância")
                    Properties.Settings.Default.txt_distance=item.Value;
                 if (item.Name == "Azymuti")
                     Properties.Settings.Default.txt_azymuti = item.Value;
           }
           Properties.Settings.Default.Save();
           if (ApplyChanges != null)
                ApplyChanges();
        }
    }
}
