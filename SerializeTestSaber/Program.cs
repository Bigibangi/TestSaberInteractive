using System;
using System.IO;

namespace SerializeTestSaber
{
    class Program
    {
        static void Main(string[] args)
        {
            var listRand = new ListRand();
            var listNode = new ListNode();
            var listNode1 = new ListNode();
            listNode.Next = listNode1;
            listNode1.Prev = listNode;
            listNode.Data = "Pft,kj";
            listRand.Head = listNode;
            listRand.Count = 2;
            FileStream fs = new FileStream("Data.txt", FileMode.OpenOrCreate);
            listRand.Serialize(fs);
            FileStream rs = new FileStream("Data.txt", FileMode.OpenOrCreate);
            listRand.Deserialize(rs);
        }
    }
}
