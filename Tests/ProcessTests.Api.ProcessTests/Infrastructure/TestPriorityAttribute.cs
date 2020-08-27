namespace ProcessTests.Api.ProcessTests.Infrastructure
{
    using System;

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    internal class TestPriorityAttribute : Attribute
    {
        public TestPriorityAttribute(int priority)
        {
            Priority = priority;
        }

        public int Priority { get; }
    }
}
