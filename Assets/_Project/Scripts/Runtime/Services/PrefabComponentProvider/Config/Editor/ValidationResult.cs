#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

namespace OnTheMoon.Runtime.Services.PrefabComponentProvider.Config.Editor
{
    public class ValidationResult
    {
        public ValidationResult(bool hasError, bool hasDuplicates, string message, Color color, List<DuplicateInfo> duplicates = null)
        {
            HasError = hasError;
            HasDuplicates = hasDuplicates;
            Message = message;
            Color = color;
            Duplicates = duplicates?.AsReadOnly();
        }

        public IReadOnlyList<DuplicateInfo> Duplicates { get; }
        public bool HasMessage => string.IsNullOrEmpty(Message) == false;

        public bool HasError { get; }

        public bool HasDuplicates { get; }

        public string Message { get; }

        public Color Color { get; }
    }
}
#endif