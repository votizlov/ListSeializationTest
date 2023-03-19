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
    
    [Test]
    public void BadFileTest()
    {
        ListRand listRand = new ListRand();
        string testFilePath = AppDomain.CurrentDomain.BaseDirectory + "ListTest.dll";
        listRand.Deserialize(File.OpenRead(testFilePath), out var processedListRand);
        Assert.Pass();
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
        for (int i = 0; i < initialList.Count; i++)
        {
            //check current node data
            if (String.Compare(initialList[i].Data, processedList[i].Data, StringComparison.Ordinal) != 0)
            {
                Assert.Fail();
            }
            
            //check rand links
            if (initialList[i].Rand != null && initialList[i].Rand.Data.CompareTo(processedList[i].Rand.Data) != 0)
            {
                Assert.Fail();
            } else if(initialList[i].Rand == null && initialList[i].Rand != processedList[i].Rand)
            {
                Assert.Fail();
            }
            
            //check next and prev links
            if (initialList[i].Next != null && String.Compare(initialList[i].Next.Data, processedList[i].Next.Data, StringComparison.Ordinal) != 0 ||
                initialList[i].Prev != null && String.Compare(initialList[i].Prev.Data, processedList[i].Prev.Data, StringComparison.Ordinal) != 0)
            {
                Assert.Fail();
            }
        }
    }
}