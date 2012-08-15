using System;
using System.Text;


namespace CBB.CheckHelper
{
    public class ByteBuffer
    {
        /// <summary>
        /// Max Buffer Length
        /// </summary>
        private static int MAX_LENGTH = 1024;

        /// <summary>
        /// Current Byte Position
        /// </summary>
        private int _Position;

        /// <summary>
        /// Byte Capacity
        /// </summary>
        private int _Capacity;

        /// <summary>
        /// Bytes data
        /// </summary>
        private byte[] _Buffer;

        /// <summary>
        /// Start position
        /// </summary>
        private int _Start;

        /// <summary>
        /// Init Byte Buffer
        /// </summary>
        /// <param name="capacity">capacity</param>
        /// <param name="position">position</param>
        /// <param name="start">start</param>
        /// <param name="buffer">buffer</param>
        private ByteBuffer(int capacity, int position, int start, byte[] buffer)
        {
            _Capacity = capacity;
            _Position = position;
            _Start = start;
            _Buffer = buffer;
        }

        /// <summary>
        /// Sub Init by capacity
        /// </summary>
        /// <param name="capacity">capacity</param>
        public ByteBuffer(int capacity)
            : this(capacity, 0, 0, new byte[capacity])
        {

        }

        /// <summary>
        /// Sub Init by Byte Data
        /// </summary>
        /// <param name="buffer">buffer</param>
        public ByteBuffer(byte[] buffer)
            : this(buffer.Length, 0, 0, new byte[buffer.Length])
        {

        }

        /// <summary>
        /// Sub Init null
        /// </summary>
        public ByteBuffer()
            : this(MAX_LENGTH, 0, 0, new byte[MAX_LENGTH])
        {

        }

        /// <summary>
        /// Flips this buffer. 
        /// The limit is set to the current position and then the position is set to zero.
        /// If the mark is defined then it is discarded.
        /// </summary>
        public void Flip()
        {
            _Capacity = _Position;
            _Position = _Start;
        }

        /// <summary>
        /// Returns a sliced buffer that shares its content with this buffer.
        /// </summary>
        /// <returns>new TestByteBuffer</returns>
        public ByteBuffer Slice()
        {
            return new ByteBuffer(_Capacity - _Position, 0, _Position, _Buffer);
        }

        /// <summary>
        /// Returns this buffer's position.
        /// </summary>
        /// <returns>_Position</returns>
        public int Position()
        {
            return _Position;
        }

        /// <summary>
        /// Sets this buffer's position.
        /// If the mark is defined and larger than the new position then it is discarded.
        /// </summary>
        /// <param name="pos">pos</param>
        public void Position(int pos)
        {
            this._Position = pos;
        }

        /// <summary>
        /// Returns this buffer's limit.
        /// </summary>
        /// <returns></returns>
        public int Limit()
        {
            return _Capacity;
        }

        /// <summary>
        /// Sets this buffer's limit.
        /// If the position is larger than the new limit then it is set to the new limit.
        /// If the mark is defined and larger than the new limit then it is discarded.
        /// </summary>
        /// <param name="lim">lim</param>
        public void Limit(int lim)
        {
            this._Capacity = lim;
        }

        #region "Read Buffer"

        /// <summary>
        /// Get Single Byte from Buffer.
        /// </summary>
        /// <returns></returns>
        public byte ReadB()
        {
            if (_Capacity >= _Position + 1)
            {
                byte temp = _Buffer[_Position];
                _Position += 1;
                return temp;
            }
            return 0;
        }

        /// <summary>
        /// Get Byte Array from Buffer.
        /// </summary>
        /// <param name="length">length</param>
        /// <returns>byte[]</returns>
        public byte[] ReadB(int length)
        {
            if (_Capacity >= _Position + length)
            {
                byte[] temp = new byte[length];
                for (int i = 0; i < length - 1; i++)
                {
                    temp[i] = _Buffer[_Position];
                    _Position += 1;
                }
                return temp;
            }
            return null;
        }

        /// <summary>
        /// Get short from Buffer.
        /// </summary>
        /// <returns>UInt16</returns>
        public UInt16 ReadH()
        {
            if (_Capacity >= _Position + 2)
            {
                UInt16 temp = BitConverter.ToUInt16(_Buffer, _Position);
                _Position += 2;
                return temp;
            }
            return 0;
        }

        /// <summary>
        /// Get UInt32 From Buffer.
        /// </summary>
        /// <returns>UInt32</returns>
        public UInt32 ReadD()
        {
            if (_Capacity >= _Position + 4)
            {
                UInt32 temp = BitConverter.ToUInt32(_Buffer, _Position);
                _Position += 4;
                return temp;
            }
            return 0;
        }

        /// <summary>
        /// Get UInt64 From Buffer.
        /// </summary>
        /// <returns>UInt64</returns>
        public UInt64 ReadQ()
        {
            if (_Capacity >= _Position + 8)
            {
                UInt64 temp = BitConverter.ToUInt64(_Buffer, _Position);
                _Position += 8;
                return temp;
            }
            return 0;
        }

        /// <summary>
        /// Get Double From Buffer
        /// </summary>
        /// <returns>double</returns>
        public double ReadDF()
        {
            if (_Capacity >= _Position + 8)
            {
                double temp = BitConverter.ToDouble(_Buffer, _Position);
                _Position += 8;
                return temp;
            }
            return 0;
        }

        /// <summary>
        /// Get String From Buffer.
        /// </summary>
        /// <returns>string</returns>
        public string ReadS()
        {
            StringBuilder sb = new StringBuilder();
            char tmp2;

            while ((tmp2 = (char)ReadB()) != 0x00)
            {
                sb.Append(tmp2);
            }
            return sb.ToString();
        }

        #endregion

        #region "Write Buffer"

        /// <summary>
        /// Write single byte to Buffer.
        /// </summary>
        /// <param name="val">byte</param>
        public void WriteB(byte val)
        {
            if (_Capacity >= _Position + 1)
            {
                _Buffer[_Position] = val;
                _Position += 1;
            }
        }

        /// <summary>
        /// Write byte array to Buffer.
        /// </summary>
        /// <param name="val">byte[]</param>
        public void WriteB(byte[] val)
        {
            if (_Capacity >= _Position + val.Length)
            {
                for (int i = 0; i < val.Length; i++)
                {
                    WriteB(val[i]);
                }
            }
        }

        /// <summary>
        /// Write short to Buffer.
        /// </summary>
        /// <param name="val">Int16</param>
        public void WriteH(Int16 val)
        {
            if (_Capacity >= _Position + 2)
            {
                byte[] tmp = new byte[2];
                tmp = BitConverter.GetBytes(val);

                _Buffer[_Position] = tmp[0];
                _Buffer[_Position + 1] = tmp[1];

                _Position += 2;
            }
        }

        /// <summary>
        /// Write short to Buffer.
        /// By specify index.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="val"></param>
        public void WriteH(int index, Int16 val)
        {
            byte[] tmp = new byte[2];
            tmp = BitConverter.GetBytes(val);

            _Buffer[index] = tmp[0];
            _Buffer[index + 1] = tmp[1];
        }

        /// <summary>
        /// Write Int32 to Buffer.
        /// </summary>
        /// <param name="val"></param>
        public void WriteD(Int32 val)
        {
            if (_Capacity >= _Position + 4)
            {
                byte[] tmp = new byte[4];
                tmp = BitConverter.GetBytes(val);

                _Buffer[_Position] = tmp[0];
                _Buffer[_Position + 1] = tmp[1];
                _Buffer[_Position + 2] = tmp[2];
                _Buffer[_Position + 3] = tmp[3];

                _Position += 4;
            }
        }

        /// <summary>
        /// Write Int64 to Buffer.
        /// </summary>
        /// <param name="val">Int64</param>
        public void WriteQ(Int64 val)
        {
            if (_Capacity >= _Position + 8)
            {
                byte[] tmp = new byte[8];
                tmp = BitConverter.GetBytes(val);

                _Buffer[_Position] = tmp[0];
                _Buffer[_Position + 1] = tmp[1];
                _Buffer[_Position + 2] = tmp[2];
                _Buffer[_Position + 3] = tmp[3];
                _Buffer[_Position + 4] = tmp[4];
                _Buffer[_Position + 5] = tmp[5];
                _Buffer[_Position + 6] = tmp[6];
                _Buffer[_Position + 7] = tmp[7];

                _Position += 8;
            }
        }

        /// <summary>
        /// Write Double to Buffer
        /// </summary>
        /// <param name="val"></param>
        public void WriteDF(double val)
        {
            if (_Capacity >= _Position + 8)
            {
                byte[] tmp = new byte[8];
                tmp = BitConverter.GetBytes(val);

                _Buffer[_Position] = tmp[0];
                _Buffer[_Position + 1] = tmp[1];
                _Buffer[_Position + 2] = tmp[2];
                _Buffer[_Position + 3] = tmp[3];
                _Buffer[_Position + 4] = tmp[4];
                _Buffer[_Position + 5] = tmp[5];
                _Buffer[_Position + 6] = tmp[6];
                _Buffer[_Position + 7] = tmp[7];

                _Position += 8;
            }
        }

        /// <summary>
        /// Write String to Buffer.
        /// </summary>
        /// <param name="text">string</param>
        public void WriteS(string text)
        {
            byte[] end = new byte[3] { 0, 0, 0 };
            if (_Capacity >= _Position + (text.Length * 2) + 2)
            {
                char[] data = text.ToCharArray();

                for (int i = 0; i < data.Length; i++)
                {
                    WriteB((byte)data[i]);
                }
                WriteB(end);
            }
        }

        #endregion

        /// <summary>
        /// Get Buffer Byte Array
        /// </summary>
        /// <returns>byte[]</returns>
        public byte[] Array()
        {
            byte[] tmp = new byte[_Capacity];
            for (int i = 0; i < _Capacity; i++)
            {
                tmp[i] = _Buffer[i];
            }
            return tmp;
        }

        /// <summary>
        /// Get Remaining size
        /// </summary>
        /// <returns>int</returns>
        public int Remaining()
        {
            return _Capacity - _Position;
        }

        public int arrayOffset()
        {
            return 0;
        }
    }
}

