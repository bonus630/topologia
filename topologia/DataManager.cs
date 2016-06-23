using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Security.Cryptography;
using System.Collections.ObjectModel;

namespace br.corp.bonus630.topologia
{
    class BindChanger : System.Runtime.Serialization.SerializationBinder
    {
        public override Type BindToType(string assemblyName, string typeName)
        {
            // Define the new type to bind to
            Type typeToDeserialize = null;
            // Get the current assembly
            string currentAssembly = Assembly.GetExecutingAssembly().FullName;
            // Create the new type and return it
            typeToDeserialize = Type.GetType(string.Format("{0}, {1}", typeName, currentAssembly));
            return typeToDeserialize;
        }
    }
  
    public class DataManager
    {
        private List<BaseTopo> baseList;
        public Cave cave;
      
        public DataManager(Cave cave)
        {
            this.cave = cave;
        }
        public bool Save()
        {
            return this.Save(false);
        }
        public bool SaveAs()
        {
            return this.Save(true);
        }
        private bool Save(bool saveAs)
        {

            if (String.IsNullOrEmpty(this.cave.FilePath)||saveAs)
            {
                Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog();
                sfd.Filter = "Cave file|.cav";
                if ((bool)sfd.ShowDialog())
                {
                    this.cave.FilePath = sfd.FileName;
                    return SaveXml();
                }
                else
                    return false;
            }
            else
                return SaveXml();

        }
        private bool SaveXml()
        {
            bool res = false;
            try {
                FileStream fs;
                if(File.Exists(this.cave.FilePath))
                    fs = new FileStream(this.cave.FilePath, FileMode.Truncate);
                else
                    fs = new FileStream(this.cave.FilePath, FileMode.OpenOrCreate);
           
            
            //    fs.WriteByte(new byte[0]);
            StreamWriter sw = new StreamWriter(fs,Encoding.UTF8);
    //        sw.WriteLine(String.Format("<?xml version=\"1.0\" encoding=\"utf-8\" ?><cave name=\"{0}\" filePath=\"{1}\" ><bases>",this.cave.CaveName,this.cave.FilePath));
    //        for(int i = 0; i<this.cave.BaseList.Count;i++)
    //        {
    //            if(this.cave.BaseList[i].IsStart)
    //                sw.WriteLine(String.Format("<base><name>{0}</name><isEntrace>{1}</isEntrace><down>{2}</down><up>{3}</up><left>{4}</left><right>{5}</right><coments>{6}</coments></base>", this.cave.BaseList[i].BaseName,
    //1, this.cave.BaseList[i].BottomSide, this.cave.BaseList[i].TopSide, this.cave.BaseList[i].LeftSide, this.cave.BaseList[i].RightSide, this.cave.BaseList[i].Comments));
    //            else
    //                sw.WriteLine(String.Format("<base><name>{0}</name><isEntrace>{1}</isEntrace><down>{2}</down><up>{3}</up><left>{4}</left><right>{5}</right><prevBaseName>{6}</prevBaseName><inclination>{7}</inclination><distance>{8}</distance><azymuti>{9}</azymuti><coments>{10}</coments></base>",this.cave.BaseList[i].BaseName,
    //                0, this.cave.BaseList[i].BottomSide, this.cave.BaseList[i].TopSide, this.cave.BaseList[i].LeftSide,this.cave.BaseList[i].RightSide,this.cave.BaseList[i].RefBase.BaseName,this.cave.BaseList[i].Incrination,this.cave.BaseList[i].Distance,this.cave.BaseList[i].Azymuti,this.cave.BaseList[i].Comments));
    //        }
    //        sw.WriteLine("</bases></cave>");
            string file = "";
                   file = String.Format("<?xml version=\"1.0\" encoding=\"utf-8\" ?><cave name=\"{0}\" date=\"{1}\" ><bases>",this.cave.CaveName,this.cave.DateTopo.Ticks);
            for(int i = 0; i<this.cave.BaseList.Count;i++)
            {
                if(this.cave.BaseList[i].IsStart)
                    file +=String.Format("<base><name>{0}</name><isEntrace>{1}</isEntrace><down>{2}</down><up>{3}</up><left>{4}</left><right>{5}</right><coments>{6}</coments></base>", this.cave.BaseList[i].BaseName,
    1, this.cave.BaseList[i].BottomSide, this.cave.BaseList[i].TopSide, this.cave.BaseList[i].LeftSide, this.cave.BaseList[i].RightSide, this.cave.BaseList[i].Comments);
                else
                    file+=String.Format("<base><name>{0}</name><isEntrace>{1}</isEntrace><down>{2}</down><up>{3}</up><left>{4}</left><right>{5}</right><prevBaseName>{6}</prevBaseName><inclination>{7}</inclination><distance>{8}</distance><azymuti>{9}</azymuti><coments>{10}</coments></base>",this.cave.BaseList[i].BaseName,
                    0, this.cave.BaseList[i].BottomSide, this.cave.BaseList[i].TopSide, this.cave.BaseList[i].LeftSide,this.cave.BaseList[i].RightSide,this.cave.BaseList[i].RefBase.BaseName,this.cave.BaseList[i].Incrination,this.cave.BaseList[i].Distance,this.cave.BaseList[i].Azymuti,this.cave.BaseList[i].Comments);
            }
            file+="</bases></cave>";
            sw.Write(this.Criptografar(file));
            
           // string teste = this.Descriptografar(this.Criptografar(file));
            sw.Close();
            fs.Close();
            res= true;
            }
            catch
            {
                res=false;
            }
            return res;
        }
       // public Cave Load()
        public bool Load(out Cave cave)
        {
            //Cave cave = new Cave();
            cave = new Cave();
            Microsoft.Win32.OpenFileDialog sfd = new Microsoft.Win32.OpenFileDialog();
            sfd.Filter = "Cave file|*.cav";
            if ((bool)sfd.ShowDialog())
            {
                cave.FilePath = sfd.FileName;
                FileStream fs = new FileStream(cave.FilePath, FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                string result = this.Descriptografar(sr.ReadToEnd());
                //string result = sr.ReadToEnd();
                fs.Close();
                sr.Close();
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(result);
                XmlNode node = xml.FirstChild.NextSibling;
                cave.CaveName = node.Attributes["name"].Value;
                cave.DateTopo = new DateTime(long.Parse(node.Attributes["date"].Value));
                //cave.FilePath = node.Attributes["filePath"].Value;
                XmlNodeList nodes = xml.GetElementsByTagName("base");
                for(int i =0;i<nodes.Count;i++)
                {
                    BaseTopo baseTopo = new BaseTopo();
                    XmlNodeList childs = nodes[i].ChildNodes;
                    baseTopo.BaseName = childs[0].InnerText;
                    baseTopo.IsStart = (childs[1].InnerText == "1") ? true : false;
                    baseTopo.BottomSide = Convert.ToDouble(childs[2].InnerText);
                    baseTopo.TopSide = Convert.ToDouble(childs[3].InnerText);
                    baseTopo.LeftSide = Convert.ToDouble(childs[4].InnerText);
                    baseTopo.RightSide = Convert.ToDouble(childs[5].InnerText);

                    if(!baseTopo.IsStart)
                    {
                        baseTopo.RefBase = cave.GetBaseForName(childs[6].InnerText);
                        baseTopo.Incrination = Convert.ToDouble(childs[7].InnerText);
                        baseTopo.Distance = Convert.ToDouble(childs[8].InnerText);
                        baseTopo.Azymuti = Convert.ToInt32(childs[9].InnerText);
                        baseTopo.Comments = childs[10].InnerText;
                    }

                    cave.BaseList.Add(baseTopo);
                }
                return true;
            }
            //return cave;
            return false;
        }
       // public List<BaseTopoItem> DataGridDataSource(Cave cave)
        public ObservableCollection<BaseTopoItem> DataGridDataSource(Cave cave)
        {
            this.baseList = cave.BaseList;
            ObservableCollection<BaseTopoItem> dataGridItens = new ObservableCollection<BaseTopoItem>();
            //List<BaseTopoItem> dataGridItens = new List<BaseTopoItem>();
            for (int i = 0; i < baseList.Count;i++ )
            {
                BaseTopoItem tempBaseTopoItem = new BaseTopoItem();
                tempBaseTopoItem.Azymuti = baseList[i].Azymuti;
                tempBaseTopoItem.BaseName = baseList[i].BaseName;
                tempBaseTopoItem.IsStart = baseList[i].IsStart;
                tempBaseTopoItem.Incrination = baseList[i].Incrination;
                tempBaseTopoItem.LeftSide = baseList[i].LeftSide;
                tempBaseTopoItem.RightSide = baseList[i].RightSide;
                tempBaseTopoItem.TopSide = baseList[i].TopSide;
                tempBaseTopoItem.BottomSide = baseList[i].BottomSide;
                tempBaseTopoItem.Distance = baseList[i].Distance;
                if (!baseList[i].IsStart)
                    tempBaseTopoItem.RefBaseName = baseList[i].RefBase.BaseName;
                else
                    tempBaseTopoItem.RefBaseName = "";
                dataGridItens.Add(tempBaseTopoItem);
            }
            return dataGridItens;
        }
          const string senha = "madreg10";
 
        private string Criptografar(string Message)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(senha));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            byte[] DataToEncrypt = UTF8.GetBytes(Message);
            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            
           return Convert.ToBase64String(Results);
        }
 
        private string Descriptografar(string Message)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(senha));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            byte[] DataToDecrypt = Convert.FromBase64String(Message);
            try
            {
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return UTF8.GetString(Results);
        }
    
    
    }
}
