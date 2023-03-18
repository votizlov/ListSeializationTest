namespace ListTest;

public class RandListUtils
{
    public static ListRand CreateListFromStrArr(string[] arr, bool isGenerateRandomLinks = false)
    {
        int[] randLinkIndicies = new int[arr.Length];
        for (int i = 0; i < randLinkIndicies.Length; i++)
        {
            if (isGenerateRandomLinks)
            {
                randLinkIndicies[i] = Random.Shared.Next(0, arr.Length);
            }
            else
            {
                randLinkIndicies[i] = -1;
            }
        }

        return CreateListFromStrArr(arr, randLinkIndicies);
    }

    public static ListRand CreateListFromStrArr(string[] arr, int[] randLinkIndicies)
    {
        ListRand output = new ListRand();
        ListNode[] nodes = new ListNode[arr.Length];

        for (int i = 0; i < arr.Length; i++)
        {
            output.Add(arr[i]);
            nodes[i] = output.Tail;
        }

        var currentNode = output.Head;

        for (int i = 0; i < randLinkIndicies.Length; i++)
        {
            if (randLinkIndicies[i] != -1)
            {
                currentNode.Rand = nodes[randLinkIndicies[i]];
            }

            currentNode = currentNode.Next;
        }

        return output;
    }
}