using System;
using System.Collections.Generic;
using System.Text;


namespace Proto.Standards
{
    public sealed partial class UUID
    {
        public Guid Uuid
        {
            get
            {
                byte[] outArray = new byte[16];
                this.UuidBytes.CopyTo(outArray, 0);
                return new Guid(outArray);
            }
            set
            {
                UuidBytes = Google.Protobuf.ByteString.CopyFrom(value.ToByteArray());
            }
        }
    }
}
