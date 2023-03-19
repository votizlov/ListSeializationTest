using ListTest;

namespace Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void NoRandLinksTest()
    {
        ListRand listRand = RandListUtils.CreateListFromStrArr(new string[] {"a", "b", "asdas"});
        string testFilePath = AppDomain.CurrentDomain.BaseDirectory + "ListRand.bin";
        ValidateList(listRand, testFilePath);
    }

    [Test]
    public void RandLinksTest()
    {
        ListRand listRand = RandListUtils.CreateListFromStrArr(new string[] {"a", "b", "asdas"}, true);
        string testFilePath = AppDomain.CurrentDomain.BaseDirectory + "ListRand1.bin";
        ValidateList(listRand,testFilePath);
    }
    
    [Test]
    public void EmptyListTest()
    {
        ListRand listRand = new ListRand();
        string testFilePath = AppDomain.CurrentDomain.BaseDirectory + "ListRand2.bin";
        ValidateList(listRand,testFilePath);
    }

    private void ValidateList(ListRand listRand, string testFilePath)
    {
        var initialList = listRand.ToList();
        listRand.Serialize(File.OpenWrite(testFilePath));
        if (!listRand.Deserialize(File.OpenRead(testFilePath), out var processedListRand))
        {
            Assert.Fail();
        }

        var processedList = processedListRand.ToList();
        for (int i = 0; i < initialList.Count - 1; i++)
        {
            if (initialList[i].Data.CompareTo(processedList[i].Data) != 0)
            {
                Assert.Fail();
            }

            if (initialList[i].Rand != null && initialList[i].Rand.Data.CompareTo(processedList[i].Rand.Data) != 0 ||
                initialList[i].Rand == processedList[i].Rand)
            {
                Assert.Fail();
            }
        }
    }
}