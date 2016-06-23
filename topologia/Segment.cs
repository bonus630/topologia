using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Corel.Interop.VGCore;

namespace br.corp.bonus630.topologia
{
    public class Segment
    {
       
        private BaseTopo currentBase;
       
        public Segment(BaseTopo currentBase)
        {
           
            this.currentBase = currentBase;
            

        }
        public void DrawSegment()
        {
            Docker.corelApp.ActiveLayer.CreateLineSegment(this.currentBase.XPosition, this.currentBase.YPosition, this.currentBase.RefBase.XPosition, this.currentBase.RefBase.YPosition);
        }
    }
}
