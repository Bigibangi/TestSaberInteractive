using System;
using System.Collections.Generic;
using System.Text;

namespace SerializeTestSaber
{
    public class ListNode
    {
        public ListNode Prev;
        public ListNode Next;
        public ListNode Rand;
        public string Data;

        public override bool Equals(object obj)
        {
            return Equals(obj as ListNode);
        }

        public bool Equals(ListNode other)
        {
            return other != null &&
                   other.Prev.Equals(this.Prev) &&
                   other.Next.Equals(this.Next) &&
                   other.Rand.Equals(this.Rand) &&
                   other.Data.Equals(this.Data);
        }

        public override int GetHashCode()
        {
            return Data.Length * 13;
        }
    }
}
