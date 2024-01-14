using System;
using System.Collections.Generic;

namespace Manager.Core.Exceptions
{
    public class DomainException : Exception
    {
        private List<string> _erros;

        public IReadOnlyCollection<string> Errors => _erros;

        public DomainException(string message, List<string> erros) : base(message)
        {
            _erros = erros;
        }

        public DomainException(string message) : base(message)
        {
            
        }

        public DomainException(string message, Exception innerException) : base(message, innerException)
        {
            
        }
        
    }
}