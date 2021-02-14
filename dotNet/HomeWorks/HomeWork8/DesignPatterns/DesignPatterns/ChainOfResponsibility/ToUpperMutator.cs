using System;

namespace DesignPatterns.ChainOfResponsibility
{
    public class ToUpperMutator : IStringMutator
    {
        private IStringMutator _nextMutator;
        
        public IStringMutator SetNext(IStringMutator next)
        {
            _nextMutator = next;

            return next;
        }

        public string Mutate(string str)
        {
            str = str.ToUpper();
            
            if (_nextMutator != null)
            {
                str = _nextMutator.Mutate(str);
            }

            return str;
        }
    }
}