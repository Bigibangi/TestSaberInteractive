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
                   other.Prev == this.Prev &&
                   other.Next == this.Next &&
                   other.Rand == this.Rand &&
                   other.Data.Equals(this.Data);
        }

        public override int GetHashCode()
        {
            return Data.Length * 13;
        }
    }
}
