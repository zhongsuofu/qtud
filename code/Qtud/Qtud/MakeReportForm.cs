using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Qtud.DBManage.Manager;
using Qtud.DBManage.Model;
using Qtud.SystemCommon;

using System.IO;
using System.Drawing.Printing;

namespace Qtud.Qtud
{

    //结构体；一次检查结果数据
    public struct TestDatas
    {
        public string uuid;    
        public string strKS;    //科室
        public string strCH  ;  //床号
        public string strBS;    //病史
        public string strNLL;   //尿流率检查结果-尿流率
        public string strNL;     //尿流率检查结果-尿量
        public string str_RJ_YL;   //充盈期膀胱容积-压力测定结果
        public string str_nl_cg;   //膀胱容量 初感
        public string str_nl_zc;   //膀胱容量 正常
        public string str_nl_zd;   //膀胱容量 最大

        public string str_syx;   //膀胱顺应性
       

        public string str_wdx;   //膀胱稳定性
         

        public string str_tsjc;   //特殊检查
        public string str_vlpp;   //vlpp
        public string str_dlpp;   //dlpp
        public string str_clpp;   //clpp
        public string str_aqrl;   //安全容量

        public string str_qtms;   //其他描述
        public string str_ndlxzd;   //尿动力学诊断 
    }

    public partial class MakeReportForm : Form
    {

        //----------------------------------------------------
        //打印文档
        PrintDocument pdDocument = new PrintDocument();

        //打印格式设置页面
        PageSetupDialog dlgPageSetup = new PageSetupDialog();

        //打印页面
        PrintDialog dlgPrint = new PrintDialog();
        bool isprint = false;

        //1、实例化打印预览
        PrintPreviewDialog dlgPrintPreview = new PrintPreviewDialog();


        private int m_CurPageIndex = 0;  //当前打印页
        private int m_CurCurveDataIndex = 0 ;  //当前打印的曲线序号
        private int m_CurCurveMode = 0;  //当前打印的曲线模式

        bool IsPrintReportheadPage = true;  //是否打印报告首页
        //----------------------------------------------------

        private PatientInfoModel m_CurSelPatientInfo;    //当前选择的病人
        private List<CurveDatas> m_PrintCurveDatas;        //打印的曲线
        private TestDatas m_TestDatas;


        Size curve3_Range = new Size(-100, 300);  //3条曲线范围：（最小值 ，最大值）
        Size nl_Range = new Size(0, 2000);  //尿量范围：（最小值 ，最大值）
        Size nll_Range = new Size(0, 100);  //尿流率范围：（最小值 ，最大值）

        private PrintInfoManager printM = new PrintInfoManager();

        public MakeReportForm(PatientInfoModel CurSelPatientInfo, List<CurveDatas> PrintCurveDatas, TestDatas _TestDatas, Size _curve3_Range, Size _nl_Range, Size _nll_Range)
        {
            InitializeComponent();

            curve3_Range = _curve3_Range;
            nl_Range = _nl_Range;
            nll_Range = _nll_Range;

            //----------------------------------------------
            pdDocument.PrintPage += new PrintPageEventHandler(OnPrintPage);
            pdDocument.BeginPrint += new PrintEventHandler(pdDocument_BeginPrint);
            pdDocument.EndPrint += new PrintEventHandler(pdDocument_EndPrint);

            //頁面設置的打印文檔設置為需要打印的文檔
            dlgPageSetup.Document = pdDocument;

            //打印界面的打印文檔設置為被打印文檔
            dlgPrint.Document = pdDocument;

            //2、打印預覽的打印文檔設置為被打印文檔
            dlgPrintPreview.Document = pdDocument;
            //----------------------------------------------

            m_CurSelPatientInfo = CurSelPatientInfo;
            m_PrintCurveDatas = PrintCurveDatas;
            m_TestDatas = _TestDatas;
             
        }

        private void button_PageSetup_Click(object sender, EventArgs e)
        {
            dlgPageSetup.ShowDialog();
        }

        private void button_PrintPreview_Click(object sender, EventArgs e)
        {
            //3、顯示打印預覽界面
            dlgPrintPreview.ShowDialog();
        }

        private void button_print_Click(object sender, EventArgs e)
        {
            try
            {
                //判斷是否有選擇文本
                //if (textBoxEdit.SelectedText != "")
                //{
                //    //如果有選擇文本，則可以選擇"打印選擇的範圍"
                //    dlgPrint.AllowSelection = true;
                //}
                //else
                //{
                //    dlgPrint.AllowSelection = false;
                //}
                //呼叫打印界面
                if (dlgPrint.ShowDialog() == DialogResult.OK)
                {

                    isprint = true;
                    /*
                     * PrintDocument對象的Print()方法在PrintController類的幫助下，執行PrintPage事件。
                     */
                    pdDocument.Print();
                }
            }
            catch (InvalidPrinterException ex)
            {
                MessageBox.Show(ex.Message, "打印错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void button_Back_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// 每個打印任務衹調用OnBeginPrint()一次。
        /// 所有要打印的內容都在此設置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void pdDocument_BeginPrint(object sender, PrintEventArgs e)
        {
          
        }


        /// <summary>
        /// printDocument的PrintPage事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPrintPage(object sender, PrintPageEventArgs e)
        {
            m_CurPageIndex++;
            DrawFuns m_DrawFuns = new DrawFuns();
            m_DrawFuns.IniPrintDraw(e.Graphics, e.MarginBounds.Size);

            Rectangle m_DrawArea = e.MarginBounds;  //绘画区域, 含行标题,及其尿流率图

            int nCurHight = 0;
            if (m_CurPageIndex == 1   )
            {
                nCurHight = m_DrawFuns.DrawReportTitle(m_CurSelPatientInfo, m_TestDatas, m_DrawArea, false ,IsPrintReportheadPage);   //绘制报告标题，病人信息等非曲线图信息，返回当前绘制的高度位置
                nCurHight += 20;   //间隔

                if (IsPrintReportheadPage)
                {
                    e.HasMorePages = true;  //分页
                    return;
                }
                

            }


            if (m_PrintCurveDatas.Count < 1)
            {
                m_DrawArea.Location = new Point((int)(m_DrawArea.Left), (int)(m_DrawArea.Top + nCurHight));
                m_DrawArea.Size = new Size((int)(m_DrawArea.Width), 5);
                nCurHight += 5;

                //打印结束
                //m_CurPageIndex = 0;  //当前打印页
                m_CurCurveDataIndex = 0;  //当前打印的曲线序号
                m_CurCurveMode = 0;  //当前打印的曲线模式
                e.HasMorePages = false;
                return;
            }


            //-------------------------------------------------------------
           
           
            int pageTailH = 25 ;  //页脚高度
            //-------------------------------------------------------------



            nCurHight += e.MarginBounds.Top;
            //-------------------------------------------------------------
            int stepH = 110;  //步高
            int nCurIndex = 0; //曲线序号
            //计算多少段
            foreach (CurveDatas OneCurveData in m_PrintCurveDatas)
            {
                if(m_CurCurveDataIndex>nCurIndex)  //已经打印过了
                {
                    nCurIndex++;
                    continue;
                } 
                 
                //--------------------------------------------------------------------
                //打印前三条曲线
                int iCount = 0;
                for (int iv = 0; iv < 3; iv++)
                {

                    if (OneCurveData.showMode[iv] == 1 && iv >= m_CurCurveMode)
                    {
                        iCount++;
                    }
                }

                bool isPrintCurveTitle = false;  //是否已经打印曲线标题
                if (iCount > 0)  //前3条曲线
                {
                    nCurHight += 20;  //预留出打印曲线标题用

                    bool  isPartCurve = false;  //部分打印
                    if (stepH * iCount + nCurHight > e.MarginBounds.Height-pageTailH)  //一页是否能打下
                    {
                        int CanPrintCnt = (int)(((e.MarginBounds.Height-pageTailH) - nCurHight) / stepH);
                        if (CanPrintCnt == 0)  //一个都打不下了
                        {
                            m_CurCurveDataIndex = nCurIndex;
                            e.HasMorePages = true;  //分页

                            //-------------------------------------------------------
                            //打印页脚（是否画蛇添足了，需求没说，但感觉加上会更好）

                            {
                                nCurHight = e.MarginBounds.Bottom - pageTailH;  //间隔
                                Rectangle tempRC4 = new Rectangle();
                                tempRC4.Location = new Point((int)(m_DrawArea.Left), nCurHight);
                                m_DrawFuns.plotLine2(new Point[] { new Point(m_DrawArea.Left, nCurHight), new Point(m_DrawArea.Right, nCurHight) });
                                nCurHight += 2;

                                tempRC4.Location = new Point((int)(m_DrawArea.Left), nCurHight);
                                tempRC4.Size = new Size(m_DrawArea.Width - 100, 20);
                                m_DrawFuns.DrawPrintOneString(m_CurSelPatientInfo.name + "  " + m_CurSelPatientInfo.cardid, 9, StringAlignment.Far, tempRC4);

                                tempRC4.Location = new Point((int)(m_DrawArea.Right - 100), nCurHight);
                                tempRC4.Size = new Size(100, 20);
                                m_DrawFuns.DrawPrintOneString("第" + m_CurPageIndex + "页", 9, StringAlignment.Far, tempRC4);

                            }
                            //-------------------------------------------------------

                            return;
                        }
                        else  // 部分打印
                        {
                            isPartCurve = true;
                            iCount = CanPrintCnt;
                            
                        }
                    }
                    else  //一页能打印下，全部打印
                    { 
                    }
                    //--------------------------------
                    //打印曲线标题
                    {
                        Rectangle tempRC = new Rectangle();
                        tempRC.Location = new Point((int)(m_DrawArea.Left), nCurHight -20);
                        tempRC.Size = new Size(m_DrawArea.Width - 2, 20);
                        m_DrawFuns.DrawPrintOneString(OneCurveData.StartTime.ToString() + " 至 " + OneCurveData.endTime.ToString(), 9, StringAlignment.Far, tempRC,true);
                        isPrintCurveTitle = true;
                    }

                    //--------------------------------
                    //打印曲线
                    {
                        m_DrawArea.Location = new Point((int)(m_DrawArea.Left), nCurHight);
                        m_DrawArea.Size = new Size((int)(m_DrawArea.Width), (int)(stepH * iCount));
                        nCurHight += stepH * iCount;

                        //------------------------------------------------------------
                        // 画横标尺 
                        int iniH = m_DrawArea.Top;
                        m_DrawFuns.plotLine2( new Point[] { new Point(m_DrawArea.Left, iniH), new Point(m_DrawArea.Right, iniH) });
                        for (int i = 0; i < iCount; i++)
                        {
                            iniH += stepH;
                            if (iniH > m_DrawArea.Bottom)
                                iniH = m_DrawArea.Bottom;
                            m_DrawFuns.plotLine2( new Point[] { new Point(m_DrawArea.Left, iniH), new Point(m_DrawArea.Right, iniH) });

                        }

                        int ntitleWitch = 100; //标题宽度
                        // 画纵标尺
                        m_DrawFuns.plotLine2( new Point[] { new Point(m_DrawArea.Left, m_DrawArea.Top), new Point(m_DrawArea.Left, iniH) });
                        m_DrawFuns.plotLine2( new Point[] { new Point(m_DrawArea.Left + ntitleWitch, m_DrawArea.Top), new Point(m_DrawArea.Left + ntitleWitch, iniH) });
                        m_DrawFuns.plotLine2( new Point[] { new Point(m_DrawArea.Right, m_DrawArea.Top), new Point(m_DrawArea.Right, iniH) });

                        //----------------------------------------------------------
                        // 画纵标尺 虚线 5端
                        int ptCnt = OneCurveData.list_Pabd.Count;
                        int nSecond = ptCnt / 2; //秒数
                        int nMinute = nSecond / 60; //分钟数

                        int nduan = 5;  //分几个区间

                        int nStepSecond = 0;
                        if (nSecond > nduan)
                        {
                            nStepSecond = (int)nSecond / nduan;
                        }
                        else
                        {
                            nStepSecond = 1;
                        }
                        Rectangle tempRC = new Rectangle();
                        tempRC.Location = new Point((int)(m_DrawArea.Left + ntitleWitch - 60), iniH + 6);
                        tempRC.Size = new Size(120, 20);

                        m_DrawFuns.DrawPrintOneString(@"0", 10, StringAlignment.Center, tempRC);

                        int stepW = (int)(m_DrawArea.Width - ntitleWitch) / nduan;
                        for (int iv = 0; iv < nduan; iv++)
                        {
                            m_DrawFuns.plotLine2( new Point[] { new Point(m_DrawArea.Left + ntitleWitch + iv * stepW, m_DrawArea.Top), new Point(m_DrawArea.Left + ntitleWitch + iv * stepW, iniH) }, true);

                            string strtitle = string.Empty;
                            if (nStepSecond < 60)
                                strtitle = (iv + 1) * nStepSecond + "\"";
                            else
                            {
                                strtitle = ((iv + 1) * nStepSecond) / 60 + "\'";
                                strtitle += ((iv + 1) * nStepSecond) % 60 + "\"";
                            }

                            if ((iv + 1) == nduan)
                            { 
                                tempRC.Location = new Point((int)(m_DrawArea.Left + ntitleWitch + (iv + 1) * stepW - 15), iniH + 6);
                            }
                            else
                            {
                                tempRC.Location = new Point((int)(m_DrawArea.Left + ntitleWitch + (iv + 1) * stepW - 15), iniH + 6);
                            }
                            m_DrawFuns.DrawPrintOneString(strtitle, 10, StringAlignment.Center, tempRC, true);
                       
                        }
                        //----------------------------------------------------------
                        // 绘制曲线
                        int ii = m_CurCurveMode;
                        int useii = 0;
                        int nPosx = -1;  //分隔线
                        while (m_CurCurveMode + useii < m_CurCurveMode + iCount && ii < 3)
                        {
                            Rectangle m_oneDrawArea = m_DrawArea;  //绘画区域
                            m_oneDrawArea.Location = new Point(m_DrawArea.Left + ntitleWitch, m_DrawArea.Top + useii * stepH);
                            m_oneDrawArea.Width = m_DrawArea.Width - ntitleWitch;
                            m_oneDrawArea.Height = stepH;

                            Rectangle m_onetitleRect = m_DrawArea;
                            m_onetitleRect.Location = new Point(m_DrawArea.Left, m_DrawArea.Top + useii * stepH);
                            m_onetitleRect.Width = ntitleWitch;
                            m_onetitleRect.Height = stepH;

                            if (OneCurveData.showMode[ii] == 1 && ii ==(int) CuvrlMode.Pves)  //Pves
                            {
                                Size m_range = curve3_Range;  //范围：（最小值 ，最大值）

                                //画行标题
                                m_DrawFuns.DrawRowtitle("Pves", "cmH2O", OneCurveData.fmax_Pves.ToString(), m_range, m_onetitleRect, Color.Blue);

                                List<StruData> tempstrudata = OneCurveData.list_Pves;
                                //画曲线
                                Dictionary<int, Index_value> temp = new Dictionary<int, Index_value>();
                                nPosx = m_DrawFuns.plotLine3(ref tempstrudata, m_range, m_oneDrawArea, Color.Blue, ref temp, OneCurveData.FirstFileEndIndex);
                                useii++;
                            }
                            else if (OneCurveData.showMode[ii] == 1 && ii == (int)CuvrlMode.Pabd)  //Pabd
                            {
                                Size m_range = curve3_Range;  //范围：（最小值 ，最大值）
                                //画行标题
                                m_DrawFuns.DrawRowtitle("Pabd", "cmH2O", OneCurveData.fmax_Pabd.ToString(), m_range, m_onetitleRect, Color.DarkViolet);

                                List<StruData> tempstrudata = OneCurveData.list_Pabd;
                                //画曲线
                                Dictionary<int, Index_value> temp = new Dictionary<int, Index_value>();
                                nPosx = m_DrawFuns.plotLine3(ref tempstrudata, m_range, m_oneDrawArea, Color.DarkViolet, ref temp, OneCurveData.FirstFileEndIndex);
                                useii++;
                            }
                            else if (OneCurveData.showMode[ii] == 1 && ii == (int)CuvrlMode.Pdet)  //Pdet
                            {
                                Size m_range = curve3_Range;  //范围：（最小值 ，最大值）

                                //画行标题
                                m_DrawFuns.DrawRowtitle("Pdet", "cmH2O", OneCurveData.fmax_Pdet.ToString(), m_range, m_onetitleRect, Color.Green);

                                List<StruData> tempstrudata = OneCurveData.list_Pdet;
                                //画曲线
                                Dictionary<int, Index_value> temp = new Dictionary<int, Index_value>();
                                nPosx = m_DrawFuns.plotLine3(ref tempstrudata, m_range, m_oneDrawArea, Color.Green, ref temp, OneCurveData.FirstFileEndIndex);
                                useii++;
                            }
                            ii++;


                        }
                         //绘制分隔线
                        if (nPosx > -1)
                        {
                            m_DrawFuns.plotLine2(new Point[] { new Point(nPosx, m_DrawArea.Top), new Point(nPosx, m_DrawArea.Top + useii * stepH) }, false, new Pen(Color.FromArgb(30, 20, 80)));

                        }

                        m_CurCurveMode =  ii ;
                        m_CurCurveDataIndex = nCurIndex;
                        if (isPartCurve)  // 
                        {
                            e.HasMorePages = true;  //分页


                            //-------------------------------------------------------
                            //打印页脚（是否画蛇添足了，需求没说，但感觉加上会更好）
                            {
                                nCurHight = e.MarginBounds.Bottom - pageTailH;  //间隔
                                Rectangle tempRC4 = new Rectangle();
                                tempRC4.Location = new Point((int)(m_DrawArea.Left), nCurHight);
                                m_DrawFuns.plotLine2(new Point[] { new Point(m_DrawArea.Left, nCurHight), new Point(m_DrawArea.Right, nCurHight) });
                                nCurHight += 2;

                                tempRC4.Location = new Point((int)(m_DrawArea.Left), nCurHight);
                                tempRC4.Size = new Size(m_DrawArea.Width - 100, 20);
                                m_DrawFuns.DrawPrintOneString(m_CurSelPatientInfo.name + "  " + m_CurSelPatientInfo.cardid, 9, StringAlignment.Far, tempRC4);

                                tempRC4.Location = new Point((int)(m_DrawArea.Right - 100), nCurHight);
                                tempRC4.Size = new Size(100, 20);
                                m_DrawFuns.DrawPrintOneString("第" + m_CurPageIndex + "页", 9, StringAlignment.Far, tempRC4);

                            }
                            //-------------------------------------------------------

                            return;
                        }
                        //-------------------------------------------------------

                    } //打印一条曲线结束
                }


                //-------------------------------------------------------
                //绘制尿量，尿流率
                iCount = 0;
                if (OneCurveData.showMode[(int)CuvrlMode.Wight] == 1 && m_CurCurveMode <=  (int)CuvrlMode.Wight )
                {
                    iCount++;
                }
                if (OneCurveData.showMode[(int)CuvrlMode.ufr] == 1 && m_CurCurveMode <= (int)CuvrlMode.ufr )
                {
                    iCount++;
                }

                nCurHight += 30;  //间隔
                if (iCount > 0)
                {
                    if (!isPrintCurveTitle)
                        nCurHight += 20;  //预留出打印曲线标题用

                    //绘制尿量，尿流率
                    if (nCurHight + stepH*2 <= e.MarginBounds.Bottom-pageTailH)  // 2条曲线打印一行
                    {

                        //--------------------------------
                        //打印曲线标题
                        if (!isPrintCurveTitle)
                        {
                            Rectangle tempRC = new Rectangle();
                            tempRC.Location = new Point((int)(m_DrawArea.Left), nCurHight - 20);
                            tempRC.Size = new Size(m_DrawArea.Width - 2, 20);
                            m_DrawFuns.DrawPrintOneString(OneCurveData.StartTime.ToString() + " 至 " + OneCurveData.endTime.ToString(), 9, StringAlignment.Far, tempRC,true);
                            isPrintCurveTitle = true;
                        }

                        //--------------------------------

                        if (OneCurveData.showMode[(int)CuvrlMode.Wight] == 1)  //尿量
                        {
                            Rectangle m_oneDrawArea = m_DrawArea;  //绘画区域 
                            m_oneDrawArea.Location = new Point((int)m_DrawArea.Left, (int)nCurHight );
                            m_oneDrawArea.Width = (int)(m_DrawArea.Width / 2 - 30);
                            m_oneDrawArea.Height = (int)(stepH * 3 / 2);

                            //绘制纵坐标
                            m_DrawFuns.plotLine2( new Point[] { new Point(m_oneDrawArea.Left, m_oneDrawArea.Top), new Point(m_oneDrawArea.Left, m_oneDrawArea.Bottom) });
                            //箭头
                            m_DrawFuns.plotLine2(new Point[] { new Point(m_oneDrawArea.Left, m_oneDrawArea.Top), new Point(m_oneDrawArea.Left - 2, m_oneDrawArea.Top + 10) });
                            m_DrawFuns.plotLine2(new Point[] { new Point(m_oneDrawArea.Left, m_oneDrawArea.Top), new Point(m_oneDrawArea.Left - 1, m_oneDrawArea.Top + 10) });
                            m_DrawFuns.plotLine2(new Point[] { new Point(m_oneDrawArea.Left, m_oneDrawArea.Top), new Point(m_oneDrawArea.Left + 1, m_oneDrawArea.Top + 10) });
                            m_DrawFuns.plotLine2(new Point[] { new Point(m_oneDrawArea.Left, m_oneDrawArea.Top), new Point(m_oneDrawArea.Left + 2, m_oneDrawArea.Top + 10) });

                            Rectangle tempRC = new Rectangle();

                            tempRC.Location = new Point(m_oneDrawArea.Left + 8, m_oneDrawArea.Top);
                            tempRC.Size = new Size(120, 20);

                            m_DrawFuns.DrawPrintOneString("V(ml)", 10, StringAlignment.Center, tempRC, true);


                            //-------------------------------------------------
                            
                            m_DrawFuns.plotLine2( new Point[] { new Point(m_oneDrawArea.Left, m_oneDrawArea.Bottom), new Point(m_oneDrawArea.Right, m_oneDrawArea.Bottom) });

                            //箭头
                            m_DrawFuns.plotLine2(new Point[] { new Point(m_oneDrawArea.Right, m_oneDrawArea.Bottom), new Point(m_oneDrawArea.Right - 10, m_oneDrawArea.Bottom - 1) });
                            m_DrawFuns.plotLine2(new Point[] { new Point(m_oneDrawArea.Right, m_oneDrawArea.Bottom), new Point(m_oneDrawArea.Right - 10, m_oneDrawArea.Bottom - 2) });
                            m_DrawFuns.plotLine2(new Point[] { new Point(m_oneDrawArea.Right, m_oneDrawArea.Bottom), new Point(m_oneDrawArea.Right - 10, m_oneDrawArea.Bottom + 1) });
                            m_DrawFuns.plotLine2(new Point[] { new Point(m_oneDrawArea.Right, m_oneDrawArea.Bottom), new Point(m_oneDrawArea.Right - 10, m_oneDrawArea.Bottom + 2) });

                            tempRC.Location = new Point(m_oneDrawArea.Right - 30, m_oneDrawArea.Bottom - 25);
                            tempRC.Size = new Size(120, 20);

                            m_DrawFuns.DrawPrintOneString("T(s)", 10, StringAlignment.Center, tempRC, true);
                            //-------------------------------------------------
                            m_oneDrawArea.Location = new Point(m_oneDrawArea.Left, m_oneDrawArea.Top + 30);
                            m_oneDrawArea.Width -= 30;
                            m_oneDrawArea.Height -= 30;
                            //-------------------------------------------------
                            Size m_range = nl_Range;  //范围：（最小值 ，最大值）

                            //绘制刻度
                            int ptCnt = OneCurveData.list_Wights.Count;
                            int nSecond = (ptCnt + 1) / 2; //秒数


                            int nduan = 12;  //分几个区间
                            int nStepSecond = 0;
                            if (nSecond >= nduan)
                            {
                                nStepSecond = (int)nSecond / nduan;
                            }
                            else
                            {
                                if (nSecond == 0)
                                {
                                    nduan = 1;
                                    nStepSecond = 1;
                                }
                                else
                                {
                                    nduan = nSecond;
                                    nStepSecond = 1;
                                }

                            }
                            //-------------------------------------------------
                            int hStep = (int)(m_oneDrawArea.Width / nduan);

                            tempRC.Location = new Point(m_oneDrawArea.Left - 5, m_oneDrawArea.Bottom + 5);
                            tempRC.Size = new Size(120, 20);
                            //绘制刻度值
                            m_DrawFuns.DrawPrintOneString("0", 10, StringAlignment.Center, tempRC, true);
                            //绘制横刻度
                            for (int k = 1; k <= nduan; k++)
                            {
                                m_DrawFuns.plotLine2(new Point[] { new Point(m_oneDrawArea.Left + k * hStep, m_oneDrawArea.Bottom - 5), new Point(m_oneDrawArea.Left + k * hStep, m_oneDrawArea.Bottom) });

                                //绘制刻度值
                                string strtitle = string.Empty;
                                if (nStepSecond < 60)
                                    strtitle = (k) * nStepSecond + "\"";
                                else
                                {
                                    strtitle = ((k) * nStepSecond) / 60 + @"\'";
                                    strtitle += ((k) * nStepSecond) % 60 + "\"";
                                }
                                tempRC.Location = new Point(m_oneDrawArea.Left + k * hStep - 10, m_oneDrawArea.Bottom + 5);
                                tempRC.Size = new Size(120, 20);
                                //绘制刻度值
                                m_DrawFuns.DrawPrintOneString(strtitle, 8, StringAlignment.Center, tempRC, true);

                            }

                            //-------------------------------------------------
                            //绘制纵刻度
                            int Vcnt = (int)(m_range.Height / 500);  //刻度数
                            int VStep = (int)(m_oneDrawArea.Height / Vcnt);
                            int Vvalue = 0;

                            for (int k = 1; k <= Vcnt; k++)
                            {
                                m_DrawFuns.plotLine2(new Point[] { new Point(m_oneDrawArea.Left, m_oneDrawArea.Bottom - k * VStep), new Point(m_oneDrawArea.Left + 5, m_oneDrawArea.Bottom - k * VStep) });


                                tempRC.Location = new Point(m_oneDrawArea.Left + 8, m_oneDrawArea.Bottom - k * VStep - 10);
                                tempRC.Size = new Size(120, 20);
                                //绘制刻度值
                                m_DrawFuns.DrawPrintOneString((Vvalue + k * 500).ToString(), 8, StringAlignment.Center, tempRC, true);

                            }

                            //-------------------------------------------------
                            Dictionary<int, Index_value> temp = new Dictionary<int, Index_value>();
                            //画曲线
                            List<StruData> tempstrudata = OneCurveData.list_Wights;
                            m_DrawFuns.plotLine3( ref tempstrudata, m_range, m_oneDrawArea, Color.Black, ref temp);
                        
                        }
                        if (OneCurveData.showMode[(int)CuvrlMode.ufr] == 1)  //尿流率
                        {
                            Rectangle m_oneDrawArea = m_DrawArea;  //绘画区域 
                            if (iCount ==1)
                                m_oneDrawArea.Location = new Point((int)m_DrawArea.Left , nCurHight);
                            else
                                m_oneDrawArea.Location = new Point((int)m_DrawArea.Left + m_DrawArea.Width / 2-10, nCurHight);
                            m_oneDrawArea.Width = (int)(m_DrawArea.Width / 2 - 30);
                            m_oneDrawArea.Height = (int)(stepH * 3 / 2);

                            //绘制坐标
                            m_DrawFuns.plotLine2( new Point[] { new Point(m_oneDrawArea.Left, m_oneDrawArea.Top), new Point(m_oneDrawArea.Left, m_oneDrawArea.Bottom) });
                            //箭头
                            m_DrawFuns.plotLine2(new Point[] { new Point(m_oneDrawArea.Left, m_oneDrawArea.Top), new Point(m_oneDrawArea.Left - 2, m_oneDrawArea.Top + 10) });
                            m_DrawFuns.plotLine2(new Point[] { new Point(m_oneDrawArea.Left, m_oneDrawArea.Top), new Point(m_oneDrawArea.Left - 1, m_oneDrawArea.Top + 10) });
                            m_DrawFuns.plotLine2(new Point[] { new Point(m_oneDrawArea.Left, m_oneDrawArea.Top), new Point(m_oneDrawArea.Left + 1, m_oneDrawArea.Top + 10) });
                            m_DrawFuns.plotLine2(new Point[] { new Point(m_oneDrawArea.Left, m_oneDrawArea.Top), new Point(m_oneDrawArea.Left + 2, m_oneDrawArea.Top + 10) });

                            Rectangle tempRC = new Rectangle();

                            tempRC.Location = new Point(m_oneDrawArea.Left + 8, m_oneDrawArea.Top);
                            tempRC.Size = new Size(120, 20);

                            m_DrawFuns.DrawPrintOneString("V(ml)", 10, StringAlignment.Center, tempRC, true);


                            //-------------------------------------------------
                            
                            m_DrawFuns.plotLine2( new Point[] { new Point(m_oneDrawArea.Left, m_oneDrawArea.Bottom), new Point(m_oneDrawArea.Right, m_oneDrawArea.Bottom) });
                            //箭头
                            m_DrawFuns.plotLine2(new Point[] { new Point(m_oneDrawArea.Right, m_oneDrawArea.Bottom), new Point(m_oneDrawArea.Right - 10, m_oneDrawArea.Bottom - 1) });
                            m_DrawFuns.plotLine2(new Point[] { new Point(m_oneDrawArea.Right, m_oneDrawArea.Bottom), new Point(m_oneDrawArea.Right - 10, m_oneDrawArea.Bottom - 2) });
                            m_DrawFuns.plotLine2(new Point[] { new Point(m_oneDrawArea.Right, m_oneDrawArea.Bottom), new Point(m_oneDrawArea.Right - 10, m_oneDrawArea.Bottom + 1) });
                            m_DrawFuns.plotLine2(new Point[] { new Point(m_oneDrawArea.Right, m_oneDrawArea.Bottom), new Point(m_oneDrawArea.Right - 10, m_oneDrawArea.Bottom + 2) });

                            tempRC.Location = new Point(m_oneDrawArea.Right - 30, m_oneDrawArea.Bottom - 25);
                            tempRC.Size = new Size(120, 20);

                            m_DrawFuns.DrawPrintOneString("T(s)", 10, StringAlignment.Center, tempRC, true);
                            //-------------------------------------------------
                            m_oneDrawArea.Location = new Point(m_oneDrawArea.Left, m_oneDrawArea.Top + 30);
                            m_oneDrawArea.Width -= 30;
                            m_oneDrawArea.Height -= 30;
                            //-------------------------------------------------
                            Size m_range = nll_Range;  //范围：（最小值 ，最大值）
                            //绘制刻度
                            int ptCnt = OneCurveData.list_Wights.Count;
                            int nSecond = ptCnt; //秒数

                            int nduan = 12;  //分几个区间
                            int nStepSecond = 0;
                            if (nSecond >= nduan)
                            {
                                nStepSecond = (int)nSecond / nduan;
                            }
                            else
                            {
                                if (nSecond == 0)
                                {
                                    nduan = 1;
                                    nStepSecond = 1;
                                }
                                else
                                {
                                    nduan = nSecond;
                                    nStepSecond = 1;
                                }

                            }
                            //-------------------------------------------------
                            int hStep = (int)(m_oneDrawArea.Width / nduan);


                            tempRC.Location = new Point(m_oneDrawArea.Left - 5, m_oneDrawArea.Bottom + 5);
                            tempRC.Size = new Size(120, 20);
                            //绘制刻度值
                            m_DrawFuns.DrawPrintOneString("0", 10, StringAlignment.Center, tempRC, true);
                            //绘制横刻度
                            for (int k = 1; k <= nduan; k++)
                            {
                                m_DrawFuns.plotLine2(new Point[] { new Point(m_oneDrawArea.Left + k * hStep, m_oneDrawArea.Bottom - 5), new Point(m_oneDrawArea.Left + k * hStep, m_oneDrawArea.Bottom) });

                                //绘制刻度值
                                string strtitle = string.Empty;
                                if (nStepSecond < 60)
                                    strtitle = (k) * nStepSecond + "\"";
                                else
                                {
                                    strtitle = ((k) * nStepSecond) / 60 + @"\'";
                                    strtitle += ((k) * nStepSecond) % 60 + "\"";
                                }
                                tempRC.Location = new Point(m_oneDrawArea.Left + k * hStep - 10, m_oneDrawArea.Bottom + 5);
                                tempRC.Size = new Size(120, 20);
                                //绘制刻度值
                                m_DrawFuns.DrawPrintOneString(strtitle, 8, StringAlignment.Center, tempRC, true);

                            }

                            //-------------------------------------------------
                            //绘制纵刻度
                            int VvaluStep = (int)(m_range.Height / 3);  //刻度数
                            int VStep = (int)(m_oneDrawArea.Height / 3); //刻度step
                            int Vvalue = 0;

                            for (int k = 1; k <= 3; k++)
                            {
                                m_DrawFuns.plotLine2(new Point[] { new Point(m_oneDrawArea.Left, m_oneDrawArea.Bottom - k * VStep), new Point(m_oneDrawArea.Left + 5, m_oneDrawArea.Bottom - k * VStep) });


                                tempRC.Location = new Point(m_oneDrawArea.Left + 8, m_oneDrawArea.Bottom - k * VStep - 10);
                                tempRC.Size = new Size(120, 20);
                                //绘制刻度值
                                m_DrawFuns.DrawPrintOneString((Vvalue + k * VvaluStep).ToString(), 8, StringAlignment.Center, tempRC, true);

                            }

                            //-------------------------------------------------
                            //画曲线
                            Dictionary<int, Index_value> temp = new Dictionary<int, Index_value>();
                            List<StruData> tempstrudata = OneCurveData.list_ufr;
                            m_DrawFuns.plotLine3( ref tempstrudata, m_range, m_oneDrawArea, Color.Black, ref temp);
                        }
                        nCurHight += (int) ( stepH * 3/2);  // 2条曲线打印一行

                        nCurHight += 20;  //间隔
                    }
                    else
                    {
                        m_CurCurveDataIndex = nCurIndex;

                        e.HasMorePages = true;  //分页


                        //------------------------------------------------------- 
                        //打印页标题（是否画蛇添足了，需求没说，但感觉加上会更好）
                        {
                            nCurHight = e.MarginBounds.Bottom - pageTailH;  //间隔
                            Rectangle tempRC4 = new Rectangle();
                            tempRC4.Location = new Point((int)(m_DrawArea.Left), nCurHight);
                            m_DrawFuns.plotLine2(new Point[] { new Point(m_DrawArea.Left, nCurHight), new Point(m_DrawArea.Right, nCurHight) });
                            nCurHight += 2;

                            tempRC4.Location = new Point((int)(m_DrawArea.Left), nCurHight);
                            tempRC4.Size = new Size(m_DrawArea.Width - 100, 20);
                            m_DrawFuns.DrawPrintOneString(m_CurSelPatientInfo.name + "  " + m_CurSelPatientInfo.cardid, 9, StringAlignment.Far, tempRC4);

                            tempRC4.Location = new Point((int)(m_DrawArea.Right - 100), nCurHight);
                            tempRC4.Size = new Size(100, 20);
                            m_DrawFuns.DrawPrintOneString("第" + m_CurPageIndex + "页", 9, StringAlignment.Far, tempRC4);

                        }
                        //------------------------------------------------------- 
                        return;
                    }
                }  //结束尿流率
                
                //------------------------------------------------------- 
                m_CurCurveMode = (int)CuvrlMode.meno;

                //绘制说明
                if (OneCurveData.strMeno != "")
                {
                    if (m_CurCurveMode <= (int)CuvrlMode.meno && nCurHight + stepH <= e.MarginBounds.Bottom-pageTailH)
                    {
                        m_DrawArea.Location = new Point((int)(m_DrawArea.Left), (int)(nCurHight));
                        m_DrawArea.Size = new Size((int)(m_DrawArea.Width), stepH);
                        nCurHight += stepH;

                        //绘制 结尾
                        m_DrawFuns.DrawPrintOneString(OneCurveData.strMeno, 10, StringAlignment.Far, m_DrawArea);
                        nCurHight += 20;  //每组曲线间隔
                    }
                    else
                    {
                        m_CurCurveDataIndex = nCurIndex;

                        e.HasMorePages = true;  //分页
                        //-------------------------------------------------------
                        //打印页脚（是否画蛇添足了，需求没说，但感觉加上会更好）
                        {
                            nCurHight = e.MarginBounds.Bottom - pageTailH;  //间隔
                            Rectangle tempRC4 = new Rectangle();
                            tempRC4.Location = new Point((int)(m_DrawArea.Left), nCurHight);
                            m_DrawFuns.plotLine2(new Point[] { new Point(m_DrawArea.Left, nCurHight), new Point(m_DrawArea.Right, nCurHight) });
                            nCurHight += 2;

                            tempRC4.Location = new Point((int)(m_DrawArea.Left), nCurHight);
                            tempRC4.Size = new Size(m_DrawArea.Width - 100, 20);
                            m_DrawFuns.DrawPrintOneString(m_CurSelPatientInfo.name + "  " + m_CurSelPatientInfo.cardid, 9, StringAlignment.Far, tempRC4);

                            tempRC4.Location = new Point((int)(m_DrawArea.Right - 100), nCurHight);
                            tempRC4.Size = new Size(100, 20);
                            m_DrawFuns.DrawPrintOneString("第" + m_CurPageIndex + "页", 9, StringAlignment.Far, tempRC4);

                        }
                        //-------------------------------------------------------
                        return;
                    }
                   
                }
                //-------------------------------------------------------



                //打印完一条曲线
                nCurIndex++;
                m_CurCurveDataIndex = nCurIndex;  
                m_CurCurveMode = 0;
                
            }
            m_CurCurveDataIndex = nCurIndex;  //曲线打印结束

            //-------------------------------------------------------
            //打印页脚（是否画蛇添足了，需求没说，但感觉加上会更好）
            {
                nCurHight = e.MarginBounds.Bottom - pageTailH;  //间隔
                Rectangle tempRC4 = new Rectangle();
                tempRC4.Location = new Point((int)(m_DrawArea.Left), nCurHight);
                m_DrawFuns.plotLine2(new Point[] { new Point(m_DrawArea.Left, nCurHight), new Point(m_DrawArea.Right, nCurHight) });
                nCurHight += 2;

                tempRC4.Location = new Point((int)(m_DrawArea.Left), nCurHight);
                tempRC4.Size = new Size(m_DrawArea.Width - 100, 20);
                m_DrawFuns.DrawPrintOneString(m_CurSelPatientInfo.name + "  " + m_CurSelPatientInfo.cardid, 9, StringAlignment.Far, tempRC4);

                tempRC4.Location = new Point((int)(m_DrawArea.Right - 100), nCurHight);
                tempRC4.Size = new Size(100, 20);
                m_DrawFuns.DrawPrintOneString("第" + m_CurPageIndex + "页", 9, StringAlignment.Far, tempRC4);

            }
            //-------------------------------------------------------

            //============================================
            //打印结束
            //m_CurPageIndex = 0;  //当前打印页
            m_CurCurveDataIndex = 0;  //当前打印的曲线序号
            m_CurCurveMode = 0;  //当前打印的曲线模式
            e.HasMorePages = false;
            return;
            //============================================

            //--------------------------
            nCurHight += 10;  //间隔 

            if (nCurHight + 30 <= e.MarginBounds.Bottom-pageTailH)  //一页是否能打下
            {
                m_DrawArea.Location = new Point((int)(m_DrawArea.Left), nCurHight);
                m_DrawArea.Size = new Size((int)(m_DrawArea.Width), 30);
                nCurHight += 30;

                //绘制 结尾
                m_DrawFuns.DrawPrintOneString("尿动力学诊断:  测试", 12, StringAlignment.Far, m_DrawArea);
                nCurHight += 80;  //间隔 
            }
            else
            {
                e.HasMorePages = true;  //分页
                return;
            }

            
            //--------------------------
            //绘制 结尾
            nCurHight += 10;  //间隔  
            if (nCurHight + 30 <= e.MarginBounds.Bottom-pageTailH)  //一页是否能打下
            {
                Rectangle m_tempRect = m_DrawArea;
                m_tempRect.Location = new Point((int)(m_DrawArea.Left), nCurHight);
                m_tempRect.Size = new Size((int)(m_DrawArea.Width / 2), 30);

                m_DrawFuns.DrawPrintOneString("检查者: __________________ ", 12, StringAlignment.Center, m_tempRect, true);

                m_tempRect.Location = new Point((int)(m_DrawArea.Left + m_DrawArea.Width / 2), nCurHight);
                m_DrawFuns.DrawPrintOneString("检查日期: __________________ ", 12, StringAlignment.Center, m_tempRect, true);

                nCurHight += 30;
            }
            else
            {
                e.HasMorePages = true;  //分页
                return;
            }
            //--------------------------

            nCurHight += 20;  //间隔 
            if (nCurHight + 10 <= e.MarginBounds.Bottom-pageTailH)  //一页是否能打下
            {
                m_DrawArea.Location = new Point((int)(m_DrawArea.Left), nCurHight);
                m_DrawArea.Size = new Size((int)(m_DrawArea.Width ), 10);
                nCurHight += 10;

                m_DrawFuns.plotLine2( new Point[] { new Point(m_DrawArea.Left, m_DrawArea.Top), new Point(m_DrawArea.Right, m_DrawArea.Top) });
                nCurHight += 10;  //间隔 
            }
            else
            {
                e.HasMorePages = true;  //分页
                return;
            }
            //-------------------------------------------------------------

            ////超过一列时，打印其他页
            //if (nCurHight >= e.MarginBounds.Height)
            //{
            //    //多頁打印
            //    e.HasMorePages = true;

            //    /*
            //     * PrintPageEventArgs類的HaeMorePages屬性為True時，通知控件器，必須再次調用OnPrintPage()方法，打印一個頁面。
            //     * PrintLoopI()有一個用於每個要打印的頁面的序例。如果HasMorePages是False，PrintLoop()就會停止。
            //     */
            //    return;
            //}

            //m_CurPageIndex = 0;  //当前打印页
            m_CurCurveDataIndex = 0;  //当前打印的曲线序号
            m_CurCurveMode = 0;  //当前打印的曲线模式
            e.HasMorePages = false;

        }


        /// <summary>
        /// EndPrint事件釋放BeginPrint方法中佔用的資源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void pdDocument_EndPrint(object sender, PrintEventArgs e)
        { 
            try
            {
                if (isprint)
                {
                    isprint = false;
                    PrintInfoModel model = new PrintInfoModel();
                    model.uuid = PublicConst.GenerateUUID();
                    model.useruuid = CurrentUser._CurUserModel.user_id;
                    model.ReportUUId = m_TestDatas.uuid;
                    model.pagecnt = m_CurPageIndex;
                    model.printDate = DateTime.Now;
                    model.printcnt = dlgPrint.PrinterSettings.Copies;  //打印份数
                    printM.Add(model);

                }
                m_CurPageIndex = 0;  //当前打印页
               
            }
            catch (System.Exception ex)
            {

                MessageBox.Show(" 打印完成，但保存打印数据失败！ ");
            }
             
        }

        private void MakeReportForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            panel_print.Location = new Point((int)(panel_Report.Width * 0.2f), panel1.Bottom + 5);
            int H = (int)(panel_Report.Width * 0.6f / 0.7f);
            panel_print.Size = new Size((int)(panel_Report.Width * 0.6f), H);  //A4纸的模板：长宽比0.7


            int iCount = 0;
            //计算多少段
            foreach (CurveDatas OneCurveData in m_PrintCurveDatas)
            {
                //三条曲线
                for (int iv = 0; iv < 3; iv++)
                {

                    if (OneCurveData.showMode[iv] == 1)
                    {
                        iCount++;
                    }
                }

                if (OneCurveData.showMode[(int)CuvrlMode.Wight] == 1 || OneCurveData.showMode[(int)CuvrlMode.ufr] == 1)
                {
                    iCount++;
                }

                if (OneCurveData.list_Pves.Count > 0)
                    iCount++;  //备注

            }

            int stepH = 120;
            int nCurHight = 4000;  //病人信息
            if ((stepH * iCount) + nCurHight > panel_Context.Height)  //重新设置窗体大小
            {
                panel_print.Size = new Size((int)(panel_Report.Width * 0.6f), (stepH * iCount) + nCurHight);  //A4纸的模板：长宽比0.7

            }



            DrawPrintContext();  //绘制报告内容
             
        }

        private void DrawPrintContext()
        {
            DrawFuns m_DrawFuns = new DrawFuns();
            m_DrawFuns.IniDraw(ref panel_Context);
            m_DrawFuns.ClearDC();

            Rectangle panel_print_RC = this.panel_Context.ClientRectangle; 

            Rectangle m_DrawArea = new Rectangle();  //绘画区域, 含行标题,及其尿流率图
            m_DrawArea.Location = new Point((int)(panel_print_RC.Left + panel_print_RC.Width * 0.1), (int)(panel_print_RC.Top + 20));
            m_DrawArea.Size = new Size((int)(panel_print_RC.Width - panel_print_RC.Width * 0.2), (int)(panel_print_RC.Height -40));

            //-------------------------------------------------------
            int nCurHight = 0; 
            nCurHight = m_DrawFuns.DrawReportTitle(m_CurSelPatientInfo, m_TestDatas, m_DrawArea, true, IsPrintReportheadPage);   //绘制报告标题，病人信息等非曲线图信息，返回当前绘制的高度位置
             
            nCurHight += 20;
            //-------------------------------------------------------
            int iCount  = 0; 
            int stepH = 120; 

            if (m_PrintCurveDatas.Count < 1)
            {
                m_DrawArea.Location = new Point((int)(m_DrawArea.Left), (int)(m_DrawArea.Top + nCurHight));
                m_DrawArea.Size = new Size((int)(m_DrawArea.Width), 5);
                nCurHight += 5; 
            }
            nCurHight += m_DrawArea.Top;
            //绘制曲线
            foreach (CurveDatas OneCurveData in m_PrintCurveDatas)
            {
                m_DrawArea.Location = new Point((int)(m_DrawArea.Left), nCurHight);
                int ncurcount = 0;
                if (OneCurveData.showMode[(int)CuvrlMode.Pves] == 1)
                {
                    ncurcount++;
                }
                if (OneCurveData.showMode[(int)CuvrlMode.Pabd] == 1)
                {
                    ncurcount++;
                }
                if (OneCurveData.showMode[(int)CuvrlMode.Pdet] == 1)
                {
                    ncurcount++;
                }
                if (ncurcount > 0)  //3条曲线
                {
                    m_DrawArea.Size = new Size((int)(m_DrawArea.Width), (int)( stepH * ncurcount ));
                    nCurHight += stepH * ncurcount;  

                    //------------------------------------------------------------
                    // 画横标尺 
                    int iniH = m_DrawArea.Top;
                    m_DrawFuns.plotLine2( new Point[] { new Point(m_DrawArea.Left, iniH), new Point(m_DrawArea.Right, iniH) });
                    for (int i = 0; i < ncurcount; i++)
                    {
                        iniH += stepH;
                        if (iniH > panel_Context.Bottom)
                            iniH = panel_Context.Bottom;
                        m_DrawFuns.plotLine2( new Point[] { new Point(m_DrawArea.Left, iniH), new Point(m_DrawArea.Right, iniH) });

                    }

                    int ntitleWitch = 120;
                    // 画纵标尺
                    m_DrawFuns.plotLine2( new Point[] { new Point(m_DrawArea.Left, m_DrawArea.Top), new Point(m_DrawArea.Left, iniH) });
                    m_DrawFuns.plotLine2( new Point[] { new Point(m_DrawArea.Left + ntitleWitch, m_DrawArea.Top), new Point(m_DrawArea.Left + ntitleWitch, iniH) });
                    m_DrawFuns.plotLine2( new Point[] { new Point(m_DrawArea.Right, m_DrawArea.Top), new Point(m_DrawArea.Right, iniH) });

                    //----------------------------------------------------------
                    // 画纵标尺 虚线 5端
                    int ptCnt = OneCurveData.list_Pabd.Count;
                    int nSecond = ptCnt / 2; //秒数
                    int nMinute = nSecond / 60; //分钟数

                    int nduan = 5;  //分几个区间

                    int nStepSecond = 0;
                    if (nSecond > nduan)
                    {
                        nStepSecond = (int)nSecond / nduan;
                    }
                    else
                    {
                        nStepSecond = 1;
                    }
                    Rectangle tempRC = new Rectangle();
                    tempRC.Location = new Point((int)(m_DrawArea.Left + ntitleWitch - 60), iniH + 6);
                    tempRC.Size = new Size(120, 20);

                    m_DrawFuns.DrawPrintOneString(@"0", 10, StringAlignment.Center, tempRC);

                    int stepW = (int)(m_DrawArea.Width - ntitleWitch) / nduan;
                    for (int iv = 0; iv < nduan; iv++)
                    {
                        m_DrawFuns.plotLine2( new Point[] { new Point(m_DrawArea.Left + ntitleWitch + iv * stepW, m_DrawArea.Top), new Point(m_DrawArea.Left + ntitleWitch + iv * stepW, iniH) }, true);

                        string strtitle = string.Empty;
                        if (nStepSecond < 60)
                            strtitle = (iv + 1) * nStepSecond + "\"";
                        else
                        {
                            strtitle = ((iv + 1) * nStepSecond) / 60 + "\'";
                            strtitle += ((iv + 1) * nStepSecond) % 60 + "\"";
                        }
                        if ((iv + 1) == nduan)
                        {
                            tempRC.Location = new Point((int)(m_DrawArea.Left + ntitleWitch + (iv + 1) * stepW - 80), iniH + 6);
                        }
                        else
                        {
                            tempRC.Location = new Point((int)(m_DrawArea.Left + ntitleWitch + (iv + 1) * stepW - 60), iniH + 6);
                        }
                        m_DrawFuns.DrawPrintOneString(strtitle, 10, StringAlignment.Center, tempRC);
                    }
                    //----------------------------------------------------------
                    int ii = 0;
                    int useii = 0;  //有效的II
                    int nPosx = -1;  //分隔线
                    while (useii < ncurcount && ii < 3)
                    {
                        Rectangle m_oneDrawArea = m_DrawArea;  //绘画区域
                        m_oneDrawArea.Location = new Point(m_DrawArea.Left + ntitleWitch, m_DrawArea.Top + useii * stepH);
                        m_oneDrawArea.Width = m_DrawArea.Width - ntitleWitch;
                        m_oneDrawArea.Height = stepH;

                        Rectangle m_onetitleRect = m_DrawArea;
                        m_onetitleRect.Location = new Point(m_DrawArea.Left, m_DrawArea.Top + useii * stepH);
                        m_onetitleRect.Width = ntitleWitch;
                        m_onetitleRect.Height = stepH;

                        if (OneCurveData.showMode[ii] == 1 && ii == (int)CuvrlMode.Pves)  //Pves
                        {
                            Size m_range = curve3_Range;  //范围：（最小值 ，最大值）

                            //画行标题
                            m_DrawFuns.DrawRowtitle("Pves", "cmH2O", OneCurveData.fmax_Pves.ToString(), m_range, m_onetitleRect, Color.Blue);

                            List<StruData> tempstrudata = OneCurveData.list_Pves;
                            //画曲线
                            Dictionary<int, Index_value> temp = new Dictionary<int, Index_value>();
                            nPosx = m_DrawFuns.plotLine3(ref tempstrudata, m_range, m_oneDrawArea, Color.Blue, ref temp, OneCurveData.FirstFileEndIndex);
                            useii++;
                        }
                        else if (OneCurveData.showMode[ii] == 1 && ii == (int)CuvrlMode.Pabd)  //Pabd
                        {
                            Size m_range = curve3_Range;  //范围：（最小值 ，最大值）
                            //画行标题
                            m_DrawFuns.DrawRowtitle("Pabd", "cmH2O", OneCurveData.fmax_Pabd.ToString(), m_range, m_onetitleRect, Color.DarkViolet);

                            List<StruData> tempstrudata = OneCurveData.list_Pabd;
                            //画曲线
                            Dictionary<int, Index_value> temp = new Dictionary<int, Index_value>();
                            nPosx = m_DrawFuns.plotLine3(ref tempstrudata, m_range, m_oneDrawArea, Color.DarkViolet, ref temp, OneCurveData.FirstFileEndIndex);
                            useii++;
                        }
                        else if (OneCurveData.showMode[ii] == 1 && ii == (int)CuvrlMode.Pdet)  //Pdet
                        {
                            Size m_range = curve3_Range;  //范围：（最小值 ，最大值）

                            //画行标题
                            m_DrawFuns.DrawRowtitle("Pdet", "cmH2O", OneCurveData.fmax_Pdet.ToString(), m_range, m_onetitleRect, Color.Green);

                            List<StruData> tempstrudata = OneCurveData.list_Pdet;
                            //画曲线
                            Dictionary<int, Index_value> temp = new Dictionary<int, Index_value>();
                            nPosx = m_DrawFuns.plotLine3(ref tempstrudata, m_range, m_oneDrawArea, Color.Green, ref temp, OneCurveData.FirstFileEndIndex);
                            useii++;
                           
                        }
                        ii++;

                    }

                    //绘制分隔线
                    if (nPosx > -1)
                    {
                        m_DrawFuns.plotLine2(new Point[] { new Point(nPosx, m_DrawArea.Top), new Point(nPosx, m_DrawArea.Top + useii * stepH) }, false, new Pen(Color.FromArgb(30, 20, 80)));

                    }
                    //-------------------------------------------------------

 
                }
                   
                //-------------------------------------------------------
                //尿流量， 尿流率曲线
                ncurcount =0;
                if (OneCurveData.showMode[(int)CuvrlMode.Wight] == 1) 
                {
                    ncurcount++;
                }
                if (OneCurveData.showMode[(int)CuvrlMode.ufr] == 1) 
                {
                    ncurcount++;                   
                }
                if(ncurcount>0)
                {
                     if (OneCurveData.showMode[(int)CuvrlMode.Wight] == 1)  //尿量
                        {
                            Rectangle m_oneDrawArea = m_DrawArea;  //绘画区域 
                            m_oneDrawArea.Location = new Point((int)m_DrawArea.Left, (int)nCurHight+30);
                            m_oneDrawArea.Width = (int)(m_DrawArea.Width / 2 - 20);
                            m_oneDrawArea.Height = (int)( stepH * 3 /2  );

                            //绘制坐标
                            m_DrawFuns.plotLine2( new Point[] { new Point(m_oneDrawArea.Left, m_oneDrawArea.Top), new Point(m_oneDrawArea.Left, m_oneDrawArea.Bottom) });
                            //箭头
                            m_DrawFuns.plotLine2(new Point[] { new Point(m_oneDrawArea.Left, m_oneDrawArea.Top), new Point(m_oneDrawArea.Left - 2, m_oneDrawArea.Top + 10) });
                            m_DrawFuns.plotLine2(new Point[] { new Point(m_oneDrawArea.Left, m_oneDrawArea.Top), new Point(m_oneDrawArea.Left - 1, m_oneDrawArea.Top + 10) });
                            m_DrawFuns.plotLine2(new Point[] { new Point(m_oneDrawArea.Left, m_oneDrawArea.Top), new Point(m_oneDrawArea.Left + 1, m_oneDrawArea.Top + 10) });
                            m_DrawFuns.plotLine2(new Point[] { new Point(m_oneDrawArea.Left, m_oneDrawArea.Top), new Point(m_oneDrawArea.Left + 2, m_oneDrawArea.Top + 10) });

                            Rectangle tempRC = new Rectangle();

                            tempRC.Location = new Point(m_oneDrawArea.Left + 8, m_oneDrawArea.Top);
                            tempRC.Size = new Size(120, 20);

                            m_DrawFuns.DrawPrintOneString("V(ml)", 10, StringAlignment.Center, tempRC, true);


                            //-------------------------------------------------
                             m_DrawFuns.plotLine2( new Point[] { new Point(m_oneDrawArea.Left, m_oneDrawArea.Bottom), new Point(m_oneDrawArea.Right, m_oneDrawArea.Bottom) });
                             //箭头
                             m_DrawFuns.plotLine2(new Point[] { new Point(m_oneDrawArea.Right, m_oneDrawArea.Bottom), new Point(m_oneDrawArea.Right - 10, m_oneDrawArea.Bottom - 1) });
                             m_DrawFuns.plotLine2(new Point[] { new Point(m_oneDrawArea.Right, m_oneDrawArea.Bottom), new Point(m_oneDrawArea.Right - 10, m_oneDrawArea.Bottom - 2) });
                             m_DrawFuns.plotLine2(new Point[] { new Point(m_oneDrawArea.Right, m_oneDrawArea.Bottom), new Point(m_oneDrawArea.Right - 10, m_oneDrawArea.Bottom + 1) });
                             m_DrawFuns.plotLine2(new Point[] { new Point(m_oneDrawArea.Right, m_oneDrawArea.Bottom), new Point(m_oneDrawArea.Right - 10, m_oneDrawArea.Bottom + 2) });

                             tempRC.Location = new Point(m_oneDrawArea.Right - 30, m_oneDrawArea.Bottom - 25);
                             tempRC.Size = new Size(120, 20);

                             m_DrawFuns.DrawPrintOneString("T(s)", 10, StringAlignment.Center, tempRC, true);
                             //-------------------------------------------------
                             m_oneDrawArea.Location = new Point(m_oneDrawArea.Left, m_oneDrawArea.Top + 30);
                             m_oneDrawArea.Width -= 30;
                             m_oneDrawArea.Height -= 30;
                             //-------------------------------------------------
                            Size m_range = nl_Range;  //范围：（最小值 ，最大值）

                            //绘制刻度
                            int ptCnt = OneCurveData.list_Wights.Count;
                            int nSecond = (ptCnt + 1) / 2; //秒数


                            int nduan = 12;  //分几个区间
                            int nStepSecond = 0;
                            if (nSecond >= nduan)
                            {
                                nStepSecond = (int)nSecond / nduan;
                            }
                            else
                            {
                                if (nSecond == 0)
                                {
                                    nduan = 1;
                                    nStepSecond = 1;
                                }
                                else
                                {
                                    nduan = nSecond;
                                    nStepSecond = 1;
                                }

                            }
                            //-------------------------------------------------
                            int hStep = (int)(m_oneDrawArea.Width / nduan);


                            tempRC.Location = new Point(m_oneDrawArea.Left - 5, m_oneDrawArea.Bottom + 5);
                            tempRC.Size = new Size(120, 20);
                            //绘制刻度值
                            m_DrawFuns.DrawPrintOneString("0", 10, StringAlignment.Center, tempRC, true);
                            //绘制横刻度
                            for (int k = 1; k <= nduan; k++)
                            {
                                m_DrawFuns.plotLine2(new Point[] { new Point(m_oneDrawArea.Left + k * hStep, m_oneDrawArea.Bottom - 5), new Point(m_oneDrawArea.Left + k * hStep, m_oneDrawArea.Bottom) });

                                //绘制刻度值
                                string strtitle = string.Empty;
                                if (nStepSecond < 60)
                                    strtitle = (k) * nStepSecond + "\"";
                                else
                                {
                                    strtitle = ((k) * nStepSecond) / 60 + @"\'";
                                    strtitle += ((k) * nStepSecond) % 60 + "\"";
                                }
                                tempRC.Location = new Point(m_oneDrawArea.Left + k * hStep - 10, m_oneDrawArea.Bottom + 5);
                                tempRC.Size = new Size(120, 20);
                                //绘制刻度值
                                m_DrawFuns.DrawPrintOneString(strtitle, 8, StringAlignment.Center, tempRC, true);

                            }

                            //-------------------------------------------------
                            //绘制纵刻度
                            int Vcnt = (int)(m_range.Height / 500);  //刻度数
                            int VStep = (int)(m_oneDrawArea.Height / Vcnt);
                            int Vvalue = 0;

                            for (int k = 1; k <= Vcnt; k++)
                            {
                                m_DrawFuns.plotLine2(new Point[] { new Point(m_oneDrawArea.Left, m_oneDrawArea.Bottom - k * VStep), new Point(m_oneDrawArea.Left + 5, m_oneDrawArea.Bottom - k * VStep) });


                                tempRC.Location = new Point(m_oneDrawArea.Left + 8, m_oneDrawArea.Bottom - k * VStep - 10);
                                tempRC.Size = new Size(120, 20);
                                //绘制刻度值
                                m_DrawFuns.DrawPrintOneString((Vvalue + k * 500).ToString(), 8, StringAlignment.Center, tempRC, true);

                            }

                            //-------------------------------------------------
                            Dictionary<int, Index_value> temp = new Dictionary<int, Index_value>();
                            //画曲线
                            List<StruData> tempstrudata = OneCurveData.list_Wights;
                            m_DrawFuns.plotLine3( ref tempstrudata, m_range, m_oneDrawArea, Color.Black, ref temp);
                        
                        }
                        if (OneCurveData.showMode[(int)CuvrlMode.ufr] == 1)  //尿流率
                        {
                            Rectangle m_oneDrawArea = m_DrawArea;  //绘画区域 

                            if (ncurcount ==1)
                                m_oneDrawArea.Location = new Point((int)m_DrawArea.Left, (int)nCurHight + 30);
                            else
                                m_oneDrawArea.Location = new Point((int)m_DrawArea.Left + m_DrawArea.Width / 2, (int)nCurHight + 30);
                            m_oneDrawArea.Width = (int)(m_DrawArea.Width / 2 - 20);
                            m_oneDrawArea.Height = (int)( stepH * 3 /2 );

                            //绘制坐标
                            m_DrawFuns.plotLine2( new Point[] { new Point(m_oneDrawArea.Left, m_oneDrawArea.Top), new Point(m_oneDrawArea.Left, m_oneDrawArea.Bottom) });

                            //箭头
                            m_DrawFuns.plotLine2(new Point[] { new Point(m_oneDrawArea.Left, m_oneDrawArea.Top), new Point(m_oneDrawArea.Left - 2, m_oneDrawArea.Top + 10) });
                            m_DrawFuns.plotLine2(new Point[] { new Point(m_oneDrawArea.Left, m_oneDrawArea.Top), new Point(m_oneDrawArea.Left - 1, m_oneDrawArea.Top + 10) });
                            m_DrawFuns.plotLine2(new Point[] { new Point(m_oneDrawArea.Left, m_oneDrawArea.Top), new Point(m_oneDrawArea.Left + 1, m_oneDrawArea.Top + 10) });
                            m_DrawFuns.plotLine2(new Point[] { new Point(m_oneDrawArea.Left, m_oneDrawArea.Top), new Point(m_oneDrawArea.Left + 2, m_oneDrawArea.Top + 10) });

                            Rectangle tempRC = new Rectangle();

                            tempRC.Location = new Point(m_oneDrawArea.Left + 8, m_oneDrawArea.Top);
                            tempRC.Size = new Size(120, 20);

                            m_DrawFuns.DrawPrintOneString("V(ml)", 10, StringAlignment.Center, tempRC, true);


                            //-------------------------------------------------
                            m_DrawFuns.plotLine2( new Point[] { new Point(m_oneDrawArea.Left, m_oneDrawArea.Bottom), new Point(m_oneDrawArea.Right, m_oneDrawArea.Bottom) });
                            //箭头
                            m_DrawFuns.plotLine2(new Point[] { new Point(m_oneDrawArea.Right, m_oneDrawArea.Bottom), new Point(m_oneDrawArea.Right - 10, m_oneDrawArea.Bottom - 1) });
                            m_DrawFuns.plotLine2(new Point[] { new Point(m_oneDrawArea.Right, m_oneDrawArea.Bottom), new Point(m_oneDrawArea.Right - 10, m_oneDrawArea.Bottom - 2) });
                            m_DrawFuns.plotLine2(new Point[] { new Point(m_oneDrawArea.Right, m_oneDrawArea.Bottom), new Point(m_oneDrawArea.Right - 10, m_oneDrawArea.Bottom + 1) });
                            m_DrawFuns.plotLine2(new Point[] { new Point(m_oneDrawArea.Right, m_oneDrawArea.Bottom), new Point(m_oneDrawArea.Right - 10, m_oneDrawArea.Bottom + 2) });

                            tempRC.Location = new Point(m_oneDrawArea.Right - 30, m_oneDrawArea.Bottom - 25);
                            tempRC.Size = new Size(120, 20);

                            m_DrawFuns.DrawPrintOneString("T(s)", 10, StringAlignment.Center, tempRC, true);
                            //-------------------------------------------------

                            m_oneDrawArea.Location = new Point(m_oneDrawArea.Left, m_oneDrawArea.Top + 30);
                            m_oneDrawArea.Width -= 30;
                            m_oneDrawArea.Height -= 30;
                            //-------------------------------------------------
                            Size m_range = nll_Range;  //范围：（最小值 ，最大值）
                            //绘制刻度
                            int ptCnt = OneCurveData.list_Wights.Count;
                            int nSecond = ptCnt; //秒数

                            int nduan = 12;  //分几个区间
                            int nStepSecond = 0;
                            if (nSecond >= nduan)
                            {
                                nStepSecond = (int)nSecond / nduan;
                            }
                            else
                            {
                                if (nSecond == 0)
                                {
                                    nduan = 1;
                                    nStepSecond = 1;
                                }
                                else
                                {
                                    nduan = nSecond;
                                    nStepSecond = 1;
                                }

                            }
                            //-------------------------------------------------

                            int hStep = (int)(m_oneDrawArea.Width / nduan);


                            tempRC.Location = new Point(m_oneDrawArea.Left - 5, m_oneDrawArea.Bottom + 5);
                            tempRC.Size = new Size(120, 20);
                            //绘制刻度值
                            m_DrawFuns.DrawPrintOneString("0", 10, StringAlignment.Center, tempRC, true);
                            //绘制横刻度
                            for (int k = 1; k <= nduan; k++)
                            {
                                m_DrawFuns.plotLine2(new Point[] { new Point(m_oneDrawArea.Left + k * hStep, m_oneDrawArea.Bottom - 5), new Point(m_oneDrawArea.Left + k * hStep, m_oneDrawArea.Bottom) });

                                //绘制刻度值
                                string strtitle = string.Empty;
                                if (nStepSecond < 60)
                                    strtitle = (k) * nStepSecond + "\"";
                                else
                                {
                                    strtitle = ((k) * nStepSecond) / 60 + @"\'";
                                    strtitle += ((k) * nStepSecond) % 60 + "\"";
                                }
                                tempRC.Location = new Point(m_oneDrawArea.Left + k * hStep - 10, m_oneDrawArea.Bottom + 5);
                                tempRC.Size = new Size(120, 20);
                                //绘制刻度值
                                m_DrawFuns.DrawPrintOneString(strtitle, 8, StringAlignment.Center, tempRC, true);

                            }

                            //-------------------------------------------------
                            //绘制纵刻度
                            int VvaluStep = (int)(m_range.Height / 3);  //刻度数
                            int VStep = (int)(m_oneDrawArea.Height / 3); //刻度step
                            int Vvalue = 0;

                            for (int k = 1; k <= 3; k++)
                            {
                                m_DrawFuns.plotLine2(new Point[] { new Point(m_oneDrawArea.Left, m_oneDrawArea.Bottom - k * VStep), new Point(m_oneDrawArea.Left + 5, m_oneDrawArea.Bottom - k * VStep) });


                                tempRC.Location = new Point(m_oneDrawArea.Left + 8, m_oneDrawArea.Bottom - k * VStep - 10);
                                tempRC.Size = new Size(120, 20);
                                //绘制刻度值
                                m_DrawFuns.DrawPrintOneString((Vvalue + k * VvaluStep).ToString(), 8, StringAlignment.Center, tempRC, true);

                            }

                            //-------------------------------------------------

                            //画曲线
                            Dictionary<int, Index_value> temp = new Dictionary<int, Index_value>();
                            List<StruData> tempstrudata = OneCurveData.list_ufr;
                            m_DrawFuns.plotLine3( ref tempstrudata, m_range, m_oneDrawArea, Color.Black, ref temp);
                   
                        }
                        nCurHight += 2*stepH;
                }
                nCurHight += 20;   
                 
                //-------------------------------------------------------
                //绘制说明
                if (OneCurveData.strMeno != "")
                {
                    m_DrawArea.Location = new Point((int)(m_DrawArea.Left), (int)(nCurHight));
                    m_DrawArea.Size = new Size((int)(m_DrawArea.Width), stepH);
                    nCurHight += stepH;

                    //绘制 结尾
                    m_DrawFuns.DrawPrintOneString(OneCurveData.strMeno, 10, StringAlignment.Far, m_DrawArea);
                    nCurHight += 10;  //每组曲线间隔
                }
                //-------------------------------------------------------
                
                 
            }
            //------------------------------------------------------- 

            m_DrawArea.Location = new Point((int)(m_DrawArea.Left), (int)(nCurHight));
            m_DrawArea.Size = new Size((int)(m_DrawArea.Width), 30);
            nCurHight += 30;


            //-------------------------------------------
            //绘制结束
            m_DrawFuns.UpdateDraw();
            m_DrawFuns.Dispose();
            return;
            //-------------------------------------------




            //绘制 结尾
            m_DrawFuns.DrawPrintOneString("尿动力学诊断:  测试", 12, StringAlignment.Far, m_DrawArea);
            nCurHight += 80;  //间隔 
            //--------------------------
            //绘制 结尾
            nCurHight += 10;  //间隔  
            Rectangle m_tempRect1 = m_DrawArea;
            m_tempRect1.Location = new Point((int)(m_DrawArea.Left), (int)(nCurHight));
            m_tempRect1.Size = new Size((int)(m_DrawArea.Width / 2), 30);

            m_DrawFuns.DrawPrintOneString("检查者: __________________ ", 12, StringAlignment.Center, m_tempRect1, true);

            m_tempRect1.Location = new Point((int)(m_DrawArea.Left + m_DrawArea.Width / 2), (int)(nCurHight));
            m_DrawFuns.DrawPrintOneString("检查日期: __________________ ", 12, StringAlignment.Center, m_tempRect1, true);

            nCurHight += 30;
           
            //--------------------------

            nCurHight += 20;  //间隔 
            m_DrawArea.Location = new Point((int)(m_DrawArea.Left), (int)(nCurHight));
            m_DrawArea.Size = new Size((int)(m_DrawArea.Width), 10);
            nCurHight = m_DrawArea.Bottom;

            m_DrawFuns.plotLine2( new Point[] { new Point(m_DrawArea.Left, m_DrawArea.Top), new Point(m_DrawArea.Right, m_DrawArea.Top) });
            nCurHight += 10;  //间隔 
            //-------------------------------------------------------

            m_DrawFuns.UpdateDraw();
        }


        //返回首页
        private void button_Retmainform_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void report_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(IsPrintReportheadPage)
            {
                report_ToolStripMenuItem.Text = "打印报告页";
            }
            else
            {
                report_ToolStripMenuItem.Text = "不打印报告页";
            }
             
            IsPrintReportheadPage = !IsPrintReportheadPage;
            DrawPrintContext();  //绘制报告内容
            
        }
        
        
    }
}
