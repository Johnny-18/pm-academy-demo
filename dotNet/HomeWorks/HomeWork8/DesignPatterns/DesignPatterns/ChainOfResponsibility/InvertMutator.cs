using System;
using System.Linq;

namespace DesignPatterns.ChainOfResponsibility
{
    public class InvertMutator : IStringMutator
    {
        private IStringMutator _nextMutator;
        
        public IStringMutator SetNext(IStringMutator next)
        {
            _nextMutator = next;

            return next;
        }

        public string Mutate(string str)
        {
            var charArray = str.ToCharArray();
            Array.Reverse(charArray);
            str = new string(charArray);

            if (_nextMutator != null)
            {
                str = _nextMutator.Mutate(str);
            }

            return str;
        }
    }
}