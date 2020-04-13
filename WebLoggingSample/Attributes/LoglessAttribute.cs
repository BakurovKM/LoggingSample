using System;
using System.Collections.Generic;

namespace WebLoggingSample.Attributes
{
    public class LoglessAttribute: Attribute
    {
        public readonly List<string> Properties = new List<string>();

        public LoglessAttribute(params string[] args)
        {
            Properties.AddRange(args);
        }
    }
}
