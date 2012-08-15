using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace CBB.MongoDB
{
    public class Utils
    {
        public static bool CheckObjectID(string id)
        {
            ObjectId oid;
            return ObjectId.TryParse(id, out oid);
        }
    }
}
