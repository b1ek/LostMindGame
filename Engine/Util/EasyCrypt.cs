using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LostMind.Engine.Util {
    class ByteData {
        #region Value
        public byte[] bytesVal;
        public string stringVal;
        public string StringValue => stringVal;
        public byte[] BytesValue => bytesVal;
        #endregion
        #region Constructor
        #region byte[]
        public ByteData(byte[] value) {
            bytesVal = value;
            stringVal = Encoding.Default.GetString(value);
        }
        public ByteData(byte[] value, Encoding en) {
            bytesVal = value;
            stringVal = en.GetString(value);
        }
        #endregion
        #region string
        public ByteData(string value) {
            bytesVal = GetBytes(value);
            stringVal = value;
        }
        public ByteData(string value, Encoding en) {
            bytesVal = GetBytes(value, en);
            stringVal = value;
        }
        #endregion
        #endregion
        #region Get Bytes/String()
        public static byte[] GetBytes(string s) =>   Encoding.Default.GetBytes(s);
        public static byte[] GetBytes(string s, Encoding en) =>    en.GetBytes(s);
        public static string GetString(byte[] b) => Encoding.Default.GetString(b);
        public static string GetString(byte[] b, Encoding en) =>  en.GetString(b);
        #endregion
        #region Conversion function
        public static implicit operator ByteData (string s) => new ByteData(s);
        public static implicit operator ByteData (byte[] v) => new ByteData(v);
        public static implicit operator string (ByteData b) => b.StringValue;
        public static implicit operator byte[] (ByteData b) => b.BytesValue;
        #endregion
    }
    static class EasyCrypt {
        #region MD5
        static MD5 md5 = null;
        public static ByteData GetMD5(ByteData data) {
            if (md5 == null)
                md5 = MD5.Create();
            ByteData hash = md5.ComputeHash(data.BytesValue);
            return hash;
        }
        public static byte[] GetMD5Bytes(ByteData data) {
            if (md5 == null)
                md5 = MD5.Create();
            ByteData hash = md5.ComputeHash(data.BytesValue);
            return hash.BytesValue;
        }
        public static string GetMD5String(ByteData data) {
            if (md5 == null)
                md5 = MD5.Create();
            ByteData hash = md5.ComputeHash(data.BytesValue);
            return hash.StringValue;
        }
        #endregion
    }
}
