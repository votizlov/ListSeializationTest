using ListTest;

namespace Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        ListRand listRand = RandListUtils.CreateListFromStrArr(new string[] {"a", "b", "asdas"});
        string testFilePath = AppDomain.CurrentDomain.BaseDirectory + "ListRand.bin";
        var initialList = listRand.ToList();
        listRand.Serialize(File.OpenWrite(testFilePath));
        var processedList = listRand.Deserialize(File.OpenRead(testFilePath)).ToList();
        ValidateLists(initialList, processedList);
    }

    [Test]
    public void Test2()
    {
        ListRand listRand = RandListUtils.CreateListFromStrArr(new string[] {"a", "b", "asdas"}, true);
        string testFilePath = AppDomain.CurrentDomain.BaseDirectory + "ListRand.bin";
        var initialList = listRand.ToList();
        listRand.Serialize(File.OpenWrite(testFilePath));
        var processedList = listRand.Deserialize(File.OpenRead(testFilePath)).ToList();
        ValidateLists(initialList, processedList);
    }

    private void ValidateLists(List<ListNode> A, List<ListNode> B)
    {
        for (int i = 0; i < A.Count - 1; i++)
        {
            if (A[i].Data.CompareTo(B[i].Data) != 0)
            {
                Assert.Fail();
            }

            if (A[i].Rand != null && A[i].Rand.Data.CompareTo(B[i].Rand.Data) != 0)
            {
                Assert.Fail();
            }
        }
    }
}