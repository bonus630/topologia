using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace br.corp.bonus630.topologia
{
    public class BitmapResources
    {
        public BitmapSource VisibleEnable
        {
            get
            {
                return genereteBitmapSource(topologia.Properties.Resources.visibleEnable);
            }
           
        }

        public BitmapSource VisibleDisable
        {
            get
            {
                return genereteBitmapSource(topologia.Properties.Resources.visibleDisable);
            }
           
        }
        public BitmapSource SelectionEnable
        {
            get
            {
                return genereteBitmapSource(topologia.Properties.Resources.selection);
            }
            
        }
        public BitmapSource SelectionDisable
        {
            get
            {
                return genereteBitmapSource(topologia.Properties.Resources.selectionDisable);
            }

        }
        public BitmapSource PrintEnable
        {
            get
            {
                return genereteBitmapSource(topologia.Properties.Resources.printEnable);
            }
            
        }
        public BitmapSource PrintDisable
        {
            get
            {
                return genereteBitmapSource(topologia.Properties.Resources.printDisable);
            }
            
        }
        public BitmapSource EditableEnable
        {
            get
            {
                return genereteBitmapSource(topologia.Properties.Resources.editableEnable);
            }
            
        }
        public BitmapSource EditableDisable
        {
            get
            {
                return genereteBitmapSource(topologia.Properties.Resources.editableDisable);
            }
            
        }
        public BitmapSource Facebook
        {
            get
            {
                return genereteBitmapSource(topologia.Properties.Resources.facebook);
            }

        }
        public BitmapSource GooglePlus
        {
            get
            {
                return genereteBitmapSource(topologia.Properties.Resources.googleplus);
            }

        }
        public BitmapSource Linkedin
        {
            get
            {
                return genereteBitmapSource(topologia.Properties.Resources.linkedin);
            }

        }
        public BitmapSource Twitter
        {
            get
            {
                return genereteBitmapSource(topologia.Properties.Resources.twitter);
            }

        }
        public BitmapSource Youtube
        {
            get
            {
                return genereteBitmapSource(topologia.Properties.Resources.youtube);
            }

        }
        public BitmapSource newLayer
        {
            get 
            {
                return genereteBitmapSource(topologia.Properties.Resources.newLayer);
            }
        }
        public BitmapSource deleteLayer
        {
            get
            {
                return genereteBitmapSource(topologia.Properties.Resources.deleteLayer);
            }
        }
        private BitmapSource genereteBitmapSource(System.Drawing.Bitmap resource)
        {
            var image = resource;
            var bitmap = new System.Drawing.Bitmap(image);
            var bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(),
                                                                                  IntPtr.Zero,
                                                                                  Int32Rect.Empty,
                                                                                  BitmapSizeOptions.FromEmptyOptions()
                  );

            bitmap.Dispose();
            return bitmapSource;
        }

        public BitmapSource Bonus630
        {
            get
            {
                return genereteBitmapSource(topologia.Properties.Resources.bonus630);
            }
        }
    }
}
