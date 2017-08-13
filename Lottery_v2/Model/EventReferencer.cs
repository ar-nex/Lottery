using System.Collections.Generic;
using Lottery_v2.Model.Database;
namespace Lottery_v2.Model
{
    public static class EventReferencer
    {
        private static Dictionary<string, ProductDb> ReferenceHolder = new Dictionary<string, ProductDb>();

        public static void AddReference(string keyName, ProductDb pdb)
        {
            ReferenceHolder.Add(keyName, pdb);
        }

        public static ProductDb GetReference(string keyName)
        {
            if (ReferenceHolder.ContainsKey(keyName))
            {
                return ReferenceHolder[keyName];
            }
            else
            {
                return new ProductDb();
            }
        }
    }
}
