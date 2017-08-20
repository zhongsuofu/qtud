using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
//using F0_DetectingCeps;
using Qtud.DBManage.Model;

namespace Qtud.Qtud
{
    class DrawFuns
    {
        private string ValueUnit = "cmH2O";

        //按像素点画线
        public void plotLinePixel(ref Panel panel,PointF[] Sp,Size size1)
        {
            int Len = Sp.Length;

            Bitmap canvas = new Bitmap(size1.Width,size1.Height);  // 画布、笔  panel.Width, panel.Height
            Graphics offScreenDC = Graphics.FromImage(canvas);
            Pen pen = new Pen(Color.Green);

            double width = canvas.Width;     // 画布长宽，中线
            double height = canvas.Height;
            double center = height / 2;  

            double max = 0;                // 数据中最大值的abs（）
            for(int i=0; i<Sp.Length;i++)
            {
                if (max < Math.Abs(Sp[i].Y))
                    max = Math.Abs(Sp[i].Y);
                if (max == 0) max = 0.0000000001;
            }

            double scaleX;
            double scaleY;
            scaleX = Sp.Length / width;   // 从Sp数组中，按照窗口的横纵比例，有选择的输出到屏幕
            scaleY = 2 * max / height;     // 窗高的一般为单位 1

            
            int xPrev = 0, yPrev = 0;
            for (int x = 0; x < width; x++)       
            {
                int y = (int)(center - (Sp[(int)(x*scaleX)].Y)/(float)scaleY );  
                // 点数多了要跳过：x*scaleX； 数值大了要缩小： 数值除以scaleY
            
                if (x == 0)
                {
                    xPrev = 0;
                    yPrev = y;
                }
                else
                {
                    offScreenDC.DrawLine(pen, xPrev, yPrev, x, y);
                    xPrev = x;
                    yPrev = y;
                }
            }
            
            panel.BackgroundImage = canvas;   
            offScreenDC.Dispose();
    
        }

        public void plotDot(ref Panel panel, PointF[] Sp,Size size1)
        {
            int Len = Sp.Length;

            Bitmap canvas = new Bitmap(size1.Width, size1.Height);  // 画布、笔
            Graphics offScreenDC = Graphics.FromImage(canvas);
            

            double width = canvas.Width;     // 画布长宽，中线
            double height = canvas.Height;
            double center = height / 2;  

            double max = 0;                // 数据中最大值的abs（）
            for (int i = 0; i < Sp.Length; i++)
            {
                if (max < Math.Abs(Sp[i].Y))
                    max = Math.Abs(Sp[i].Y);
                if (max == 0) max = 0.0000000001;
            }

            double scaleX;
            double scaleY;
            scaleX = Sp.Length / width;   // 从Sp数组中，按照窗口的横纵比例，有选择的输出到屏幕
            scaleY = 2 * max / height;     // 窗高的一般为单位 1


            int xPrev = 0, yPrev = 0;
            for (int x = 0; x < Len; x++ )
            {
                int y = (int)(center - (Sp[x].Y) / (float)scaleY);
                // 点数多了要跳过：x*scaleX； 数值大了要缩小： 数值除以scaleY

                // offScreenDC.DrawLine(pen, xPrev, yPrev, x, y);                    
                xPrev = (int)(x/scaleX);
                yPrev = y;
                offScreenDC.FillEllipse(Brushes.Red, xPrev, yPrev, 5, 5);               
            }

            panel.BackgroundImage = canvas;
            offScreenDC.Dispose();
        }

        //按坐标点画线
        public void plotLine(ref Panel panel, PointF[] Sp, Size size1)
        {
            int Len = Sp.Length;

            Bitmap canvas = new Bitmap(size1.Width, size1.Height);  // 画布、笔
            Graphics offScreenDC = Graphics.FromImage(canvas);
            Pen pen = new Pen(Color.Red );

            double width = canvas.Width;     // 画布长宽，中线
            double height = canvas.Height;
            double center = height / 2;

            double max = 0;                // 数据中最大值的abs（）
            for (int i = 0; i < Sp.Length; i++)
            {
                if (max < Math.Abs(Sp[i].Y))
                    max = Math.Abs(Sp[i].Y);
                if (max == 0) max = 0.0000000001;
            }

            double scaleX;
            double scaleY;
            scaleX = Sp.Length / width;   // 从Sp数组中，按照窗口的横纵比例，有选择的输出到屏幕
            scaleY = 2 * max / height;     // 窗高的一般为单位 1

                        
            for (int i = 0; i < Len-1; i++)
            {
                int xFirst = (int)(Sp[i].X / scaleX);
                int yFirst = (int)(center - (Sp[i].Y) / (float)scaleY);
                int xNext = (int)(Sp[i+1].X / scaleX);
                int yNext = (int)(center - (Sp[i+1].Y) / (float)scaleY); 
              
                offScreenDC.DrawLine(pen, xFirst, yFirst, xNext, yNext);   
                
            }
            panel.BackgroundImage = canvas;
            offScreenDC.Dispose();
        }

        public void plotLineBeziers(ref Panel panel, PointF[] Sp)
        {
            int Len = Sp.Length;

            Bitmap canvas = new Bitmap(panel.Width, panel.Height);  // 画布、笔
            Graphics offScreenDC = Graphics.FromImage(canvas);
            Pen pen = new Pen(Color.Red);

            double width = canvas.Width;     // 画布长宽，中线
            double height = canvas.Height;
            double center = height / 2;

            double max = 0;                // 数据中最大值的abs（）
            for (int i = 0; i < Sp.Length; i++)
            {
                if (max < Math.Abs(Sp[i].Y))
                    max = Math.Abs(Sp[i].Y);
                if (max == 0) max = 0.0000000001;
            }

            double scaleX;
            double scaleY;
            scaleX = Sp.Length / width;   // 从Sp数组中，按照窗口的横纵比例，有选择的输出到屏幕
            scaleY = 2 * max / height;     // 窗高的一般为单位 1


            for (int i = 0; i < Len; i++)
            {
                Sp[i].X = (int)(Sp[i].X / scaleX);
                Sp[i].Y = (int)(center - (Sp[i].Y) / (float)scaleY);
            }
            offScreenDC.DrawBeziers(pen, Sp);

            panel.BackgroundImage = canvas;
            offScreenDC.Dispose();
        }



        //============================================================================================
        //按坐标点画线，直接画给定的点，不算中间位置
        private Bitmap canvas = null;
        private Graphics offScreenDC = null;
        private Panel m_panel = null;
        public void IniDraw(ref MyPanel panel )
        {
            canvas = new Bitmap(panel.Width, panel.Height);  // 画布、笔
            canvas.MakeTransparent();
                 
            offScreenDC = Graphics.FromImage(canvas);
            offScreenDC.Clear(Color.White);
            m_panel = panel;
        }

        public void SetValueUnit(string _ValueUnit)
        {
            ValueUnit = _ValueUnit;
        }

        public void ClearDC( )
        {
            if (offScreenDC!=null)
                offScreenDC.Clear(Color.White);

        }


        //填充背景色
        public void FillRectangleBG(Color clr, Rectangle rect)
        {
            if (offScreenDC != null)
            {
                SolidBrush drawBrush = new SolidBrush(clr);
                offScreenDC.FillRectangle(drawBrush, rect);
            }

        }


        public void IniPrintDraw(Graphics graphic, Size DrawSize)
        {
            canvas = new Bitmap(DrawSize.Width, DrawSize.Height);  // 画布、笔

            offScreenDC = graphic ;
           
        }

        public void UpdateDraw( )
        { 
            m_panel.BackgroundImage = canvas;  
            //offScreenDC.Dispose();
        }

        public void Dispose()
        {
            if (offScreenDC != null) 
                offScreenDC.Dispose(); 
        }

        //isflag ，是否画虚线
        public void plotLine2(Point[] Sp, bool isflag = false, Pen pen = null)
        {
            int Len = Sp.Length;

            //Bitmap canvas = new Bitmap(size1.Width, size1.Height);  // 画布、笔
            //Graphics offScreenDC = Graphics.FromImage(canvas);
            if(pen  == null)
                pen = new Pen(Color.FromArgb(100, 100, 100));

            if (isflag)
            {
                pen.DashStyle = DashStyle.Custom;
                pen.DashPattern = new float[] { 1f, 2f };
            }
            
            for (int i = 0; i < Len - 1; i++)
            {
                int xFirst =  (Sp[i].X );
                int yFirst =  (Sp[i].Y)  ;
                int xNext =  Sp[i + 1].X  ;
                int yNext =  Sp[i + 1].Y;

                offScreenDC.DrawLine(pen, xFirst, yFirst, xNext, yNext);

            }
            //panel.BackgroundImage = canvas;
            //offScreenDC.Dispose();
        }

        //按坐标点画线,按中心横线  返回分隔线X位置
        public int plotLine3(ref List<StruData> list_data, Size range, Rectangle DrawRect, Color Pencolor, ref Dictionary<int, Index_value> x_value_map,int FirstFileIndex = -1)
        {
            int Len = list_data.Count();
            if (Len < 1)
                return -1;

            //Bitmap canvas = new Bitmap(size1.Width, size1.Height);  // 画布、笔
            //Graphics offScreenDC = Graphics.FromImage(canvas);
            Pen pen = new Pen(Pencolor);

            double width = DrawRect.Size.Width;     // 画布长宽，中线
            double height = DrawRect.Size.Height;
            //double center = DrawRect.Top + height / 2;

            //double max = 0;                // 数据中最大值的abs（）
            //foreach (StruData tempData in list_data)  //遍历文件
            //{
            //    if (max < Math.Abs(tempData.value))
            //        max = Math.Abs(tempData.value);
            //    if (max == 0) max = 0.0000000001;
            //}

            double scaleX;  
            double scaleY;
            if (Len>width)
                scaleX = (Len-1) / width;   // 从Sp数组中，按照窗口的横纵比例，有选择的输出到屏幕
            else if (Len == width)
                scaleX = 1;   // 从Sp数组中，按照窗口的横纵比例，有选择的输出到屏幕
            else
                scaleX = width / Len;

            scaleY = (range.Height - range.Width) / height;     // 窗高的一般为单位 1
             
            


            int xFirst = (int) DrawRect.Left;
            int yFirst = (int)DrawRect.Bottom - (int)((list_data[0].value  - range.Width) / scaleY);
            if (list_data[0].value < range.Width || yFirst > DrawRect.Bottom)
            {
                yFirst = (int)DrawRect.Bottom  ;
            }
            if (list_data[0].value > range.Height || yFirst < DrawRect.Top)
            {
                yFirst = (int)DrawRect.Top  ;

            }

            Index_value m_Index_value = new Index_value 
            {
                nIndex = 0,
                Value = list_data[0].value.ToString(),
            };
            x_value_map.Add(0, m_Index_value);

            int PosX = -1;  //分隔线X位置

            if (Len >= width)
            {
                for (int i = 1; i < width; i++)
                {
                    int xNext = (int)i + DrawRect.Left;

                    int ipos = (int)(i * scaleX);  //采样
                    if (ipos > Len - 1)
                        return PosX;


                    int yNext = (int)DrawRect.Bottom - (int)((list_data[ipos].value - range.Width) / scaleY);

                    if ((int)list_data[ipos].value < range.Width || yNext > DrawRect.Bottom)
                    {
                        yNext = (int)DrawRect.Bottom;
                    }
                    if ((int)list_data[ipos].value > range.Height || yNext < DrawRect.Top)
                    {
                        yNext = (int)DrawRect.Top;

                    }

                    offScreenDC.DrawLine(pen, xFirst, yFirst, xNext, yNext);

                    Index_value m_Index_value1 = new Index_value 
                    {
                        nIndex = ipos,
                        Value = list_data[ipos].value.ToString(),
                    };
                    x_value_map.Add(i, m_Index_value1);

                    xFirst = xNext;
                    yFirst = yNext;

                    if (FirstFileIndex > -1 && FirstFileIndex <= ipos && PosX == -1)
                    {
                        PosX = xFirst;
                    }
                }
            }
            else
            {  
                for (int i = 0; i < Len ; i++)
                { 
                    int xNext =  DrawRect.Left + (int) ((i+1)*scaleX);
                    int yNext = (int)DrawRect.Bottom - (int)((list_data[i].value - range.Width) / scaleY);

                    if ((int)list_data[i].value < range.Width || yNext > DrawRect.Bottom)
                    {
                        yNext = (int)DrawRect.Bottom;
                    }
                    if ((int)list_data[i].value > range.Height || yNext < DrawRect.Top)
                    {
                        yNext = (int)DrawRect.Top;

                    }
                    
                    offScreenDC.DrawLine(pen, xFirst, yFirst, xNext, yNext);

                    for (int iw = (xFirst - DrawRect.Left) + 1; iw <= xNext - DrawRect.Left; iw++)
                    {
                        Index_value m_Index_value1 = new Index_value 
                        {
                            nIndex = i,
                            Value = list_data[i].value.ToString(),
                        };
                        x_value_map.Add(iw, m_Index_value1);
                    }

                    xFirst = xNext;
                    yFirst = yNext;

                    if (FirstFileIndex > -1 && FirstFileIndex <= i && PosX == -1)
                    {
                        PosX = xFirst;
                    }
                }

            }

            return PosX;
            
            //panel.BackgroundImage = canvas;
            //offScreenDC.Dispose();
        }



        //画行标题
        public void DrawRowtitle( string strTitle, string strUnitName,string strMaxValue, Size range, Rectangle DrawRect, Color Pencolor)
        { 

            //Bitmap canvas = new Bitmap(size1.Width, size1.Height);  // 画布、笔
            //Graphics offScreenDC = Graphics.FromImage(canvas);
            Pen pen = new Pen(Pencolor); 

            // Create font and brush.
            Font drawFont = new Font("Arial", 11);
            SolidBrush drawBrush = new SolidBrush(Pencolor);

            offScreenDC.DrawString(strTitle, drawFont, drawBrush, new PointF(DrawRect.Left + 3, DrawRect.Top + 5)); 
            offScreenDC.DrawString(range.Height.ToString(), drawFont, drawBrush, new PointF(DrawRect.Right - DrawRect.Width / 3 -3, DrawRect.Top + 5));

            offScreenDC.DrawString(strUnitName, drawFont, drawBrush, new PointF(DrawRect.Left + 3, DrawRect.Bottom - 25));
            offScreenDC.DrawString(range.Width.ToString(), drawFont, drawBrush, new PointF(DrawRect.Right - DrawRect.Width / 3 - 3, DrawRect.Bottom - 25));

            //--------------------------------------------
            // Create point for upper-left corner of drawing.
            PointF drawPoint = new PointF(DrawRect.Left + DrawRect.Width / 4-2, DrawRect.Top + DrawRect.Height * 2 / 5);
            Font drawFont1 = new Font("Arial", 23);
            if (float.Parse(strMaxValue) < 0)
            {
                if (strMaxValue.Length > 4)
                {
                    drawFont1 = new Font("Arial", 20);
                    drawPoint = new PointF(DrawRect.Left + DrawRect.Width / 4, DrawRect.Top + DrawRect.Height * 2 / 5);
                }
                // Draw string to screen.
                offScreenDC.DrawString(strMaxValue, drawFont1, drawBrush, drawPoint);
            }
            else
            {
                drawPoint = new PointF(DrawRect.Left, DrawRect.Top + DrawRect.Height * 2 / 5);

                RectangleF rtf = new Rectangle(new Point(DrawRect.Left, DrawRect.Top + DrawRect.Height * 2 / 5), new Size(DrawRect.Width, DrawRect.Height * 2 / 5));

                StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
                format.LineAlignment = StringAlignment.Center;  // 更正： 垂直居中
                format.Alignment = StringAlignment.Center;      // 水平居中

                offScreenDC.DrawString(strMaxValue, drawFont1, drawBrush, rtf, format);
            } 
             
            //panel.BackgroundImage = canvas;
            //offScreenDC.Dispose();
        }

        //绘制报告标题，病人信息等非曲线图信息，返回当前绘制的高度位置  isHideTail是否显示页脚
        public int DrawReportTitle(PatientInfoModel CurSelPatientInfo, TestDatas _TestData, Rectangle DrawRect, bool isHideTail = false, bool IsPrintReportheadPage = true)
        {
            int nCurHeight = 0;
            string strTemp = string.Empty;

            Font drawFont = new Font("宋体 ", 22);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            Pen pen = new Pen(Color.Black,2); 

            StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
            format.LineAlignment = StringAlignment.Center;  // 更正： 垂直居中
            format.Alignment = StringAlignment.Center;      // 水平居中

            nCurHeight +=  50;
            RectangleF rtf = new Rectangle(new Point(0, 0), new Size(DrawRect.Width  , nCurHeight));
            rtf.Location = new PointF(DrawRect.Left, DrawRect.Top);
            strTemp = "尿动力学报告";
            if(strTemp.Length <=11)
                offScreenDC.DrawString(strTemp, drawFont, drawBrush, rtf, format);
            else if (strTemp.Length <= 13)
                offScreenDC.DrawString(strTemp, new Font("宋体 ", 20), drawBrush, rtf, format);
            else if (strTemp.Length <= 15)
                offScreenDC.DrawString(strTemp, new Font("宋体 ", 18), drawBrush, rtf, format);
             
            offScreenDC.DrawLine(pen, DrawRect.Left, DrawRect.Top + nCurHeight, DrawRect.Right, DrawRect.Top + nCurHeight);

            //------------------------------------------------------------
            nCurHeight += 10; //横线
            int nrowH = 25;
            Font drawFont2 = new Font("宋体", 12);
            rtf.Size = new Size((int)DrawRect.Width/2, nrowH);
            rtf.Location = new PointF(DrawRect.Left, DrawRect.Top + nCurHeight);
            format.Alignment = StringAlignment.Far;      // 水平居中
            offScreenDC.DrawString("姓名： " + CurSelPatientInfo.name, drawFont2, drawBrush, rtf, format);


            rtf.Location = new PointF(DrawRect.Left + (int)DrawRect.Width / 2, DrawRect.Top + nCurHeight);
            if (CurSelPatientInfo.sex == 0)
            {
                strTemp = "女";
            }
            else
            {
                strTemp = "男";
            }
            offScreenDC.DrawString("性别： " + strTemp, drawFont2, drawBrush, rtf, format);
            //------------------------------------------------------------
            nCurHeight += nrowH; //第一行  
            rtf.Location = new PointF(DrawRect.Left, DrawRect.Top + nCurHeight);
            format.Alignment = StringAlignment.Far;      // 水平居中
            if (  CurSelPatientInfo.cardid != "")
                offScreenDC.DrawString("身份证号： " + CurSelPatientInfo.cardid, drawFont2, drawBrush, rtf, format);
            else
                offScreenDC.DrawString("身份证号： ", drawFont2, drawBrush, new PointF(DrawRect.Left, DrawRect.Top + nCurHeight + 5)); 


            strTemp = string.Empty;
            if (CurSelPatientInfo.birth != null)
            {
                DateTime dt = DateTime.Now;
                int year = dt.Year - CurSelPatientInfo.birth.Year;
                strTemp = year.ToString();

            }

            rtf.Location = new PointF(DrawRect.Left + (int)DrawRect.Width / 2, DrawRect.Top + nCurHeight);
            offScreenDC.DrawString("年龄： "+strTemp+" 岁 ", drawFont2, drawBrush, rtf, format);
            //------------------------------------------------------------

            nCurHeight += nrowH; //第二行  
            rtf.Location = new PointF(DrawRect.Left, DrawRect.Top + nCurHeight);
            format.Alignment = StringAlignment.Far;      // 水平居中
            if (_TestData.strKS != null && _TestData.strKS != "" )
                offScreenDC.DrawString("科室： "+_TestData.strKS, drawFont2, drawBrush, rtf, format);
            else
                offScreenDC.DrawString("科室： " , drawFont2, drawBrush, new PointF(DrawRect.Left, DrawRect.Top + nCurHeight+10)); 


            rtf.Location = new PointF(DrawRect.Left + (int)DrawRect.Width / 2, DrawRect.Top + nCurHeight);
            //offScreenDC.DrawString("床号： 2  床 ", drawFont2, drawBrush, rtf, format);
            if (_TestData.strCH != null && _TestData.strCH != "")
                offScreenDC.DrawString("床号： " + _TestData.strCH + " 床", drawFont2, drawBrush, rtf, format);
            else
                offScreenDC.DrawString("床号： ", drawFont2, drawBrush, new PointF(DrawRect.Left + (int)DrawRect.Width / 2, DrawRect.Top + nCurHeight + 10));

            //------------------------------------------------------------
            nCurHeight += nrowH; //第三行  
            rtf.Location = new PointF(DrawRect.Left, DrawRect.Top + nCurHeight);
            rtf.Width = DrawRect.Width;
            format.Alignment = StringAlignment.Far;      // 水平居中
            if (_TestData.strBS != null && _TestData.strBS != "")
            {
                nrowH = 60;
                rtf.Height = nrowH;
                offScreenDC.DrawString("病史:  " + _TestData.strBS, drawFont2, drawBrush, rtf, format);
            }
            else
            {
                nrowH = 30;
                offScreenDC.DrawString("病史： ", drawFont2, drawBrush, new PointF(DrawRect.Left, DrawRect.Top + nCurHeight + 10));
            }
             
            //------------------------------------------------------------
            nCurHeight += nrowH + 5;   // 画横线  
            offScreenDC.DrawLine(pen, DrawRect.Left, DrawRect.Top + nCurHeight, DrawRect.Right, DrawRect.Top + nCurHeight);
            nCurHeight += 5;   // 画横线  


            if (!IsPrintReportheadPage)
                return nCurHeight;
            //------------------------------------------------------------
            nrowH = 30;
            rtf.Height = nrowH;
            rtf.Width = DrawRect.Width;
            rtf.Location = new PointF(DrawRect.Left, DrawRect.Top + nCurHeight);
            if (_TestData.strNLL != null && _TestData.strNLL != "")
                offScreenDC.DrawString("尿流率检查结果： 最大尿流率 " + _TestData.strNLL + @" ml/s    尿流量 " + _TestData.strNL + @" ml", new Font("宋体", 14, FontStyle.Bold), drawBrush, rtf, format);
            else
                offScreenDC.DrawString("尿流率检查结果： 最大尿流率         ml/s   尿流量         ml ", new Font("宋体", 14, FontStyle.Bold), drawBrush, new PointF(DrawRect.Left, DrawRect.Top + nCurHeight + 10));
            //------------------------------------------------------------
            nCurHeight += nrowH;
            nCurHeight += 20;  

            nrowH = 25;
            rtf.Height = nrowH;
            rtf.Width = DrawRect.Width;

            rtf.Location = new PointF(DrawRect.Left, DrawRect.Top + nCurHeight);
            //if (_TestData.str_RJ_YL != null && _TestData.str_RJ_YL != "")
            //    offScreenDC.DrawString("充盈期膀胱容积-压力测定结果： " + _TestData.str_RJ_YL, new Font("宋体", 14, FontStyle.Bold), drawBrush, rtf, format);
            //else
                offScreenDC.DrawString("充盈期膀胱容积-压力测定结果： ", new Font("宋体", 14, FontStyle.Bold), drawBrush, new PointF(DrawRect.Left, DrawRect.Top + nCurHeight + 10));
            //------------------------------------------------------------
            nCurHeight += nrowH;
             

            rtf.Location = new PointF(DrawRect.Left, DrawRect.Top + nCurHeight);
            offScreenDC.DrawString("膀胱容量 ml： ", new Font("宋体", 12), drawBrush, new PointF(DrawRect.Left, DrawRect.Top + nCurHeight + 10));

            int stepw = (int)((DrawRect.Width / 4));

            offScreenDC.DrawString("初感 " + _TestData.str_nl_cg, new Font("宋体", 12), drawBrush, new PointF(DrawRect.Left + stepw * 1, DrawRect.Top + nCurHeight + 10));

            offScreenDC.DrawString("正常 " + _TestData.str_nl_zc, new Font("宋体", 12 ), drawBrush, new PointF(DrawRect.Left + stepw * 2, DrawRect.Top + nCurHeight + 10));
            offScreenDC.DrawString("最大 " + _TestData.str_nl_zd, new Font("宋体", 12 ), drawBrush, new PointF(DrawRect.Left + stepw * 3, DrawRect.Top + nCurHeight + 10));

            
            //------------------------------------------------------------
            nCurHeight += nrowH;

            rtf.Location = new PointF(DrawRect.Left, DrawRect.Top + nCurHeight);
            nrowH = 30;
            rtf.Height = nrowH;
            offScreenDC.DrawString("膀胱顺应性： ", new Font("宋体", 12), drawBrush, new PointF(DrawRect.Left, DrawRect.Top + nCurHeight + 10));
            offScreenDC.DrawString( _TestData.str_syx, new Font("宋体", 12), drawBrush, new PointF(DrawRect.Left +  stepw * 1, DrawRect.Top + nCurHeight + 10));
           
            //offScreenDC.DrawString("正常 " + _TestData.str_nl_cg, new Font("宋体", 12), drawBrush, new PointF(DrawRect.Left + stepw * 1, DrawRect.Top + nCurHeight + 10));
            //offScreenDC.DrawString("高顺应性 " + _TestData.str_nl_cg, new Font("宋体", 12), drawBrush, new PointF(DrawRect.Left + stepw * 2, DrawRect.Top + nCurHeight + 10));
            //offScreenDC.DrawString("低顺应性 " + _TestData.str_nl_cg, new Font("宋体", 12), drawBrush, new PointF(DrawRect.Left + stepw * 3, DrawRect.Top + nCurHeight + 10));

            //------------------------------------------------------------
            nCurHeight += nrowH;

            rtf.Location = new PointF(DrawRect.Left, DrawRect.Top + nCurHeight);
            nrowH = 30;
            rtf.Height = nrowH;
            offScreenDC.DrawString("膀胱稳定性： ", new Font("宋体", 12), drawBrush, new PointF(DrawRect.Left, DrawRect.Top + nCurHeight + 10));
            offScreenDC.DrawString( _TestData.str_wdx, new Font("宋体", 12), drawBrush, new PointF(DrawRect.Left + stepw * 1, DrawRect.Top + nCurHeight + 10));
            //offScreenDC.DrawString("正常 " + _TestData.str_nl_cg, new Font("宋体", 12), drawBrush, new PointF(DrawRect.Left + stepw * 1, DrawRect.Top + nCurHeight + 10));
            //offScreenDC.DrawString("逼尿肌活动过度 " + _TestData.str_nl_cg, new Font("宋体", 12), drawBrush, new PointF(DrawRect.Left + stepw * 2, DrawRect.Top + nCurHeight + 10));

            //------------------------------------------------------------
            nCurHeight += nrowH;
            nCurHeight += 20;

            nrowH = 30;
            rtf.Height = nrowH;
            rtf.Width = DrawRect.Width;

            rtf.Location = new PointF(DrawRect.Left, DrawRect.Top + nCurHeight);
            //if (_TestData.str_tsjc != null && _TestData.str_tsjc != "")
            //    offScreenDC.DrawString("特殊检查： " + _TestData.str_tsjc, new Font("宋体", 14, FontStyle.Bold), drawBrush, rtf, format);
            //else
                offScreenDC.DrawString("特殊检查： ", new Font("宋体", 14, FontStyle.Bold), drawBrush, new PointF(DrawRect.Left, DrawRect.Top + nCurHeight + 10));
            //------------------------------------------------------------

            nCurHeight += nrowH;
            offScreenDC.DrawString("Valsalva 腹腔漏尿点压力（VLPP)： ", new Font("宋体", 14, FontStyle.Bold), drawBrush, new PointF(DrawRect.Left, DrawRect.Top + nCurHeight + 10));
           
            if (_TestData.str_vlpp != null && _TestData.str_vlpp != "")
                offScreenDC.DrawString(_TestData.str_vlpp, new Font("宋体", 14, FontStyle.Bold), drawBrush, new PointF(DrawRect.Right - (int)(DrawRect.Width / 5 * 1.5)  , DrawRect.Top + nCurHeight + 10));


            offScreenDC.DrawString(" " + ValueUnit, new Font("宋体", 14, FontStyle.Bold), drawBrush, new PointF(DrawRect.Right - (int)(DrawRect.Width / 5), DrawRect.Top + nCurHeight + 10));
            //------------------------------------------------------------
            nCurHeight += nrowH;
            offScreenDC.DrawString("逼尿肌漏尿点压（DLPP)： ", new Font("宋体", 14, FontStyle.Bold), drawBrush, new PointF(DrawRect.Left, DrawRect.Top + nCurHeight + 10));
            if (_TestData.str_dlpp != null && _TestData.str_dlpp != "")
                offScreenDC.DrawString(_TestData.str_dlpp, new Font("宋体", 14, FontStyle.Bold), drawBrush, new PointF(DrawRect.Right - (int)(DrawRect.Width / 5* 1.5)  , DrawRect.Top + nCurHeight + 10));
            offScreenDC.DrawString(" " + ValueUnit, new Font("宋体", 14, FontStyle.Bold), drawBrush, new PointF(DrawRect.Right - (int)(DrawRect.Width / 5), DrawRect.Top + nCurHeight + 10));
            //------------------------------------------------------------
             nCurHeight += nrowH;
             offScreenDC.DrawString("咳嗽诱导腹腔漏尿点压力测定（ALPP)： ", new Font("宋体", 14, FontStyle.Bold), drawBrush, new PointF(DrawRect.Left, DrawRect.Top + nCurHeight + 10));
          
            if (_TestData.str_clpp != null && _TestData.str_clpp != "")
                offScreenDC.DrawString(_TestData.str_clpp, new Font("宋体", 14, FontStyle.Bold), drawBrush, new PointF(DrawRect.Right - (int)(DrawRect.Width / 5 * 1.5)  , DrawRect.Top + nCurHeight + 10));
            offScreenDC.DrawString(" " + ValueUnit, new Font("宋体", 14, FontStyle.Bold), drawBrush, new PointF(DrawRect.Right - (int)(DrawRect.Width / 5), DrawRect.Top + nCurHeight + 10));
            //------------------------------------------------------------
            nCurHeight += nrowH;
            offScreenDC.DrawString("膀胱安全容量： ", new Font("宋体", 14, FontStyle.Bold), drawBrush, new PointF(DrawRect.Left, DrawRect.Top + nCurHeight + 10));
           
            if (_TestData.str_aqrl != null && _TestData.str_aqrl != "")
                offScreenDC.DrawString(_TestData.str_aqrl, new Font("宋体", 14, FontStyle.Bold), drawBrush, new PointF(DrawRect.Right - (int)(DrawRect.Width / 5 * 1.5)  , DrawRect.Top + nCurHeight + 10));
            offScreenDC.DrawString(" ml", new Font("宋体", 14, FontStyle.Bold), drawBrush, new PointF(DrawRect.Right - (int)(DrawRect.Width / 5), DrawRect.Top + nCurHeight + 10));
            //------------------------------------------------------------
              
            nCurHeight += nrowH;
            nCurHeight += 20;

            nrowH = 20;  
            rtf.Location = new PointF(DrawRect.Left, DrawRect.Top + nCurHeight);
            offScreenDC.DrawString("其他描述：", new Font("宋体", 14, FontStyle.Bold), drawBrush, new PointF(DrawRect.Left, DrawRect.Top + nCurHeight + 10));


            nCurHeight += nrowH;
            nrowH = 150;
            rtf.Height = nrowH;
            rtf.Location = new PointF(DrawRect.Left, DrawRect.Top + nCurHeight);
            if (_TestData.str_qtms != null && _TestData.str_qtms != "")
            {
               
                offScreenDC.DrawString( _TestData.str_qtms, new Font("宋体", 12), drawBrush, rtf, format);
            }

            
            //------------------------------------------------------------
             
             nCurHeight += nrowH;
             nrowH = 20;
             rtf.Height = nrowH;
             rtf.Location = new PointF(DrawRect.Left, DrawRect.Top + nCurHeight);
             offScreenDC.DrawString("尿动力学诊断：", new Font("宋体", 14, FontStyle.Bold), drawBrush, new PointF(DrawRect.Left, DrawRect.Top + nCurHeight + 10));

             nCurHeight += nrowH;

             nrowH = 150;
             rtf.Height = nrowH;
             rtf.Location = new PointF(DrawRect.Left, DrawRect.Top + nCurHeight);
            if (_TestData.str_ndlxzd != null && _TestData.str_ndlxzd != "")
            { 
                offScreenDC.DrawString(_TestData.str_ndlxzd, new Font("宋体", 12), drawBrush, rtf, format);
            }

            //------------------------------------------------------------
            nCurHeight += nrowH;
            nrowH = 30;
            rtf.Height = nrowH;
            rtf.Location = new PointF(DrawRect.Left, DrawRect.Top + nCurHeight);
            offScreenDC.DrawString("检查者：_________________ ", new Font("宋体", 12), drawBrush, new PointF(DrawRect.Left, DrawRect.Top + nCurHeight + 10));

            rtf.Location = new PointF(DrawRect.Left, DrawRect.Top + nCurHeight);
            offScreenDC.DrawString("检查日期：_________________ ", new Font("宋体", 12), drawBrush, new PointF(DrawRect.Left + DrawRect.Width/2, DrawRect.Top + nCurHeight + 10));

             //------------------------------------------------------------ 
            nCurHeight += nrowH;
            nCurHeight += 20;

            //offScreenDC.DrawLine(pen, DrawRect.Left, DrawRect.Top + nCurHeight, DrawRect.Right, DrawRect.Top + nCurHeight);
            //nCurHeight += 10;

            //-------------------------------------------------------
            //打印页脚（是否画蛇添足了，需求没说，但感觉加上会更好）

            if(!isHideTail)
            {
                int pageTailH = 25;

                int _nCurHeight = DrawRect.Bottom - pageTailH;  //间隔
                Rectangle tempRC4 = new Rectangle();
                tempRC4.Location = new Point((int)(DrawRect.Left), _nCurHeight);
                offScreenDC.DrawLine(pen, DrawRect.Left, _nCurHeight, DrawRect.Right, _nCurHeight);
                _nCurHeight += 2;

                tempRC4.Location = new Point((int)(DrawRect.Left), _nCurHeight);
                tempRC4.Size = new Size(DrawRect.Width - 100, 20);
                DrawPrintOneString(CurSelPatientInfo.name + "  " + CurSelPatientInfo.id, 9,StringAlignment.Far, tempRC4);

                tempRC4.Location = new Point((int)(DrawRect.Right - 100), _nCurHeight);
                tempRC4.Size = new Size(100, 20);
                DrawPrintOneString("第1页", 9, StringAlignment.Far, tempRC4);
            }
            nCurHeight += 10;
            //-------------------------------------------------------
            return nCurHeight;
        }



        //绘制一条字符串
        public void DrawPrintOneString(string str, int fontsize, StringAlignment salignment, Rectangle DrawRect, bool isflag = false)
        { 

            Font drawFont = new Font("宋体 ", fontsize);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            Pen pen = new Pen(Color.Black);

            RectangleF rtf = new Rectangle(new Point(0, 0), new Size(DrawRect.Width, DrawRect.Height));
            rtf.Location = new PointF(DrawRect.Left, DrawRect.Top);

            if (!isflag)
            {
                StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);
                format.LineAlignment = StringAlignment.Center;  // 更正： 垂直居中
                format.Alignment = salignment;      // 水平居中

                offScreenDC.DrawString(str, drawFont, drawBrush, rtf, format);
            }
            else
            {
                offScreenDC.DrawString(str, drawFont, drawBrush, new PointF(DrawRect.Left + 3, DrawRect.Top + 3));

            }
             
        }

        //按坐标点画线,按中心横线
        public void plotLine4(ref Panel panel, ref List<StruData> list_data, Rectangle DrawRect)
        {
            int Len = list_data.Count();

            //Bitmap canvas = new Bitmap(size1.Width, size1.Height);  // 画布、笔
            //Graphics offScreenDC = Graphics.FromImage(canvas);
            Pen pen = new Pen(Color.Red);

            double width = DrawRect.Size.Width;     // 画布长宽，中线
            double height = DrawRect.Size.Height;
            double center = DrawRect.Top + height / 2;

            double max = 0;                // 数据中最大值的abs（）
            foreach (StruData tempData in list_data)  //遍历文件
            {
                if (max < Math.Abs(tempData.value ))
                    max = Math.Abs(tempData.value);
                if (max == 0) max = 0.0000000001;
            }

            double scaleX;
            double scaleY;
            scaleX = Len / width;   // 从Sp数组中，按照窗口的横纵比例，有选择的输出到屏幕
            scaleY = 2 * max / height;     // 窗高的一般为单位 1
             
            for (int i = 0; i < Len - 1; i++)
            {
                int xFirst = (int)(i / scaleX) + DrawRect.Left;
                int yFirst = (int)(center - (list_data[i].value) / (float)scaleY)  ;
                int xNext = (int)((i + 1) / scaleX) + DrawRect.Left;
                int yNext = (int)(center - (list_data[i + 1].value) / (float)scaleY)  ; 

                offScreenDC.DrawLine(pen, xFirst, yFirst, xNext, yNext);
                i++;

            }
            //panel.BackgroundImage = canvas;
            //offScreenDC.Dispose();
        }

        //=====================================================================
    }
}
