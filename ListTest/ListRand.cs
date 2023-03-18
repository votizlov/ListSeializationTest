using System.Collections;

namespace ListTest;

public class ListRand
{
    public ListNode? Head;
    public ListNode? Tail;
    public int Count;

    public void Add(string data, ListNode? randomNode = null)
    {
        if (Head == null)
        {
            var t = new ListNode(null, null, randomNode, data);
            Head = t;
            Tail = t;
        }
        else
        {
            var t = new ListNode(Tail, null, randomNode, data);
            Tail.Next = t;
            Tail = t;
        }

        Count++;
    }

    private void Add(ListNode node)
    {
        if (Head == null)
        {
            Head = node;
            Tail = node;
        }
        else
        {
            Tail.Next = node;
            Tail = node;
        }

        Count++;
    }

    public void Serialize(FileStream s)
    {
        var currentNode = Head;
        Dictionary<ListNode, int> nodesDict = new Dictionary<ListNode, int>();
        BinaryWriter binaryWriter = new BinaryWriter(s);
        int counter = 0;

        binaryWriter.Write(Count);

        while (currentNode != null)
        {
            nodesDict.Add(currentNode, counter);
            binaryWriter.Write(currentNode.Data);

            currentNode = currentNode.Next;
            counter++;
        }

        currentNode = Head;

        while (currentNode != null)
        {
            if (currentNode.Rand != null)
            {
                binaryWriter.Write(nodesDict[currentNode.Rand]);
            }
            else
            {
                binaryWriter.Write(-1);
            }

            currentNode = currentNode.Next;
        }

        binaryWriter.Flush();
        binaryWriter.Close();
    }

    public ListRand Deserialize(FileStream s)
    {
        BinaryReader binaryReader = new BinaryReader(s);
        ListRand output = new ListRand();
        int nodesCount = binaryReader.ReadInt32();
        ListNode[] nodes = new ListNode[nodesCount];
        ListNode prev = null;

        for (int i = 0; i < nodesCount; i++)
        {
            nodes[i] = new ListNode(prev, null, null, binaryReader.ReadString());
            prev = nodes[i];
            output.Add(nodes[i]);
        }

        int randNodeIndex;

        for (int i = 0; i < nodesCount; i++)
        {
            randNodeIndex = binaryReader.ReadInt32();
            if (randNodeIndex == -1)
                continue;
            nodes[i].Rand = nodes[randNodeIndex];
        }

        return output;
    }

    public List<ListNode> ToList()
    {
        var output = new List<ListNode>();
        var currentNode = Head;
        while (currentNode != null)
        {
            output.Add(currentNode);
            currentNode = currentNode.Next;
        }

        return output;
    }
}