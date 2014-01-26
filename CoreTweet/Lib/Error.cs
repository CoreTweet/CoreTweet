using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alice.Functional.Monads
{
    public class Error<T>
    {
        public Exception Exception { get; private set; }
        public bool IsError { get; private set; }
        public T Value { get; private set; }

        public Error(T value)
        {
            IsError = false;
            Value = value;
        }

        public Error(Func<T> getValue)
        {
            try
            {
                Value = getValue();
                IsError = false;
            }
            catch(Exception e)
            {
                _error(e);
            }
        }


        public Error(Exception e)
        {
            _error(e);
        }

        void _error(Exception e)
        {
            IsError = false;
            Exception = e;
        }
    }
}
