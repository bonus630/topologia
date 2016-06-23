using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Corel.Interop.VGCore;
 
namespace br.corp.bonus630.topologia
{
    [Serializable]
    public class Cave : DocumentManager
    {
     
        private List<BaseTopo> baseList = new List<BaseTopo>();
        [NonSerialized]
        private Document doc;
        [NonSerialized]
        private Grid grid;
        private string caveName = "";
        private string filePath="";
        public string startBase;
        private DateTime dateTopo;
        public event EventHandler BaseRemove;
        public event EventHandler BaseAdd;
        public event EventHandler BaseModify;
        public event EventHandler BaseAlone;
        public event EventHandler CaveModify;
        public int AngleCorrection { get { return 0; } }


        public DateTime DateTopo { get { return this.dateTopo; } set { this.dateTopo = value; } }
        public List<BaseTopo> BaseList
        {
            get { return baseList; }
            set 
            {
                baseList = value;
                caveModify();
            }
        }
         public string CaveName
        {
            get { return caveName; }
            set 
            {
                caveName = value;
                caveModify();
            }
        }
        public string FilePath
        {
            get { return filePath; }
            set { filePath = value; }
        }
        public Cave()
        {
           // doc = topologia.corelApp.ActiveDocument;
            
        }
        private void caveModify()
        {
            if(CaveModify!=null)
                CaveModify(this,null);
        }
        public void AddBase(BaseTopo baseTopo)
        {
            this.baseList.Add(baseTopo);
            if (BaseAdd != null)
                BaseAdd(baseTopo, null);
            caveModify();
            if (baseTopo.RefBase == null && !this.ExistReference(baseTopo))
            {
                if(BaseAlone != null)
                BaseAlone(this,null);
            }
           
        }
        public bool ExistReference(BaseTopo baseTopo)
        {
            for (int i = 0; i < this.BaseList.Count; i++)
            {
                if (baseTopo == this.BaseList[i].RefBase)
                    return true;
            }
            return false;
        }
        public void NewCave()
        {
            this.caveName = "";
            this.filePath = "";
            //for(int i =0;i<this.baseList.Count;i++)
            //{
            //    this.RemoveBase(this.baseList[i]);
            //}
            this.baseList.Clear();
            caveModify();
        }
        public void EditBase(BaseTopo baseTopo, int index)
        {

            this.baseList[index] = baseTopo;
            //cb_baseAnt.Items.Remove(this.baseTopo.BaseName);
            // btn_add_Click(null, null);
            //  this.baseTopo = null;
            if (BaseModify != null)
                BaseModify(baseTopo, null);
            caveModify();
            this.baseList[index] = baseTopo;
        }
       public void RemoveBase(BaseTopo baseTopo)
       {
           if (this.baseList.Remove(baseTopo))
           {
               if (BaseRemove != null)
                   BaseRemove(baseTopo, null);
               caveModify();
           }
       }
        public BaseTopo GetBaseForName(string name)
        {
            for (int i = 0; i < baseList.Count; i++)
            {
                if (baseList[i].BaseName == name)
                    return baseList[i];
            }
            return null;
        }
        public BaseTopo GetBaseForName(string name,bool layername)
        {
            if(name.Contains("Base"))
                 return this.GetBaseForName(name.Remove(0, 5));
            return null;
        }
        public bool BaseNameCheck(string name)
        {
            for (int i = 0; i < baseList.Count; i++)
            {
                if (baseList[i].BaseName == name)
                    return true;
            }
            return false;
        }
        public void DrawAzymuti()
        {
            this.doc = Docker.corelApp.ActiveDocument;
            this.doc.Unit = cdrUnit.cdrMillimeter;
            Layer azymuti;
            if (!DocumentManager.checkLayerExist("Azimuti"))
                azymuti = Docker.corelApp.ActivePage.CreateLayer("Azimuti");
            else
            {
                azymuti = Docker.corelApp.ActivePage.Layers["Azimuti"];
                DocumentManager.deleteAllShapes(azymuti);
            }
            azymuti.Activate();
            double x = (Math.Sin((360-this.GetBaseForName(this.startBase).Azymuti) * Math.PI / 180)) * 100;
            double y = (Math.Cos((360-this.GetBaseForName(this.startBase).Azymuti) * Math.PI / 180)) * 100;
           Shape line = doc.ActiveLayer.CreateLineSegment(0,0,x,y);
           line.Outline.EndArrow = Docker.corelApp.ArrowHeads[2];
           Corel.Interop.VGCore.Shape texto = Docker.corelApp.ActiveLayer.CreateArtisticText(x-4, y+4, "NM",
                                                                                        Corel.Interop.VGCore.cdrTextLanguage.cdrBrazilianPortuguese,
                                                                                        Corel.Interop.VGCore.cdrTextCharSet.cdrCharSetDefault, "Arial", 10.0f,
                                                                                        Corel.Interop.VGCore.cdrTriState.cdrFalse, Corel.Interop.VGCore.cdrTriState.cdrFalse,

                    Corel.Interop.VGCore.cdrFontLine.cdrNoFontLine, Corel.Interop.VGCore.cdrAlignment.cdrNoAlignment);
           texto.Rotate(this.GetBaseForName(this.startBase).Azymuti);
          
                   
        }
        public void DrawScale()
        {
            this.DrawScale(0, 0);
        }
        public void DrawScale(double x, double y)
        {
            this.doc = Docker.corelApp.ActiveDocument;
            this.doc.Unit = cdrUnit.cdrMillimeter;
            Scale scale = new Scale();
            scale.Size = grid.BigExtension;
            
            //if (!DocumentManager.checkLayerExist("Bases Topograficas"))
            //    bases = this.doc.ActivePage.CreateLayer("Bases Topograficas");
            //else
            //{
            //    bases = this.doc.ActivePage.Layers["Bases Topograficas"];
            //    DocumentManager.deleteAllShapes(bases);
            //}
            scale.DrawScale(x,y);

        }
        public void DrawBases()
        {
            this.doc = Docker.corelApp.ActiveDocument;
            this.doc.Unit = cdrUnit.cdrMillimeter;
            Layer bases;
            if (!DocumentManager.checkLayerExist("Bases Topograficas"))
                bases = Docker.corelApp.ActivePage.CreateLayer("Bases Topograficas");
            else
            {
                bases = Docker.corelApp.ActivePage.Layers["Bases Topograficas"];
                DocumentManager.deleteAllShapes(bases);
            }

           // Layer bases = doc.ActivePage.CreateLayer("Bases Topograficas");
            
            bases.Activate();
            for(int i=0;i<baseList.Count;i++)
            {
                baseList[i].AngleCorrection = this.GetBaseForName(this.startBase).Azymuti;
                baseList[i].DrawBase();
            }
           // bases.Editable = false;
        }
        public Ret DrawGrid()
        {
            Color blue = new Color();
            blue.CMYKAssign(100, 0, 0, 0);
             this.doc = Docker.corelApp.ActiveDocument;
             this.doc.Unit = cdrUnit.cdrMillimeter;
            Layer layer;
             if (!DocumentManager.checkLayerExist("Grid"))
                 layer = Docker.corelApp.ActivePage.CreateLayer("Grid");
             else
             {
                 layer = Docker.corelApp.ActivePage.Layers["Grid"];
                 DocumentManager.deleteAllShapes(layer);
             }
            // = doc.ActivePage.CreateLayer("Grid");
            layer.Color = blue;
            layer.Activate();
            grid = new Grid(this);
           
            layer.Editable = false;
            layer.Printable = false;
            return grid.DrawGrid();
        }
        public void DrawVisada()
        {
            this.doc = Docker.corelApp.ActiveDocument;
            this.doc.Unit = cdrUnit.cdrMillimeter;
            Layer layer;
            if (!DocumentManager.checkLayerExist("Visada"))
                layer = Docker.corelApp.ActivePage.CreateLayer("Visada");
            else
            {
                layer = Docker.corelApp.ActivePage.Layers["Visada"];
                DocumentManager.deleteAllShapes(layer);
            }
                //= doc.ActivePage.CreateLayer("Visada");
            layer.Activate();
            Visada visada = new Visada();
            Segment[] segments = new Segment[this.baseList.Count-1];
            int i = 0;
            foreach(BaseTopo baseT in baseList)
            {
                
                if(!baseT.IsStart){
                    Segment segment = new Segment(baseT);
                    segments[i] = segment;
                    i++;
                }
            }
            visada.Segments = segments;
            visada.DrawVisada();
            layer.Editable = false;
            layer.Printable = false;
        }
        public void DrawLong( string baseNameStart,string baseNameEnd,string baseOrientation)
        {
            this.doc = Docker.corelApp.ActiveDocument;
            this.doc.Unit = cdrUnit.cdrMillimeter;
            Layer layer;
            if (!DocumentManager.checkLayerExist("Perfil Longitudinal"))
                layer = Docker.corelApp.ActivePage.CreateLayer("Perfil Longitudinal");
            else
            {
                layer = Docker.corelApp.ActivePage.Layers["Perfil Longitudinal"];
                DocumentManager.deleteAllShapes(layer);
            }
          
            layer.Activate();
            try
            {
               
                List<BaseTopo> bases = this.retriveListBasesBetweenTwoBases(baseNameStart, baseNameEnd);
               
                //Vamos criar aqui nossa malha de pontos
                //Muita geometria

                Segment[] segments = new Segment[bases.Count-1];

                for (int i =0;i<bases.Count-1;i++)
                {
                    segments[i] = new Segment(bases[i]);
                    
                }
                Visada visada = new Visada();
                visada.Segments = segments;
                visada.DrawVisada();
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }
        private void Draw3d(int xAngle, int yAngle, int zAngle)
        {

        }
        public double CalculeTotalSize(string baseNameStart, string baseNameEnd)
        {
            try
            {
                double result = 0;
                List<BaseTopo> baseList = this.retriveListBasesBetweenTwoBases(baseNameStart, baseNameEnd);
                foreach (BaseTopo b in baseList)
                {
                    if(baseList.Contains(b.RefBase))
                        result += b.Distance;
                }
                return result;
            }
            catch(Exception erro)
            {
                throw erro;
            }
        }
        public List<BaseTopo> retriveListBasesBetweenTwoBases(string baseNameStart, string baseNameEnd)
        {
            

            BaseTopo firstBase = this.GetBaseForName(baseNameStart);
            BaseTopo lastBase = this.GetBaseForName(baseNameEnd);
            BaseTopo tempBase;
            List<BaseTopo> listBase = new List<BaseTopo>();
            bool searching = true;
            if (firstBase.RefBase != null)
            {
                tempBase = firstBase.RefBase;
                listBase.Add(firstBase);
                while (searching)
                {
                    if (tempBase.RefBase != null)
                    {
                        listBase.Add(tempBase);
                        
                       
                    }
                    else
                        searching = false;
                    if (tempBase == lastBase)
                        {
                            listBase.Add(lastBase);
                            return listBase;
                        }
                    tempBase = tempBase.RefBase;
                }
            }
            if(lastBase.RefBase != null)
            {
                searching = true;
                //listBase.Clear();
                tempBase = lastBase.RefBase;
                listBase.Add(lastBase);
                while (searching)
                {
                    if(listBase.Contains(tempBase))
                    {
                        searching = false;
                        return listBase;
                    }
                    if (tempBase.RefBase != null)
                    {
                        listBase.Add(tempBase);
                        
                       
                    }
                    else
                        searching = false; 
                    if (tempBase == firstBase)
                        {
                            listBase.Add(firstBase);
                            return listBase;
                        }
                    tempBase = tempBase.RefBase;
                }
            }
            else
            {
                listBase.Clear();
                return listBase;
            }
            throw new Exception("Não existe conexão entre essas duas bases");
        }



        internal void Generate3DPoints()
        {
            foreach (BaseTopo baseTopo in this.baseList)
            {
                baseTopo.SetPositions();
            }
        }

        internal double CalculeCont()
        {
            double i = 0;
            foreach (BaseTopo baseT in baseList)
            {

                if (!baseT.IsStart)
                {
                    i += baseT.Distance;
                }
            }
            return i;
        }
    }
}
