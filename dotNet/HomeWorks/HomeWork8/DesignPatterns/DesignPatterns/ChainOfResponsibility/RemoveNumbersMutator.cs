using System;
using System.Linq;

namespace DesignPatterns.ChainOfResponsibility
{
    public class RemoveNumbersMutator : IStringMutator
    {
        private IStringMutator _nextMutator;
        
        public IStringMutator SetNext(IStringMutator next)
        {
            _nextMutator = next;

            return next;
        }

        public string Mutate(string str)
        {
            var charArray = str.Where(x => !char.IsDigit(x)).ToArray();
            str = new string(charArray);

            if (_nextMutator != null)
            {
                str = _nextMutator.Mutate(str);
            }

            return str;
        }
    }
}