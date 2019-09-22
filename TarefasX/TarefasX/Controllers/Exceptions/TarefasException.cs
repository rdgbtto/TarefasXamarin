using System;

namespace TarefasX.Controllers.Exceptions
{
    class TarefasException : ApplicationException
    {
        public TarefasException(string message) : base(message)
        {
        }
    }
}
