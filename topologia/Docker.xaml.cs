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
using Corel.Interop.VGCore;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace br.corp.bonus630.topologia
{
  
    //Eventos de click dos botões das camadas no gerenciador de camadas não são registrados ao mudar de documento
   

    public struct Ret
    {
        public Ret(double left,double top, double rigth,double bottom)
        {
            this.Left = left;
            this.Top = top;
            this.Rigth = rigth;
            this.Bottom = bottom;
        }
        public double Left;
        public double Top;
        public double Rigth;
        public double Bottom;
    }
 
    public partial class Docker : UserControl
    {

        
        public static Corel.Interop.VGCore.Application corelApp;
        private Cave cave;
        private Document Doc;
        private List<Cave> caves = new List<Cave>();
        private OnScreenText oText;
        //teste
       // private List<List<GridLayer>> listLayer = new List<List<GridLayer>>();
        private BitmapResources bitmapsResources = new BitmapResources();
        private int currentDocIndex = 0;
        private bool showBaseDetails = true;
        public Docker(Corel.Interop.VGCore.Application corelApp)
        {
            
            Docker.corelApp = corelApp;
            
            Docker.corelApp.DocumentNew += corelApp_DocumentNew;
            Docker.corelApp.DocumentOpen += corelApp_DocumentOpen;
            Docker.corelApp.WindowActivate += corelApp_WindowActivate;
            Docker.corelApp.DocumentClose+=corelApp_DocumentClose;
            Docker.corelApp.SelectionChange += corelApp_SelectionChange;

            InitializeComponent();
            btn_new_layer.Source = bitmapsResources.newLayer;
            btn_delete_layer.Source = bitmapsResources.deleteLayer;
            CreateSocialLinks();
            caves.Add(null);
            oText = Docker.corelApp.Application.CreateOnScreenText();
            if (corelApp.ActiveDocument != null)
                Starts(Docker.corelApp.ActiveDocument);


           
        }

        void corelApp_SelectionChange()
        {
            Debug.WriteLine(Docker.corelApp.ActiveShape.Name);
        }

        private void CreateSocialLinks()
        {
            
            facebook.Source = bitmapsResources.Facebook;
            youtube.Source = bitmapsResources.Youtube;
            twitter.Source = bitmapsResources.Twitter;
            linkedin.Source = bitmapsResources.Linkedin;
            googleplus.Source = bitmapsResources.GooglePlus;
            bonus630.Source = bitmapsResources.Bonus630;

        }

        void media_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Image img = sender as Image;
            switch(img.Name)
            {
                case "facebook":
                    System.Diagnostics.Process.Start("http://www.facebook.com/pages/Bonus630/153173424746060?ref=h");
                    break;
                case "youtube":
                    System.Diagnostics.Process.Start("https://www.youtube.com/channel/UCkwOJW__phv_xqgwX5hhXeA");
                    break;
                case "twitter":
                    System.Diagnostics.Process.Start("https://twitter.com/bonus630");
                    break;
                case "linkedin":
                    System.Diagnostics.Process.Start("http://www.linkedin.com/profile/view?id=138226977&trk=nav_responsive_tab_profile");
                    break;
                case "googleplus":
                    System.Diagnostics.Process.Start("https://plus.google.com/u/0/+Bonus630Tkplus/");
                    break;
                case "bonus630":
                    System.Diagnostics.Process.Start("http://bonus630.tk/");
                    break;
            }
            
        }
              
        void corelApp_WindowActivate(Document Doc, Corel.Interop.VGCore.Window Window)
        {
            this.Doc = Doc;
            if (caves.Count > Doc.Index)
            {
                this.cave = caves[Docker.corelApp.ActiveDocument.Index];
                //Enable();
                updateCave(this.cave);
                Debug.WriteLine("linha:128");
                Debug.WriteLine("Caves:" + caves.Count + " DocIndex:" + Doc.Index+" CaveName:"+caves[Doc.Index].CaveName);
                Debug.WriteLine("WindowIndex - " + Docker.corelApp.ActiveDocument.Index.ToString());
            }
            
            //if(this.Doc!= null)
            //{
            //    box_layers.Children.Clear();
            //    foreach (Layer layer in Doc.ActivePage.Layers)
            //    {
            //        GridLayer grid = new GridLayer(layer, bitmapsResources);

            //        box_layers.Children.Add(grid);
            //        box_layers.Children.Add(new Separator());
            //    }
            //}
            
        }
               
        private void Starts(Document Doc)
        {
            this.Doc = Doc;
            this.Doc.Unit = cdrUnit.cdrMillimeter;
            Debug.WriteLine(string.Format("DocIndex:{0}",Doc.Index));
            this.cave = new Cave();
            caves.Add(this.cave);
            //Debug.WriteLine("No ponto" + caves[Doc.Index].CaveName);
            #if DEBUG
                fillCave();
            #endif
           
            btn_ShowEditor.IsEnabled = true;
            //Enable();
            updateCave(this.cave);
           
            Doc.LayerChange += Doc_LayerChange;
            Doc.LayerCreate += Doc_LayerCreate;
            Doc.LayerDelete += Doc_LayerDelete;
            Doc.LayerActivate += Doc_LayerActivate;
            Doc.SelectionChange += Doc_SelectionChange;
            Doc.AfterSave += Doc_AfterSave;
           
        }

       

        void Doc_SelectionChange()
        {
          
           //Retorna ponto do click referênte a página aberta
            // int teste = this.Doc.GetUserClick(out x, out y, out a, 0, false, cdrCursorShape.cdrCursorWinWait);
            
            
            if (!showBaseDetails) return;
            
            BaseTopo baseTopo = this.cave.GetBaseForName(Docker.corelApp.ActiveShape.Name,true);
            if (baseTopo != null)
            {
                double x = 0;
                double y = 0;
                Docker.corelApp.ActiveShape.GetPosition(out x, out y);

               
                oText.SetTextAndPosition(baseTopo.BaseName, x, y);
                oText.Show();

                lba_baseName.Content = baseTopo.BaseName;
                lba_azymuti.Content = string.Format("{0}º", baseTopo.Azymuti);
                lba_inclination.Content = string.Format("{0}º", baseTopo.Incrination);
                //  POINT p;
                //  GetCursorPos(out p);
                //  (new BaseDetails(baseTopo,p)).Show();
            }

           // if (baseTopo == null || Docker.corelApp.ActiveShape == null)
             //   oText.Hide();

        }


        #region Eventos de Layers
        void Doc_LayerDelete(int Count)
        {
            box_layers.Children.Clear();

            //Removo a layer deletada antigo
            foreach (Layer layer in Doc.ActivePage.Layers)
            {
                GridLayer grid = new GridLayer(layer, bitmapsResources, false);

                box_layers.Children.Add(grid);
                //if (grid.GridCheckLayer(layer))
                //    grid.GridLayerActive(true);
                //else
                //    grid.GridLayerActive(false);

                box_layers.Children.Add(new Separator());
            }
            (box_layers.Children[0] as GridLayer).GridLayerActive(Doc.ActiveLayer);

            //Removo a layer deletada novo
            //foreach (GridLayer item in listLayer[this.currentDocIndex])
            //{
            //    foreach(Layer l in Doc.ActivePage.Layers)
            //    {

            //        if (item.GridCheckLayer(l))
            //        {
            //            box_layers.Children.Add(item);
            //            box_layers.Children.Add(new Separator());
            //            item.GridLayerActive(true);
            //        }
                   
            //    }
            //}
           
        }
        void Doc_LayerActivate(Layer Layer)
        {

            for (int i = 0; i < box_layers.Children.Count; i++)
            {
                if (box_layers.Children[i].GetType() == typeof(GridLayer))
                {
                    //(box_layers.Children[i] as GridLayer).GridLayerActive(Layer);
                    if ((box_layers.Children[i] as GridLayer).GridCheckLayer(Layer))
                        (box_layers.Children[i] as GridLayer).GridLayerActive(true);
                    else
                        (box_layers.Children[i] as GridLayer).GridLayerActive(false);
                }

            }
            
        }
        void Doc_LayerCreate(Layer Layer)
        {
            //Crio um novo gridlayer antigo
            GridLayer grid = new GridLayer(Layer, bitmapsResources, false);
            box_layers.Children.Add(grid);
            grid.GridLayerActive(Doc.ActiveLayer);
            box_layers.Children.Add(new Separator());
           
            //Crio um novo gridlayer novo
            //GridLayer grid = new GridLayer(Layer, bitmapsResources, false);
            //listLayer[this.currentDocIndex].Add(grid);
            //box_layers.Children.Add(grid);
            //grid.GridLayerActive(Doc.ActiveLayer);
            //box_layers.Children.Add(new Separator());

        }
        void Doc_LayerChange(Layer Layer)
        {
            for (int i = 0; i < box_layers.Children.Count; i++)
            {
                if (box_layers.Children[i].GetType() == typeof(GridLayer))
                {
                    if ((box_layers.Children[i] as GridLayer).GridCheckLayer(Layer))
                        (box_layers.Children[i] as GridLayer).GridLayerChange(Layer);
                }

            }
            

        }
        #endregion


        #region Eventos Do Documento
        void corelApp_DocumentOpen(Corel.Interop.VGCore.Document Doc, string FileName)
        {

            Starts(Doc);
        }
        void Doc_AfterSave(bool SaveAs, string FileName)
        {
            this.cave.FilePath = FileName;
            this.cave.CaveName = Doc.Name;
        }
       // private void Enable()
        //{
        //    if (this.cave.BaseList.Count > 1 && this.Doc !=null)
        //    {
        //        btn_showGrid.IsEnabled = true;
               
        //        btn_Draw.IsEnabled = true;
        //        cb_refbase.IsEnabled = true;
        //        cb_refbase.Items.Clear();
        //        box_perfils.Children.Clear();
        //        for (int i = 0; i < cave.BaseList.Count; i++)
        //        {

        //            if (!cave.BaseList[i].IsStart)
        //            {
        //                cb_refbase.Items.Add(cave.BaseList[i].BaseName);
        //                box_perfils.Children.Add(new CheckBox { Content = String.Format("Base {0}", cave.BaseList[i].BaseName), Name = String.Format("cb_base_{0}", cave.BaseList[i].BaseName), Height = 30 });
        //            }
        //            else
        //                box_perfils.Children.Add(new CheckBox { Content = String.Format("Entrada {0}", cave.BaseList[i].BaseName), Name = String.Format("cb_base_{0}", cave.BaseList[i].BaseName), Height = 30,IsChecked=true });
        //        }
        //        cb_refbase.SelectedIndex = 0;
        //    }
        //    else
        //    {
        //        btn_showGrid.IsEnabled = false;
               
        //        btn_Draw.IsEnabled = false;
        //        cb_refbase.IsEnabled = false;
        //    }
        //}
        //void cave_BaseModify(object sender, EventArgs e)
        //{
        //    Enable();
        //    //cb_refbase.Items.Clear();
        //   // for(int i = 0; i<this.cave.BaseList.Count;i++)
        //  //  {
        //   //     cb_refbase.Items.Add(this.cave.BaseList[i]);
        //  //  }
        //}

        //void cave_BaseAdd(object sender, EventArgs e)
        //{
        //    Enable();
        //    //cb_refbase.Items.Add((sender as BaseTopo).BaseName);
        //}

        //void cave_BaseRemove(object sender, EventArgs e)
        //{
        //    Enable();
        //   // cb_refbase.Items.Remove((sender as BaseTopo).BaseName);
        //}
        void corelApp_DocumentNew(Corel.Interop.VGCore.Document Doc, bool FromTemplate, string Template, bool IncludeGraphics)
        {
            Starts(Doc);
        }
        private void corelApp_DocumentClose(Document Doc)
        {
            //Temos um bug aqui, doc.index maior que o número de elementos na lista de caves, possivel correção subtrair por 1?
            //Ou as cavernas não são criadas ao abrir um documento?
            try
            {
                caves.RemoveAt(Doc.Index);
            }
            catch(ArgumentOutOfRangeException e)
            {
                Debug.WriteLine(string.Format("Error: DocumentClose | DocIndex:{0} caveCount:{1} erro:{2}", this.currentDocIndex, caves.Count, e.Message));

            }
            //listLayer.RemoveAt(Doc.Index);
            if (Docker.corelApp.ActiveDocument == null)
                this.currentDocIndex = 0;
            updateCave(null);
        }
        #endregion
       
        //Este método deverá ser apagado 
        private void fillCave()
        {
            this.cave.CaveName = "Cavidade 01";
           // this.cave.FilePath = "\\";

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
            baseTopografica1.Distance = 6;
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
            baseTopografica3.BaseName = "4";
            baseTopografica3.RefBase = baseTopografica1;
            baseTopografica3.Distance = 3;
            baseTopografica3.Incrination = 45;
            baseTopografica3.Azymuti = 90;
            baseTopografica3.BottomSide = 0;
            baseTopografica3.TopSide = 1;
            baseTopografica3.LeftSide = 1.5;
            baseTopografica3.RightSide = 1;

            BaseTopo baseTopografica4 = new BaseTopo();
            baseTopografica4.BaseName = "3";
            baseTopografica4.IsStart = false;
            baseTopografica4.RefBase = baseTopografica2;
            baseTopografica4.Distance = 3;
            baseTopografica4.Incrination = 45;
            baseTopografica4.Azymuti = 350;
            baseTopografica4.BottomSide = 0;
            baseTopografica4.TopSide = 1;
            baseTopografica4.LeftSide = 1.5;
            baseTopografica4.RightSide = 1;

            BaseTopo baseTopografica5 = new BaseTopo();
            baseTopografica5.IsStart = false;
            baseTopografica5.BaseName = "5";
            baseTopografica5.RefBase = baseTopografica2;
            baseTopografica5.Distance = 4;
            baseTopografica5.Incrination = 45;
            baseTopografica5.Azymuti = 300;
            baseTopografica5.BottomSide = 0;
            baseTopografica5.TopSide = 1;
            baseTopografica5.LeftSide = 1.5;
            baseTopografica5.RightSide = 1;


            bs.Add(baseTopografica);
            bs.Add(baseTopografica1);
            bs.Add(baseTopografica2);
            bs.Add(baseTopografica3);
            bs.Add(baseTopografica4);
            bs.Add(baseTopografica5);
            cave.BaseList = bs;
        }

        private void data_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.currentDocIndex = 0;
            updateCave((sender as Data).cave);
            this.cave.Generate3DPoints();
        }

        //private void btn_showGrid_Click(object sender, RoutedEventArgs e)
        //{
        //    changeLayerVisible("Grid");
        //    foreach (Corel.Interop.VGCore.Shape shape in Doc.ActiveLayer.Shapes)
        //    {
        //        if (shape.Name == "Base 0")
        //        {
        //            DataItems dats = shape.ObjectData;
        //            foreach (DataItem dat in dats)
        //            {
        //                global::System.Windows.MessageBox.Show((string)dat.Value);
        //            }
        //        }
        //    }
        //}
        //private void changeLayerVisible(string layerName)
        //{
        //    Layers layers = Doc.ActivePage.Layers;
        //    for(int i=0;i<layers.Count;i++)
        //    {
        //        if(layers[i].Name == layerName)
        //        {
        //            layers[i].Visible = !layers[i].Visible;
        //        }
        //    }
        //}
        private void updateCave(Cave cave)
        {
            if (Docker.corelApp.ActiveDocument == null)
            {
                box_layers.Children.Clear();
                this.cave = null;
                disableEdits();
                return;
            }
            else
            {
                
                if (Docker.corelApp.ActiveDocument.Index == currentDocIndex)
                {
                    Debug.WriteLine("To no if");
                    return;
                }
                this.currentDocIndex = Docker.corelApp.ActiveDocument.Index;
            }
            box_layers.Children.Clear();

            //Preencho a lista de layers antigo
            foreach (Layer layer in Doc.ActivePage.Layers)
            {
                GridLayer grid = new GridLayer(layer, bitmapsResources, false);

                box_layers.Children.Add(grid);
                box_layers.Children.Add(new Separator());
            }

            //Preencho a lista de layers novo
            //foreach (GridLayer item in listLayer[this.currentDocIndex])
            //{
            //    box_layers.Children.Add(item);
            //    box_layers.Children.Add(new Separator());
            //}
            this.Doc_LayerActivate(Docker.corelApp.ActiveDocument.ActiveLayer);
            this.cave = cave;
            //bug problemas com o index do documento e a lista de cavernas

            try
            {
                caves[this.currentDocIndex] = cave;
            }
            catch(IndexOutOfRangeException e)
            {
                Debug.WriteLine(string.Format("Error: updateCave | DocIndex:{0} caveCount:{1} erro:{2}", this.currentDocIndex, caves.Count, e.Message));
            }
            string caveName = this.cave.CaveName;
            if (!string.IsNullOrEmpty(caveName))
            {
                lba_caveName.Content = caveName;
                this.Doc.Name = caveName;
            }
            else
                lba_caveName.Content = "Sem nome";
            Debug.WriteLine("linha:527");
            Debug.WriteLine("updateCave " + this.cave.CaveName + " " + Doc.Index);
            //Temos um bug aqui 
            
            if (this.cave.BaseList.Count > 1 && this.Doc != null)
            {
                enableEdits();
               
            }
            else
            {

                disableEdits();
               
            }
        }
               
        private void enableEdits()
        {


            btn_Draw.IsEnabled = true;
            cb_refbase.IsEnabled = true;
            cb_refbase.Items.Clear();
            cb_firstbase_check.IsEnabled = true;
            cb_lastbase_check.IsEnabled = true;
            cb_firstbase_check.Items.Clear();
            cb_lastbase_check.Items.Clear();
            box_perfils.Children.Clear();
            for (int i = 0; i < cave.BaseList.Count; i++)
            {
                cb_firstbase_check.Items.Add(cave.BaseList[i].BaseName);
                cb_lastbase_check.Items.Add(cave.BaseList[i].BaseName);
                if (!cave.BaseList[i].IsStart)
                {
                    cb_refbase.Items.Add(cave.BaseList[i].BaseName);
                    box_perfils.Children.Add(new CheckBox { Content = String.Format("Base {0}", cave.BaseList[i].BaseName), Name = String.Format("cb_base_{0}", cave.BaseList[i].BaseName), Height = 30 });

                }
                else
                {
                    box_perfils.Children.Add(new CheckBox { Content = String.Format("Entrada {0}", cave.BaseList[i].BaseName), Name = String.Format("cb_base_{0}", cave.BaseList[i].BaseName), Height = 30, IsChecked = true });

                }
            }
            cb_refbase.SelectedIndex = 0;
            cb_firstbase_check.SelectedIndex = 0;
            cb_lastbase_check.SelectedIndex = 0;
        }
        private void disableEdits()
        {
            btn_Draw.IsEnabled = false;
            cb_refbase.IsEnabled = false;
            cb_firstbase_check.IsEnabled = false;
            cb_lastbase_check.IsEnabled = false;
        }
        private void cb_firstbase_check_DropDownClosed(object sender, EventArgs e)
        {
            changeBasesRange();
        }

        private void cb_lastbase_check_DropDownClosed(object sender, EventArgs e)
        {
            changeBasesRange();
        }
        private void changeBasesRange()
        {
            try
            {
                lba_totalsize.Content = String.Format("{0} m", this.cave.CalculeTotalSize(cb_firstbase_check.SelectedItem.ToString(), cb_lastbase_check.SelectedItem.ToString()));
                cb_long_base.Items.Clear();
                
                
                foreach (BaseTopo baseTop in this.cave.retriveListBasesBetweenTwoBases(cb_firstbase_check.SelectedItem.ToString(), cb_lastbase_check.SelectedItem.ToString()))
                {
                    cb_long_base.Items.Add(baseTop.BaseName);
                    
                }
                cb_long_base.SelectedIndex = 0;
            }
            catch (Exception erro)
            {
                global::System.Windows.MessageBox.Show(erro.Message);

            }
        }
        #region Ações dos botões
        private void btn_draw_Click(object sender, RoutedEventArgs e)
        {
            Scale.scale = Int32.Parse(txt_scale.Text);
            this.Doc.Unit = cdrUnit.cdrMillimeter;
            cave.startBase = (string)cb_refbase.SelectedItem;
            ////


            ////////
            cave.DrawBases();
            cave.DrawVisada();
            Ret ret = cave.DrawGrid();
            for (int i = 0; i < this.cave.BaseList.Count; i++)
            {
                if ((bool)(box_perfils.Children[i] as CheckBox).IsChecked)
                    ret = this.cave.BaseList[i].DrawBasePerfil(ret.Rigth + 5, 6);
            }

            // cave.DrawScale();
            cave.DrawAzymuti();
        }
        private void btn_showData_Click(object sender, RoutedEventArgs e)
        {
            Data data = new Data(this.cave);
            data.Closing += data_Closing;
            data.ShowDialog();

        }
        private void btn_area_Click(object sender, RoutedEventArgs e)
        {
            Docker.corelApp.ActiveDocument.Unit = cdrUnit.cdrCentimeter;
            try
            {
                Corel.Interop.VGCore.Shape shape = Docker.corelApp.ActiveDocument.ActiveShape;
                if (shape.Type == cdrShapeType.cdrCurveShape)
                {
                    shape.Curve.Closed = true;
                    lba_area.Content = string.Format("{0:0.00}m²", shape.Curve.Area * Scale.scale / 100);
                    Docker.corelApp.ActiveDocument.Undo();
                }
            }
            catch { global::System.Windows.MessageBox.Show("Nenhuma curva selecionada"); }
        }

        private void btn_new_layer_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Layer layer = this.Doc.ActivePage.CreateLayer("Nova Camada");
            layer.Activate();
            Doc_LayerActivate(layer);
        }

        private void btn_drawlong_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                cave.DrawLong(cb_firstbase_check.SelectedItem.ToString(), cb_lastbase_check.SelectedItem.ToString(),cb_long_base.SelectedItem.ToString());
            }
            catch (Exception erro)
            {
                global::System.Windows.MessageBox.Show(erro.Message);
            }
        }

       

        private void btn_delete_layer_MouseUp(object sender, MouseButtonEventArgs e)
        {
          
            this.Doc.ActivePage.ActiveLayer.Delete();
            Doc_LayerDelete(1);
        }

        private void btn_vol_Click(object sender, RoutedEventArgs e)
        {
         
        }

        private void btn_drawScale_Click(object sender, RoutedEventArgs e)
        {
            double x = 0;
            double y = 0;
            int shift = 0;
           // ToolState toolState = new testeTool();
           // Docker.corelApp.RegisterToolState("1c3ad59d-a013-4992-a24f-36ea43555a85", "Escala", toolState);
           // Docker.corelApp.ActiveToolStateGuid = "1c3ad59d-a013-4992-a24f-36ea43555a85";
          //  global::System.Windows.MessageBox.Show(Docker.corelApp.Application.ActiveToolStateGuid);
           // SnapPoint sp = Docker.corelApp.Application.ActiveDocument.
           //CommandBar bar = Docker.corelApp.FrameWork.StatusBar;

           ////bar.ShowPopup(100,100);

           //foreach (Corel.Interop.VGCore.Controls item in bar.Controls)
           //{
           //    for (int i = 0; i < item.Count; i++)
           //    {
           //        global::System.Windows.MessageBox.Show(item[i].Caption);
           //    }
           //}
            int i = this.Doc.GetUserClick(out x, out y,out shift,10000,false,cdrCursorShape.cdrCursorSmallcrosshair);
            //this.oText.SetTextAndPosition("teste", x, y);
            cave.DrawScale(x,y);
        }
        #endregion

        private void btn_cont_Click(object sender, RoutedEventArgs e)
        {
            lba_cont.Content = string.Format("{0} m", this.cave.CalculeCont());
        }
    }
}
