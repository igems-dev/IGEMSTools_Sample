using HidSharp;
using System.IO;

namespace StraighterSample
{

    //This class uses HidSharp. Read more here: https://www.zer7.com/software/hidsharp

    class GSensor
    {
        //private static HidDevice _device;
        private readonly int _vendorId;
        private readonly int _productId;


        private static HidStream _hidStream;
        private static DeviceList _list;


        private string _errorMessage = string.Empty;

        public event EventHandler SensorFoundAgain;
        public event EventHandler SensorLost;

        private const int VendorIdStraighter = 0x04d8;
        private const int ProductIdStraighter = 0xec73;

        public GSensor(int vendorId = VendorIdStraighter, int productId = ProductIdStraighter)
        {
            _vendorId = vendorId;
            _productId = productId;
        }


        public string ErrorMessage { get { return _errorMessage; } }
        public bool Error { get; private set; }



        bool connected;
        public bool Connected
        {
            get => connected;
            set => connected = value;
        }

        /// <summary>
        /// Set the filter in the sensor
        /// </summary>
        /// <param name="filterIndex">0 = 0.125 Hz, 1 = 0.25 Hz, 2 = 0.5 Hz, 3 = 1.0 Hz, 4 = 2 Hz, 5 = 4 Hz, 6 = 8 Hz, 7 = 16 Hz, 8 = 32 Hz</param>
        public void SetFilter(byte filterIndex)
        {
            SetParameter(ParameterNumbers.FilterIndex, filterIndex);
        }

        public void SetZero()
        {
            SetParameter(ParameterNumbers.SetZero, 1);
        }

        private void SetParameter(ParameterNumbers pn, byte value)
        {
            byte[] sendarray = new byte[4];

            sendarray[0] = 0; // endpoint #
            sendarray[1] = 1; // read or write
            sendarray[2] = (byte)pn; // Parameter number
            sendarray[3] = value;

            _hidStream?.Write(sendarray);
        }

        public void GetXY(out double x, out double y)
        {
            byte[] data = GetParameterValue(ParameterNumbers.XYAngle);

            if (data == null || data[2] != 4) //4 means XY data will follow
            {
                x = 0.0;
                y = 0.0;
                return;
            }

            byte[] xArr = new byte[4];
            Array.Copy(data, 3, xArr, 0, 4);
            Array.Reverse(xArr);

            byte[] yArr = new byte[4];
            Array.Copy(data, 7, yArr, 0, 4);
            Array.Reverse(yArr);

            int xUnscaled = BitConverter.ToInt32(xArr, 0);
            x = xUnscaled / 1000.0;

            int yUnscaled = BitConverter.ToInt32(yArr, 0);
            y = yUnscaled / 1000.0;
        }

        public int GetSerial()
        {
            byte[] data = GetParameterValue(ParameterNumbers.SerialNumber);

            byte[] serialArr = new byte[4];
            Array.Copy(data, 2, serialArr, 0, 4);
            Array.Reverse(serialArr);

            int serial = BitConverter.ToInt32(serialArr, 0);
            return serial;
        }



        private byte[] GetParameterValue(ParameterNumbers parameter)
        {
            if (_hidStream == null)
                return null;

            byte[] sendarray = new byte[3];

            sendarray[0] = 0; // endpoint #
            sendarray[1] = 0; // read
            sendarray[2] = (byte)parameter; // Parameter number

            try
            {
                _hidStream.Write(sendarray);

                byte[] data = _hidStream.Read();

                return data;

            }
            catch (Exception)
            {
                //Device probably disconnected
                return null;
            }

        }


        private enum ParameterNumbers
        {
            ProductId = 0x00,
            SerialNumber = 0x01,
            XAngle = 0x02,
            YAngle = 0x03,
            XYAngle = 0x04,
            Angle360 = 0x05,
            SensorTemperature = 0x06,
            FilterIndex = 0x07,
            XAxisDirection = 0x08,
            YAxisDirection = 0x09,
            Direction360 = 0x0A,
            SingleOrDualAxis = 0x0B,
            SetZero = 0x0C
        }






        public bool Connect()
        {
            _list = DeviceList.Local;
            _list.Changed += List_Changed;



            var device = _list.GetHidDevices(_vendorId, _productId).FirstOrDefault();
            
            if (device == null)
                return false;

            if (!device.TryOpen(out var hidStream))
            {
                Console.WriteLine("Failed to open device.");
                return false;
            }

            _hidStream = hidStream;
            _hidStream.ReadTimeout = Timeout.Infinite;

            connected = true;

            return true;
        }

        private void List_Changed(object sender, DeviceListChangedEventArgs e)
        {
            var device = _list.GetHidDevices(_vendorId, _productId).FirstOrDefault();


            if (device == null)
            {
                Connected = false;
                SensorLost(this, e);
            }
            else
            {
                Connected = true;
                SensorFoundAgain(this, e);
            }
        }

        public void Dispose()
        {
            if (_hidStream == null)
                return;

            _hidStream.Close();

            _hidStream.Dispose();
        }
    }
}
