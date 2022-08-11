using System;
using System.Collections.Generic;
using System.Text;

namespace Monument.Types.Records
{
    public record ExampleRecord
    {
        public int Id { get; set; }

        public ExampleRecord? NestedRecord { get; set; }
    }
}
