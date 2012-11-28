using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CycelAnalystTest
{
    class Program
    {
        static bool _continue;
        static SerialPort _serialPort;
        static bool reset = false;
        static byte curretRead;
        private static byte lastReadByte;
        static int read = 0;
        static byte[] buffer = new byte[256];
        static byte[,] readValues = new byte[32, 8];

        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string name;
            string message;
            StringComparer stringComparer = StringComparer.OrdinalIgnoreCase;

            // Create a new SerialPort object with default settings.
            _serialPort = new SerialPort();

            // Allow the user to set the appropriate properties.
            _serialPort.PortName = "COM6";
            _serialPort.BaudRate = 9600;
            _serialPort.Parity = Parity.None;
            _serialPort.DataBits = 8;
            _serialPort.StopBits = StopBits.One;
            _serialPort.Encoding = System.Text.ASCIIEncoding.ASCII;
            _serialPort.Handshake = Handshake.None;

            // Set the read/write timeouts
            _serialPort.ReadTimeout = 500;
            _serialPort.WriteTimeout = 500;
            _serialPort.ReadBufferSize = 10000;
            _serialPort.WriteBufferSize = 10000;
            _serialPort.DataReceived += _serialPort_DataReceived;

            _serialPort.Open();
            _continue = true;

            Console.WriteLine("Type QUIT to exit");

            while (_continue)
            {
                message = Console.ReadLine();
                if (stringComparer.Equals("quit", message))
                {
                    _continue = false;
                }
                if (stringComparer.Equals("U", message))
                {
                    _serialPort.Write("U");
                }
                if (stringComparer.Equals("reset", message))
                {
                    Reset();
                }
                if (stringComparer.Equals("read", message))
                {
                    ReadData(0x00);
                }
                if (stringComparer.Equals("gc", message))
                {
                    //I'm too lazy to figure this out, so I'll flatten it.
                    byte[] valArray = new byte[256];
                    int add = 0;
                    for (int i = 0; i < 32; i++)
                    {
                        for (int k = 0; k < 8; k++)
                        {
                            valArray[add] = readValues[i, k];
                            add++;
                        }
                    }
                    //Open our text file. 
                    string file = System.IO.File.ReadAllText("memtable.txt");
                    using (StreamWriter writer = File.CreateText("MemMap.cs"))
                    {
                        writer.Write(@"using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
public class MemMap
{

private byte[] valArray;

public MemMap(byte[] vals)
{
valArray = vals;
}
");

                        string[] lines = file.Split('\n');
                        foreach (string line in lines)
                        {
                            //Console.WriteLine(line);
                            string[] cols = line.Split('\t');
                            if (cols.Length > 5)
                            {
                                //foreach (string col in cols)
                                //{
                                //    Console.WriteLine(col);
                                //}
                                writer.WriteLine("/// <summary>");
                                writer.WriteLine("/// " + cols[4]);
                                writer.WriteLine("/// </summary>");
                                string type = "UNKNOWN";
                                int length = Convert.ToInt32(cols[3]);
                                switch (length)
                                {
                                    case 1:
                                        {
                                            type = "byte";
                                            break;
                                        }
                                    case 2:
                                        {
                                            type = "UInt16";
                                            break;
                                        }
                                    case 4:
                                        {
                                            type = "UInt32";
                                            break;
                                        }
                                }

                                writer.WriteLine("public {0} {1}", type, cols[2]);
                                writer.WriteLine("{");
                                writer.WriteLine("\tget{");
                                switch (length)
                                {
                                    case 1:
                                        {
                                            writer.WriteLine("return valArray[{0}];", cols[0]);
                                            break;
                                        }
                                    case 2:
                                        {
                                            writer.WriteLine("return BitConverter.ToUInt16(valArray, {0});", cols[0]);
                                            break;
                                        }
                                    case 4:
                                        {
                                            writer.WriteLine("return BitConverter.ToUInt32(valArray, {0});", cols[0]);
                                            break;
                                        }
                                }
                                writer.WriteLine("\t}");
                                writer.WriteLine("}");
                            }

                        }
                        writer.WriteLine("}");
                        writer.Flush();
                        writer.Close();
                    }
                }
                if (stringComparer.Equals("rst", message))
                {
                    _serialPort.Write(new byte[] { 0x00 }, 0, 1);
                }
                if (stringComparer.Equals("dspb", message))
                {
                    int add = 0;
                    for (int i = 0; i < 32; i++)
                    {
                        for (int k = 0; k < 8; k++)
                        {
                            Console.WriteLine(String.Format("Add {0} value {1}", add, (int)readValues[i, k]));
                            add++;
                        }
                    }
                }
                if (stringComparer.Equals("mm", message))
                {
                    byte[] valArray = new byte[256];
                    int add = 0;
                    for (int i = 0; i < 32; i++)
                    {
                        for (int k = 0; k < 8; k++)
                        {
                            valArray[add] = readValues[i, k];
                            add++;
                        }
                    }
                    MemMap map = new MemMap(valArray);
                }

                if (stringComparer.Equals("readAllBytes", message))
                {
                    for (int i = 0; i < 240; i = i + 8)
                    {
                        ReadData((byte)i);
                        reset = false;
                        int j = 0;
                        while (!reset)
                        {
                            Thread.Sleep(10);
                            j++;
                            if (j > 1500) //15second timeout... I think, I don't math.
                                reset = true;
                        }
                    }
                }
            }
            _serialPort.Close();
        }


        static void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            read += _serialPort.Read(buffer, read, 32);
            if (read > 8)
            {
                // At this point, let's scroll through our bytes until we get to our first index
                for (int i = 0; i < read; i++)
                {
                    if (buffer[i] == 0x55 && buffer[i + 1] == 0x55)
                    {
                        // Start of message, let's find the end.
                        for (int j = i + 4; j < read; j++)
                        {
                            if (buffer[j] == 0x04 && buffer[j - 1] != 0x05) // If we have a 4, that isn't preceded by a 5, we've reach eom
                            {
                                byte[] cutBuffer = buffer.Skip(i + 4).Take(System.Math.Abs((i + 4) - j + 1)).ToArray();
                                // cutBuffer[0] is data length read
                                // [1] is the address read offset. The first two values are always 0, which means to extract the 8 bytes we skip every one. The issue is if we get back a 0x05, it's an escape character, and
                                // therefore needs to be skipped.
                                byte dataVal = cutBuffer[4] == 0x05 ? cutBuffer[5] : cutBuffer[4];
                                byte[] values = new byte[8];
                                int valRegister = 0;
                                int currentAdd = 0;
                                if ((int)cutBuffer[1] != 0)
                                    currentAdd = (int)cutBuffer[1] / 8;
                                for (int k = 4; k < 19; k++)
                                {
                                    //At position 1, we have a garunteed value, if it is 0, we skip 1.
                                    readValues[currentAdd, valRegister] = cutBuffer[k] == 0x05 ? cutBuffer[k + 1] : cutBuffer[k];
                                    if (cutBuffer[k] == 0x05) // If we have an escape, skip two
                                    {
                                        k = k + 2;
                                    }
                                    else
                                    {
                                        k++;
                                    }
                                    valRegister++;
                                }
                                Console.WriteLine(String.Format("Response read for byte {0} value {1}", cutBuffer[1], dataVal));
                                lastReadByte = dataVal;
                                byte[] bufferSwap = new byte[256];
                                buffer.Skip(j).ToArray().CopyTo(bufferSwap, 0);
                                buffer = bufferSwap;
                                i = 0;
                                //ReadData((byte)(((int)cutBuffer[1])+1));
                                reset = true;
                                break;
                                // We've reached a value, let's reset i to 0 after we reset our buffer
                            }
                        }
                    }
                }
            }
            if (ASCIIEncoding.ASCII.GetString(buffer).Contains('\n') || read > 200)
            {
                read = 0;
                buffer = new byte[256];
            }
        }

        public static void ReadData(byte addressByte)
        {
            curretRead = addressByte;
            byte[] data = { 0x04, 0x08, addressByte, 0x00, 0x00 };
            byte csum = Chk(data);
            byte[] start = { 0x55, 0x55, 0x05 };
            byte[] end = { 0x04 };
            _serialPort.Write(start, 0, start.Length);
            _serialPort.Write(data, 0, data.Length);
            _serialPort.Write(new byte[] { csum }, 0, 1);
            _serialPort.Write(end, 0, end.Length);
        }

        public static void Reset()
        {
            byte[] resetBytes = { 0x55, 0x55, 0x00, 0x00, 0x00, 0x04 };
            _serialPort.Write(resetBytes, 0, resetBytes.Length);
        }


        static byte Chk(byte[] data)
        {
            byte sum = 0;
            foreach (byte b in data)
            {
                sum += b;
            }
            sum = (byte)~sum;
            sum++;
            return sum;
        }

    }
}
