using System;
using System.Collections.Generic;
using System.Text;

namespace Proto.Standards
{
    public sealed partial class DateTimeOffset
    {
        public System.DateTimeOffset RealDate
        {
            get {
                var tick = this.Ticks;
                var offset = this.Offset;

                return new System.DateTimeOffset(ticks: tick, new TimeSpan(0, offset, 0));         
            }
            set
            {
                this.Ticks = value.Ticks;
                this.Offset = Convert.ToInt32(value.Offset.TotalMinutes);
            }
        }
    }
}
