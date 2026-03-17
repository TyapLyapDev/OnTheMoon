#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;

namespace OnTheMoon.Runtime.Services.PrefabComponentProvider.Config.Editor
{
    public static class DuplicateChecker
    {
        public static List<DuplicateInfo> FindDuplicates(TypePathPair[] pairs)
        {
            Dictionary<string, List<TypePathPair>> typeToPairs = new();

            foreach (TypePathPair pair in pairs)
            {
                if (string.IsNullOrEmpty(pair.TypeName))
                    continue;

                if (typeToPairs.TryGetValue(pair.TypeName, out List<TypePathPair> list) == false)
                {
                    list = new List<TypePathPair>();
                    typeToPairs[pair.TypeName] = list;
                }

                list.Add(pair);
            }

            return typeToPairs
                .Where(kvp => kvp.Value.Count > 1)
                .Select(kvp => new DuplicateInfo(kvp.Key, kvp.Value))
                .ToList();
        }
    }
}
#endif