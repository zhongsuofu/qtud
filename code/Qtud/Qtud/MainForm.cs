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
    public struct StruData
        {
            public float value;   //值
            public int time;    //时间点序号，0，1，2，3...
            public bool isShow; //是否作为显示采样点
        }

    public partial class MainForm : Form
    {
        #region 变量
        private UserManager um = new UserManager();
        private PatientInfoManager pim = new PatientInfoManager();
        private List<PatientInfoModel> listPatientInfo;  //病人数据列表
        private string[] usbDevs;  //USB设备列表
        private string strUsbPath = @"data\";  //检测数据所在的文件夹  
        private string strInfoPath = @"info\";  //检测数据所在的文件夹  
        private string strDataPath = string.Empty;  //设备号列表，检测数据所在的文件夹完整路径 J:\data 
        
        private string strDatePath = string.Empty;  //日期文件列表，检测数据所在的文件夹完整路径 J:\data\1508491901
        private string strTxtfile = string.Empty;   //TXT文件名
 
        private List<string> CheckDatalist = new List<string>(); //检测编号列表  "J:\data\" 文件夹下的文件
        private List<string> CheckDatelist = new List<string>(); //检测日期列表  "J:\data\1508491901\"  文件夹下的文件


        //当前检测数据
        StruDataManage m_StruDataManage = new StruDataManage
        {
            StartTime = DateTime.Now, //(filename.Replace('.', ':')),
            endTime = DateTime.Now ,
            list_Pabd = new List<StruData>(),
            list_Pdet = new List<StruData>(),
            list_Pves = new List<StruData>(),
            list_Wights = new List<StruData>(),
        };


        public struct StrucheckTime
        {
            public string  checkdate ;
            public string  checktime;
            public string checkmode; 
        }
        private List<StrucheckTime> Checkdtlist = new List<StrucheckTime>(); //解析日期TXT文件数据列表


        //-------------------------------------------------------
        
        //结构体；检测数据
        public struct StruDataManage
        {
            public DateTime StartTime  ; //开始时间
            public DateTime endTime; //结束时间

            public List<StruData> list_Wights ;  //尿量值
            public List<StruData> list_Pves  ;   //膀胱压力
            public List<StruData> list_Pabd  ;   //直肠压力
            public List<StruData> list_Pdet ;   //逼尿肌压力 = 膀胱压力 - 直肠压力
            
        }
        //-------------------------------------------------------

        #endregion

        #region 构造函数
        public void UpdateListBox()
        {
            string strWhere = string.Empty;
            string sRet = string.Empty;
            parient_list.Items.Clear();
            try
            {
                strWhere += " cardid<>''";//非冻结
                strWhere += "   order by lastchecktime desc limit 0,30 ";
                
                listPatientInfo = pim.GetModelList(strWhere);
                if (0 < listPatientInfo.Count)//没找到数据
                {
                    int i = 0;
                    foreach (PatientInfoModel data in listPatientInfo)
                    {
                        i++;

                        //解密
                        //string DBase64 = System.Text.Encoding.Default.GetString(Convert.FromBase64String(data.cardid));  //Base解码
                        //string strcardid = DES.DESDecoder(DBase64, Encoding.Default, null, null);  //DES解码 
                        //data.cardid = strcardid;
                        data.cardid = data.cardid;
                         
                        string str = data.name;
                        if (data.name.Length < 5)
                        {
                            str += "         ";
                            str = str.Substring(0, 5);
                        }
                        if (data.cardid.Length >9)
                        {
                            str +=   " (" + data.cardid.Substring(0, 4) + @"***" + data.cardid.Substring(data.cardid.Length - 5) + ")";
                            
                        }
                        else
                            str +=  " (" + data.cardid + ")";

                        //判断是否添加到listbox1
                        if (!this.parient_list.Items.Contains(str))
                        {
                            this.parient_list.Items.Add(str); 
                        }
                         
                    }
                    this.parient_list.SelectedIndex = 0;
                } 
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        //更新USB设备列表
        private void UpdateUsbList()
        {
            listBox_usb_Dev.Items.Clear();
            usbDevs = GetMobileDiskList();
            for (int i = 0; i < usbDevs.Length; i++)
            {
                //判断是否添加到listbox1
                if (usbDevs[i].ToString() != "" && !this.listBox_usb_Dev.Items.Contains(usbDevs[i].ToString()))
                {
                    this.listBox_usb_Dev.Items.Add(usbDevs[i].ToString());
                }
            }
        }
        public MainForm()
        {
            InitializeComponent();

            UpdateListBox();
             
            //更新USB设备列表
            UpdateUsbList();
             
             
        }
        #endregion

        #region  检测USB设备是否插入
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
                            foreach (DriveInfo drive in s)
                            {
                                if (drive.Name.ToString() == "A:\\")
                                {
                                    continue;
                                }
                                if (drive.DriveType == DriveType.Removable)
                                {
                                    if (!this.listBox_usb_Dev.Items.Contains(drive.Name.ToString()))
                                    {
                                        listBox_usb_Dev.Items.Add(drive.Name.ToString());
                                    }

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
                                    break;
                                }
                            }
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
                                   DEV_BROADCAST_VOLUME vol;
                                   vol = (DEV_BROADCAST_VOLUME)Marshal.PtrToStructure(m.LParam, typeof(DEV_BROADCAST_VOLUME));
                                   string ID = vol.dbcv_unitmask.ToString("x");
                                   string moveDev = IO(ID) + @"\";

                                   for (int v = 0; v < usbDevs.Length; v++)
                                   {
                                       if (usbDevs[v].ToString() == moveDev)
                                       {
                                           usbDevs[v] = "";
                                           break;
                                       }
                                   } 

                               }
                           }  

                            //----------------------------------------------------
                            //更新列表
                           listBox_usb_Dev.Items.Clear();
                           for (int i = 0; i < usbDevs.Length; i++)
                           {
                               //判断是否添加到listbox1
                               if (usbDevs[i].ToString()!= "" && !this.listBox_usb_Dev.Items.Contains(usbDevs[i].ToString()))
                               {
                                   this.listBox_usb_Dev.Items.Add(usbDevs[i].ToString());
                               }
                           }
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



        //获取电脑上所有移动磁盘的列表(usb ,移动磁盘)
        private string[] GetMobileDiskList()
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
            return drs.ToArray();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            comboBox_checkMode.SelectedIndex= 7;
            //----------------------------------------------------
            this.Hide();

            loginDlg login = new loginDlg();

            DialogResult dlgResult = login.ShowDialog();
            if (dlgResult == DialogResult.OK)
            {
                this.Show(); //显示  
            }
            else if (dlgResult == DialogResult.Cancel)
            {
                Application.Exit();  //关闭程序

            }
            //----------------------------------------------------
        }
         

        //添加病人信息
        private void 添加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ADDPatientInfo(); 

        }

         

        private void ADDPatientInfo()
        {
            PatientInfoDlg m_PatientInfoDlg = new PatientInfoDlg(null);
            DialogResult dlgResult = m_PatientInfoDlg.ShowDialog();
            if (dlgResult == DialogResult.OK)  //添加完成
            {
                UpdateListBox();

            }
        }

        //添加病人
        private void toolStripButton_ADDP_Click(object sender, EventArgs e)
        {
            ADDPatientInfo();

        }


        //浏览文件夹
        private void button1_Click(object sender, EventArgs e)
        {

            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();

            dialog.Description = "请选择检查数据所在文件夹";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    MessageBox.Show(this, "文件夹路径不能为空", "提示");
                    return;
                }
                strDataPath = dialog.SelectedPath;
                UpdateCheckDataList(strDataPath); 
                
            } 
        }


        //选择USB磁盘
        private void listBox_usb_Dev_SelectedValueChanged(object sender, EventArgs e)
        {
             string strUsb =  string.Empty;
             if (listBox_usb_Dev.SelectedItem != null)
             {
                 strUsb = listBox_usb_Dev.SelectedItem.ToString();

                 strUsb += strUsbPath;

                 strDataPath = strUsb;
                 UpdateCheckDataList(strDataPath);
             }

           
 
        }

        private void UpdateCheckDataList(string strPath)  //  J:\data\
        {
            listBox_checkNO.Items.Clear();
            CheckDatalist.Clear();

            DirectoryInfo theFolder = new DirectoryInfo(strPath);
            DirectoryInfo[] dirInfo = theFolder.GetDirectories();

            //遍历文件夹
            int i = 0;
            foreach (DirectoryInfo NextFolder in dirInfo)
            {
                if (NextFolder.Name != null)
                {
                    CheckDatalist.Insert(0,NextFolder.Name);
                    this.listBox_checkNO.Items.Insert(0,NextFolder.Name);
                    i++;
                }
                
            }
        }

        private void listBox_check_Data_SelectedValueChanged(object sender, EventArgs e)
        {
            if (listBox_checkNO.SelectedItem != null)
            {
                strDatePath = strDataPath + listBox_checkNO.SelectedItem.ToString();
                UpdateChedkDataList(strDatePath);
            }
        }

        private void UpdateChedkDataList(string strPath)  //J:\data\1508491901
        {
            listBox_Check_date.Items.Clear();
            CheckDatelist.Clear();

            DirectoryInfo theFolder = new DirectoryInfo(strPath); 

            FileInfo[] fileInfo = theFolder.GetFiles();
            int i = 0;
            foreach (FileInfo NextFile in fileInfo)  //遍历文件
            {
                if (NextFile.Name != null && NextFile.Name.Contains(".txt") && NextFile.Length > 0)
                {
                    CheckDatelist.Insert(0,NextFile.Name);
                    this.listBox_Check_date.Items.Insert(0,NextFile.Name);
                    i++;
                }
               
            }
             
        }

        private void listBox_Check_time_SelectedValueChanged(object sender, EventArgs e)
        {
            string strTxtpath = strDatePath;
            if (listBox_Check_date.SelectedItem != null)
            {
                strTxtpath += @"\" + listBox_Check_date.SelectedItem.ToString();
                strTxtfile = listBox_Check_date.SelectedItem.ToString();
             
            ReadTxt(strTxtpath);
            }
        }

        private void ReadTxt(string path)
        {
            Checkdtlist.Clear();
            listBox_checkTime.Items.Clear();

            int nIndex =  comboBox_checkMode.SelectedIndex;
             
            try
            {
                StreamReader sr = new StreamReader(path, Encoding.Default);
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] lineCodes = line.Split(' ');
                    StrucheckTime _StrucheckTime;
                    if (lineCodes.Count() >= 3)
                    {
                        _StrucheckTime.checkdate = lineCodes[0].Trim();
                        _StrucheckTime.checktime = lineCodes[1].Trim();
                        _StrucheckTime.checkmode = lineCodes[3].Trim();

                        Checkdtlist.Insert(0, _StrucheckTime);


                        if (nIndex == 0)
                        {
                            string strTemp = @"[" + _StrucheckTime.checkmode + "] " + _StrucheckTime.checktime;
                            listBox_checkTime.Items.Insert(0, strTemp);
                        }
                        else
                        {
                            if (_StrucheckTime.checkmode.Substring(0, 1) == (nIndex - 1).ToString())
                            {
                                string strTemp = @"[" + _StrucheckTime.checkmode + "] " + _StrucheckTime.checktime;
                                listBox_checkTime.Items.Insert(0, strTemp);
                            }
                        }
                 

                    }
                }
                sr.Close();
            }
            catch (System.IO.IOException ex)
            {
                return;
            } 

        }

        private void listBox_checkTime_SelectedValueChanged(object sender, EventArgs e)
        {
             //---------------------------------------------
            if (listBox_checkTime.SelectedItem == null)
                return;

            //获取数据文件 
            string strtimefile  = listBox_checkTime.SelectedItem.ToString();
            int nindex  = strtimefile.IndexOf(" ");
            string strDataInfoFile = strDatePath + @"\" + strInfoPath + strTxtfile.Substring(0, strTxtfile.Length - 4) +@"\";

            List<string> dataFileArr = new List<string>();  //数据文件列表
           
            string filename = strTxtfile.Substring(0, strTxtfile.Length - 4) + " " + strtimefile.Substring(nindex + 1);

            DirectoryInfo theFolder = new DirectoryInfo(strDataInfoFile);

            FileInfo[] fileInfo = theFolder.GetFiles();
            int i = 0;
            foreach (FileInfo NextFile in fileInfo)  //遍历文件
            {
                if (NextFile.Name != null &&  NextFile.Length > 0)
                {
                    if (NextFile.Name.IndexOf(filename) >= 0)
                    {
                        dataFileArr.Add( NextFile.Name); 
                    }
                    else if (dataFileArr.Count() > 0)
                    {
                        break;
                    }

                    i++;
                }

            }
            //---------------------------------------------
            filename = filename.Substring(2); 
            
            m_StruDataManage.StartTime = DateTime.Parse(filename.Replace('.', ':')); 
            m_StruDataManage.endTime = DateTime.Parse(filename.Replace('.', ':')); 
            //m_StruDataManage.list_Pabd = new List<StruData>();
            //m_StruDataManage.list_Pdet = new List<StruData>();
            //m_StruDataManage.list_Pves = new List<StruData>();
            //m_StruDataManage.list_Wights = new List<StruData>();

            //获取数据文件 
            foreach (string TempDataFile in dataFileArr)
            {
               string strTempfile =  strDataInfoFile + TempDataFile;
               int nTimeIndex =0;  //时间点序号
               BinaryReader br;
               // 读取文件
               try
               {
                   br = new BinaryReader(new FileStream(strTempfile, FileMode.Open));
               }
               catch (IOException e1)
               {
                   MessageBox.Show( "读取文件失败" +  strTempfile) ;
                   return;
               }
               try
               {
                   while (true)
                   {
                       //---------------------------------
                       byte []byteWeight = br.ReadBytes(2);  //高低位需要反过来 
                       if (byteWeight.Count() < 1)
                           break;   //读取结束
                       int nWeight = Convert.ToInt16((byteWeight[1].ToString("X2") + byteWeight[0].ToString("X2")), 16);  //br.ReadInt16()
                        
                       byte []bytePves  = br.ReadBytes(2);
                       int nPves =  Convert.ToInt16((bytePves[1].ToString("X2") + bytePves[0].ToString("X2")), 16);  //br.ReadInt16()

                       byte []bytePabd = br.ReadBytes(2);
                       int nPabd =  Convert.ToInt16((bytePabd[1].ToString("X2") + bytePabd[0].ToString("X2")), 16);  //br.ReadInt16()
                        
                       br.ReadInt16();  //-1 表示无效ff

                       StruData WeightData;
                       if (nWeight > 0)
                           WeightData.value = nWeight / 10;
                       else
                           WeightData.value = 0;

                       WeightData.time = nTimeIndex;
                       WeightData.isShow = false;
                       m_StruDataManage.list_Wights.Add(WeightData);


                       if (nPves > 999)
                           nPves = 999;
                       if (nPves < -99)
                           nPves = -99;
                       StruData PvesData;
                       if (nPves > 0)
                           PvesData.value = nPves / 10;
                       else
                           PvesData.value = 0;

                       PvesData.time = nTimeIndex;
                       PvesData.isShow = false;
                       m_StruDataManage.list_Pves.Add(PvesData);


                       if (nPabd > 999)
                           nPabd = 999;
                       if (nPabd < -99)
                           nPabd = -99;
                       StruData PabdData;
                       if (nPabd > 0)
                           PabdData.value = nPabd / 10;
                       else
                           PabdData.value = 0;

                       PabdData.time = nTimeIndex;
                       PabdData.isShow = false;
                       m_StruDataManage.list_Pabd.Add(PabdData);
                        
                       StruData PdetData;
                       PdetData.value = PvesData.value - PabdData.value;


                       if (PdetData.value > 999)
                           PdetData.value = 999;
                       if (PdetData.value < -99)
                           PdetData.value = -99;
                       PdetData.time = nTimeIndex;
                       PdetData.isShow = false;
                       m_StruDataManage.list_Pdet.Add(PdetData);


                       //---------------------------------

                       nTimeIndex++;
                   } 
               }
               catch (EndOfStreamException e2)
               {
                   //MessageBox.Show("读取结束 " + strTempfile);

                   br.Close();

                   if (m_StruDataManage.list_Pdet.Count() > 0 && m_StruDataManage.list_Pdet.Count() % 2 == 0)
                       m_StruDataManage.endTime = m_StruDataManage.StartTime.AddSeconds(m_StruDataManage.list_Pdet.Count() / 2);
                   else if (m_StruDataManage.list_Pdet.Count() > 0)
                       m_StruDataManage.endTime = m_StruDataManage.StartTime.AddSeconds( (m_StruDataManage.list_Pdet.Count()+1) / 2);


                   DrawCurve(m_StruDataManage);
                   return;
               }
               br.Close();

               if (m_StruDataManage.list_Pdet.Count() > 0 && m_StruDataManage.list_Pdet.Count() % 2 == 0)
                   m_StruDataManage.endTime = m_StruDataManage.StartTime.AddSeconds(m_StruDataManage.list_Pdet.Count() / 2);
               else if (m_StruDataManage.list_Pdet.Count() > 0)
                   m_StruDataManage.endTime = m_StruDataManage.StartTime.AddSeconds((m_StruDataManage.list_Pdet.Count() + 1) / 2);
              
                DrawCurve(m_StruDataManage);


            //   StreamReader sr = new StreamReader(strTempfile, Encoding.Default);
            //    String line;
            //    while ((line = sr.ReadLine()) != null)
            //    {
            //        string[] lineCodes = line.Split(' ');
            //        StrucheckTime _StrucheckTime;
            //        //if (lineCodes.Count() >= 3)
            //        //{
            //        //    _StrucheckTime.checkdate = lineCodes[0].Trim();
            //        //    _StrucheckTime.checktime = lineCodes[1].Trim();
            //        //    _StrucheckTime.checkmode = lineCodes[3].Trim();

            //        //    Checkdtlist.Insert(0, _StrucheckTime);

            //        //    string strTemp = @"[" + _StrucheckTime.checkmode + "] " + _StrucheckTime.checktime;

            //        //    listBox_checkTime.Items.Insert(0, strTemp);

            //        //}
            //    }
            }

            //---------------------------------------------

            //     public struct StruDataManage
        //{
        //    public List<StruData> list_Wights = new List<StruData>();  //尿量值
        //    public List<StruData> list_Pves = new List<StruData>();   //膀胱压力
        //    public List<StruData> list_Pabd = new List<StruData>();   //直肠压力
        //    public List<StruData> list_Pdet = new List<StruData>();   //逼尿肌压力 = 膀胱压力 - 直肠压力
            
        //}

            return;

            LinearGradientBrush brush = new LinearGradientBrush(this.panel_draw.ClientRectangle, Color.Empty, Color.Empty, 100);
            ColorBlend blend = new ColorBlend();
            blend.Colors = new Color[] { Color.Red, Color.Green, Color.Blue };
            blend.Positions = new float[] { 0, .5f, 1 };
            brush.InterpolationColors = blend;
            Pen pen5 = new Pen(brush);
            Graphics g5 = this.panel_draw.CreateGraphics();
            Point[] p = new Point[] { new Point(0, 0), new Point(100, 100), new Point(50, 100), new Point(200, 100) };
            g5.SmoothingMode = SmoothingMode.AntiAlias;
            g5.DrawCurve(pen5, p);

            
        }


        //画曲线
        private void DrawCurve(StruDataManage oneDataManage)
        {
            //看看需要画那几条类型的数据曲线
            int nCurveCnt = 0;
            if (checkBox_Pves.Checked)
                nCurveCnt++;
            if (checkBox_Pabd.Checked)
                nCurveCnt++;
            if (checkBox_Pdet.Checked)
                nCurveCnt++;

            Rectangle clientR = this.panel_draw.ClientRectangle;
            if (nCurveCnt > 0)
            {

                clientR.Location = new Point(clientR.Left + 20, clientR.Top + 20);
                clientR.Size = new Size(clientR.Width - clientR.Left - 20, clientR.Height - clientR.Top - 30);
                
                // 画横标尺
                DrawFuns m_DrawFuns = new DrawFuns();
                m_DrawFuns.IniDraw(ref panel_draw );
                int iniH = clientR.Top;
                m_DrawFuns.plotLine2( new Point[] { new Point(clientR.Left, iniH), new Point(clientR.Right, iniH) });
                for(int i =0; i<nCurveCnt; i++)
                {
                    iniH += clientR.Height / nCurveCnt;
                    m_DrawFuns.plotLine2( new Point[] { new Point(clientR.Left, iniH), new Point(clientR.Right, iniH) });

                }

                // 画纵标尺
                m_DrawFuns.plotLine2( new Point[] { new Point(clientR.Left, clientR.Top), new Point(clientR.Left, clientR.Bottom) });
                m_DrawFuns.plotLine2( new Point[] { new Point(clientR.Left + 100, clientR.Top), new Point(clientR.Left + 100, clientR.Bottom) });
                m_DrawFuns.plotLine2( new Point[] { new Point(clientR.Right, clientR.Top), new Point(clientR.Right, clientR.Bottom) });


                // 画纵标尺 虚线 5端
                int ptCnt = oneDataManage.list_Pabd.Count;
                int nSecond = ptCnt / 2; //秒数
                int nMinute = nSecond / 60; //分钟数

                int nduan = 1;  //分几个区间
                if (nMinute >= 5)
                {
                    nduan = 5;
                }
                else if (nMinute > 1)
                    nduan = nMinute;
                else if (nSecond >= 5) //按秒分段
                {
                    nduan = 5;
                }
                else
                    nduan = nSecond;
                    
                //画曲线
                //m_DrawFuns.plotLine3(ref oneDataManage.list_Pabd,clientR);

                m_DrawFuns.UpdateDraw( );

                return;
                LinearGradientBrush brush = new LinearGradientBrush(clientR, Color.Empty, Color.Empty, 100);
                ColorBlend blend = new ColorBlend();
                blend.Colors = new Color[] { Color.Red, Color.Green, Color.Blue };
                blend.Positions = new float[] { 0, .5f, 1 };
                brush.InterpolationColors = blend;
                Pen pen5 = new Pen(brush);

                Graphics g5 = this.panel_draw.CreateGraphics();
                Point[] p = new Point[] { new Point(0, 0), new Point(100, 100), new Point(50, 100), new Point(200, 100) };
                g5.SmoothingMode = SmoothingMode.AntiAlias;
                g5.DrawCurve(pen5, p);
            }
            else
            {
                //清除曲线图
                DrawFuns m_DrawFuns = new DrawFuns();
                m_DrawFuns.IniDraw(ref panel_draw );
                m_DrawFuns.UpdateDraw();
            }
        }

        private void comboBox_checkMode_SelectedValueChanged(object sender, EventArgs e)
        {
            int nIndex = comboBox_checkMode.SelectedIndex ;
            listBox_checkTime.Items.Clear();

            foreach (StrucheckTime TempStrucheckTime in Checkdtlist)
            {
                if (nIndex == 0)
                {
                    string strTemp = @"[" + TempStrucheckTime.checkmode + "] " + TempStrucheckTime.checktime;
                    listBox_checkTime.Items.Insert(0, strTemp);
                }
                else
                { 
                    if (TempStrucheckTime.checkmode.Substring(0, 1) == (nIndex-1).ToString())
                    {
                        string strTemp = @"[" + TempStrucheckTime.checkmode + "] " + TempStrucheckTime.checktime;

                        listBox_checkTime.Items.Insert(0, strTemp);
                    }
                }
                
               
            }
            
        }

        private void checkBox_Pves_CheckedChanged(object sender, EventArgs e)
        {
            DrawCurve(m_StruDataManage);

        }

        private void checkBox_Pabd_CheckedChanged(object sender, EventArgs e)
        {

            DrawCurve(m_StruDataManage);
        }

        private void checkBox_Pdet_CheckedChanged(object sender, EventArgs e)
        {
            DrawCurve(m_StruDataManage);

        }

        private void comboBox_checkMode_SelectedIndexChanged(object sender, EventArgs e)
        {

        } 

    }
}
