using System;
using System.Collections.Generic;

namespace DataStructures
{
    class BinaryTreeNode<T>
    {
        BinaryTreeNode() { }

        public BinaryTreeNode(T data)
        {
            this.Value = data;
            this.Parent = null;
            this.Left = null;
            this.Right = null;
        }

        int higher(int left, int right)
        {
            return left > right ? left : right;
        }

        public int Height
        {
            get
            {
                int childHeight = 0;
                if (null != this.Left && null != this.Right)
                    childHeight = this.higher(this.Left.Height, this.Right.Height);
                else if (null != this.Left)
                    childHeight = this.Left.Height;
                else if (null != this.Right)
                    childHeight = this.Right.Height;
                return childHeight + 1;
            }
        }
        
        public T Value { get; set; }
        
        public BinaryTreeNode<T> Parent { get; set; }
        
        public BinaryTreeNode<T> Left { get; set; }
        
        public BinaryTreeNode<T> Right { get; set; }

        public override string ToString()
        {
            return string.Format("{0} [Left={1}, Right={2}]", this.Value,
                null != this.Left ? this.Left.Value.ToString() : "NA",
                null != this.Right ? this.Right.Value.ToString() : "NA");
        }
    }

    abstract class BinaryTree<T>
    {
        public BinaryTreeNode<T> Root { get; set; }

        public BinaryTree()
        {
            this.Root = null;
        }

        protected void BreadthFirst(BinaryTreeNode<T> current, Action<BinaryTreeNode<T>> action)
        {
            var q = new Queue<BinaryTreeNode<T>>();
            if (null != current)
                q.Enqueue(current);
            while (q.Count > 0)
            {
                var node = q.Dequeue();
                action(node);
                if (node.Left != null)
                    q.Enqueue(node.Left);
                if (node.Right != null)
                    q.Enqueue(node.Right);
            }
        }
        
        protected void DepthFirst(BinaryTreeNode<T> current, Action<BinaryTreeNode<T>> action)
        {
            if (null != current)
            {
                this.DepthFirst(current.Left, action);
                this.DepthFirst(current.Right, action);
                action(current);
            }
        }

        public virtual void Clear()
        {
            this.DepthFirst(this.Root, o => o = null);
        }

        public abstract bool Contains(T data);

        public abstract bool Remove(T data);

        public T[] DepthFirst()
        {
            var list = new List<T>();
            this.DepthFirst(this.Root, o => list.Add(o.Value));
            return list.ToArray();
        }
    }
}
