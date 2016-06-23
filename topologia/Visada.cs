using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Corel.Interop.VGCore;

namespace br.corp.bonus630.topologia
{
    
    public class Visada
    {
        private Segment[] segments;
       
        public Segment[] Segments { set { this.segments = value; } }
        public Visada()
        {
            
        }
        public void AddNewSegment(Segment segment)
        {
            
        }
        public void DrawVisada()
        {
            for (int i = 0; i < this.segments.Length;i++ )
            {
                segments[i].DrawSegment();
            }
        }
    }
}
