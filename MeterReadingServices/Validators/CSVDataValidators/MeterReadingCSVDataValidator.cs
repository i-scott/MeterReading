﻿using System;
using System.Collections.Generic;

namespace MeterReadingServices.Validators.CSVDataValidators
{
    public class MeterReadingCSVDataValidator : IMeterReadingCSVValidator
    {
        private readonly IDictionary<string, ICSVValidator> _validators;

        public MeterReadingCSVDataValidator(IDictionary<string, ICSVValidator> validators)
        {
            _validators = validators;
        }

        public bool IsValid(string[] headers, string[] strings)
        {
            if (headers.Length != strings.Length) throw new ArgumentException("Header, CSV Values, length mismatch");

            for (int i = 0; i < headers.Length; i++)
            {
                if(string.IsNullOrEmpty(headers[i])) continue;

                if (!_validators.TryGetValue(headers[i], out var validator)) throw new ArgumentNullException($"Unknown validator {headers[i]}");

                if (!validator.IsValid(strings[i])) return false;
            }

            return true;
        }
    }
}
