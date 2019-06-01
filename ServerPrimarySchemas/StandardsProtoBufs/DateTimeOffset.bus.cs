using System;
using System.Collections.Generic;
using System.Text;

namespace StandardsProto
{
    public sealed partial class ProtoDateTimeOffset
    {
        public DateTimeOffset RealDate
        {
            get {
                var tick = this.Ticks;
                var offset = this.Offset;

                return new DateTimeOffset(ticks: tick, new TimeSpan(0, offset, 0));         
            }
            set
            {
                this.Ticks = value.Ticks;
                this.Offset = Convert.ToInt32(value.Offset.TotalMinutes);
            }
        }
    }
}
