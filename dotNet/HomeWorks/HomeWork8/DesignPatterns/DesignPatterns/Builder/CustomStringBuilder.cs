using System;

namespace DesignPatterns.Builder
{
    public class CustomStringBuilder : ICustomStringBuilder
    {
        private char[] _charArray;

        public int Capacity { get; private set; }

        public CustomStringBuilder()
        {
            Capacity = 0;
            _charArray = new char[Capacity];
        }

        public CustomStringBuilder(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                Capacity = 0;
                _charArray = new char[Capacity];
            }
            else
            {
                Capacity = text.Length;
                _charArray = new char[Capacity];
                Array.Copy(text.ToCharArray(), _charArray, Capacity);
            }
        }

        public ICustomStringBuilder Append(string str)
        {
            var length = Capacity;
            Capacity += str.Length;
            
            Array.Resize(ref _charArray, Capacity);
            
            Array.Copy(str.ToCharArray(), 0, _charArray, length, str.Length);
            
            return this;
        }

        public ICustomStringBuilder Append(char ch)
        {
            Capacity++;
            
            Array.Resize(ref _charArray, Capacity);
            
            _charArray[Capacity - 1] = ch;

            return this;
        }

        public ICustomStringBuilder AppendLine()
        {
            return Append('\n');
        }

        public ICustomStringBuilder AppendLine(string str)
        {
            AppendLine();
            Append(str);
            return AppendLine();
        }

        public ICustomStringBuilder AppendLine(char ch)
        {
            AppendLine();
            return Append(ch);
        }

        public string Build()
        {
            if (Capacity == 0)
                return string.Empty;
            
            var result = new string(_charArray);

            _charArray = new char[0];
            Capacity = 0;
            
            return result;
        }
    }
}