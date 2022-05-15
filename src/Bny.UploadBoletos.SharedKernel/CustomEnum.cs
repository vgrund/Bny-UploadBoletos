using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bny.UploadBoletos.SharedKernel
{
    public abstract class CustomEnum
        //<TKey, TValue> 
        //where TKey : IEquatable<TKey>, IComparable<TKey>
    {
        public string Name { get; private set; }
        public int Value { get; private set; }

        private CustomEnum(int val, string name)
        {
            Value = val;
            Name = name;
        }

        public abstract IEnumerable<CustomEnum> List();

       // protected static readonly Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();

        public CustomEnum FromString(string stringValue)
        {
            return List().Single(r => String.Equals(r.Name, stringValue, StringComparison.OrdinalIgnoreCase));
        }

        public static CustomEnum FromValue(int value)
        {
            return List().Single(r => r.Value == value);
        }
    }
}
