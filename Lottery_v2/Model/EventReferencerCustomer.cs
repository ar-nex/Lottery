using System.Collections.Generic;
using Lottery_v2.Model.Database;

namespace Lottery_v2.Model
{
    public static class EventReferencerCustomer
    {
        private static Dictionary<string, CustomerDb> ReferenceHolder = new Dictionary<string, CustomerDb>();

        public static void AddReference(string keyName, CustomerDb cdb)
        {
            ReferenceHolder.Add(keyName, cdb);
        }

        public static CustomerDb GetReference(string keyName)
        {
            if (ReferenceHolder.ContainsKey(keyName))
                return ReferenceHolder[keyName];
            else
                return new CustomerDb();
        }
    }
}
