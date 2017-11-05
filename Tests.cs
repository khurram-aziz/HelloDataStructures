using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructures
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void AvlTreeTests()
        {
            var avl = new AvlTree<int>();
            avl.Add(1); avl.Add(2);
            avl.Add(3);
            CollectionAssert.AreEqual(new int[] { 2, 1, 3 }, avl.BreadthFirst());
            avl.Add(4);
            avl.Add(5);
            CollectionAssert.AreEqual(new int[] { 2, 1, 4, 3, 5 }, avl.BreadthFirst());
            avl.Add(6);
            CollectionAssert.AreEqual(new int[] { 4, 2, 5, 1, 3, 6 }, avl.BreadthFirst());
            avl.Add(7);
            CollectionAssert.AreEqual(new int[] { 4, 2, 6, 1, 3, 5, 7 }, avl.BreadthFirst());
            avl.Add(8);
            CollectionAssert.AreEqual(new int[] { 4, 2, 6, 1, 3, 5, 7, 8 }, avl.BreadthFirst());
            avl.Add(9);
            CollectionAssert.AreEqual(new int[] { 4, 2, 6, 1, 3, 5, 8, 7, 9 }, avl.BreadthFirst());
        }
    }
}
