using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Corel.Interop.VGCore;

namespace br.corp.bonus630.topologia
{
    class testeTool : ToolState
    {
        OnScreenText text;
        ToolStateAttributes toolAttributes;
        public bool IsDrawing
        {
            get { throw new NotImplementedException(); }
        }
        public testeTool()
        {
            global::System.Windows.MessageBox.Show("Estou construido");
            
        }
        public void OnAbort()
        {
            throw new NotImplementedException();
        }

        public void OnClick(Point pt, ref bool Handled)
        {
            throw new NotImplementedException();
        }

        public void OnCommit(Point pt)
        {
            throw new NotImplementedException();
        }

        public void OnDelete(ref bool Handled)
        {
            throw new NotImplementedException();
        }

        public void OnExitState()
        {
            throw new NotImplementedException();
        }

        public void OnKeyDown(int KeyCode, ref bool Handled)
        {
            throw new NotImplementedException();
        }

        public void OnKeyUp(int KeyCode, ref bool Handled)
        {
            throw new NotImplementedException();
        }

        public void OnLButtonDblClick(Point pt)
        {
            throw new NotImplementedException();
        }

        public void OnLButtonDown(Point pt)
        {
            throw new NotImplementedException();
        }

        public void OnLButtonDownLeaveGrace(Point pt)
        {
            throw new NotImplementedException();
        }

        public void OnLButtonUp(Point pt)
        {
            throw new NotImplementedException();
        }

        public void OnMouseMove(Point pt)
        {
            text.SetTextAndPosition(string.Format("x:{0} - y:{1}",pt.x,pt.y),pt.x,pt.y);
        }

        public void OnRButtonDown(Point pt, ref bool Handled)
        {
            throw new NotImplementedException();
        }

        public void OnRButtonUp(Point pt, ref bool Handled)
        {
            throw new NotImplementedException();
        }

        public void OnSnapMouse(Point pt, ref bool Handled)
        {
            throw new NotImplementedException();
        }

        public void OnStartState(ToolStateAttributes StateAttributes)
        {
            toolAttributes = StateAttributes;
            toolAttributes.SetCursor(cdrCursorShape.cdrCursorEyeDrop);
            toolAttributes.AllowTempPickState = false;
            toolAttributes.PropertyBarGuid = "4c09c866-b21f-4e04-9c35-e0dae7b4ad90";
            text = Docker.corelApp.CreateOnScreenText();
        }

        public void OnTimer(int TimerId, int TimeEllapsed)
        {
            throw new NotImplementedException();
        }
    }
}
