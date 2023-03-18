namespace ListTest;

public class ListNode
{
    public ListNode? Prev;
    public ListNode? Next;
    public ListNode? Rand; // произвольный элемент внутри списка
    public string Data;

    public ListNode(ListNode? prev, ListNode? next, ListNode? rand, string data)
    {
        Prev = prev;
        Next = next;
        Rand = rand;
        Data = data;
    }
}