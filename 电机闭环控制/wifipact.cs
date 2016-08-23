using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace 电机闭环控制
{

    /// <summary>
    /// 与ESP8622WIFI通讯
    /// </summary>
    public class wifipact
    {

        public List<EquiPmentData> Equi = new List<EquiPmentData>();
        /// <summary>
        /// 选中的设备
        /// </summary>
        public int devSelect = -1;//选中的设备
        int Port = 2333;//目标端口

        /// <summary>
        /// 重新发送次数
        /// </summary>
        public uint timeOutCount = 99;//超时重发次数，超过不发

        /// <summary>
        /// 设置命令条目数
        /// </summary>
        public int setComEntry = 0;
        /// <summary>
        /// 获取命令条目数
        /// </summary>
        public int getComEntry = 0;

        /// <summary>
        /// 设置命令注册
        /// </summary>
        public void setComRegistered(int s)
        {
        }

        /// <summary>
        /// 读取buffpack区32位
        /// </summary>
        UInt16 readpackbuff = 0;
        public Int32 ReadInt32()
        {
            int v;
            v = packbuff[readpackbuff++];
            v <<= 8;
            v |= packbuff[readpackbuff++];
            v <<= 8;
            v |= packbuff[readpackbuff++];
            v <<= 8;
            v |= packbuff[readpackbuff++];
            return v;
        }
        /// <summary>
        /// 读取buffpack区16位
        /// </summary>
        public Int16 ReadInt16()
        {
            Int16 v;
            int s = 0;
            v = packbuff[readpackbuff++];
            v <<= 8;
            v |= packbuff[readpackbuff++];
            return v;
        }
        /// <summary>
        /// 读取buffpack区8位
        /// </summary>
        public byte ReadInt8()
        {
            byte v;
            v = packbuff[readpackbuff++];
            return v;
        }


        public void SelectEqui(int i)
        {
            devSelect = i;
            string str = "useIP()";
            byte[] by = Encoding.Default.GetBytes(str);
            Search.Send(by, by.Length, new IPEndPoint(Equi[devSelect].ip.Address, Port));

        }




        public delegate void getComBack(wifipact e);
        getComBack[] getComEvent;
        /// <summary>
        /// 获取命令注册
        /// </summary>
        public void getComRegistered(int s, getComBack e)
        {
            getComEvent[s] = e;
        }


        bool[] concmd;
        public void conGatCmd(byte gcmd)
        {
            getData(gcmd);
            concmd[gcmd] = true;
        }
        public void stopConGatCmd(byte gcmd)
        {
            concmd[gcmd] = false;
        }

        private byte sendID = 0;
        byte[][] sendarraydata;// = new byte[maxSdevEntry][];
        byte[] sendarrayid;// = new byte[maxSdevEntry];
        int[] sendTimeOutCount;// = new int[maxSdevEntry];//超时次数，超过不发

        private byte recvID = 0;
        byte[][] recvarraydata;// = new byte[maxGdevEntry][];
        byte[] recvarrayid;// = new byte[maxGdevEntry];
        int[] recvTimeOutCount;// = new int[maxGdevEntry];

        public wifipact(int setNames, int getNames, int po = 2333)
        {
            Port = po;
            setComEntry = setNames;
            getComEntry = getNames;

            sendarraydata = new byte[setComEntry][];
            sendarrayid = new byte[setComEntry];
            sendTimeOutCount = new int[setComEntry];

            recvarraydata = new byte[getComEntry][];
            recvarrayid = new byte[getComEntry];
            recvTimeOutCount = new int[getComEntry];

            getComEvent = new getComBack[getComEntry];

            concmd = new bool[getComEntry];


            //接收线程
            new Task(() =>
            {
                //while (!this.IsDisposed)
                while (true)
                    try
                    {
                        IPEndPoint p = new IPEndPoint(IPAddress.Any, 0);
                        byte[] buff = Search.Receive(ref p);
                        lasttarget = p.Address;
                        string value = Encoding.Default.GetString(buff);
                        int len = buff.Length;
                        for (uint l = 0; l < len; l++)
                        {
                            switch (recvmode)
                            {
                                case 0:
                                    if (buff[l] == 0xaa)
                                        recvmode = 1;
                                    break;
                                case 1:
                                    packlen = buff[l];
                                    recvmode = 2;
                                    break;
                                case 2:
                                    packlen <<= 8;
                                    packlen |= buff[l];
                                    RecvCheck = 0;
                                    count = 0;
                                    if (packlen > maxpacklen)
                                        recvmode = 0;
                                    else
                                        recvmode = 3;
                                    break;
                                case 3:
                                    recvcmd = buff[l];
                                    if (packlen != 0)
                                        recvmode = 7;
                                    else
                                        recvmode = 6;
                                    break;
                                case 4:
                                    packbuff[count] = buff[l];
                                    RecvCheck += buff[l];
                                    count++;
                                    if (count >= packlen)
                                        recvmode = 5;
                                    break;
                                case 5:
                                    if (buff[l] == RecvCheck)
                                    {
                                        recvmode = 6;
                                    }
                                    else
                                    {
                                        recvmode = 0;
                                        RecvFlag = 0;
                                    }
                                    break;
                                case 6:
                                    if (buff[l] == 0x55)
                                    {
                                        recvmode = 0;
                                        RecvFlag = 1;
                                    }
                                    else
                                    {
                                        recvmode = 0;
                                        RecvFlag = 0;
                                    }
                                    break;
                                case 7:
                                    recvid = buff[l];
                                    RecvCheck += buff[l];
                                    recvmode = 8;
                                    break;
                                case 8:
                                    recvdev = buff[l];
                                    RecvCheck += buff[l];
                                    packlen -= 2;
                                    if (packlen == 0)
                                        recvmode = 5;
                                    else
                                        recvmode = 4;

                                    break;
                            }
                        }
                        if (scannBut == true)
                        {
                            int index = Equi.FindIndex(s => s.ip.Address == p.Address.Address);

                            if (index != -1)
                            {
                                if (RecvFlag == 1)
                                {
                                    RecvFlag = 0;
                                    string s = Encoding.Default.GetString(packbuff, 0, packlen);
                                    Equi[index].type = s;
                                }
                            }
                            else
                            {
                                int pos = value.IndexOf("ID:");
                                if (pos != -1)
                                {
                                    string str = "";
                                    for (int i = 0; i < value.Length; i++)
                                    {
                                        if (char.IsDigit(value, i))
                                        {
                                            str += value[i];
                                        }
                                    }
                                    EquiPmentData bufEqui = new EquiPmentData();

                                    bufEqui.idstr = str;
                                    bufEqui.id = int.Parse(str);
                                    bufEqui.ip = p.Address;
                                    bufEqui.num = scannNum++;
                                    Equi.Add(bufEqui);
                                }
                            }
                        }
                        else
                        {
                            if (RecvFlag == 1)//得到数据
                            {
                                RecvFlag = 0;
                                readpackbuff = 0;
                                if ((cmd)recvcmd == cmd.setdata)//确认序列号
                                {
                                    if (sendarrayid[recvdev] == recvid)//发送设置确认成功
                                    {
                                        sendarraydata[recvdev] = null;
                                        System.Console.WriteLine(recvid);
                                    }
                                }
                                else if ((cmd)recvcmd == cmd.getdata)
                                {
                                    if (recvarrayid[recvdev] == recvid)
                                    {
                                        recvarraydata[recvdev] = null;
                                        getComEvent[recvdev](this);

                                        if(concmd[recvdev]==true)
                                        {
                                            getData(recvdev);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch { }
            }).Start();
            //发送服务
            new Task(() =>
            {
                while (true)
                {
                    try
                    {
                        SendSerive();
                    }
                    catch
                    { }
                    Thread.Sleep(100);
                }
            }).Start();
        }
        /// <summary>
        /// 发送服务
        /// </summary>
        void SendSerive()
        {
            if (scannBut == true)
            {
                scannCount++;
                lasttarget = new IPAddress(0xffffffff);
                GetIPID();
                if (scannCount >= 5)
                {
                    scannCount = 0;
                    scannBut = false;

                    Equi.Sort(delegate (EquiPmentData a, EquiPmentData b) { return a.id.CompareTo(b.id); });//排序
                    for (int i = 0; i < Equi.Count; i++)
                    {
                        Equi[i].num = i;
                    }
                    SDelegate();
                }
            }


            if (Equi.Count == 0) return;
            if (devSelect == -1) return;
            for (int i = 0; i < recvarraydata.Length; i++)
            {
                if (recvarraydata[i] != null)
                {
                    Search.Send(recvarraydata[i], recvarraydata[i].Length, new IPEndPoint(Equi[devSelect].ip.Address, Port));

                    recvTimeOutCount[i]++;
                    if (recvTimeOutCount[i] >= timeOutCount)
                    {
                        recvTimeOutCount[i] = 0;
                        recvarraydata[i] = null;
                    }
                }
            }
            for (int i = 0; i < sendarraydata.Length; i++)
            {
                if (sendarraydata[i] != null)
                {
                    Search.Send(sendarraydata[i], sendarraydata[i].Length, new IPEndPoint(Equi[devSelect].ip.Address, Port));

                    sendTimeOutCount[i]++;
                    if (sendTimeOutCount[i] >= timeOutCount)
                    {
                        sendTimeOutCount[i] = 0;
                        sendarraydata[i] = null;
                    }
                }
            }
        }
        void GetIPID()
        {
            string str = "print(\"ID:\"..node.chipid())";
            byte[] by = Encoding.Default.GetBytes(str);
            Search.Send(by, by.Length, new IPEndPoint(lasttarget.Address, Port));
        }
        /// <summary>
        /// 命令号
        /// </summary>
        public enum cmd
        {
            alive = 0,
            getdata,
            setdata,
        }






        IPAddress lasttarget;
        

     


        public UdpClient Search = new UdpClient(0);

        bool scannBut = false;

        byte recvdev;
        byte recvid;
        uint RecvFlag;//接收解码成功标志位
        uint recvmode;
        UInt16 packlen;
        byte RecvCheck;
        const uint maxpacklen = 1024;
        byte[] packbuff = new byte[maxpacklen];
        uint count;
        uint recvcmd;//读到的CMD

        uint scannCount = 0;//扫描次数
        int scannNum = 0;

        public delegate void scannDelegate();
        scannDelegate SDelegate;
        public void scann(scannDelegate e)
        {
            Equi.Clear();
            scannBut = true;
            SDelegate = e;
        }
        /// <summary>
        /// 数据设置，带确认
        /// </summary>
        /// <param name="sdev"></param>
        /// <param name="list"></param>
        public void setData(byte sdev, params object[] list)
        {
            List<byte> buff = new List<byte>();

            buff.Add(sendID);
            buff.Add((byte)sdev);

            foreach (object obj in list)
            {
                Type typ = obj.GetType();
                if (typ == typeof(byte))
                {
                    buff.Add((byte)obj);
                }
                else if (typ == typeof(Int16))
                {
                    Int16 i = (Int16)obj;
                    buff.Add((byte)(i >> 8));
                    buff.Add((byte)(i));
                }
                else if (typ == typeof(int))
                {
                    int i = (int)obj;
                    buff.Add((byte)(i >> 24));//发送为大端模式
                    buff.Add((byte)(i >> 16));
                    buff.Add((byte)(i >> 8));
                    buff.Add((byte)(i));
                }
            }

            byte[] by = Encode(cmd.setdata, buff.ToArray());
            sendTimeOutCount[(byte)sdev] = 0;
            System.Console.WriteLine("r:" + sendID);
            sendarrayid[(byte)sdev] = sendID++;
            sendarraydata[(byte)sdev] = by;
            

        }
        /// <summary>
        /// 发送获取数据命令，带确认
        /// </summary>
        /// <param name="gdev"></param>
        public void getData(byte gdev)
        {
            byte[] buff = new byte[2];
            buff[0] = recvID;
            buff[1] = (byte)gdev;
            byte[] by = Encode(cmd.getdata, buff);
            recvTimeOutCount[(byte)gdev] = 0;
            recvarrayid[(byte)gdev] = recvID++;
            recvarraydata[(byte)gdev] = by;
        }
        /// <summary>
        /// 发送数据，不确认
        /// </summary>
        /// <param name="Cmd"></param>
        /// <param name="data"></param>
        public void sendData(cmd Cmd, byte[] data = null)
        {
            byte[] d;
            d = Encode(Cmd, data);
            Search.Send(d, d.Length, new IPEndPoint(lasttarget.Address, Port));
        }
        /// <summary>
        /// 加码  将数据加入协议中
        /// </summary>
        /// <param name="Cmd"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public byte[] Encode(cmd Cmd, byte[] data = null)
        {
            byte[] d = new byte[(data == null ? 0 + 5 : data.Length + 6)];
            int i = 0;
            byte sum = 0;
            d[i++] = 0xaa;
            if (data != null)
            {
                d[i++] = (byte)(data.Length / 256);
                d[i++] = (byte)data.Length;
            }
            else
            {
                d[i++] = 0;
                d[i++] = 0;

            }
            d[i++] = (byte)Cmd;

            if (data != null)
            {
                foreach (byte b in data)
                {
                    sum += b;
                    d[i++] = b;
                }
                d[i++] = sum;
            }
            d[i++] = 0x55;

            return d;
        }

        public class EquiPmentData
        {
            public int num;
            public int id;
            public string idstr;
            public IPAddress ip;
            public string type;
        }
    }
}
