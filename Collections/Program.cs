namespace Collections
{
    public class Program
    {
        static void Main(string[] args)
        {
            SystemCollections systemCollections = new SystemCollections();
            systemCollections.OnArrayList();
            Console.WriteLine("--------------------------------------------------");
            systemCollections.OnHashtable();
            Console.WriteLine("--------------------------------------------------");
            systemCollections.OnQueue();
            Console.WriteLine("--------------------------------------------------");
            systemCollections.OnStack();
            Console.WriteLine("--------------------------------------------------");
             
            CollectionsGeneric collectionsGeneric = new CollectionsGeneric();
            collectionsGeneric.OnList();
            Console.WriteLine("--------------------------------------------------");
            collectionsGeneric.OnDictionary();
            Console.WriteLine("--------------------------------------------------");
            collectionsGeneric.OnQueue();
            Console.WriteLine("--------------------------------------------------");
            collectionsGeneric.OnStack();
            Console.WriteLine("--------------------------------------------------");
            collectionsGeneric.OnLinkedList();
            Console.WriteLine("--------------------------------------------------");
            collectionsGeneric.OnHashSet();
            Console.WriteLine("--------------------------------------------------");
            collectionsGeneric.OnSortedSet();
            Console.WriteLine("--------------------------------------------------");


        }
    }
}
