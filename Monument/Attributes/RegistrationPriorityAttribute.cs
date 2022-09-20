using System;
using System.Collections.Generic;
using System.Text;

namespace Monument.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class RegistrationPriorityAttribute : Attribute
    {
        public const int MAX_PRIORITY = int.MaxValue;
        public const int VERY_HIGH_PRIORITY = 1000;
        public const int HIGH_PRIORITY = 500;
        public const int NORMAL_PRIORITY = 100;
        public const int LOW_PRIORITY = 10;
        public const int VERY_LOW_PRIORITY = 0;
        public const int MIN_PRIORITY = int.MinValue;

        internal int Priority { get; set; }

        public RegistrationPriorityAttribute(int priority = NORMAL_PRIORITY)
        {
            this.Priority = priority;
        }
    }
}
