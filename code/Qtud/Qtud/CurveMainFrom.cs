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
using System.Threading;
using System.Management;  //获取所有USB用到

using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;
  

namespace Qtud.Qtud
{
    public struct StruPosData  //每个点
    {
        public float value;   //值
        public int time;    //时间点序号，0，1，2，3...
        public bool isShow; //是否作为显示采样点
    }

    //结构体；一条曲线数据
    public struct CurveDatas
    { 
        public DateTime StartTime; //开始时间
        public DateTime endTime; //结束时间
        public float fmax_Wight;   //最大值
        public float fmax_Pves;   //最大值
        public float fmax_Pabd;   //最大值
        public float fmax_Pdet;   //最大值
        public float fmax_ufr;   //最大值

        public string str_range;  //范围， 压力最小值，压力最大值，尿量最小值，尿量最大值，尿流率最小值，尿流率最大值

        public Byte []showMode;  //显示检测的模式,  //pves, pabd, pdet, wights 

        public List<StruData> list_Wights;  //尿量值
        public List<StruData> list_Pves;   //膀胱压力
        public List<StruData> list_Pabd;   //直肠压力
        public List<StruData> list_Pdet;   //逼尿肌压力 = 膀胱压力 - 直肠压力
        public List<StruData> list_ufr;    //尿流率
        public string strMeno ;   //备注

        public int FirstFileEndIndex;  //6模式，两个文件结束分隔线点
        public List<string> list_Files;    //文件列表
        
    };

    public enum CuvrlMode
    {
        Pves = 0,
        Pabd = 1,
        Pdet = 2,
        Wight = 3,
        ufr = 4,   //尿流率
        meno = 5,   //说明
    };

    public struct Index_value
    {
       public int nIndex;
       public string Value;
    };

    public partial class MainFrom_Curve : Form
    {
        #region  变量

        private PatientInfoModel m_CurSelPatientInfo;    //当前选择的病人
        private List<string> m_ListUSBDevs = new List<string>();  //USB设备列表

        private string  m_strFloder= @"d:\qtud_data\";    //备份文件夹 

        private string ValueUnit = "cmH2O";  //数值单位
        Dictionary<string, List<string>> Dev_listSerial_Map = new Dictionary<string, List<string>>();    //设备号 -检测号列表映射

        private int m_SelMode = 6;  //-1全部
        private List<TreeNode> m_CheckNode_List = new List<TreeNode>();   //最多两个
        private Rectangle m_CurCurveArea = new Rectangle();  //曲线绘制区域,鼠标滑动时，显示当前值
        private Rectangle m_CurSelCurveArea = new Rectangle();  //选取的曲线绘制区域
        
        Dictionary<int, Index_value> Pves_X_Value_Map = new Dictionary<int, Index_value>();    //曲线X坐标点与值的映射
        Dictionary<int, Index_value> Pabd_X_Value_Map = new Dictionary<int, Index_value>();    //曲线X坐标点与值的映射
        Dictionary<int, Index_value> Pdet_X_Value_Map = new Dictionary<int, Index_value>();    //曲线X坐标点与值的映射
        Dictionary<int, Index_value> Wight_X_Value_Map = new Dictionary<int, Index_value>();    //曲线X坐标点与值的映射
        Dictionary<int, Index_value> Ufr_X_Value_Map = new Dictionary<int, Index_value>();    //曲线X坐标点与值的映射
        private bool m_isDownLeft = false;  //是否在曲线区域按下鼠标左键

        int nSelStartindex = 0;  //子图中的开始序号

        Size curve3_Range = new Size(-100, 300);  //3条曲线范围：（最小值 ，最大值）
        Size nl_Range = new Size(0, 3000);  //尿量范围：（最小值 ，最大值）
        Size nll_Range = new Size(0, 100);  //尿流率范围：（最小值 ，最大值）

        DrawFuns m_DrawFuns = new DrawFuns();

        //TXT文件内容结构体
        public struct Struct_Txt_Data
        {
            public string strCheckDate;
            public string strCheckTime;
            public string strCheckMode;
        }


        //-------------------------------------------------------
        public struct StruFileInfo  //一个检测号文件
        {
            public string  strFileMode;      //文件检测模式
            public string  m_filePath;    //文件路径
        }

        public struct StruOneDayFileInfo  //一个检测号文件
        {
            public string strTxtFile;                    //Txt文件
            public List<StruFileInfo> m_StruFileInfo;    //文件列表
        }
        public struct StruCheckFileInfo  //一个检测号文件
        {
            public bool isLoad;     //是否导入                  //Txt文件
            public List<StruOneDayFileInfo> m_StruOneDayFileInfo;    //文件列表
        }
        Dictionary<string, StruCheckFileInfo> m_checkNum_Files_map = new Dictionary<string, StruCheckFileInfo>();    //检查号与文件的映射
        //-------------------------------------------------------

       

        //一条曲线数据
        private  CurveDatas m_CurveDatas = new CurveDatas
        {
            StartTime = DateTime.Now, //(filename.Replace('.', ':')),
            endTime = DateTime.Now,
            showMode = new byte[5],  //全部显示
            str_range = string.Empty,

            list_Pabd = new List<StruData>(),
            list_Pdet = new List<StruData>(),
            list_Pves = new List<StruData>(),
            list_Wights = new List<StruData>(),
            list_ufr = new List<StruData>(),

            fmax_Wight =-100f,
            fmax_Pves =-100f,
            fmax_Pabd =-100f,
            fmax_Pdet =-100f,
            fmax_ufr =-100f, 

            strMeno = string.Empty,
            FirstFileEndIndex = -1,

            list_Files =new  List<string>()

        };

        List<CurveDatas> m_PrintCurveDatas = new List<CurveDatas>();  //加入到打印列表里的
        bool m_isSavePrintCurve = false;    //是否需要提示保存
        //-------------------------------------------------------

        //选择的子级
        private CurveDatas m_SubCurveDatas = new CurveDatas
        {
            StartTime = DateTime.Now, //(filename.Replace('.', ':')),
            endTime = DateTime.Now,
            showMode = new byte[5],  //全部显示

            str_range = string.Empty,

            list_Pabd = new List<StruData>(),
            list_Pdet = new List<StruData>(),
            list_Pves = new List<StruData>(),
            list_Wights = new List<StruData>(),
            list_ufr = new List<StruData>(),

            fmax_Wight = -100f,
            fmax_Pves = -100f,
            fmax_Pabd = -100f,
            fmax_Pdet = -100f,
            fmax_ufr = -100f, 

            strMeno = string.Empty,
            FirstFileEndIndex = -1,
            list_Files =new  List<string>() 
        };

        public TestDatas m_TestDatas = new TestDatas();

        
        private tbl_patient_checknum_link_Manager patient_checknum_link_Manager = new tbl_patient_checknum_link_Manager();
        private tbl_patient_checknum_file_info_Manager patient_checknum_file_info_Manager = new tbl_patient_checknum_file_info_Manager();  //文件存储管理
       
        private ReportInfoModel m_ReportInfoModel = new ReportInfoModel();
        private ReportInfoManager m_ReportInfoManager = new ReportInfoManager();  //报告

        private tbl_curve_info_Model curve_info_Model = new tbl_curve_info_Model();
        private tbl_curve_info_Manager m_curve_info_Manager = new tbl_curve_info_Manager(); //曲线

        #endregion

        #region  界面操作函数
        public MainFrom_Curve(PatientInfoModel CurSelPatientInfo, TestDatas _TestDatas)
        {
            InitializeComponent();

            m_CurSelPatientInfo = CurSelPatientInfo;
            m_TestDatas = _TestDatas;

            m_ReportInfoModel.uuid = m_TestDatas.uuid;
        }

        ~MainFrom_Curve()
        {
            // 这里是清理非托管资源的用户代码段
            m_DrawFuns.Dispose();
        }

        private void MainFrom2_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle =FormBorderStyle.Sizable;
            m_DrawFuns.IniDraw(ref panel_Draw);

            //-----------------------------------
            string strIniFile = "config.ini";
            strIniFile = Directory.GetCurrentDirectory() + "\\" + strIniFile;
            //获取指定KEY的值  
            ValueUnit = INIOperationClass.INIGetStringValue(strIniFile, "Setting", "strUnit", null);
            if (ValueUnit==null)
                ValueUnit = "cmH2O"; 
            //-----------------------------------
            

            string str = string.Empty;
            if (m_CurSelPatientInfo.sex ==1)
                str = "男";
            else
                str = "女";
            label_Info.Text = "患者：" + m_CurSelPatientInfo.name + @"  " + str + @"  " + m_CurSelPatientInfo.id;

            m_CurSelCurveArea.Location = new Point(0, 0);
            m_CurSelCurveArea.Size = new Size(0, 0);

            comboBox_checkMode.SelectedIndex = m_SelMode+1;
            m_ListUSBDevs = GetMobileDiskList(); 
            UpdateUsbTree();
             
       
        }
         
    
        private void button_Back_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void treeView_File_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            //隐藏节点前的checkbox
            if (int.Parse(e.Node.Tag.ToString()) < 200)
                HideCheckBox(this.treeView_File, e.Node);
            e.DrawDefault = true;

        }

        private void treeView_File_AfterCheck(object sender, TreeViewEventArgs e)
        {
            
            if (e.Node != null)
            {
                if (e.Node.Checked)
                { 
                    //----------------------------------------------------
                    //检查是否导出了
                    string strTag = e.Node.Tag.ToString();
                    if (strTag.IndexOf("qtud_data") > -1)  //选的是历史数据
                    {

                    }
                    else
                    {
                        //判断是否导出

                        int ipos = strTag.ToString().IndexOf(",");  //401,G:\1508491901\info\ID2017-06-30\ID2017-06-30 11.42.32
                        string strPath = strTag.ToString().Substring(ipos + 1); //G:\1508491901\info\ID2017-06-30\ID2017-06-30 11.42.32

                        ipos = strPath.IndexOf("\\");
                        strPath = strPath.Substring(ipos + 1); // 1508491901\info\ID2017-06-30\ID2017-06-30 11.42.32

                        ipos = strPath.IndexOf("\\");
                        string checkNo = strPath.Substring(0, ipos); // 1508491901


                        string strWhere = string.Empty;
                        strWhere = @"patient_uuid='" + m_CurSelPatientInfo.uuid + @"' and  checkNum='" + checkNo + @"'";
                        List<tbl_patient_checknum_link_Model> tempModelist = patient_checknum_link_Manager.GetModelList(strWhere);

                        if (tempModelist.Count < 1 )
                        {
                            MessageBox.Show("请先导出数据");
                            m_CheckNode_List.Add(e.Node);

                            return;
                        }

                        //查看文件是否存在
                        for (int r = 0; r < tempModelist.Count; r++)
                        {
                            if (!File.Exists(tempModelist[r].txtPath))
                            {
                                MessageBox.Show("请先导出数据");
                                m_CheckNode_List.Add(e.Node);
                                return;
                            }
                        }
                        //查看是否有文件没导出
                        if (m_checkNum_Files_map.ContainsKey(checkNo))
                        {
                            StruCheckFileInfo tempStruCheckFileInfo = m_checkNum_Files_map[checkNo];

                            foreach (StruOneDayFileInfo OneDayFileInfo in tempStruCheckFileInfo.m_StruOneDayFileInfo)
                            {
                                string strTxtFile = OneDayFileInfo.strTxtFile;  //G:\3384328829\ID2017-08-10.txt

                                ipos = strTxtFile.IndexOf("\\");
                                string strDesPath = m_strFloder + m_CurSelPatientInfo.id + strTxtFile.Substring(ipos);

                                int n = 0;
                                for ( n = 0; n < tempModelist.Count; n++)
                                {
                                    if (strDesPath == tempModelist[n].txtPath)
                                    {
                                        break;
                                    } 
                                }
                                if (tempModelist.Count == n)
                                {
                                    MessageBox.Show("请先导出数据");
                                    m_CheckNode_List.Add(e.Node);
                                    return;
                                }
                            }
                         }
                    }

                    //----------------------------------------------------
                    string nodeText = e.Node.Text;
                    string nMode = nodeText.Substring(1, 1);
                    if (nMode == "6")
                    {
                        if (m_CheckNode_List.Count == 2)
                        {
                            MessageBox.Show("最多只能选取2个数据文件");
                            e.Node.Checked = false; 
                            return;
                        }
                        if (m_CheckNode_List.Count == 1)  //两个文件合并
                        {
                            TreeNode FirstNode = m_CheckNode_List[0];
                            if (FirstNode.Parent.Tag.ToString() != e.Node.Parent.Tag.ToString())
                            {
                                MessageBox.Show("请选择连续的数据文件");
                                e.Node.Checked = false;
                                return;
                            }
                             
                            int ipos = FirstNode.Tag.ToString().IndexOf(",");
                            string strNodeid1 = FirstNode.Tag.ToString().Substring(0,ipos);

                            ipos = e.Node.Tag.ToString().IndexOf(",");
                            string strNodeid2 = e.Node.Tag.ToString().Substring(0, ipos);
                            if(int.Parse(strNodeid2) - int.Parse(strNodeid1) == 1  )
                            {
                                m_CheckNode_List.Insert(0, e.Node);
                            }
                            else if (int.Parse(strNodeid2) - int.Parse(strNodeid1) == -1)
                            {
                                m_CheckNode_List.Add(e.Node);
                            }
                            else
                            {
                                MessageBox.Show("请选择连续的数据文件");
                                e.Node.Checked = false;
                                return;
                            }

                            List<string> m_File6List = new List<string>();
                            int iiv = 0;
                            foreach (TreeNode oneNode in m_CheckNode_List)  //遍历文件
                            {
                                string strDataFile = string.Empty;  //数据文件
                                strDataFile = oneNode.Tag.ToString();  //400,E:\1508491901\info\ID2017-06-29\ID2017-06-29 16.57.10
                                strDataFile = strDataFile.Substring(strDataFile.IndexOf(",") + 1);   //E:\1508491901\info\ID2017-06-29\ID2017-06-29 16.57.10

                                int ipos1 = strDataFile.LastIndexOf(@"\ID");
                                string strDate = strDataFile.Substring(ipos1 + 3);  // 2017-06-29 16.57.10
                                string strpPath = strDataFile.Substring(0, ipos1 + 1);  // E:\1508491901\info\ID2017-06-29\
                                 
                                //--------------------------------------------
                                //获取路径下，文件名含有strDate的文件
                                List<string> m_strDataFilList = UpdateDataFileList(strpPath, strDate);
                                if (m_strDataFilList.Count == 1)
                                {
                                    if(iiv ==0)
                                    {
                                        ipos1 = m_strDataFilList[0].LastIndexOf(@".hold");
                                        if (ipos1<0)
                                        {
                                            MessageBox.Show("所选数据文件不匹配");
                                            e.Node.Checked = false;
                                            return;
                                        }

                                        nSelStartindex = 0;
                                        IniCurveData(ref m_SubCurveDatas);
                                        IniCurveData(ref m_CurveDatas);
                                        m_CurveDatas.StartTime = DateTime.Parse(strDate.Replace('.', ':'));  // 2017-06-29 16:57:10
                                        m_CurveDatas.endTime = m_CurveDatas.StartTime;
                                    }
                                    foreach (string onefile in m_strDataFilList)  //遍历文件
                                    {
                                        m_File6List.Add(onefile);
                                    }
                                }
                                iiv++;
                            }

                            DrawNodeCurve(e.Node, m_File6List);

                            return;
                        }
                    }
                    else
                    {
                        if (m_CheckNode_List.Count > 0)
                        {
                            MessageBox.Show("最多只能选取1个数据文件");
                            e.Node.Checked = false;
                            return;
                        }
                    }

                    m_CheckNode_List.Add(e.Node);

                    DrawNodeCurve(e.Node);


                }
                else 
                {
                    TreeNode treeNode = null;

                    int i = 0;
                    for(; i<m_CheckNode_List.Count; i++)
                    {
                        if (m_CheckNode_List[i].Tag == e.Node.Tag)
                        {
                            m_CheckNode_List.RemoveAt(i);
                            break;
                        }
                    }
                    if (m_CheckNode_List.Count == 0)
                    {
                        nSelStartindex = 0;
                        IniCurveData(ref m_CurveDatas);
                        IniCurveData(ref m_SubCurveDatas);

                        DrawEmpty();  //绘图空图

                    }
                    else  //把上一个作为这次的绘图
                    {
                        treeNode = m_CheckNode_List[m_CheckNode_List.Count - 1];
                        if (treeNode != null)
                        {
                            DrawNodeCurve(treeNode);
                        }

                    }
                }

                //{
                //    //节点信息
                //    string strNodeInfo = string.Empty;
                //    strNodeInfo += string.Format("Name：{0}\r\n", e.Node.Name);
                //    strNodeInfo += string.Format("Text：{0}\r\n", e.Node.Text);
                //    strNodeInfo += string.Format("Nodes：{0}\r\n", e.Node.Nodes.Count.ToString());
                //    strNodeInfo += string.Format("Level：{0}\r\n", e.Node.Level);
                //}
               
                 
            }
        }

        private void IniCurveData(ref CurveDatas _CurveDatas)
        {
            //--------------------------------------------
            //清空
            _CurveDatas.StartTime = DateTime.Now; //(filename.Replace('.', ':')),
            _CurveDatas.endTime = DateTime.Now;
            _CurveDatas.showMode[0] = 0;
            _CurveDatas.showMode[1] = 0;
            _CurveDatas.showMode[2] = 0;
            _CurveDatas.showMode[3] = 0;
            _CurveDatas.showMode[4] = 0;

            _CurveDatas.str_range = string.Empty;

            _CurveDatas.list_Pabd.RemoveRange(0, _CurveDatas.list_Pabd.Count);
            _CurveDatas.list_Pdet.RemoveRange(0, _CurveDatas.list_Pdet.Count);
            _CurveDatas.list_Pves.RemoveRange(0, _CurveDatas.list_Pves.Count);
            _CurveDatas.list_Wights.RemoveRange(0, _CurveDatas.list_Wights.Count);
            _CurveDatas.list_ufr.RemoveRange(0, _CurveDatas.list_ufr.Count);

            _CurveDatas.list_Files.RemoveRange(0, _CurveDatas.list_Files.Count);

            _CurveDatas.fmax_Wight = -1000;   //最大值
            _CurveDatas.fmax_Pves = -1000;   //最大值
            _CurveDatas.fmax_Pabd = -1000;   //最大值
            _CurveDatas.fmax_Pdet = -1000;   //最大值
            _CurveDatas.fmax_ufr = -1000;   //最大值

            _CurveDatas.strMeno = string.Empty;
            _CurveDatas.FirstFileEndIndex =-1;
            //--------------------------------------------
        }

        private void DrawNodeCurve(TreeNode treeNode, List<string> m_File6List=null)
        {
            List<string> m_strDataFilList = m_File6List;
            if (m_File6List == null)
            {
                nSelStartindex = 0;
                IniCurveData(ref m_SubCurveDatas);
                IniCurveData(ref m_CurveDatas);

                string strDataFile = string.Empty;  //数据文件
                strDataFile = treeNode.Tag.ToString();  //400,E:\1508491901\info\ID2017-06-29\ID2017-06-29 16.57.10
                strDataFile = strDataFile.Substring(strDataFile.IndexOf(",") + 1);   //E:\1508491901\info\ID2017-06-29\ID2017-06-29 16.57.10

                int ipos = strDataFile.LastIndexOf(@"\ID");
                string strDate = strDataFile.Substring(ipos + 3);  // 2017-06-29 16.57.10
                string strpPath = strDataFile.Substring(0, ipos + 1);  // E:\1508491901\info\ID2017-06-29\

                m_CurveDatas.StartTime = DateTime.Parse(strDate.Replace('.', ':'));  // 2017-06-29 16:57:10
                m_CurveDatas.endTime = m_CurveDatas.StartTime;

                //--------------------------------------------
                //获取路径下，文件名含有strDate的文件
                m_strDataFilList = UpdateDataFileList(strpPath, strDate);
            }
            

            if (m_strDataFilList.Count > 0)
            {
                ReadOneDataFile(m_strDataFilList, ref  m_CurveDatas);

                m_CurveDatas.list_Files = m_strDataFilList;
            }
            //--------------------------------------------


            DrawCurve(m_CurveDatas);  //绘图
        }

        private void checkBox_Pves_CheckedChanged(object sender, EventArgs e)
        {
            if (m_SubCurveDatas.list_Pabd.Count > 0)
            {
                DrawCurve(m_SubCurveDatas);  //绘子图
            }
            else
            {
                DrawCurve(m_CurveDatas);  //绘图
            }

        }

        private void checkBox_Pabd_CheckedChanged(object sender, EventArgs e)
        {
            if (m_SubCurveDatas.list_Pabd.Count > 0)
            {
                DrawCurve(m_SubCurveDatas);  //绘子图
            }
            else
            {
                DrawCurve(m_CurveDatas);  //绘图
            }
        }

        private void checkBox_Pdet_CheckedChanged(object sender, EventArgs e)
        {
            if (m_SubCurveDatas.list_Pabd.Count > 0)
            {
                DrawCurve(m_SubCurveDatas);  //绘子图
            }
            else
            {
                DrawCurve(m_CurveDatas);  //绘图
            }
        }

        private void checkBox_nl_CheckedChanged(object sender, EventArgs e)
        {
            if (m_SubCurveDatas.list_Pabd.Count > 0)
            {
                DrawCurve(m_SubCurveDatas);  //绘子图
            }
            else
            {
                DrawCurve(m_CurveDatas);  //绘图
            }
        }

        private void checkBox_nll_CheckedChanged(object sender, EventArgs e)
        {
            if (m_SubCurveDatas.list_Pabd.Count > 0)
            {
                DrawCurve(m_SubCurveDatas);  //绘子图
            }
            else
            {
                DrawCurve(m_CurveDatas);  //绘图
            }
        }

        #endregion

        //防止加载闪烁
        protected override CreateParams CreateParams
        {

            get
            {

                CreateParams cp = base.CreateParams;

                cp.ExStyle |= 0x02000000;

                return cp;

            }

        }

        #region 隐藏前面的checkbox

        //隐藏前面的checkbox
        private const int TVIF_STATE = 0x8;
        private const int TVIS_STATEIMAGEMASK = 0xF000;
        private const int TV_FIRST = 0x1100;
        private const int TVM_SETITEM = TV_FIRST + 63;
        private void HideCheckBox(TreeView tvw, TreeNode node)
        {

            TVITEM tvi = new TVITEM();

            tvi.hItem = node.Handle;

            tvi.mask = TVIF_STATE;

            tvi.stateMask = TVIS_STATEIMAGEMASK;

            tvi.state = 0;

            SendMessage(tvw.Handle, TVM_SETITEM, IntPtr.Zero, ref tvi);

        }

        [StructLayout(LayoutKind.Sequential, Pack = 8, CharSet = CharSet.Auto)]

        private struct TVITEM
        {
            public int mask;
            public IntPtr hItem;
            public int state;
            public int stateMask;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpszText;
            public int cchTextMax;
            public int iImage;
            public int iSelectedImage; public int cChildren; public IntPtr lParam;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]

        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, ref TVITEM lParam);
#endregion

      

        #region  获取电脑上所有移动磁盘的列表(usb ,移动磁盘)
        //获取电脑上所有移动磁盘的列表(usb ,移动磁盘)
        private List<string> GetMobileDiskList()
        {
            System.Management.ManagementClass mc = new System.Management.ManagementClass("Win32_DiskDrive");
            ManagementObjectCollection moc = mc.GetInstances();

            List<string> drs = new List<string>();
            foreach (ManagementObject mo in moc)
            {
                if (mo.Properties["InterfaceType"].Value.ToString() != "USB")
                    continue;
                foreach (ManagementObject mo1 in mo.GetRelated("Win32_DiskPartition"))
                {
                    foreach (ManagementBaseObject mo2 in mo1.GetRelated("Win32_LogicalDisk"))
                    {
                        drs.Add(mo2.Properties["Name"].Value.ToString() + @"\");
                    }

                }
            }
            return drs ;
            //return drs.ToArray();
        }
        #endregion

        #region 检测USB设备是否插入 
        public const int WM_DEVICECHANGE = 0x219;
        public const int DBT_DEVICEARRIVAL = 0x8000;
        public const int DBT_CONFIGCHANGECANCELED = 0x0019;
        public const int DBT_CONFIGCHANGED = 0x0018;
        public const int DBT_CUSTOMEVENT = 0x8006;
        public const int DBT_DEVICEQUERYREMOVE = 0x8001;
        public const int DBT_DEVICEQUERYREMOVEFAILED = 0x8002;
        public const int DBT_DEVICEREMOVECOMPLETE = 0x8004;
        public const int DBT_DEVICEREMOVEPENDING = 0x8003;
        public const int DBT_DEVICETYPESPECIFIC = 0x8005;
        public const int DBT_DEVNODES_CHANGED = 0x0007;
        public const int DBT_QUERYCHANGECONFIG = 0x0017;
        public const int DBT_USERDEFINED = 0xFFFF;

        public const int DBT_DEVTYP_VOLUME = 0x00000002;

        protected override void WndProc(ref Message m)
        {
            m = NewMethod(m);
            base.WndProc(ref m);

        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DEV_BROADCAST_VOLUME
        {
            public int dbcv_size;
            public int dbcv_devicetype;
            public int dbcv_reserved;
            public int dbcv_unitmask;
        }


        private Message NewMethod(Message m)
        {
            try
            {
                if (m.Msg == WM_DEVICECHANGE)
                {
                    switch (m.WParam.ToInt32())
                    {
                        case WM_DEVICECHANGE:
                            break;
                        case DBT_DEVICEARRIVAL://U盘插入
                            DriveInfo[] s = DriveInfo.GetDrives();
                            int ncnt = m_ListUSBDevs.Count;
                            foreach (DriveInfo drive in s)
                            {
                                if (drive.Name.ToString() == "A:\\")
                                {
                                    continue;
                                }
                                if (drive.DriveType == DriveType.Removable)
                                {

                                    int kk = 0;
                                    for ( kk = 0; kk < m_ListUSBDevs.Count; kk++ )
                                    {
                                        if(m_ListUSBDevs[kk] == drive.Name.ToString())
                                            break;
                                    }
                                    if (kk >= m_ListUSBDevs.Count)
                                        m_ListUSBDevs.Insert(0, drive.Name.ToString());
                                      
                                    //-----------------------------------------------

                                    //DialogResult dr = MessageBox.Show("是否要拷贝U盘中的信息？", "U盘", MessageBoxButtons.OKCancel);
                                    //if (dr == DialogResult.OK)
                                    //{
                                    //    SaveFileDialog Save = new SaveFileDialog();
                                    //    Save.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                                    //    Save.ValidateNames = true; //文件有效性验证ValidateNames，验证用户输入是否是一个有效的Windows文件名
                                    //    Save.RestoreDirectory = true;
                                    //    //Save.CheckPathExists = true; //验证文件有效性
                                    //    Save.Filter = @"所有文件|*.*\";
                                    //    //Save.FilterIndex=2; 
                                    //    if (Save.ShowDialog() == DialogResult.OK)
                                    //    {
                                    //        string filename = Save.FileName;
                                    //        if (filename.ToString().Trim() != string.Empty)
                                    //        {
                                    //            //UClass uc = new UClass();
                                    //            //uc.srcdir = drive.Name.ToString();
                                    //            //uc.desdir = filename.ToString().Trim();
                                    //            //uc.CopyDirectory();
                                    //            GetMulitThread(drive.Name.ToString(), filename.ToString().Trim());
                                    //        }
                                    //    }
                                    //}

                                    //CopyDirectory(drive.Name.ToString(), "E:\\");
                                    
                                }
                            }
                            if (ncnt != m_ListUSBDevs.Count)
                                UpdateUsbTree();
                            break;
                        case DBT_CONFIGCHANGECANCELED:
                            break;
                        case DBT_CONFIGCHANGED:
                            break;
                        case DBT_CUSTOMEVENT:
                            break;
                        case DBT_DEVICEQUERYREMOVE:
                            break;
                        case DBT_DEVICEQUERYREMOVEFAILED:
                            break;
                        case DBT_DEVICEREMOVECOMPLETE: //U盘卸载
                            DriveInfo[] I = DriveInfo.GetDrives();


                            foreach (DriveInfo DrInfo in I)
                            {
                                int devType = Marshal.ReadInt32(m.LParam, 4);

                                if (devType == DBT_DEVTYP_VOLUME)
                                {
                                    //DEV_BROADCAST_VOLUME vol;
                                    //vol = (DEV_BROADCAST_VOLUME)Marshal.PtrToStructure(m.LParam, typeof(DEV_BROADCAST_VOLUME));
                                    //string ID = vol.dbcv_unitmask.ToString("x");
                                    //string moveDev = IO(ID) + @"\";

                                    //int ii = 0;
                                    //foreach (string strUsb in m_ListUSBDevs)
                                    //{
                                    //    if (strUsb == moveDev)
                                    //    {
                                    //        m_ListUSBDevs.RemoveAt(ii);
                                    //        break;
                                    //    }
                                    //    ii++;
                                    //}

                                    //------------------------------------------
                                    //DriveInfo[] ss = DriveInfo.GetDrives();
                                    //m_ListUSBDevs.Clear();
                                    //foreach (DriveInfo drive in ss)
                                    //{
                                    //    if (drive.Name.ToString() == "A:\\")
                                    //    {
                                    //        continue;
                                    //    }
                                    //    if (drive.DriveType == DriveType.Removable)
                                    //    { 
                                    //        m_ListUSBDevs.Add(drive.Name.ToString());
                                    //    }
                                    //}

                                    //------------------------------------------

                                    Thread.Sleep(5000);
                                    m_ListUSBDevs = GetMobileDiskList();
                                    UpdateUsbTree();
                                     
                                    break;
                                }
                            }

                            //----------------------------------------------------
                             
                             
                            break;
                        case DBT_DEVICEREMOVEPENDING:
                            break;
                        case DBT_DEVICETYPESPECIFIC:
                            break;
                        case DBT_DEVNODES_CHANGED:
                            break;
                        case DBT_QUERYCHANGECONFIG:
                            break;
                        case DBT_USERDEFINED:
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //if (oThread != null)
                //    oThread.Abort();
            }
            return m;
        }


        public string IO(string ff)
        {
            string Value = "";
            switch (ff)
            {
                case "1":
                    Value = "A:";
                    break;
                case "2":
                    Value = "B:";
                    break;
                case "4":
                    Value = "C:";
                    break;
                case "8":
                    Value = "D:";
                    break;
                case "10":
                    Value = "E:";
                    break;
                case "20":
                    Value = "F:";
                    break;
                case "40":
                    Value = "G:";
                    break;
                case "80":
                    Value = "H:";
                    break;
                case "100":
                    Value = "I:";
                    break;
                case "200":
                    Value = "J:";
                    break;
                case "400":
                    Value = "K:";
                    break;
                case "800":
                    Value = "L:";
                    break;
                case "1000":
                    Value = "M:";
                    break;
                case "2000":
                    Value = "N:";
                    break;
                case "4000":
                    Value = "O:";
                    break;
                case "8000":
                    Value = "P:";
                    break;
                case "10000":
                    Value = "Q:";
                    break;
                case "20000":
                    Value = "R:";
                    break;
                case "40000":
                    Value = "S:";
                    break;
                case "80000":
                    Value = "T:";
                    break;
                case "100000":
                    Value = "U:";
                    break;
                case "200000":
                    Value = "V:";
                    break;
                case "400000":
                    Value = "W:";
                    break;
                case "800000":
                    Value = "X:";
                    break;
                case "1000000":
                    Value = "Y:";
                    break;
                case "2000000":
                    Value = "Z:";
                    break;
                default: break;
            }
            return Value;
        }
        #endregion

        #region 树相关

        //更新USB设备列表
        private void UpdateUsbTree()
        {
            treeView_File.Nodes.Clear();
            Dev_listSerial_Map.Clear();
            m_CheckNode_List.Clear();

            nSelStartindex = 0;
            IniCurveData(ref m_SubCurveDatas);
            IniCurveData(ref m_CurveDatas);

            DrawEmpty();  //绘制空

            //添加根节点
            TreeNode treeNode0 = new TreeNode();
            treeNode0.Tag = 0;//把自己的id存放在该节点tag对象里
            treeNode0.Text = "检查设备";
            treeView_File.Nodes.Add(treeNode0);
            treeView_File.SelectedNode = treeNode0;
            treeView_File.CheckBoxes = true;
            HideCheckBox(this.treeView_File, treeNode0);


            TreeNode treeNodelishi0 = new TreeNode();
            treeNodelishi0.Tag = 0;//把自己的id存放在该节点tag对象里
            treeNodelishi0.Text = "历史数据";
            bool islishi = false;

            int iv = 100;
            foreach (string strUsb in m_ListUSBDevs)
            {
                if (strUsb != "")
                {
                    if ( treeView_File.SelectedNode != null)
                    {
                        if (strUsb.Length > 10) //历史数据
                        {
                            if (!islishi)
                            {
                                islishi = true;

                                treeView_File.SelectedNode = null;
                                treeView_File.Nodes.Add(treeNodelishi0);
                                treeView_File.SelectedNode = treeNodelishi0;
                                treeView_File.CheckBoxes = true;
                                HideCheckBox(this.treeView_File, treeNodelishi0);

                            }
                        }
                        List<string> listSerial = UpdateCheckSerialList(strUsb);
                        if (listSerial.Count > 0)  //只显示有检查文件夹的盘符
                        {
                            //创建一个节点对象，并初始化 
                            TreeNode treeNode1 = new TreeNode();

                            //在TreeView组件中加入子节点 
                            treeNode1.Tag = iv;//把自己的id存放在该节点tag对象里
                            treeNode1.Text = strUsb.ToString().Trim();
                            treeView_File.SelectedNode.Nodes.Add(treeNode1);
                            treeView_File.SelectedNode = treeNode1;
                            HideCheckBox(this.treeView_File, treeNode1);

                            //----------------------------------
                            //一个设备下的文件展开
                            Update_Dev_listSerial_Map(treeNode1, strUsb.ToString());

                            //----------------------------------
                            if (islishi)
                                treeView_File.SelectedNode = treeNodelishi0;
                            else
                                treeView_File.SelectedNode = treeNode0;

                            //treeView_File.ExpandAll();
                            iv++;
                        }
                    }
                    



                }
            }

        }

        //1, 盘符对应这个盘下的检查文件夹
        private void Update_Dev_listSerial_Map(TreeNode parentNode,  string strDev)  // G:\
        {
            if (strDev == "")
            {
                return;
            }

            List<string> listSerial = UpdateCheckSerialList(strDev);
            if(listSerial.Count > 0)
            {
                //Dev_listSerial_Map.Add(strDev, listSerial);

                int iv = 200;
                foreach (string strSerial in listSerial)
                { 
                    //----------------------------------
                    //创建一个节点对象，并初始化 
                    TreeNode treeNode2 = new TreeNode();

                    //在TreeView组件中加入子节点 
                    treeNode2.Tag = iv + @"," + strDev + strSerial;//把自己的id存放在该节点tag对象里
                    treeNode2.Text = strSerial.ToString().Trim();
                    treeView_File.SelectedNode.Nodes.Add(treeNode2);
                    treeView_File.SelectedNode = treeNode2;
                    HideCheckBox(this.treeView_File, treeNode2);

                    //----------------------------------

                    StruCheckFileInfo tempCheckFileInfo = new StruCheckFileInfo
                    {
                        isLoad = false,
                        m_StruOneDayFileInfo = new List<StruOneDayFileInfo>()
                    };

                    Update_Serial_Txt_SubTree(treeNode2, strDev + strSerial + @"\",ref tempCheckFileInfo);  // txt 文件路径列表 G:\1508491901\

                    if (!m_checkNum_Files_map.ContainsKey(strSerial))
                        m_checkNum_Files_map[strSerial] = tempCheckFileInfo;
                    
                    //----------------------------------

                    treeView_File.SelectedNode = parentNode;

                    iv++;
                    //----------------------------------
                }

                treeView_File.Nodes[0].Expand();
            }
            
        }

        //3 更新序列号下面的文件子树
        private void Update_Serial_Txt_SubTree(TreeNode parentNode, string strSerialPath, ref StruCheckFileInfo tempCheckFileInfo)  // strSerialPath = G:\1508491901\ 
        {
            if (strSerialPath == "")
            {
                return;
            }

            List<string> m_ListTxtFiles = UpdateTxtFileList(strSerialPath);  // txt 文件列表  

            if (m_ListTxtFiles.Count > 0)
            {  
                int ii3 = 300;
                foreach (string strtxtfile in m_ListTxtFiles)  //TXT文件 
                {
                    //--------------------------------------------
                    //创建一个节点对象，并初始化 
                    TreeNode treeNode3 = new TreeNode();

                    int ipos = strtxtfile.LastIndexOf(@"\");
                    string strfile = strtxtfile.Substring(ipos + 1);  // ID2017-07-01.txt

                    //在TreeView组件中加入子节点 
                    treeNode3.Tag = ii3 + @"," + strSerialPath + @"info\" + strfile.Substring(0, strfile.Length - 4);  //01,G:\3384328829\info\ID2017-06-29
                    treeNode3.Text = strfile.Substring(0, strfile.Length - 4);  //ID2017-07-01
                    treeView_File.SelectedNode.Nodes.Add(treeNode3);
                    treeView_File.SelectedNode = treeNode3;
                    HideCheckBox(this.treeView_File, treeNode3);

                    //--------------------------------------------

                    StruOneDayFileInfo tempOneDayFileInfo = new StruOneDayFileInfo
                    {
                        strTxtFile = string.Empty,
                        m_StruFileInfo = new List<StruFileInfo>()
                         
                    };
                    tempOneDayFileInfo.strTxtFile =  strtxtfile;

                    Update_Date_TimeFile_SubTree(treeNode3, strtxtfile, ref tempOneDayFileInfo);

                    tempCheckFileInfo.m_StruOneDayFileInfo.Add(tempOneDayFileInfo);
                    //--------------------------------------------

                    ii3++;
                    treeView_File.SelectedNode = parentNode;
                }
            }
        }

        // 5 ,更新序列号下面的文件子树
        private void Update_Date_TimeFile_SubTree(TreeNode parentNode, string strTXTPath, ref StruOneDayFileInfo tempCheckFileInfo)  // strDev = G:\1508491901\ID2017-07-01.txt 
        {
            if (strTXTPath == "")
            {
                return;
            }

            //读取TXT文件
            List<Struct_Txt_Data> m_checkDatas = ReadTxtFile(strTXTPath);
            if (m_checkDatas.Count > 0)
            {
                int ii4 = 400;
                foreach (Struct_Txt_Data Stru_Txt_Data in m_checkDatas)  //txt 数据内容
                {

                    //--------------------------------------------
                    int ipos1 = strTXTPath.LastIndexOf(@"\");
                    string strPath1 = strTXTPath.Substring(0, ipos1 + 1);  // G:\1508491901\
                    string tempPath1 = strPath1 + @"info\" + Stru_Txt_Data.strCheckDate + @"\" + Stru_Txt_Data.strCheckDate + " " + Stru_Txt_Data.strCheckTime;

                    StruFileInfo m_tempFileInfo = new StruFileInfo();
                    m_tempFileInfo.strFileMode = Stru_Txt_Data.strCheckMode;
                    m_tempFileInfo.m_filePath = tempPath1;
                    tempCheckFileInfo.m_StruFileInfo.Add(m_tempFileInfo);
                    //--------------------------------------------

                    if (m_SelMode != -1 )
                    {
                        if (Stru_Txt_Data.strCheckMode == "")  //没类型，不显示
                            continue; 
                        if (Stru_Txt_Data.strCheckMode.Substring(0, 1) != m_SelMode.ToString())
                            continue;
                    }

                    int ipos = strTXTPath.LastIndexOf(@"\");
                    string strPath = strTXTPath.Substring(0,ipos+1);  // G:\1508491901\
                    //--------------------------------------------
                    //创建一个节点对象，并初始化 
                    TreeNode treeNode4 = new TreeNode();

                    //在TreeView组件中加入子节点
                    string tempPath =  strPath + @"info\" + Stru_Txt_Data.strCheckDate + @"\" + Stru_Txt_Data.strCheckDate + " " + Stru_Txt_Data.strCheckTime ;

                    treeNode4.Tag = ii4 + @"," +tempPath; //400,G:\3384328829\info\ID2017-06-29\ID2017-06-29 10.27.23
                    treeNode4.Text = @"[" + Stru_Txt_Data.strCheckMode + @"] " + Stru_Txt_Data.strCheckTime;
                     
                     
                    //if (m_CheckNode_List.Count == 1)
                    //{
                    //    m_CheckNode_List.Add(treeNode4);
                    //    treeNode4.Checked = true;
                    //}

                    treeView_File.SelectedNode.Nodes.Add(treeNode4);

                    ii4++;
                    //--------------------------------------------


                    //--------------------------------------------
                    ////按文件量
                    //string strDataFolder = strDev + strSerial + @"\info\" + Stru_Txt_Data.strCheckDate + @"\";
                    //List<string> m_strDataFils = UpdateDataFileList(strDataFolder, Stru_Txt_Data);
                    //int ii4 = 400;
                    //foreach (string strOneFile in m_strDataFils)  //data 文件名
                    //{

                    //    //--------------------------------------------
                    //    //创建一个节点对象，并初始化 
                    //    TreeNode treeNode4 = new TreeNode();

                    //    //在TreeView组件中加入子节点 
                    //    int nindex = strOneFile.IndexOf(" ");

                    //    treeNode4.Tag = ii4 + @"," + strDataFolder + strOneFile;//把自己的id存放在该节点tag对象里
                    //    treeNode4.Text = @"[" + Stru_Txt_Data.strCheckMode + @"] " + strOneFile.Substring(nindex + 1).ToString().Trim();
                    //    treeView_File.SelectedNode.Nodes.Add(treeNode4);

                    //    ii4++;
                    //    //--------------------------------------------
                    //}
                    //--------------------------------------------

                }
            }
        }

        

        //2 获取盘符下的检测编号列表,返回检查号列表：1234567890，1122334455
        private List<string>  UpdateCheckSerialList(string strPath)  //  G:\
        {

            List<string> listSerial = new List<string>();

            DirectoryInfo theFolder = new DirectoryInfo(strPath);
            DirectoryInfo[] dirInfo = theFolder.GetDirectories();

            //遍历文件夹
            int i = 0;
            foreach (DirectoryInfo NextFolder in dirInfo)
            {
                if (NextFolder.Name != null && NextFolder.Name!="")
                {
                    if (NextFolder.Name.Length == 10)
                    {
                        string strinfoPath = strPath + NextFolder.Name + @"\info";
                        if (Directory.Exists(strinfoPath))
                        { 
                            listSerial.Insert(0, NextFolder.Name);
                        }
                    }
                    i++;
                }

            }

            return listSerial;
        }

        //4 获取TXT文件列表
        private List<string> UpdateTxtFileList(string strPath)  //G:\1508491901\
        {
            if (strPath == "")
            {
                return (new List<string>());
            }

            List<string> listTxtFile = new List<string>();

            DirectoryInfo theFolder = new DirectoryInfo(strPath);

            FileInfo[] fileInfo = theFolder.GetFiles();
            int i = 0;
            foreach (FileInfo NextFile in fileInfo)  //遍历文件
            {
                if (NextFile.Name != null && NextFile.Name.Contains(".txt") && NextFile.Length > 0)
                {
                    listTxtFile.Insert( 0, strPath + NextFile.Name);
                     
                }

            } 
            return listTxtFile;
        }

        //6,读取TXT文件内容
        private List<Struct_Txt_Data> ReadTxtFile(string path)
        {
            List<Struct_Txt_Data> m_listTxtDatas = new List<Struct_Txt_Data>();
            try
            {
                StreamReader sr = new StreamReader(path, Encoding.Default);
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] lineCodes = line.Split(' ');
                    Struct_Txt_Data structTxtData;
                    if (lineCodes.Count() >= 3)
                    {
                        structTxtData.strCheckDate = lineCodes[0].Trim();
                        structTxtData.strCheckTime = lineCodes[1].Trim();
                        structTxtData.strCheckMode = lineCodes[3].Trim();

                        m_listTxtDatas.Insert(0,structTxtData); 
                    }
                }
                sr.Close();
            }
            catch (System.IO.IOException ex)
            {
                return new List<Struct_Txt_Data>();
            }
            return m_listTxtDatas;
        }
        #endregion



        //获取Data文件列表
        private List<string> UpdateDataFileList(string strPath, string strsubFileName  )  //G:\1508491901\info\ID2017-06-29
        {
            List<string> listDataFile = new List<string>();

            DirectoryInfo theFolder = new DirectoryInfo(strPath);

            FileInfo[] fileInfo = theFolder.GetFiles();
            foreach (FileInfo NextFile in fileInfo)  //遍历文件
            {
                if (NextFile.Name != null  && NextFile.Length > 0)
                {
                    if (NextFile.Name.IndexOf(strsubFileName) >= 0) 
                    {
                        listDataFile.Add(strPath+   NextFile.Name); 

                    }
                    else if (listDataFile.Count> 0)
                    {
                        break;
                    }

                }

            }
            return listDataFile;
        }

        //6模式下，读取文件列表文件内容 ，多个文件合并到一条曲线中，
        private void ReadOneDataFile(List<string> File_List, ref CurveDatas m_TempCurveDatas)
        {
            if (File_List.Count < 1)
                return;

            int nTimeIndex = 0;  //时间点序号

            float fLastWeightVal = -1000f;
            int nufrIndex = 0;  //时间点序号

            foreach (string strTempfile in File_List)
            {
                BinaryReader br;
                // 读取文件
                try
                {
                    br = new BinaryReader(new FileStream(strTempfile, FileMode.Open));
                }
                catch (IOException e1)
                {
                    MessageBox.Show("读取文件失败 " + strTempfile);
                    return;
                }
                try
                { 
                    while (true)
                    {
                        //---------------------------------
                        byte[] byteWeight = br.ReadBytes(2);  //高低位需要反过来 
                        if (byteWeight.Count() < 1)
                            break;   //读取结束
                        int nWeight = Convert.ToInt16((byteWeight[1].ToString("X2") + byteWeight[0].ToString("X2")), 16);  //br.ReadInt16()

                        byte[] bytePves = br.ReadBytes(2);
                        int nPves = Convert.ToInt16((bytePves[1].ToString("X2") + bytePves[0].ToString("X2")), 16);  //br.ReadInt16()

                        byte[] bytePabd = br.ReadBytes(2);
                        int nPabd = Convert.ToInt16((bytePabd[1].ToString("X2") + bytePabd[0].ToString("X2")), 16);  //br.ReadInt16()

                        br.ReadInt16();  //-1 表示无效ff

                        //---------------------------------
                        StruData WeightData;
                        WeightData.value = nWeight / 10; 
                        if (m_TempCurveDatas.fmax_Wight < WeightData.value)
                        {
                            m_TempCurveDatas.fmax_Wight = WeightData.value;
                        }
                        WeightData.time = nTimeIndex;
                        WeightData.isShow = false;
                        m_TempCurveDatas.list_Wights.Add(WeightData);

                        if (nTimeIndex % 2 == 0)
                        {
                            if (fLastWeightVal == -1000f)
                            {
                                fLastWeightVal = WeightData.value;

                            }
                            else
                            {
                                StruData ufrData;
                                ufrData.value =System.Math.Abs( WeightData.value - fLastWeightVal) ;
                                ufrData.time = nufrIndex;
                                ufrData.isShow = false;
                                m_TempCurveDatas.list_ufr.Add(ufrData);

                                if (m_TempCurveDatas.fmax_ufr < ufrData.value)
                                {
                                    m_TempCurveDatas.fmax_ufr = ufrData.value;
                                }
                                fLastWeightVal = WeightData.value;
                                nufrIndex++;
                            } 
                        }
                        



                        StruData PvesData;
                        PvesData.value = nPves;

                        if (m_TempCurveDatas.fmax_Pves < PvesData.value)
                        {
                            m_TempCurveDatas.fmax_Pves = PvesData.value;
                        }

                        PvesData.time = nTimeIndex;
                        PvesData.isShow = false;
                        m_TempCurveDatas.list_Pves.Add(PvesData);

                        StruData PabdData;
                        PabdData.value = nPabd ;

                        if (m_TempCurveDatas.fmax_Pabd < PabdData.value)
                        {
                            m_TempCurveDatas.fmax_Pabd = PabdData.value;
                        }
                        PabdData.time = nTimeIndex;
                        PabdData.isShow = false;
                        m_TempCurveDatas.list_Pabd.Add(PabdData);

                        StruData PdetData;
                        PdetData.value = PvesData.value - PabdData.value;

                        if (m_TempCurveDatas.fmax_Pdet < PdetData.value)
                        {
                            m_TempCurveDatas.fmax_Pdet = PdetData.value;
                        }

                        PdetData.time = nTimeIndex;
                        PdetData.isShow = false;
                        m_TempCurveDatas.list_Pdet.Add(PdetData);


                        //---------------------------------

                        nTimeIndex++;
                    }

                    if (m_TempCurveDatas.FirstFileEndIndex <0)
                        m_TempCurveDatas.FirstFileEndIndex = nTimeIndex;
                }
                catch (EndOfStreamException e2)
                {
                    //MessageBox.Show("读取结束 " + strTempfile);

                    br.Close(); 
                  
                    continue;
                }
                br.Close();
            }

            m_TempCurveDatas.endTime = m_TempCurveDatas.StartTime.AddMilliseconds(m_TempCurveDatas.list_Pdet.Count() * 500);

            //if (m_TempCurveDatas.list_Pdet.Count() > 0 && m_TempCurveDatas.list_Pdet.Count() % 2 == 0)
            //    m_TempCurveDatas.endTime = m_TempCurveDatas.StartTime.AddSeconds(m_TempCurveDatas.list_Pdet.Count() / 2);
            //else if (m_TempCurveDatas.list_Pdet.Count() > 0)
            //    m_TempCurveDatas.endTime = m_TempCurveDatas.StartTime.AddSeconds((m_TempCurveDatas.list_Pdet.Count() + 1) / 2);


            m_SubCurveDatas.StartTime = m_TempCurveDatas.StartTime;
            m_SubCurveDatas.endTime = m_TempCurveDatas.endTime;
        }


        // 读取文件列表文件内容 ，多个文件合并到一条曲线中，
        public void ReadFiles(List<string> File_List, ref CurveDatas m_TempCurveDatas, int nstartIndex, int nendIndex )
        {
            if (File_List.Count < 1)
                return;

            int nTimeIndex = 0;  //时间点序号

            float fLastWeightVal = -1000f;
            int nufrIndex = 0;  //时间点序号

            foreach (string strTempfile in File_List)
            {
                BinaryReader br;
                // 读取文件
                try
                {
                    br = new BinaryReader(new FileStream(strTempfile, FileMode.Open));
                }
                catch (IOException e1)
                {
                    MessageBox.Show("读取文件失败 " + strTempfile);
                    return;
                }
                try
                {
                    while (true)
                    {
                        //---------------------------------
                        byte[] byteWeight = br.ReadBytes(2);  //高低位需要反过来 
                        if (byteWeight.Count() < 1)
                            break;   //读取结束
                        int nWeight = Convert.ToInt16((byteWeight[1].ToString("X2") + byteWeight[0].ToString("X2")), 16);  //br.ReadInt16()

                        byte[] bytePves = br.ReadBytes(2);
                        int nPves = Convert.ToInt16((bytePves[1].ToString("X2") + bytePves[0].ToString("X2")), 16);  //br.ReadInt16()

                        byte[] bytePabd = br.ReadBytes(2);
                        int nPabd = Convert.ToInt16((bytePabd[1].ToString("X2") + bytePabd[0].ToString("X2")), 16);  //br.ReadInt16()

                        br.ReadInt16();  //-1 表示无效ff

                        //---------------------------------
                        StruData WeightData;
                        WeightData.value = nWeight / 10;
                        
                        WeightData.time = nTimeIndex;
                        WeightData.isShow = false;
                        if (nTimeIndex >= nstartIndex && nTimeIndex <= nendIndex)
                        {
                            m_TempCurveDatas.list_Wights.Add(WeightData);
                            if (m_TempCurveDatas.fmax_Wight < WeightData.value)
                            {
                                m_TempCurveDatas.fmax_Wight = WeightData.value;
                            }
                        }

                        if (nTimeIndex % 2 == 0)
                        {
                            if (fLastWeightVal == -1000f)
                            {
                                fLastWeightVal = WeightData.value;

                            }
                            else
                            {
                                StruData ufrData;
                                ufrData.value = System.Math.Abs(WeightData.value - fLastWeightVal);
                                ufrData.time = nufrIndex;
                                ufrData.isShow = false;
                                if (nTimeIndex >= nstartIndex && nTimeIndex <= nendIndex)
                                {
                                    m_TempCurveDatas.list_ufr.Add(ufrData);

                                    if (m_TempCurveDatas.fmax_ufr < ufrData.value)
                                    {
                                        m_TempCurveDatas.fmax_ufr = ufrData.value;
                                    }
                                }
                                fLastWeightVal = WeightData.value;
                                nufrIndex++;
                            }
                        }




                        StruData PvesData;
                        PvesData.value = nPves;
                         
                        PvesData.time = nTimeIndex;
                        PvesData.isShow = false;
                        if (nTimeIndex >= nstartIndex && nTimeIndex <= nendIndex)
                        {
                            m_TempCurveDatas.list_Pves.Add(PvesData);
                            if (m_TempCurveDatas.fmax_Pves < PvesData.value)
                            {
                                m_TempCurveDatas.fmax_Pves = PvesData.value;
                            }
                        }

                        StruData PabdData;
                        PabdData.value = nPabd; 
                        PabdData.time = nTimeIndex;
                        PabdData.isShow = false;
                        if (nTimeIndex >= nstartIndex && nTimeIndex <= nendIndex)
                        {
                            m_TempCurveDatas.list_Pabd.Add(PabdData);
                            if (m_TempCurveDatas.fmax_Pabd < PabdData.value)
                            {
                                m_TempCurveDatas.fmax_Pabd = PabdData.value;
                            }
                        }

                        StruData PdetData;
                        PdetData.value = PvesData.value - PabdData.value;
                         
                        PdetData.time = nTimeIndex;
                        PdetData.isShow = false;
                        if (nTimeIndex >= nstartIndex && nTimeIndex <= nendIndex)
                        {
                            m_TempCurveDatas.list_Pdet.Add(PdetData);
                            if (m_TempCurveDatas.fmax_Pdet < PdetData.value)
                            {
                                m_TempCurveDatas.fmax_Pdet = PdetData.value;
                            }
                        }


                        //---------------------------------

                        nTimeIndex++;
                    }

                    if (m_TempCurveDatas.FirstFileEndIndex < 0)
                        m_TempCurveDatas.FirstFileEndIndex = nTimeIndex;
                }
                catch (EndOfStreamException e2)
                {
                    //MessageBox.Show("读取结束 " + strTempfile);

                    br.Close();

                    continue;
                }
                br.Close();
            }

            m_TempCurveDatas.endTime = m_TempCurveDatas.StartTime.AddMilliseconds(m_TempCurveDatas.list_Pdet.Count() * 500);

            //if (m_TempCurveDatas.list_Pdet.Count() > 0 && m_TempCurveDatas.list_Pdet.Count() % 2 == 0)
            //    m_TempCurveDatas.endTime = m_TempCurveDatas.StartTime.AddSeconds(m_TempCurveDatas.list_Pdet.Count() / 2);
            //else if (m_TempCurveDatas.list_Pdet.Count() > 0)
            //    m_TempCurveDatas.endTime = m_TempCurveDatas.StartTime.AddSeconds((m_TempCurveDatas.list_Pdet.Count() + 1) / 2);
             
        }


        //画曲线
        private void DrawCurve(CurveDatas oneDataManage,int curX = -1)
        {
            if (oneDataManage.list_Pabd.Count < 1)
                return;


            //看看需要画那几条类型的数据曲线
            string[] checkModes = new string[5];   //记录显示类型的序号
            int nCurveCnt = 0;  //前3条曲线
            int nCurveWeight = 0;  //尿量，尿流率曲线
            int nCurveTotal = 0;
            if (checkBox_Pves.Checked)
            { 
                m_CurveDatas.showMode[(int)CuvrlMode.Pves] = 1;

                checkModes[nCurveCnt] = "Pves";
                nCurveCnt++;
            }
            else
            {
                m_CurveDatas.showMode[(int)CuvrlMode.Pves] = 0; 
            }

            if (checkBox_Pabd.Checked)
            {
                m_CurveDatas.showMode[(int)CuvrlMode.Pabd] = 1;
                 
                checkModes[nCurveCnt] = "Pabd";
                nCurveCnt++;
            }
            else
            {
                m_CurveDatas.showMode[(int)CuvrlMode.Pabd] = 0; 
            }

            if (checkBox_Pdet.Checked)
            {
                m_CurveDatas.showMode[(int)CuvrlMode.Pdet] = 1;
               
                checkModes[nCurveCnt] = "Pdet";
                nCurveCnt++; 
            }
            else
            {
                m_CurveDatas.showMode[(int)CuvrlMode.Pdet] = 0;  
            }

            nCurveTotal = nCurveCnt;
            if (checkBox_nl.Checked)
            {
                m_CurveDatas.showMode[(int)CuvrlMode.Wight] = 1;
                checkModes[nCurveTotal] = "尿量";

                nCurveTotal++;
            }
            else
            {
                m_CurveDatas.showMode[(int)CuvrlMode.Wight] = 0;
            }

            if (checkBox_nll.Checked)
            {
                m_CurveDatas.showMode[(int)CuvrlMode.ufr] = 1;
                checkModes[nCurveTotal] = "尿流率";
                nCurveTotal++;
            }
            else
            {
                m_CurveDatas.showMode[(int)CuvrlMode.ufr] = 0;
            }
            

            Rectangle clientR = this.panel_Draw.ClientRectangle;

            Rectangle m_DrawArea = new Rectangle();  //绘画区域, 含行标题,及其尿流率图
            //m_DrawArea.Location = new Point((int)(clientR.Left + clientR.Width * 0.05), (int)(clientR.Top + clientR.Height * 0.05));
            //m_DrawArea.Size = new Size((int)(clientR.Width - clientR.Width * 0.1), (int)(clientR.Height - clientR.Height * 0.1));
            m_DrawArea.Location = new Point((int)(clientR.Left + 40), (int)(clientR.Top + 40));
            m_DrawArea.Size = new Size((int)(clientR.Width - 80), (int)(clientR.Height - 60));

            m_CurCurveArea.Location = new Point(0,0);
            m_CurCurveArea.Size = new Size(0,0);

            nCurveWeight = nCurveTotal - nCurveCnt;

            if (nCurveTotal > 0)
            {

                // 画横标尺 
                //DrawFuns m_DrawFuns = new DrawFuns();
                //m_DrawFuns.IniDraw(ref panel_Draw);
                m_DrawFuns.ClearDC();
                if (m_CurSelCurveArea.Width > 0)  //已经选择
                {
                    m_DrawFuns.FillRectangleBG(Color.Gainsboro, m_CurSelCurveArea);
                }
                int iniH = m_DrawArea.Top;

                //-------------------------------------------------
                {
                    //绘制标题
                    Rectangle tempRC = new Rectangle();
                    tempRC.Location = new Point((int)(m_DrawArea.Left), iniH + 6);
                    tempRC.Size = new Size(m_DrawArea.Width, 30);
                    m_DrawFuns.DrawPrintOneString(oneDataManage.StartTime.ToString() + " 至 " + oneDataManage.endTime.ToString(), 10, StringAlignment.Center, tempRC,true);

                    iniH += 30;
                    m_DrawArea.Location = new Point((int)(m_DrawArea.Left), iniH);
                    m_DrawArea.Size = new Size((int)(m_DrawArea.Width), (int)(m_DrawArea.Height - 30));

                    iniH = m_DrawArea.Top;


                }
                //-------------------------------------------------
                int stepH = 0;

                nCurveCnt = nCurveTotal;  //现在5条曲线一起画了
                if (nCurveCnt > 0)  //前3条曲线
                {

                    stepH = (int)((int)(m_DrawArea.Size.Height  / nCurveCnt) * 6) / 7;
                    m_DrawFuns.plotLine2( new Point[] { new Point(m_DrawArea.Left, iniH), new Point(m_DrawArea.Right, iniH) });

                    int ntitleWitch = 120;

                    m_CurCurveArea.Location = new Point(m_DrawArea.Left + ntitleWitch, iniH);
                    m_CurCurveArea.Size = new Size(m_DrawArea.Width - ntitleWitch, stepH * nCurveCnt);

                    for (int i = 0; i < nCurveCnt; i++)
                    {
                        iniH += stepH;
                        if (iniH > clientR.Bottom)
                            iniH = clientR.Bottom;
                        m_DrawFuns.plotLine2( new Point[] { new Point(m_DrawArea.Left, iniH), new Point(m_DrawArea.Right, iniH) });

                    }

                   
                    // 画纵标尺
                    m_DrawFuns.plotLine2( new Point[] { new Point(m_DrawArea.Left, m_DrawArea.Top), new Point(m_DrawArea.Left, iniH) });
                    m_DrawFuns.plotLine2( new Point[] { new Point(m_DrawArea.Left + ntitleWitch, m_DrawArea.Top), new Point(m_DrawArea.Left + ntitleWitch, iniH) });
                    m_DrawFuns.plotLine2( new Point[] { new Point(m_DrawArea.Right, m_DrawArea.Top), new Point(m_DrawArea.Right, iniH) });

                    //----------------------------------------------------------
                    // 画纵标尺 虚线 5端
                    int ptCnt = oneDataManage.list_Pabd.Count;
                    int nSecond = (ptCnt+1) / 2; //秒数
                    int nMinute = nSecond / 60; //分钟数

                    int nduan = 5;  //分几个区间
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

                    if (curX != -1)
                        m_DrawFuns.plotLine2(new Point[] { new Point(curX, m_DrawArea.Top), new Point(curX, iniH) });
                    

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
                            strtitle = (iv + 1) * nStepSecond + "秒";
                        else
                        {
                            strtitle = ((iv + 1) * nStepSecond) / 60 + "分";
                            strtitle += ((iv + 1) * nStepSecond) % 60 + "秒";
                        }

                        if ((iv + 1) == nduan)
                        {
                            tempRC.Location = new Point((int)(m_DrawArea.Left + ntitleWitch + (iv + 1) * stepW - 60), iniH + 6);
                        }
                        else
                        {
                            tempRC.Location = new Point((int)(m_DrawArea.Left + ntitleWitch + (iv + 1) * stepW - 60), iniH + 6);
                        }
                        m_DrawFuns.DrawPrintOneString(strtitle, 10, StringAlignment.Center, tempRC,false);

                    }

                    Pves_X_Value_Map.Clear();
                    Pabd_X_Value_Map.Clear();
                    Pdet_X_Value_Map.Clear();
                    Wight_X_Value_Map.Clear();
                    Ufr_X_Value_Map.Clear();
                    //----------------------------------------------------------

                    int nPosx = -1;  //分隔线
                    for (int ii = 0; ii < nCurveCnt; ii++)
                    {
                        Rectangle m_oneDrawArea = m_DrawArea;  //绘画区域
                        m_oneDrawArea.Location = new Point(m_DrawArea.Left + ntitleWitch, m_DrawArea.Top + ii * stepH);
                        m_oneDrawArea.Width = m_DrawArea.Width - ntitleWitch;
                        m_oneDrawArea.Height = stepH;

                        Rectangle m_onetitleRect = m_DrawArea;
                        m_onetitleRect.Location = new Point(m_DrawArea.Left, m_DrawArea.Top + ii * stepH);
                        m_onetitleRect.Width = ntitleWitch;
                        m_onetitleRect.Height = stepH;

                        if (checkModes[ii] == "Pves")
                        {
                            Size m_range = curve3_Range;  //范围：（最小值 ，最大值）

                             
                            //画行标题
                            m_DrawFuns.DrawRowtitle("Pves", ValueUnit, oneDataManage.fmax_Pves.ToString(), m_range, m_onetitleRect, Color.Blue);

                            //画曲线
                            nPosx = m_DrawFuns.plotLine3(ref oneDataManage.list_Pves, m_range, m_oneDrawArea, Color.Blue, ref Pves_X_Value_Map, oneDataManage.FirstFileEndIndex);
                        }
                        else if (checkModes[ii] == "Pabd")
                        {
                            Size m_range = curve3_Range;  //范围：（最小值 ，最大值）
                            //画行标题
                            m_DrawFuns.DrawRowtitle("Pabd", ValueUnit, oneDataManage.fmax_Pabd.ToString(), m_range, m_onetitleRect, Color.DarkViolet);

                            //画曲线
                            nPosx = m_DrawFuns.plotLine3(ref oneDataManage.list_Pabd, m_range, m_oneDrawArea, Color.DarkViolet, ref Pabd_X_Value_Map, oneDataManage.FirstFileEndIndex);
                        }
                        else if (checkModes[ii] == "Pdet")
                        {
                            Size m_range = curve3_Range;  //范围：（最小值 ，最大值）
                            
                            //画行标题
                            m_DrawFuns.DrawRowtitle("Pdet", ValueUnit, oneDataManage.fmax_Pdet.ToString(), m_range, m_onetitleRect, Color.Green);

                            //画曲线
                            nPosx = m_DrawFuns.plotLine3(ref oneDataManage.list_Pdet, m_range, m_oneDrawArea, Color.Green, ref Pdet_X_Value_Map, oneDataManage.FirstFileEndIndex);
                        }
                        else if (checkModes[ii] == "尿量")
                        {
                            Size m_range = nl_Range;  //范围：（最小值 ，最大值）

                            //画行标题
                            m_DrawFuns.DrawRowtitle("尿量", "ml", oneDataManage.fmax_Wight.ToString(), m_range, m_onetitleRect, Color.DeepSkyBlue);

                            //画曲线
                            nPosx = m_DrawFuns.plotLine3(ref oneDataManage.list_Wights, m_range, m_oneDrawArea, Color.DeepSkyBlue, ref Wight_X_Value_Map, oneDataManage.FirstFileEndIndex);
                       
                        }
                        else if (checkModes[ii] == "尿流率")
                        {
                            Size m_range = nll_Range;  //范围：（最小值 ，最大值）
                           
                            //画行标题
                            m_DrawFuns.DrawRowtitle("尿流率", "ml/s", oneDataManage.fmax_ufr.ToString(), m_range, m_onetitleRect, Color.DarkOrange);

                            //画曲线
                            nPosx = m_DrawFuns.plotLine3(ref oneDataManage.list_ufr, m_range, m_oneDrawArea, Color.DarkOrange, ref Ufr_X_Value_Map, (int)(oneDataManage.FirstFileEndIndex / 2));
                       
                        }
                    }

                    //绘制分隔线
                    if (nPosx > -1)
                    { 
                        m_DrawFuns.plotLine2(new Point[] { new Point(nPosx, m_DrawArea.Top), new Point(nPosx, m_DrawArea.Top + nCurveCnt * stepH) }, false, new Pen(Color.FromArgb(20, 72, 40)));

                    }
                }

                if (1 == 0)  //用于屏蔽代码
                {
                    //--------------------------------------------------------
                    //绘制尿流率，尿量
                    if (nCurveWeight > 0)
                    {
                        stepH = (int)(m_DrawArea.Size.Height / 3);

                        m_DrawArea.Location = new Point(m_DrawArea.Left, iniH + 40);
                        m_DrawArea.Size = new Size(m_DrawArea.Width, (int)stepH - 80);
                        if (checkBox_nl.Checked)  //尿量
                        {
                            Rectangle m_oneDrawArea = m_DrawArea;  //绘画区域 
                            m_oneDrawArea.Location = new Point((int)m_DrawArea.Left, m_DrawArea.Top);
                            m_oneDrawArea.Width = (int)(m_DrawArea.Width / 2 - 20);
                            m_oneDrawArea.Height = (int)(m_DrawArea.Height);

                            //绘制纵坐标
                            m_DrawFuns.plotLine2(new Point[] { new Point(m_oneDrawArea.Left, m_oneDrawArea.Top), new Point(m_oneDrawArea.Left, m_oneDrawArea.Bottom) });

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

                            m_DrawFuns.plotLine2(new Point[] { new Point(m_oneDrawArea.Left, m_oneDrawArea.Bottom), new Point(m_oneDrawArea.Right, m_oneDrawArea.Bottom) });

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
                            int ptCnt = oneDataManage.list_Wights.Count;
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
                                    strtitle = (k) * nStepSecond + "秒";
                                else
                                {
                                    strtitle = ((k) * nStepSecond) / 60 + @"分";
                                    strtitle += ((k) * nStepSecond) % 60 + "秒";
                                }
                                tempRC.Location = new Point(m_oneDrawArea.Left + k * hStep - 10, m_oneDrawArea.Bottom + 5);
                                tempRC.Size = new Size(120, 20);
                                //绘制刻度值
                                m_DrawFuns.DrawPrintOneString(strtitle, 10, StringAlignment.Center, tempRC, true);

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
                                m_DrawFuns.DrawPrintOneString((Vvalue + k * 500).ToString(), 10, StringAlignment.Center, tempRC, true);

                            }

                            //-------------------------------------------------

                            //画行标题
                            //m_DrawFuns.DrawRowtitle("Pdet", "cmH2O", oneDataManage.fmax_Pdet.ToString(), m_range, m_onetitleRect, Color.Green);

                            Dictionary<int, Index_value> temp = new Dictionary<int, Index_value>();
                            //画曲线
                            m_DrawFuns.plotLine3(ref oneDataManage.list_Wights, m_range, m_oneDrawArea, Color.Black, ref temp);

                        }
                        if (checkBox_nll.Checked) //尿流率
                        {
                            Rectangle m_oneDrawArea = m_DrawArea;  //绘画区域 
                            m_oneDrawArea.Location = new Point((int)m_DrawArea.Left + (int)(m_DrawArea.Width / 2 - 10), m_DrawArea.Top);
                            m_oneDrawArea.Width = (int)(m_DrawArea.Width / 2 - 20);
                            m_oneDrawArea.Height = (int)(m_DrawArea.Height);


                            //绘制坐标
                            m_DrawFuns.plotLine2(new Point[] { new Point(m_oneDrawArea.Left, m_oneDrawArea.Top), new Point(m_oneDrawArea.Left, m_oneDrawArea.Bottom) });

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
                            m_DrawFuns.plotLine2(new Point[] { new Point(m_oneDrawArea.Left, m_oneDrawArea.Bottom), new Point(m_oneDrawArea.Right, m_oneDrawArea.Bottom) });

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
                            int ptCnt = oneDataManage.list_Wights.Count;
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
                                    strtitle = (k) * nStepSecond + "秒";
                                else
                                {
                                    strtitle = ((k) * nStepSecond) / 60 + @"分";
                                    strtitle += ((k) * nStepSecond) % 60 + "秒";
                                }
                                tempRC.Location = new Point(m_oneDrawArea.Left + k * hStep - 10, m_oneDrawArea.Bottom + 5);
                                tempRC.Size = new Size(120, 20);
                                //绘制刻度值
                                m_DrawFuns.DrawPrintOneString(strtitle, 10, StringAlignment.Center, tempRC, true);

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
                                m_DrawFuns.DrawPrintOneString((Vvalue + k * VvaluStep).ToString(), 10, StringAlignment.Center, tempRC, true);

                            }

                            //-------------------------------------------------

                            //画行标题
                            //m_DrawFuns.DrawRowtitle("Pdet", "cmH2O", oneDataManage.fmax_Pdet.ToString(), m_range, m_onetitleRect, Color.Green);

                            //画曲线
                            Dictionary<int, Index_value> temp = new Dictionary<int, Index_value>();
                            m_DrawFuns.plotLine3(ref oneDataManage.list_ufr, m_range, m_oneDrawArea, Color.Black, ref temp);
                        }
                    }
                }//用于屏蔽

                m_DrawFuns.UpdateDraw();
                panel_Draw.Refresh();

            }
            else
            {
                //清除曲线图
                DrawEmpty();
            }
        }

        private void DrawEmpty()  //清空画布
        {
            m_CurCurveArea.Location = new Point(0,0);
            m_CurCurveArea.Size = new Size(0,0);
            //DrawFuns m_DrawFuns = new DrawFuns(); 
            //m_DrawFuns.IniDraw(ref panel_Draw);
            m_DrawFuns.ClearDC();
            m_DrawFuns.UpdateDraw();
            panel_Draw.Refresh();
        }

        private void button_make_Report_Click(object sender, EventArgs e)
        {
            if (m_isSavePrintCurve)
            {
                DialogResult dr = MessageBox.Show("打印曲线已修改，是否保存修改", "保存", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                {
                    SaveCurveFunc();
                }
                else
                {
                    m_isSavePrintCurve = false;
                }
            }

            this.Hide();

            //TestDatas m_TestDatas = new TestDatas();
            //TestResultDlg m_TestResForm = new TestResultDlg(m_CurSelPatientInfo);
            //DialogResult dlgResult1 = m_TestResForm.ShowDialog();
            //if (dlgResult1 == DialogResult.OK)
            //{
            //    m_TestDatas = m_TestResForm.m_TestDatas;
            //}


            MakeReportForm m_reportForm = new MakeReportForm(m_CurSelPatientInfo, m_PrintCurveDatas, m_TestDatas, curve3_Range, nl_Range, nll_Range);
            DialogResult dlgResult = m_reportForm.ShowDialog();
            if (dlgResult == DialogResult.Cancel )
            {
                dlgResult = DialogResult.Cancel;
                Close();
                return;
            }
            
            this.Show(); //显示  
        }


        //全部
        private void All_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_SelMode = -1;

            comboBox_checkMode.SelectedIndex = m_SelMode + 1;
            //UpdateUsbTree();
        }
        private void ToolStripMenuItem0_Click(object sender, EventArgs e)
        {
            m_SelMode = 0;
            comboBox_checkMode.SelectedIndex = m_SelMode + 1;

            //UpdateUsbTree();

        }

        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            m_SelMode = 1;
            comboBox_checkMode.SelectedIndex = m_SelMode + 1;
            //UpdateUsbTree();
        }

        private void ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            m_SelMode = 2;
            comboBox_checkMode.SelectedIndex = m_SelMode + 1;
            //UpdateUsbTree();
        }

        private void ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            m_SelMode = 3;
            comboBox_checkMode.SelectedIndex = m_SelMode + 1;
            //UpdateUsbTree();
        }

        private void ToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            m_SelMode = 4;
            comboBox_checkMode.SelectedIndex = m_SelMode + 1;
            //UpdateUsbTree();
        }

        private void ToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            m_SelMode = 5;
            comboBox_checkMode.SelectedIndex = m_SelMode + 1;
            //UpdateUsbTree();
        }

        private void ToolStripMenuItem6_Click(object sender, EventArgs e)
        {
            m_SelMode = 6;
            comboBox_checkMode.SelectedIndex = m_SelMode + 1;
            //UpdateUsbTree();
        }

        private void button_Add_Report_Click(object sender, EventArgs e)
        {
            AddReport();

        }

        //部分复制
        private void CopyPartCurveData(ref CurveDatas m_TempCurveDatas, CurveDatas sourData,int nstartIndex, int nendIndex)
        {
            m_TempCurveDatas.StartTime = sourData.StartTime;
            m_TempCurveDatas.endTime = sourData.endTime;
 
            if(sourData.FirstFileEndIndex > nstartIndex && sourData.FirstFileEndIndex < nendIndex)
                m_TempCurveDatas.FirstFileEndIndex = sourData.FirstFileEndIndex-nstartIndex;
            else
                m_TempCurveDatas.FirstFileEndIndex = -1;

            //m_TempCurveDatas.strMeno = m_CurveDatas.strMeno;

            for (int i = 0; i < 5; i++)
            {
                m_TempCurveDatas.showMode[i] = sourData.showMode[i]; 
            }

            m_TempCurveDatas.fmax_Wight = -100;
            m_TempCurveDatas.fmax_Pves = -100;
            m_TempCurveDatas.fmax_Pabd = -100;
            m_TempCurveDatas.fmax_Pdet = -100;
            m_TempCurveDatas.fmax_ufr = -100;

            for(int tt = 0 ; tt<sourData.list_Files.Count ; tt++)
            {
                m_TempCurveDatas.list_Files.Add(sourData.list_Files[tt]);
            }
             

            for (int i = nstartIndex; i < nendIndex; i++)
            {
                m_TempCurveDatas.list_Pabd.Add(sourData.list_Pabd[i]);
                if(m_TempCurveDatas.fmax_Pabd<sourData.list_Pabd[i].value)
                    m_TempCurveDatas.fmax_Pabd = sourData.list_Pabd[i].value;

            }

            for (int i = nstartIndex; i < nendIndex; i++)
            {
                m_TempCurveDatas.list_Pdet.Add(sourData.list_Pdet[i]);
                if (m_TempCurveDatas.fmax_Pdet < sourData.list_Pdet[i].value)
                    m_TempCurveDatas.fmax_Pdet = sourData.list_Pdet[i].value;
            }

            for (int i = nstartIndex; i <= nendIndex; i++)
            {
                m_TempCurveDatas.list_Pves.Add(sourData.list_Pves[i]);
                if (m_TempCurveDatas.fmax_Pves < sourData.list_Pves[i].value)
                    m_TempCurveDatas.fmax_Pves = sourData.list_Pves[i].value;
            }

            int nufrIndex = 0;
            float fLastWeightVal = -1000f;
            for (int i = nstartIndex; i < nendIndex; i++)
            {
                m_TempCurveDatas.list_Wights.Add(sourData.list_Wights[i]);
                if (m_TempCurveDatas.fmax_Wight < sourData.list_Wights[i].value)
                    m_TempCurveDatas.fmax_Wight = sourData.list_Wights[i].value;

               
                if ( i % 2 == 0)
                {
                    if (fLastWeightVal == -1000f)
                    {
                        fLastWeightVal = sourData.list_Wights[i].value;

                    }
                    else
                    {
                        StruData ufrData;
                        ufrData.value = sourData.list_Wights[i].value - fLastWeightVal;
                        ufrData.time = nufrIndex;
                        ufrData.isShow = false;
                        m_TempCurveDatas.list_ufr.Add(ufrData);

                        if (m_TempCurveDatas.fmax_ufr < ufrData.value)
                        {
                            m_TempCurveDatas.fmax_ufr = ufrData.value;
                        }
                        fLastWeightVal = sourData.list_Wights[i].value;
                        nufrIndex++;
                    }
                    

                }
            }
             
        }


        //全部复制
        private void CopyCurveData(ref CurveDatas m_TempCurveDatas, CurveDatas sourData)
        {
            m_TempCurveDatas.StartTime = sourData.StartTime;
            m_TempCurveDatas.endTime = sourData.endTime;

            m_TempCurveDatas.strMeno = sourData.strMeno;
            m_TempCurveDatas.FirstFileEndIndex = sourData.FirstFileEndIndex;

            for (int i = 0; i < 5; i++)
            {
                m_TempCurveDatas.showMode[i] = sourData.showMode[i];

            }
            for (int i = 0; i < sourData.list_Pabd.Count; i++)
            {
                m_TempCurveDatas.list_Pabd.Add(sourData.list_Pabd[i]);
            }

            for (int i = 0; i < sourData.list_Pdet.Count; i++)
            {
                m_TempCurveDatas.list_Pdet.Add(sourData.list_Pdet[i]);
            }

            for (int i = 0; i < sourData.list_Pves.Count; i++)
            {
                m_TempCurveDatas.list_Pves.Add(sourData.list_Pves[i]);
            }

            for (int i = 0; i < sourData.list_Wights.Count; i++)
            {
                m_TempCurveDatas.list_Wights.Add(sourData.list_Wights[i]);
            }

            for (int i = 0; i < sourData.list_ufr.Count; i++)
            {
                m_TempCurveDatas.list_ufr.Add(sourData.list_ufr[i]);
            }


            for (int i = 0; i < sourData.list_Files.Count; i++)
            {
                m_TempCurveDatas.list_Files.Add(sourData.list_Files[i]);
            }
            

            m_TempCurveDatas.fmax_Wight = sourData.fmax_Wight;
            m_TempCurveDatas.fmax_Pves = sourData.fmax_Pves;
            m_TempCurveDatas.fmax_Pabd = sourData.fmax_Pabd;
            m_TempCurveDatas.fmax_Pdet = sourData.fmax_Pdet;
            m_TempCurveDatas.fmax_ufr = sourData.fmax_ufr;
            
        }
        private void toolStripMenuItem_AddReport_Click(object sender, EventArgs e)
        {
            AddReport();
        }

        private void AddReport()
        {
            if (m_CurveDatas.list_Pabd.Count == 0)
            {
                MessageBox.Show("请先选择数据文件");
                return;
            }

            foreach (CurveDatas onedatas in m_PrintCurveDatas)
            {
                if (m_SubCurveDatas.list_Pabd.Count > 0)
                {
                    if (onedatas.StartTime == m_SubCurveDatas.StartTime
                   && onedatas.endTime == m_SubCurveDatas.endTime)
                    {
                        MessageBox.Show("已加入报告列表，不能重复加入");
                        return;
                    }
                }
                else
                {
                    if (onedatas.StartTime == m_CurveDatas.StartTime
                  && onedatas.endTime == m_CurveDatas.endTime)
                    {
                        MessageBox.Show("已加入报告列表，不能重复加入");
                        return;
                    }
                } 
            }

            string strmeno = string.Empty;
            CurveMeno m_CurveMeno = null;
            if (m_SubCurveDatas.list_Pabd.Count > 0)
                m_CurveMeno = new CurveMeno(m_SubCurveDatas);
            else
                m_CurveMeno = new CurveMeno(m_CurveDatas);

            DialogResult dlgResult = m_CurveMeno.ShowDialog();
            if (dlgResult == DialogResult.OK)
            {
                strmeno = m_CurveMeno.GetStrMeno();
               
            }
            else
            {
                return;
            }

            CurveDatas m_TempCurveDatas = new CurveDatas
            {
                StartTime = DateTime.Now, //(filename.Replace('.', ':')),
                endTime = DateTime.Now,
                showMode = new byte[5],  //全部显示
                FirstFileEndIndex = -1,
                str_range = string.Empty,

                list_Pabd = new List<StruData>(),
                list_Pdet = new List<StruData>(),
                list_Pves = new List<StruData>(),
                list_Wights = new List<StruData>(),
                list_ufr = new List<StruData>(),
                
                list_Files = new List<string>()

            };

            string strTemp = string.Empty;
            if (m_SubCurveDatas.list_Pabd.Count > 0)
            {
                m_SubCurveDatas.strMeno = strmeno;
                CopyCurveData(ref m_TempCurveDatas, m_SubCurveDatas);
                strTemp = m_PrintCurveDatas.Count + ": " + m_SubCurveDatas.StartTime.ToString();
            }
            else
            {
                m_CurveDatas.strMeno = strmeno;
                CopyCurveData(ref m_TempCurveDatas, m_CurveDatas);
                strTemp = m_PrintCurveDatas.Count + ": " + m_CurveDatas.StartTime.ToString();
            }

            m_TempCurveDatas.str_range = curve3_Range.Width + "," + curve3_Range.Height + "," + nl_Range.Width + "," + nl_Range.Height + "," + nll_Range.Width + "," + nll_Range.Height;

            m_PrintCurveDatas.Add(m_TempCurveDatas); 
            listBox_SelSeg.Items.Add(strTemp);
            m_isSavePrintCurve = true;
             
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            m_PrintCurveDatas.RemoveAt(listBox_SelSeg.SelectedIndex);
            listBox_SelSeg.Items.RemoveAt(listBox_SelSeg.SelectedIndex);
            m_isSavePrintCurve = true;

        }

        private void 删除全部ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox_SelSeg.Items.Clear();
            m_PrintCurveDatas.Clear();
            m_isSavePrintCurve = true;
        }

        private void panel_Draw_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (m_CurCurveArea.Left < e.X && e.X < m_CurCurveArea.Right
                    && m_CurCurveArea.Top < e.Y && e.Y < m_CurCurveArea.Bottom)
                {
                    m_isDownLeft = true;

                    m_CurSelCurveArea.Location = new Point(e.X, m_CurCurveArea.Top);
                    m_CurSelCurveArea.Size = new Size(1, m_CurCurveArea.Height);


                    
                    
                }

            }
        }

        private void panel_Draw_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (m_CurCurveArea.Left < e.X && e.X < m_CurCurveArea.Right
                    && m_CurCurveArea.Top < e.Y && e.Y < m_CurCurveArea.Bottom)
                {
                    if (m_isDownLeft)
                    {
                        m_isDownLeft = false;

                        if (e.X > m_CurSelCurveArea.Left)
                        {
                            m_CurSelCurveArea.Size = new Size(e.X - m_CurSelCurveArea.Left, m_CurCurveArea.Height);
                        }
                        else
                        {
                            m_CurSelCurveArea.Size = new Size(m_CurSelCurveArea.Left - e.X, m_CurCurveArea.Height);
                            m_CurSelCurveArea.Location = new Point(e.X, m_CurSelCurveArea.Top);

                        }

                        
                        {
                            int startIndex = 0;
                            int endIndex = 0;
                            if (Pves_X_Value_Map.Count > 0)
                            {
                                int nnn = m_CurSelCurveArea.Left - m_CurCurveArea.Left;
                                if (Pves_X_Value_Map.ContainsKey(nnn))
                                {
                                    startIndex = Pves_X_Value_Map[nnn].nIndex;

                                }

                                nnn = m_CurSelCurveArea.Right - m_CurCurveArea.Left;
                                if (Pves_X_Value_Map.ContainsKey(nnn))
                                {
                                    endIndex = Pves_X_Value_Map[nnn].nIndex;

                                }
                            }
                            else if (Pabd_X_Value_Map.Count > 0)
                            {
                                int nnn = m_CurSelCurveArea.Left - m_CurCurveArea.Left;
                                if (Pabd_X_Value_Map.ContainsKey(nnn))
                                {
                                    startIndex = Pabd_X_Value_Map[nnn].nIndex;

                                }

                                nnn = m_CurSelCurveArea.Right - m_CurCurveArea.Left;
                                if (Pabd_X_Value_Map.ContainsKey(nnn))
                                {
                                    endIndex = Pabd_X_Value_Map[nnn].nIndex;

                                }
                            }
                            else if (Pdet_X_Value_Map.Count > 0)
                            {
                                int nnn = m_CurSelCurveArea.Left - m_CurCurveArea.Left;
                                if (Pdet_X_Value_Map.ContainsKey(nnn))
                                {
                                    startIndex = Pdet_X_Value_Map[nnn].nIndex;

                                }

                                nnn = m_CurSelCurveArea.Right - m_CurCurveArea.Left;
                                if (Pdet_X_Value_Map.ContainsKey(nnn))
                                {
                                    endIndex = Pdet_X_Value_Map[nnn].nIndex;

                                }
                            }
                            else if (Wight_X_Value_Map.Count > 0)
                            {
                                int nnn = m_CurSelCurveArea.Left - m_CurCurveArea.Left;
                                if (Wight_X_Value_Map.ContainsKey(nnn))
                                {
                                    startIndex = Wight_X_Value_Map[nnn].nIndex;

                                }

                                nnn = m_CurSelCurveArea.Right - m_CurCurveArea.Left;
                                if (Wight_X_Value_Map.ContainsKey(nnn))
                                {
                                    endIndex = Wight_X_Value_Map[nnn].nIndex;

                                }
                            }
                            else if (Ufr_X_Value_Map.Count > 0)
                            {
                                int nnn = m_CurSelCurveArea.Left - m_CurCurveArea.Left;
                                if (Ufr_X_Value_Map.ContainsKey(nnn))
                                {
                                    startIndex = Ufr_X_Value_Map[nnn].nIndex;

                                }

                                nnn = m_CurSelCurveArea.Right - m_CurCurveArea.Left;
                                if (Ufr_X_Value_Map.ContainsKey(nnn))
                                {
                                    endIndex = Ufr_X_Value_Map[nnn].nIndex;

                                }
                            }
                          
                           
                            startIndex += nSelStartindex;
                            endIndex += nSelStartindex;
                            if (startIndex < 0 || m_CurveDatas.list_Pabd.Count <= startIndex)
                                return;
                            if (endIndex < 0 || m_CurveDatas.list_Pabd.Count <= endIndex)
                                return;

                            if (endIndex - startIndex < 2)
                            {
                                m_CurSelCurveArea.Location = new Point(0, 0);
                                m_CurSelCurveArea.Size = new Size(0, 0);

                                if (m_SubCurveDatas.list_Pabd.Count>0)
                                    DrawCurve(m_SubCurveDatas);  //绘图
                                else
                                    DrawCurve(m_CurveDatas);
                                return;
                            }

                           

                            IniCurveData(ref m_SubCurveDatas);

                            CopyPartCurveData(ref m_SubCurveDatas, m_CurveDatas, startIndex,  endIndex);
                            m_SubCurveDatas.StartTime = m_CurveDatas.StartTime.AddMilliseconds((int)(startIndex * 500));
                            m_SubCurveDatas.endTime = m_CurveDatas.StartTime.AddMilliseconds((int)(endIndex * 500));

                            m_CurSelCurveArea.Location = new Point(0, 0);
                            m_CurSelCurveArea.Size = new Size(0, 0);

                            nSelStartindex = startIndex; 

                            DrawCurve(m_SubCurveDatas);  //绘图
                        }
                    }
                }
                
             

              
            }
        }

        private void panel_Draw_MouseMove(object sender, MouseEventArgs e)
        {
            if (m_CurCurveArea.Left <= e.X && e.X <= m_CurCurveArea.Right
                   && m_CurCurveArea.Top <= e.Y && e.Y <= m_CurCurveArea.Bottom)
            {
                //---------------------------------------------------
                if (Pves_X_Value_Map.Count > 0)  //标示当前坐标值 !m_isDownLeft &&
                {
                    int []ModeIndex = new int [5];  //5种曲线
                    int index = 0;
                    for(int i =0; i<5;i++)
                    {
                        if(m_CurveDatas.showMode[i]== 1)
                        {
                            ModeIndex[index] = i;
                            index++;
                        }
                    }

                    if (index < 1)
                        return;

                    int StepH =(int) m_CurCurveArea.Height/index;
                    int nCurStep = (int) (e.Y-m_CurCurveArea.Top)/StepH;  //序号
                    if ((e.Y - m_CurCurveArea.Top) % StepH != 0)
                        nCurStep++;

                    if (nCurStep>0)
                        nCurStep--;
                    if (nCurStep > 4)
                        nCurStep = 4;

                    string strValue = string.Empty;
                    if (ModeIndex[nCurStep] == 0 && Pves_X_Value_Map.ContainsKey(e.X - m_CurCurveArea.Left))
                    {
                        //strValue = "Pves ";
                        strValue += Pves_X_Value_Map[e.X - m_CurCurveArea.Left].Value; 
                    }
                    else if (ModeIndex[nCurStep] == 1 && Pabd_X_Value_Map.ContainsKey(e.X - m_CurCurveArea.Left))
                    {
                        //strValue = "Pabd ";
                        strValue += Pabd_X_Value_Map[e.X - m_CurCurveArea.Left].Value;
                    }
                    else if (ModeIndex[nCurStep] == 2 && Pdet_X_Value_Map.ContainsKey(e.X - m_CurCurveArea.Left))
                    {
                        //strValue = "Pdet ";
                        strValue += Pdet_X_Value_Map[e.X - m_CurCurveArea.Left].Value;
                    }
                    else if (ModeIndex[nCurStep] == 3 && Wight_X_Value_Map.ContainsKey(e.X - m_CurCurveArea.Left))
                    {
                        //strValue = "Pdet ";
                        strValue += Wight_X_Value_Map[e.X - m_CurCurveArea.Left].Value;
                    }
                    else if (ModeIndex[nCurStep] == 4 && Ufr_X_Value_Map.ContainsKey(e.X - m_CurCurveArea.Left))
                    {
                        //strValue = "Pdet ";
                        strValue += Ufr_X_Value_Map[e.X - m_CurCurveArea.Left].Value;
                    }
                    
                    //标示当前坐标值
                    if (strValue != "")
                    {
                        label_tip.Text = strValue;
                        this.label_tip.Location = new Point(e.X + 10, e.Y+10);
                        label_tip.Visible = true;
                    }

                    if (m_SubCurveDatas.list_Pabd.Count > 0)
                        DrawCurve(m_SubCurveDatas, e.X);
                    else
                        DrawCurve(m_CurveDatas, e.X);  //绘图
                 
                }
                //--------------------------------------------------- 
                if (m_isDownLeft)
                {
                    if (e.X > m_CurSelCurveArea.Left)
                    {
                        m_CurSelCurveArea.Size = new Size(e.X - m_CurSelCurveArea.Left, m_CurCurveArea.Height);
                    }
                    else
                    {
                        m_CurSelCurveArea.Size = new Size(m_CurSelCurveArea.Left - e.X, m_CurCurveArea.Height);
                        m_CurSelCurveArea.Location = new Point(e.X, m_CurSelCurveArea.Top);

                    }

                    if (m_SubCurveDatas.list_Pabd.Count > 0)
                        DrawCurve(m_SubCurveDatas);
                    else
                        DrawCurve(m_CurveDatas);  //绘图
                } 
                //-----------------------------------------------------
            }
            else
            {
                label_tip.Visible = false;

                if (m_isDownLeft)
                {
                    m_isDownLeft = false;
                    m_CurSelCurveArea.Size = new Size(0,0);
                    m_CurSelCurveArea.Location = new Point(0, 0);

                    if (m_SubCurveDatas.list_Pabd.Count > 0)
                        DrawCurve(m_SubCurveDatas); 
                    else
                        DrawCurve(m_CurveDatas);  //绘图
                }
            }
        }



        //----------------------------------------------------------
       

        private void menoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int nSel =  listBox_SelSeg.SelectedIndex ;
            int i = 0;
            for (i = 0; i < m_PrintCurveDatas.Count; i++)
            {
                if (i == nSel)
                {
                    CurveDatas tempdata = m_PrintCurveDatas[i]; 
                    CurveMeno m_CurveMeno = new CurveMeno(m_PrintCurveDatas[i]);

                    DialogResult dlgResult = m_CurveMeno.ShowDialog();
                    if (dlgResult == DialogResult.OK)
                    {
                        if (tempdata.strMeno != m_CurveMeno.GetStrMeno())
                            m_isSavePrintCurve = true;
                        tempdata.strMeno = m_CurveMeno.GetStrMeno();

                        m_PrintCurveDatas.RemoveAt(i);
                        m_PrintCurveDatas.Insert(i, tempdata);

                    }
                    break;
                } 
            }
             
        }

        private void 全部取消ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = 0;
            for (i = m_CheckNode_List.Count-1; i >= 0; i--)
            {
                TreeNode oneNode = m_CheckNode_List[i];
                oneNode.Checked = false;
            }
                //foreach (TreeNode oneNode in m_CheckNode_List)  //遍历文件
                //{
                //    oneNode.Checked = false;
                //}
            treeView_File.EndUpdate();
            m_CheckNode_List.Clear();
            DrawEmpty();
        }

        private void panel_Draw_Click(object sender, EventArgs e)
        {
            
        }


        //查看原图
        private void srcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_isDownLeft = false;
            m_CurSelCurveArea.Size = new Size(0, 0);
            m_CurSelCurveArea.Location = new Point(0, 0);
            nSelStartindex = 0;
            IniCurveData(ref m_SubCurveDatas);
            DrawCurve(m_CurveDatas);  //绘图

        }

        private void refresh_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_ListUSBDevs.Clear();
            m_ListUSBDevs = GetMobileDiskList();

            UpdateUsbTree();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel_Draw_Resize(object sender, EventArgs e)
        {
            if (m_SubCurveDatas.list_Pabd.Count > 0)
                DrawCurve(m_SubCurveDatas);
            else
                DrawCurve(m_CurveDatas);  //绘图
        }

        private void Range_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RangSettingDlg m_RangSettingDlg = new RangSettingDlg(curve3_Range, nl_Range, nll_Range);
            DialogResult res = m_RangSettingDlg.ShowDialog();
            if (res == DialogResult.OK)
            {
                curve3_Range = m_RangSettingDlg.pvet_range;
                nl_Range = m_RangSettingDlg.nl_range;
                nll_Range = m_RangSettingDlg.nll_range;

                if (m_SubCurveDatas.list_Pabd.Count > 0)
                    DrawCurve(m_SubCurveDatas);
                else
                    DrawCurve(m_CurveDatas);  //绘图
            }
        }

        private void Export_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("正在开发中");
        }

        //导出一个检查号下的文件，isAllExport是否全部导出 G:\1508491901\info\ID2017-06-30\ID2017-06-30 11.42.32

        private void ExportFile(string checkNo, ref  StruCheckFileInfo tempStruCheckFileInfo, bool isAllExport = false)
        {
            foreach (StruOneDayFileInfo OneDayFileInfo in tempStruCheckFileInfo.m_StruOneDayFileInfo)
            {
                string strTxtFile = OneDayFileInfo.strTxtFile;  //G:\3384328829\ID2017-08-10.txt

                string strDesPath = m_strFloder + m_CurSelPatientInfo.id + @"\" + checkNo;
                if (!Directory.Exists(strDesPath))  //创建文件夹
                {
                    Directory.CreateDirectory(strDesPath);
                }
                strDesPath += @"\";
                string strtempPath = string.Empty;

                tbl_patient_checknum_link_Model m_model = new tbl_patient_checknum_link_Model();
                int npos = strTxtFile.LastIndexOf("\\");
                if (npos > -1)
                {
                    strtempPath = strDesPath + strTxtFile.Substring(npos+1);   //目标txt文件
                     
                    if (!File.Exists(strtempPath))  //创建文件夹
                    {
                        File.Copy(strTxtFile, strtempPath);  //复制txt文件 
                    }

                    strtempPath = strtempPath.Replace('\\', '*');
                    string strWhere = string.Empty;
                    strWhere = @"patient_uuid='" + m_CurSelPatientInfo.uuid + @"' and  checkNum='" + checkNo + @"' and  txtpath='" + strtempPath + @"' ";
                    List<tbl_patient_checknum_link_Model> tempModelist = patient_checknum_link_Manager.GetModelList(strWhere);
                  
                    if (tempModelist.Count < 1)
                    {
                        //txt文件可以单独存储一个表。存储到数据库
                        //---------------------------------------

                        m_model.uuid = PublicConst.GenerateUUID();
                        m_model.checknum = checkNo;
                        m_model.patient_uuid = m_CurSelPatientInfo.uuid;

                        m_model.txtPath = strtempPath;
                        patient_checknum_link_Manager.Add(m_model);
                        //---------------------------------------
                    }
                    else
                    {
                        m_model = tempModelist[0];
                    }
                }
                else
                {
                    continue;
                }

                foreach (StruFileInfo tempFileInfo in OneDayFileInfo.m_StruFileInfo)
                {
                    strtempPath = tempFileInfo.m_filePath;   //G:\1508491901\info\ID2017-06-30\ID2017-06-30 11.42.32
                    string[] strCodes = strtempPath.Split('\\');
                    strtempPath = strDesPath + strCodes[2] + @"\" + strCodes[3];   // + \info\ID2017-06-30
                    if (!Directory.Exists(strtempPath))  //创建文件夹
                    {
                        Directory.CreateDirectory(strtempPath);
                    }
                    strtempPath += @"\";

                    //--------------------------------------------
                    //获取路径下，文件名含有strDate的文件
                    npos = tempFileInfo.m_filePath.LastIndexOf('\\');
                    List<string> m_strDataFilList = UpdateDataFileList(tempFileInfo.m_filePath.Substring(0, npos+1), tempFileInfo.m_filePath.Substring(npos+1));
                    for (int v = 0; v < m_strDataFilList.Count; v++)
                    {
                        npos  = m_strDataFilList[v].LastIndexOf("\\");
                        string strtarFile = strtempPath + m_strDataFilList[v].Substring(npos + 1);
                        if (!File.Exists(strtarFile))  //创建文件夹
                        {
                            File.Copy(m_strDataFilList[v], strtarFile);  //复制txt文件
                        }


                        strtarFile = strtarFile.Replace('\\', '*');
                        string strWhere = @"check_uuid='" + m_model.uuid + @"' and  path='" + strtarFile + @"'  ";
                        List<tbl_patient_checknum_file_info_Model> tempModelist = patient_checknum_file_info_Manager.GetModelList(strWhere);

                        if (tempModelist.Count < 1)
                        {
                            // 存储到数据库
                            //---------------------------------------

                            tbl_patient_checknum_file_info_Model file_Model = new tbl_patient_checknum_file_info_Model();
                            file_Model.check_uuid = m_model.uuid;
                            file_Model.createtime = DateTime.Now;
                            file_Model.checkmode = tempFileInfo.strFileMode;
                            file_Model.uuid = PublicConst.GenerateUUID();
                            file_Model.path = strtarFile;
                            patient_checknum_file_info_Manager.Add(file_Model);
                            //---------------------------------------
                        }

                        
                    }
                    //--------------------------------------------

                    
                }
            }
        }

        private void Export_Click(object sender, EventArgs e)
        {
            if (m_CheckNode_List.Count < 1)
            {
                MessageBox.Show("请先选择需要导出的文件");
                return;
            }

            TreeNode FirstNode = m_CheckNode_List[0];

            string strTag = FirstNode.Tag.ToString();
            if (strTag.IndexOf("\\qtud_data\\"+ m_CurSelPatientInfo.id) > -1)  //选的是历史数据
            {
                MessageBox.Show("历史数据不能再次导出");
                return;
            }

            int ipos = FirstNode.Tag.ToString().IndexOf(",");  //401,G:\1508491901\info\ID2017-06-30\ID2017-06-30 11.42.32
            string strPath= FirstNode.Tag.ToString().Substring( ipos +1); //G:\1508491901\info\ID2017-06-30\ID2017-06-30 11.42.32
          
            ipos = strPath.IndexOf("\\");
            strPath = strPath.Substring(ipos + 1); // 1508491901\info\ID2017-06-30\ID2017-06-30 11.42.32

            ipos = strPath.IndexOf("\\"); 
            string checkNo = strPath.Substring(0,ipos ); // 1508491901

            if (checkNo != "")  // 判断是否已经导出到其他患者名下
            {
                string strWhere = string.Empty;
                strWhere = @" uuid in (select patient_uuid from tbl_patient_checknum_link where checkNum='" + checkNo + "' )";
                  
                PatientInfoManager pim = new PatientInfoManager();
                List< PatientInfoModel> pInfoModelList = pim.GetModelList(strWhere);
                if (pInfoModelList.Count > 0)
                {
                    MessageBox.Show("文件曾经导出在 \"" + pInfoModelList[0].id + @" " + pInfoModelList[0].name + "\" 名下，\r\n请确认: 检查数据是否是当前患者数据。");
                    return;
                }
            }

            if (checkNo != "" && m_checkNum_Files_map.ContainsKey(checkNo))
            {
                DialogResult res = MessageBox.Show("请确认: 导出的数据是当前患者 \"" + m_CurSelPatientInfo.id + @" " + m_CurSelPatientInfo.name + "\" 的数据。");
                if (res != DialogResult.OK)
                    return;

                try
                { 
                    string strWhere = string.Empty;
                    strWhere = @"patient_uuid='" + m_CurSelPatientInfo.uuid + @"' and  checkNum='" + checkNo + @"'";
                    List<tbl_patient_checknum_link_Model> tempModelist = patient_checknum_link_Manager.GetModelList(strWhere);
                    if (tempModelist.Count < 1)  //从没导入
                    { 
                        StruCheckFileInfo tempStruCheckFileInfo = m_checkNum_Files_map[checkNo];

                        ExportFile(checkNo, ref tempStruCheckFileInfo, true);
                        tempStruCheckFileInfo.isLoad = true;
                    }
                    else //部分导出
                    {
                        StruCheckFileInfo tempStruCheckFileInfo = m_checkNum_Files_map[checkNo];

                        ExportFile(checkNo, ref tempStruCheckFileInfo, false);
                        tempStruCheckFileInfo.isLoad = true;
                    }
                    UpdateUsbTree();
                    MessageBox.Show("导出完成！");

                }
                catch (System.Exception ex)
                {

                }
            }

        }

        private void comboBox_checkMode_SelectedIndexChanged(object sender, EventArgs e)
        {
           m_SelMode =  comboBox_checkMode.SelectedIndex - 1;
           UpdateUsbTree();
        }


        //显示历史记录
        private void button_show_history_Click(object sender, EventArgs e)
        {
            m_ListUSBDevs.Clear();
            m_ListUSBDevs = GetMobileDiskList();

            string strWhere = string.Empty;
            strWhere = @"patient_uuid='" + m_CurSelPatientInfo.uuid +@"' order by txtpath ";
            List<tbl_patient_checknum_link_Model> tempModelist = patient_checknum_link_Manager.GetModelList(strWhere);
            string lastpath = string.Empty;
            foreach (tbl_patient_checknum_link_Model tempmodel in tempModelist)
            {
                int npos = tempmodel.txtPath.LastIndexOf("\\");
                string strDesPath = m_strFloder + m_CurSelPatientInfo.id; // +@"\" + tempmodel.checknum;
                  
                if (npos > -1)
                {
                    strDesPath = tempmodel.txtPath.Substring(0,npos);
                    npos =  strDesPath.LastIndexOf("\\");
                    strDesPath = tempmodel.txtPath.Substring(0, npos);
                }
                if (lastpath == strDesPath)
                    continue;
                else
                {
                    lastpath = strDesPath;
                }
                if (!Directory.Exists(strDesPath))  //创建文件夹
                {
                    MessageBox.Show(@"无历史数据！");
                    continue;
                }
                strDesPath += @"\";
                m_ListUSBDevs.Add(strDesPath);
            }
            UpdateUsbTree();
        }


        private bool SavaReport()
        { 
            //----------------------------------------------------
            ReportInfoModel model = new ReportInfoModel();

            model.uuid = PublicConst.GenerateUUID();
            model.name = m_CurSelPatientInfo.cardid;
            model.CreateDate = DateTime.Now;
            model.patient_uuid = m_CurSelPatientInfo.uuid;

            model.ks = m_TestDatas.strKS;
            model.ch = m_TestDatas.strCH;
            model.nlljcjg = (m_TestDatas.strNLL);
            model.nlljcjg_nl = (m_TestDatas.strNL);
            model.pgrlylcd = (m_TestDatas.str_RJ_YL);

            model.pgrl_cg = (m_TestDatas.str_nl_cg);
            model.pgrl_zc = (m_TestDatas.str_nl_zc);
            model.pgrl_zd = (m_TestDatas.str_nl_zd);

            model.pgsyx = m_TestDatas.str_syx;
            model.pgwdx = m_TestDatas.str_wdx;
            model.tsjc = m_TestDatas.str_tsjc;

            model.vlpp = (m_TestDatas.str_vlpp);
            model.dlpp = (m_TestDatas.str_dlpp);
            model.clpp = (m_TestDatas.str_clpp);
            model.pgaqrl = (m_TestDatas.str_aqrl);

            model.otherInfo = m_TestDatas.str_qtms;
            model.testresult = m_TestDatas.str_ndlxzd;

            m_ReportInfoModel = model;

            m_TestDatas.uuid = model.uuid;
            try
            {  
                m_ReportInfoManager.Add(model);
                //MessageBox.Show(" 保存成功！ ");
                return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(" 保存失败！ ");

                return false;
            }
        }

        private void SavaCurve()
        {  
            if (m_ReportInfoModel.uuid != ""  )
            {
                try
                {
                    //删除之前的曲线与文件的连接关系
                    //tbl_curve_file_link
                    tbl_curve_file_link_Manager m_file_link_manager = new tbl_curve_file_link_Manager();
                    string strWhere = string.Empty;
                    strWhere = @" curve_uuid in ( Select uuid from tbl_curve_info where  report_uuid='" + m_ReportInfoModel.uuid + @"' )";
                    m_file_link_manager.Delete(strWhere);

                    //删除之前的曲线信息
                    strWhere = @" report_uuid='" + m_ReportInfoModel.uuid + @"' ";
                    m_curve_info_Manager.Delete(strWhere);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(" 保存失败！ "); 
                }
            }
           
            int cc = 0;
            //添加曲线
            foreach (CurveDatas onedatas in m_PrintCurveDatas)
            {

                //保存曲线
                curve_info_Model.uuid = PublicConst.GenerateUUID();
                curve_info_Model.report_uuid = m_ReportInfoModel.uuid;
                curve_info_Model.rangs = onedatas.str_range;
                curve_info_Model.starttime = onedatas.StartTime;
                curve_info_Model.endtime = onedatas.endTime ;
                curve_info_Model.meno = onedatas.strMeno;
                curve_info_Model.nindex = cc;

                string strMode = string.Empty;
                for(int u= 0; u<5; u++)  //5个检查模式
                {
                    strMode+=onedatas.showMode[u].ToString() + ","; 
                }
                curve_info_Model.strmode = strMode;

                try
                {
                    m_curve_info_Manager.Add(curve_info_Model);
                    cc++;
                }
                catch (System.Exception ex)
                {
                	
                }

                int kkk = 0;
                //保存曲线与文件的关系
                foreach (string onefile in onedatas.list_Files)  //J:\3384328829\info\ID2017-08-10\ID2017-08-10 08.19.11.hold
                {
                    int npos = onefile.IndexOf('\\');
                    string strDesPath = m_strFloder + m_CurSelPatientInfo.id + @"\" + onefile.Substring(npos+1);

                    try
                    {
                        string strWhere = string.Empty;
                        strDesPath = strDesPath.Replace('\\', '*'); 

                        strWhere = @" path='" + strDesPath + @"' ";
                         
                        List<tbl_patient_checknum_file_info_Model> modelList = patient_checknum_file_info_Manager.GetModelList(strWhere);
                        for (int k = 0; k < modelList.Count; k++)
                        {
                            tbl_curve_file_link_Model m_curve_file_link_Model = new tbl_curve_file_link_Model();
                            m_curve_file_link_Model.curve_uuid = curve_info_Model.uuid;
                            m_curve_file_link_Model.file_uuid = modelList[k].uuid;
                            m_curve_file_link_Model.nindex = kkk;

                            tbl_curve_file_link_Manager m_curve_file_link_Manager = new tbl_curve_file_link_Manager();
                            m_curve_file_link_Manager.Add(m_curve_file_link_Model);

                            kkk++;
                        }
                    }
                    catch (System.Exception ex)
                    {

                    }
                  
                }

            }
          


        }

        private void SaveCurveFunc()
        {
            if (m_TestDatas.uuid == string.Empty)  //新建
            {
                bool ret = SavaReport();
                if (ret)
                {
                    SavaCurve();
                }
                else
                    return;
            }
            else
            {
                SavaCurve();
            }
            m_isSavePrintCurve = false;
            MessageBox.Show(" 保存完成！ "); 
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            SaveCurveFunc();

        }

        private void MainFrom_Curve_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (m_isSavePrintCurve)
            {
                DialogResult dr = MessageBox.Show("打印曲线已修改，是否保存修改", "保存", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                {
                    SaveCurveFunc();
                }
                else
                {
                    m_isSavePrintCurve = false;
                }
            }
        }
         
        //----------------------------------------------------------

        
    }
}
