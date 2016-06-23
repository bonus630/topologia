using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace br.corp.bonus630.topologia
{
    
    public class BaseTopoItem
    {


      
        private double  incrination, distance,  leftSide, rightSide, topSide, bottomSide;
        private string baseName, comments;

        private bool isStart = true;

        public bool IsStart
        {
            get { return isStart; }
            set { isStart = value; }
        }

        
        public string BaseName
        {
            get { return baseName; }
            set { baseName = value; }
        }
        public string RefBaseName
        {
            get { return refBaseName; }
            set { refBaseName = value; }
        }
        private int azymuti;

        public int Azymuti
        {
            get { return azymuti; }
            set { azymuti = value; }
        }
       
        public double LeftSide
        {
            get { return leftSide; }
            set { leftSide = value; }
        }
        
        public double RightSide
        {
            get { return rightSide; }
            set { rightSide = value; }
        }
     
        public double TopSide
        {
            get { return topSide; }
            set { topSide = value; }
        }
       
        public double BottomSide
        {
            get { return bottomSide; }
            set { bottomSide = value; }
        }
        

        public double Distance
        {
            get { return distance; }
            set { 
                distance = value;
              
            }
        }
       
         public double Incrination
        {
            get { return incrination; }
            set { incrination = value; }
        }
        
        public string Comments
        {
            get { return comments; }
            set { comments = value; }
        }
        private string refBaseName;

      
        
      
     
        

    }
}
