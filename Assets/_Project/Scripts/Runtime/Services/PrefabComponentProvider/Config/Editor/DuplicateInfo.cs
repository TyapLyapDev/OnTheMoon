#if UNITY_EDITOR
using System.Collections.Generic;

namespace OnTheMoon.Runtime.Services.PrefabComponentProvider.Config.Editor
{
    public class DuplicateInfo
    {
        public string TypeName { get; }
        public IReadOnlyList<TypePathPair> Pairs { get; }

        public DuplicateInfo(string typeName, List<TypePathPair> pairs)
        {
            TypeName = typeName;
            Pairs = pairs.AsReadOnly();
        }
    }
}
#endif