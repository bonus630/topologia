using System;
using System.Collections.Generic;
using Corel.Interop.VGCore;

namespace br.corp.bonus630.topologia
{
    public class Scale
    {
       
        private double size = 10;
        private double xSPosition = 0;
        private double ySPosition = 0;
        public double Size
        {
            get { return this.size;  }
            set { size = value; }
        }
        public static int scale = 50;

        public Ret DrawScale(double startX, double startY)
        {
            xSPosition = Docker.corelApp.ConvertUnits(startX,cdrUnit.cdrMillimeter,cdrUnit.cdrCentimeter);
            ySPosition = Docker.corelApp.ConvertUnits(startY, cdrUnit.cdrMillimeter, cdrUnit.cdrCentimeter);
            return DrawScale();
        }
        public Ret DrawScale() 
        {
            Docker.corelApp.ActiveDocument.Unit = cdrUnit.cdrCentimeter;
            Layer layer;
            string layerName = String.Format("Escala 1:{0}", Scale.scale);
            if (!DocumentManager.checkLayerExist(layerName))
                layer = Docker.corelApp.ActivePage.CreateLayer(layerName);
            else
            {
                layer = Docker.corelApp.ActivePage.Layers[layerName];
                DocumentManager.deleteAllShapes(layer);
            }
            layer.Activate();
           // Corel.Interop.VGCore.Layer layer = Docker.corelApp.ActivePage.CreateLayer();
            
            double prop = 100 / Convert.ToDouble(scale);
            List<double> pontos = new List<double>();
            Corel.Interop.CorelDRAW.Color cor = new Corel.Interop.CorelDRAW.Color();
            cor.CMYKAssign(0, 0, 0, 100);

            while (this.size != 0)
            {
                pontos.Add(this.size);
                this.size = Math.Round(this.size / 2, 0, MidpointRounding.ToEven);
                if (this.size < 1)
                {
                    this.size = 0;
                    pontos.Add(this.size);
                }


            }

            //Docker.corelApp.ActiveLayer.CreateRectangle2(0.0, 0.0, pontos[0] * prop, 0.25);
            Docker.corelApp.ActiveLayer.CreateRectangle2(xSPosition, ySPosition, pontos[0] * prop, 0.25);
            bool branco = true;
            for (int i = 0; i < pontos.Count; i++)
            {
                double AtualX = pontos[i] * prop;
                double AnteX = 0;
                if (i + 1 < pontos.Count)
                {
                    AnteX = pontos[i + 1] * prop;
                }
                string unit;
                if (i == 0)
                {
                    unit = "m";
                }
                else
                {
                    unit = "";
                }
                //Corel.Interop.VGCore.Shape linha = Docker.corelApp.ActiveLayer.CreateLineSegment(AtualX, 0.0, AtualX, 0.5);
                //Corel.Interop.VGCore.Shape texto = Docker.corelApp.ActiveLayer.CreateArtisticText(AtualX - 0.4, 0.6, pontos[i].ToString() + unit,
                //                                                                            Corel.Interop.VGCore.cdrTextLanguage.cdrBrazilianPortuguese,
                //                                                                            Corel.Interop.VGCore.cdrTextCharSet.cdrCharSetDefault, "Arial", 12.0f,
                //                                                                            Corel.Interop.VGCore.cdrTriState.cdrFalse, Corel.Interop.VGCore.cdrTriState.cdrFalse,
                //                                                                            Corel.Interop.VGCore.cdrFontLine.cdrNoFontLine, Corel.Interop.VGCore.cdrAlignment.cdrNoAlignment);
                Corel.Interop.VGCore.Shape linha = Docker.corelApp.ActiveLayer.CreateLineSegment(AtualX+xSPosition, ySPosition, AtualX+xSPosition,ySPosition+ 0.5);
                Corel.Interop.VGCore.Shape texto = Docker.corelApp.ActiveLayer.CreateArtisticText(AtualX + xSPosition - 0.4, ySPosition + 0.6, pontos[i].ToString() + unit,
                                                                                            Corel.Interop.VGCore.cdrTextLanguage.cdrBrazilianPortuguese,
                                                                                            Corel.Interop.VGCore.cdrTextCharSet.cdrCharSetDefault, "Arial", 12.0f,
                                                                                            Corel.Interop.VGCore.cdrTriState.cdrFalse, Corel.Interop.VGCore.cdrTriState.cdrFalse,
                                                                                            Corel.Interop.VGCore.cdrFontLine.cdrNoFontLine, Corel.Interop.VGCore.cdrAlignment.cdrNoAlignment);
                texto.AlignToShape(Corel.Interop.VGCore.cdrAlignType.cdrAlignHCenter, linha);
                if (AtualX - AnteX > 0)
                {
                    double posicaoY = 0.0;
                    if (branco)
                    {
                        posicaoY = 0.125;
                        branco = false;
                    }
                    else
                    {
                        posicaoY = 0.0;
                        branco = true;
                    }
                    //Corel.Interop.VGCore.Shape retPreto = Docker.corelApp.ActiveLayer.CreateRectangle2(AnteX, posicaoY, AtualX - AnteX, 0.125);
                    Corel.Interop.VGCore.Shape retPreto = Docker.corelApp.ActiveLayer.CreateRectangle2(AnteX+xSPosition, posicaoY+ySPosition, (AtualX - AnteX),0.125);

                    retPreto.Fill.ApplyUniformFill(cor);
                }
            }

            Corel.Interop.VGCore.ShapeRange shaperange = Docker.corelApp.ActiveLayer.SelectableShapes.All();
            shaperange.AddToSelection();
            Corel.Interop.VGCore.Shape grupo = Docker.corelApp.ActiveSelection.Group();

            //Corel.Interop.VGCore.Shape escala = Docker.corelApp.ActiveLayer.CreateArtisticText(0.0, -0.6, "Escala Métrica" + System.Environment.NewLine + "1:" + scale,
            //                                                                               Corel.Interop.VGCore.cdrTextLanguage.cdrBrazilianPortuguese, Corel.Interop.VGCore.cdrTextCharSet.cdrCharSetDefault, "Arial",
            //                                                                                20.0f, Corel.Interop.VGCore.cdrTriState.cdrFalse, Corel.Interop.VGCore.cdrTriState.cdrFalse, Corel.Interop.VGCore.cdrFontLine.cdrNoFontLine,
            //                                                                                Corel.Interop.VGCore.cdrAlignment.cdrCenterAlignment);
            Corel.Interop.VGCore.Shape escala = Docker.corelApp.ActiveLayer.CreateArtisticText(xSPosition,ySPosition -0.6, "Escala Métrica" + System.Environment.NewLine + "1:" + scale,
                                                                                          Corel.Interop.VGCore.cdrTextLanguage.cdrBrazilianPortuguese, Corel.Interop.VGCore.cdrTextCharSet.cdrCharSetDefault, "Arial",
                                                                                           20.0f, Corel.Interop.VGCore.cdrTriState.cdrFalse, Corel.Interop.VGCore.cdrTriState.cdrFalse, Corel.Interop.VGCore.cdrFontLine.cdrNoFontLine,
                                                                                           Corel.Interop.VGCore.cdrAlignment.cdrCenterAlignment);
            escala.AlignToShape(Corel.Interop.VGCore.cdrAlignType.cdrAlignHCenter, grupo);
            grupo.AddToSelection();
            escala.AddToSelection();
            double x, y, w, h;
            Docker.corelApp.ActiveSelection.GetSize(out w,out h);
            Docker.corelApp.ActiveSelection.GetPosition(out x, out y);
            
            Docker.corelApp.ActiveSelection.Group();
            Docker.corelApp.ActiveDocument.Unit = cdrUnit.cdrMillimeter;
            return new Ret(x, y, w, h);
        }
    }
}
