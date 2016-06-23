using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace br.corp.bonus630.topologia
{
    public class TextBoxMaskBehavior
    {
        public static void TextBox_PreviewTextInput(object sender,TextCompositionEventArgs e)
        {
            Regex rg = new Regex(@"^(\d+)?([\.|,]{1})?([\d]{0,2})?$");
            //string text = (e.Source as TextBox).Text;
            string prevText = e.Text;
            bool stop = false;
            stop = !rg.IsMatch(prevText);

          //  if (prevText == "," || prevText == ".")
           // {
           //     stop = (text.Contains(",") || text.Contains("."));
          //  }


            e.Handled = stop;
        }
    }
}
