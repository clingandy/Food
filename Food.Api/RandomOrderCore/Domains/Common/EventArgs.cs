using System;

namespace RandomOrderCore.Domains.Common
{
    [Serializable]
    public class EventArgs<T> : EventArgs
    {
        public T Argument;

        public EventArgs() : this(default)
        {
        }

        public EventArgs(T argument)
        {
            Argument = argument;
        }
    }
}
