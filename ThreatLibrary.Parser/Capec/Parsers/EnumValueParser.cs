using System;
using System.Collections.Generic;
using System.Linq;

namespace ThreatLibrary.Parser.Capec.Parsers
{
    class EnumValueParser<T> where T : struct
    {
        readonly Dictionary<string, T> _valueDefinitions;

        public EnumValueParser(params (string key, T value)[] options)
        {
            _valueDefinitions = options.ToDictionary(o => o.key, o => o.value);
        }

        public T Parse(string value)
        {
            return _valueDefinitions.ContainsKey(value)
                ? _valueDefinitions[value]
                : throw new FormatException($"Unknown {nameof(T)} value: {value}.");
        }
    }
}