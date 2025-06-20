#region

using System.Net;

#endregion

#region Rfc info

/*
2.2 AAAA data format

   A 128 bit IPv6 address is encoded in the data portion of an AAAA
   resource record in network byte order (high-order byte first).
 */

#endregion

namespace FSDns
{
    public class RecordAAAA : Record
    {
        public IPAddress Address;

        public RecordAAAA(RecordReader rr)
        {
            IPAddress.TryParse(
                string.Format("{0:x}:{1:x}:{2:x}:{3:x}:{4:x}:{5:x}:{6:x}:{7:x}",
                              rr.ReadUInt16(),
                              rr.ReadUInt16(),
                              rr.ReadUInt16(),
                              rr.ReadUInt16(),
                              rr.ReadUInt16(),
                              rr.ReadUInt16(),
                              rr.ReadUInt16(),
                              rr.ReadUInt16()), out Address);
        }

        public override string ToString()
        {
            return Address.ToString();
        }
    }
}