using System.Collections.Generic;
using Lottery_v2.Model.Database;

namespace Lottery_v2.Model
{
    public static class EventReferencerSale
    {
        private static Dictionary<string, SaleDb> ReferenceHolder = new Dictionary<string, SaleDb>();

        public static void AddReference(string keyName, SaleDb sdb)
        {
            ReferenceHolder.Add(keyName, sdb);
        }

        public static SaleDb GetReference(string keyName)
        {
            if (ReferenceHolder.ContainsKey(keyName))
                return ReferenceHolder[keyName];
            else
                return new SaleDb();
        }
    }
}
