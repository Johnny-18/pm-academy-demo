using System;

namespace DesignPatterns.ChainOfResponsibility
{
    public class TrimMutator : IStringMutator
    {
        private IStringMutator _nextMutator;
        
        public IStringMutator SetNext(IStringMutator next)
        {
            _nextMutator = next;

            return next;
        }

        public string Mutate(string str)
        {
            str = str.Trim();

            if (_nextMutator != null)
            {
                str = _nextMutator.Mutate(str);
            }

            return str;
        }
    }
}