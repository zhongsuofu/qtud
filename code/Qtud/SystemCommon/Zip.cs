using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Compression;

using ICSharpCode;
using ICSharpCode.SharpZipLib;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip.Compression; 
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using ICSharpCode.SharpZipLib.GZip;

namespace Qtud.SystemCommon
{
    /// <summary>   
    /// Zip 压缩文件   
    /// </summary>   
    public class Zip
    {
        public Zip()
        {

        }

        #region 压缩文件方法
        /// <summary>   
        /// 功能：压缩文件（暂时只压缩文件夹下一级目录中的文件，文件夹及其子级被忽略）   
        /// </summary>   
        /// <param name="dirPath">被压缩的文件夹夹路径</param>   
        /// <param name="zipFilePath">生成压缩文件的路径，为空则默认与被压缩文件夹同一级目录，名称为：文件夹名+.zip</param>   
        /// <param name="err">出错信息</param>   
        /// <returns>是否压缩成功</returns>   
        public static bool ZipFile(string dirPath, string zipFilePath, out string err)
        {
            err = "";
            if (dirPath == string.Empty)
            {
                err = "要压缩的文件夹不能为空！";
                return false;
            }
            if (!Directory.Exists(dirPath))
            {
                err = "要压缩的文件夹不存在！";
                return false;
            }
            //压缩文件名为空时使用文件夹名＋.zip   
            if (zipFilePath == string.Empty)
            {
                if (dirPath.EndsWith("//"))
                {
                    dirPath = dirPath.Substring(0, dirPath.Length - 1);
                }
                zipFilePath = dirPath + ".zip";
            }

            try
            {
                string[] filenames = Directory.GetFiles(dirPath);
                using (ZipOutputStream s = new ZipOutputStream(File.Create(zipFilePath)))
                {
                    s.SetLevel(9);
                    byte[] buffer = new byte[4096];
                    foreach (string file in filenames)
                    {
                        ZipEntry entry = new ZipEntry(Path.GetFileName(file));
                        entry.DateTime = DateTime.Now;
                        s.PutNextEntry(entry);
                        using (FileStream fs = File.OpenRead(file))
                        {
                            int sourceBytes;
                            do
                            {
                                sourceBytes = fs.Read(buffer, 0, buffer.Length);
                                s.Write(buffer, 0, sourceBytes);
                            } while (sourceBytes > 0);
                        }
                    }
                    s.Finish();
                    s.Close();
                }
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return false;
            }
            return true;
        }
        #endregion

        #region 解压文件方法
        /// <summary>   
        /// 功能：解压zip格式的文件。   
        /// </summary>   
        /// <param name="zipFilePath">压缩文件路径</param>   
        /// <param name="unZipDir">解压文件存放路径,为空时默认与压缩文件同一级目录下，跟压缩文件同名的文件夹</param>   
        /// <param name="err">出错信息</param>   
        /// <returns>解压是否成功</returns>   
        public static bool UnZipFile(string zipFilePath, string unZipDir, out string err)
        {
            err = "";
            if (zipFilePath == string.Empty)
            {
                err = "压缩文件不能为空！";
                return false;
            }
            if (!File.Exists(zipFilePath))
            {
                err = "压缩文件不存在！";
                return false;
            }
            //解压文件夹为空时默认与压缩文件同一级目录下，跟压缩文件同名的文件夹   
            if (unZipDir == string.Empty)
                unZipDir = zipFilePath.Replace(Path.GetFileName(zipFilePath), Path.GetFileNameWithoutExtension(zipFilePath));
            if (!unZipDir.EndsWith("//"))
                unZipDir += "//";
            if (!Directory.Exists(unZipDir))
                Directory.CreateDirectory(unZipDir);

            try
            {
                using (ZipInputStream s = new ZipInputStream(File.OpenRead(zipFilePath)))
                {

                    ZipEntry theEntry;
                    while ((theEntry = s.GetNextEntry()) != null)
                    {
                        string directoryName = Path.GetDirectoryName(theEntry.Name);
                        string fileName = Path.GetFileName(theEntry.Name);
                        if (directoryName.Length > 0)
                        {
                            Directory.CreateDirectory(unZipDir + directoryName);
                        }
                        if (!directoryName.EndsWith("//"))
                            directoryName += "//";
                        if (fileName != String.Empty)
                        {
                            using (FileStream streamWriter = File.Create(unZipDir + theEntry.Name))
                            {

                                int size = 2048;
                                byte[] data = new byte[2048];
                                while (true)
                                {
                                    size = s.Read(data, 0, data.Length);
                                    if (size > 0)
                                    {
                                        streamWriter.Write(data, 0, size);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }//while   
                }
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return false;
            }
            return true;
        }//解压结束  
        #endregion


        /// <summary>
        /// 字符串压缩到字节数组
        /// 返回：已压缩的字节数组
        /// </summary>
        /// <param name="stringToCompress">待压缩的字符串</param>
        /// <returns></returns>
        public   byte[] Compress(string stringToCompress)
        {
            byte[] bytData = Encoding.UTF8.GetBytes(stringToCompress);
            byte[] compressedData = CompressBytes(bytData);
            return compressedData;
        }

        /// <summary>
        /// 字节数组解压缩到字符串
        /// 返回：已压缩的字符串
        /// </summary>
        /// <param name="bytData">待解压缩的字节数组</param>
        /// <returns></returns>
        public   string DeCompress(byte[] bytData)
        {
            byte[] decompressedData = DecompressBytes(bytData);
            return Encoding.UTF8.GetString(decompressedData);
        }

        /// <summary>
        /// 字符串压缩
        /// 返回：已压缩的字符串
        /// </summary>
        /// <param name="stringToCompress">待压缩的字符串</param>
        /// <returns></returns>
        public   string CompressString(string stringToCompress)
        {
            byte[] bytData = Encoding.UTF8.GetBytes(stringToCompress);
            byte[] compressedData = CompressBytes(bytData);
            return Convert.ToBase64String(compressedData);
        }

        /// <summary>
        /// 字符串解压缩
        /// 返回：已压缩的字符串
        /// </summary>
        /// <param name="CompressTostring">待解压缩的字符串</param>
        /// <returns></returns>
        public   string DeCompressString(string CompressTostring)
        {
            byte[] bytData = Convert.FromBase64String(CompressTostring);
            byte[] decompressedData = DecompressBytes(bytData);
            return Encoding.UTF8.GetString(decompressedData);
        }

        /// <summary>
        /// 字节数组压缩
        /// 返回：已压缩的字节数组
        /// </summary>
        /// <param name="data">待压缩的字节数组</param>
        /// <returns></returns>
        public   byte[] CompressBytes(byte[] data)
        {
            ICSharpCode.SharpZipLib.Zip.Compression.Deflater f = new ICSharpCode.SharpZipLib.Zip.Compression.Deflater(ICSharpCode.SharpZipLib.Zip.Compression.Deflater.BEST_COMPRESSION);
            f.SetInput(data);
            f.Finish();

            MemoryStream o = new MemoryStream(data.Length);

            try
            {
                byte[] buf = new byte[1024];
                while (!f.IsFinished)
                {
                    int got = f.Deflate(buf);
                    o.Write(buf, 0, got);
                }
            }
            finally
            {
                o.Close();
            }
            return o.ToArray();
        }

        /// <summary>
        /// 字节数组解压缩
        /// 返回：已解压缩的字节数组
        /// </summary>
        /// <param name="data">待解压缩的字节数组</param>
        /// <returns></returns>
        public   byte[] DecompressBytes(byte[] data)
        {
            Inflater f = new Inflater();
            f.SetInput(data);

            MemoryStream o = new MemoryStream(data.Length);
            try
            {
                byte[] buf = new byte[1024];
                while (!f.IsFinished)
                {
                    int got = f.Inflate(buf);
                    o.Write(buf, 0, got);
                }
            }
            finally
            {
                o.Close();
            }
            return o.ToArray();
        } 
     
    }
}
