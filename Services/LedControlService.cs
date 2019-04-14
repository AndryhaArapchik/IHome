using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public class LedControlService: ILedControlService
    {
        public static bool _IsOpen { get; set; }
        public bool IsOpen { get { return _IsOpen; } set { _IsOpen = value; } }
        private string port { get; set; }
        private int delay { get; set; }
        private SerialPort serialPort { get; set; }
        public LedControlService(string port)
        {
            delay = 300;
            this.port = port;
            serialPort = new SerialPort
            {
                PortName = port,
                BaudRate = 38400,
                ReadTimeout = 50
            };
        }
        private void Open()
        {
            serialPort.Open();
            _IsOpen = true;
        }
        public void Dispose()
        {
            if (serialPort != null && serialPort.IsOpen)
            {
                serialPort.Close();
                serialPort = null;
            }
            _IsOpen = false;
        }
        public string GetColor()
        {
            byte[] buffer = new byte[3];
            try
            {
                Open();
                serialPort.Write("eeee");
                Thread.Sleep(50);
                serialPort.Write("g");
                Thread.Sleep(50);
                var res = serialPort.Read(buffer, 0, 3);
            }
            catch (Exception)
            {
                Dispose();
                throw;
            }
            finally
            {
                Dispose();
            }
            return string.Format("rgb({0:D3},{1:D3},{2:D3})", Convert.ToInt32(buffer[0]), Convert.ToInt32(buffer[1]), Convert.ToInt32(buffer[2]));
        }
        public void SetColor(string rgbString)
        {
            rgbString = rgbString.Trim();
            rgbString = rgbString.Substring(rgbString.IndexOf('(') + 1);
            rgbString = rgbString.Substring(0, rgbString.IndexOf(')'));
            var rgb = rgbString.Split(',');
            try
            {
                Open();
                SendData(Convert.ToInt32(rgb[0]), Convert.ToInt32(rgb[1]), Convert.ToInt32(rgb[2]));
            }
            catch (Exception)
            {
                Dispose();
                throw;
            }
            finally
            {
                Dispose();
            }
        }

        private void SendData(int r, int g, int b)
        {
            var buffer = new char[3];
            int countRepeat = 0;
            while ((new string(buffer) != "oka") && countRepeat < 5)
            {
                countRepeat++;
                try
                {
                    serialPort.Write("s");
                    byte[] values = new byte[4] { Convert.ToByte(r), Convert.ToByte(g), Convert.ToByte(b), Convert.ToByte('e') };
                    if (serialPort.ReadChar() == 'Y')
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            serialPort.Write(values, i, 1);
                            Thread.Sleep(3);
                        }
                        var res = serialPort.Read(buffer, 0, 3);
                    }
                }
                catch (Exception)
                {
                    serialPort.Write("eeee");
                }
            }
            if (countRepeat > 4)
            {
                throw new Exception();
            }
        }
    }
}
