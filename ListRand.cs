using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace SerializeTestSaber
{
    public class ListRand
    {
        public ListNode Head;
        public ListNode Tail;
        public int Count;

        public void Serialize(FileStream s)
        {
            var currentNode = Head;
            var type = typeof(ListNode);
            int count = 0;
            var indexDict = new Dictionary<ListNode, int>();
            indexDict.Add(currentNode, count);
            using (StreamWriter sw = new StreamWriter(s, Encoding.Default))
            {
                sw.WriteLine(String.Format("{0}:{1}", "Count", Count));
                //sw.WriteLine(String.Format("{0}:{1}", "Encoding", sw.Encoding.ToString()));
                Serialize(sw, currentNode.Next, indexDict, ++count);
                //sw.WriteLine("--------------");
                foreach (var field in type.GetFields())
                {
                    var value = field.GetValue(currentNode);
                    if (value is ListNode) { value = value as ListNode is null ? "NULL" : indexDict[value as ListNode]; }
                    else if (value is String) { value = value as String; }
                    else if (value is null) { value = "NULL"; }
                    sw.WriteLine(String.Format("{0}:{1}", field.Name, value));
                }
            }
        }

        private void Serialize(StreamWriter sw, ListNode currentNode, Dictionary<ListNode, int> indexDict, int count)
        {
            if (currentNode is null)
                return;
            indexDict.Add(currentNode, count);
            Serialize(sw, currentNode.Next, indexDict, ++count);
            //sw.WriteLine("--------------");
            foreach (var field in currentNode.GetType().GetFields())
            {
                var value = field.GetValue(currentNode);
                if (value is ListNode) { value = value as ListNode is null ? "NULL" : indexDict[value as ListNode]; }
                else if (value is String) { value = value as String; }
                else if (value is null) { value = "NULL"; }
                sw.WriteLine(String.Format("{0}:{1}", field.Name, value));
            }
            
        }

        public void Deserialize(FileStream s)
        {
            var list = new List<ListNode>();
            using (StreamReader sr = new StreamReader(s, encoding: default))
            {
                Count = Convert.ToInt32(sr.ReadLine()
                                          .Split(":")
                                          .GetValue(1));
                //var enc = Encoding.GetEncoding(sr.ReadLine()
                //                                 .Split(":")
                //                                 .GetValue(1)
                //                                 .ToString());
                if (Count > 0)
                {
                    var currentNode = new ListNode();
                    var count = 0;
                    Deserialize(sr, currentNode, list, ++count);
                    Tail = list.ElementAt(0);
                    Head = list.Last<ListNode>();
                }
            }
        }

        private void Deserialize(StreamReader sr, ListNode currentNode, List<ListNode> list, int count)
        {
            if (count > Count)
            {
                return;
            }
            currentNode = new ListNode();
            list.Add(currentNode);
            Deserialize(sr, currentNode.Prev, list, ++count);
            foreach (var field in currentNode.GetType().GetFields())
            {
                var str = sr.ReadLine().Split(":");
                if (str[1].Equals("NULL")) continue;
                if (str[0].Equals("Data") && !str[1].Equals("NULL"))
                {
                    field.SetValue(currentNode, str[1]);
                    continue;
                }
                field.SetValue(currentNode, list.ElementAt(list.Count - Convert.ToInt32(str[1]) - 1));
            }
        }

        public void AddNode(ListNode listNode)
        {
            if (Head == null)
            {
                Tail = Head = listNode;
                Count++;
            }
            else
            {
                Tail.Next = listNode;
                listNode.Prev = Tail;
                Tail = listNode;
                Count++;
            }
        }
    }
}