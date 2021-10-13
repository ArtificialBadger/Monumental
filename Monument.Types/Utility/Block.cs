using System;
using System.Collections.Generic;
using System.Text;

namespace Monument.Types.Utility
{
    public readonly struct Block
    {
        public Block(double width, double height, double depth)
        {
            Width = width;
            Height = height;
            Depth = depth;
        }

        public double Width { get; }
        public double Height { get; }
        public double Depth { get; }
    }
}
