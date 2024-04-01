using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance.DataStructure
{
    public class Category : IComparable
    {
        public string Product { get; }
        public MessageType MessageType { get; }
        public MessageTopic MessageTopic { get; }

        public Category(string product, MessageType messageType, MessageTopic messageTopic)
        {
            Product = product;
            MessageType = messageType;
            MessageTopic = messageTopic;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Category))
                return false;

            Category other = (Category)obj;
            return Product == other.Product &&
                   MessageType == other.MessageType &&
                   MessageTopic == other.MessageTopic;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Product.GetHashCode();
                hash = hash * 23 + MessageType.GetHashCode();
                hash = hash * 23 + MessageTopic.GetHashCode();
                return hash;
            }
        }

        public int CompareTo(Category other)
        {
            if (other == null)
                return 1;

            int productComparison = string.Compare(Product, other.Product, StringComparison.Ordinal);
            if (productComparison != 0)
                return productComparison;

            int messageTypeComparison = MessageType.CompareTo(other.MessageType);
            if (messageTypeComparison != 0)
                return messageTypeComparison;

            return MessageTopic.CompareTo(other.MessageTopic);
        }

        public bool ImplementsIComparable()
        {
            return this is IComparable<Category>;
        }

        public static bool operator ==(Category x, Category y)
        {
            return x?.Equals(y) ?? y is null;
        }

        public static bool operator !=(Category x, Category y)
        {
            return !(x == y);
        }

        public static bool operator <(Category x, Category y)
        {
            return x.CompareTo(y) < 0;
        }

        public static bool operator >(Category x, Category y)
        {
            return x.CompareTo(y) > 0;
        }

        public static bool operator <=(Category x, Category y)
        {
            return x.CompareTo(y) <= 0;
        }

        public static bool operator >=(Category x, Category y)
        {
            return x.CompareTo(y) >= 0;
        }

        public override string ToString()
        {
            return $"{Product}.{MessageType}.{MessageTopic}";
        }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }
    }
}