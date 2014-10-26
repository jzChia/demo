using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Net;
using SharpPcap;
using System.Threading;

namespace TransferManager
{
    public class ProcessPerformanceInfo : IDisposable
    {
        public int ProcessID { get; set; }//进程ID
        public string ProcessName { get; set; }//进程名
        public long NetSendBytes { get; set; }//网络发送数据字节数
        public long NetRecvBytes { get; set; }//网络接收数据字节数
        public long NetTotalBytes { get; set; }//网络数据总字节数
        public List<ICaptureDevice> CapDevs = new List<ICaptureDevice>();
        public long AccNetSendBytes { get; set; }
        public long AccNetRecvBytes { get; set; }
        
        ///</summary>
        /// 实现IDisposable的方法
        /// </summary>
        public void Dispose()
        {
            foreach (ICaptureDevice dev in CapDevs)
            {
                dev.StopCapture();
                dev.Close();
            }
        }
    }

    public class CaptureHelper
    {
        public ProcessPerformanceInfo ProcInfo { get; set; }

        public CaptureHelper(int processID) 
        {
            ProcInfo = new ProcessPerformanceInfo();
            ProcInfo.ProcessID = processID;
        }

        public void Initialize() 
        {
            int pid = ProcInfo.ProcessID;
            //存放进程使用的端口号链表
            List<int> ports = new List<int>();

            #region 获取指定进程对应端口号
            Process pro = new Process();
            pro.StartInfo.FileName = "cmd.exe";
            pro.StartInfo.UseShellExecute = false;
            pro.StartInfo.RedirectStandardInput = true;
            pro.StartInfo.RedirectStandardOutput = true;
            pro.StartInfo.RedirectStandardError = true;
            pro.StartInfo.CreateNoWindow = true;
            pro.Start();
            pro.StandardInput.WriteLine("netstat -ano");
            pro.StandardInput.WriteLine("exit");
            Regex reg = new Regex("\\s+", RegexOptions.Compiled);
            string line = null;
            ports.Clear();
            while ((line = pro.StandardOutput.ReadLine()) != null)
            {
                line = line.Trim();
                if (line.StartsWith("TCP", StringComparison.OrdinalIgnoreCase))
                {
                    line = reg.Replace(line, ",");
                    string[] arr = line.Split(',');
                    if (arr[4] == pid.ToString())
                    {
                        string soc = arr[1];
                        int pos = soc.LastIndexOf(':');
                        int pot = int.Parse(soc.Substring(pos + 1));
                        ports.Add(pot);
                    }
                }
                else if (line.StartsWith("UDP", StringComparison.OrdinalIgnoreCase))
                {
                    line = reg.Replace(line, ",");
                    string[] arr = line.Split(',');
                    if (arr[3] == pid.ToString())
                    {
                        string soc = arr[1];
                        int pos = soc.LastIndexOf(':');
                        int pot = int.Parse(soc.Substring(pos + 1));
                        ports.Add(pot);
                    }
                }
            }
            pro.Close();
            #endregion

            //获取本机IP地址
            IPAddress[] addrList = Dns.GetHostAddresses(Dns.GetHostName());

            string IP = string.Empty;
            for (int i = 0; i < addrList.Length; i++) 
            {
                if (addrList[i].AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork) 
                {
                    IP = addrList[i].ToString();
                    break;
                }
            }
           
            var devices = CaptureDeviceList.Instance;
            int count = devices.Count;
            if (count < 1)
            {
                Console.WriteLine("No device found on this machine");
                return;
            }

            //开始抓包
            
            for (int i = 0; i < count; ++i)
            {
                if (ports.Count != 0)
                {
                    for (int j = 0; j < ports.Count; ++j)
                    {
                        CaptureFlowRecv(IP, ports[j], i);
                        CaptureFlowSend(IP, ports[j], i);
                    }
                }
                else 
                {
                    CaptureFlowSend(IP, i);
                    CaptureFlowRecv(IP, i);
                }
            }
        }

        public void CaptureFlowSend(string IP, int deviceID)
        {
            ICaptureDevice device = (ICaptureDevice)CaptureDeviceList.New()[deviceID];
            device.OnPacketArrival += new PacketArrivalEventHandler(device_OnPacketArrivalSend);
            int readTimeoutMilliseconds = 100;
            device.Open(DeviceMode.Promiscuous, readTimeoutMilliseconds);
            string filter = "src host " + IP;
            device.Filter = filter;
            device.StartCapture();
            ProcInfo.CapDevs.Add(device);
        }

        public void CaptureFlowRecv(string IP, int deviceID)
        {
            ICaptureDevice device = CaptureDeviceList.New()[deviceID];
            device.OnPacketArrival += new PacketArrivalEventHandler(device_OnPacketArrivalRecv);
            int readTimeoutMilliseconds = 100;
            device.Open(DeviceMode.Promiscuous, readTimeoutMilliseconds);
            string filter = "dst host " + IP;
            device.Filter = filter;
            device.StartCapture();
            ProcInfo.CapDevs.Add(device);
        }

        public void CaptureFlowSend(string IP, int portID, int deviceID)
        {
            ICaptureDevice device = (ICaptureDevice)CaptureDeviceList.New()[deviceID];
            device.OnPacketArrival += new PacketArrivalEventHandler(device_OnPacketArrivalSend);
            int readTimeoutMilliseconds = 0;
            device.Open(DeviceMode.Promiscuous, readTimeoutMilliseconds);
            string filter = "src host " + IP + " and src port " + portID;
            device.Filter = filter;
            device.StartCapture();
            ProcInfo.CapDevs.Add(device);
        }

        public void CaptureFlowRecv(string IP, int portID, int deviceID)
        {
            ICaptureDevice device = CaptureDeviceList.New()[deviceID];
            device.OnPacketArrival += new PacketArrivalEventHandler(device_OnPacketArrivalRecv);
            int readTimeoutMilliseconds = 0;
            device.Open(DeviceMode.Promiscuous, readTimeoutMilliseconds);
            string filter = "dst host " + IP + " and dst port " + portID;
            device.Filter = filter;
            device.StartCapture();
            ProcInfo.CapDevs.Add(device);
        }

        private void device_OnPacketArrivalSend(object sender, CaptureEventArgs e)
        {
            ProcInfo.NetSendBytes += e.Packet.Data.LongLength;
            ProcInfo.AccNetSendBytes += e.Packet.Data.LongLength;
        }

        private void device_OnPacketArrivalRecv(object sender, CaptureEventArgs e)
        {
            ProcInfo.NetRecvBytes += e.Packet.Data.LongLength;
            ProcInfo.AccNetRecvBytes += e.Packet.Data.LongLength;
        }

        public void Reset() 
        {
            ProcInfo.AccNetRecvBytes = 0;
            ProcInfo.AccNetSendBytes = 0;
        }

        public void RefershInfo()
        {
            ProcInfo.NetRecvBytes = 0;
            ProcInfo.NetSendBytes = 0;
            ProcInfo.NetTotalBytes = 0;
            Thread.Sleep(100);
            ProcInfo.NetTotalBytes = ProcInfo.NetRecvBytes + ProcInfo.NetSendBytes;
        }
    }
}
