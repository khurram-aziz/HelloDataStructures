using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DataStructures
{
    class AvlTree<T> : BinarySearchTree<T> where T : IComparable<T>
    {
        int balanceFactor(BinaryTreeNode<T> current)
        {
            if (null == current) return 0;
            int l = null != current.Left ? current.Left.Height : 0;
            int r = null != current.Right ? current.Right.Height : 0;
            return (current.Height - r) - (current.Height - l);
        }

        BinaryTreeNode<T> rotateLeft(BinaryTreeNode<T> current)
        {
            var pivot = current.Right;
            pivot.Parent = current.Parent;
            current.Right = pivot.Left;

            pivot.Left = current;
            current.Parent = pivot;

            if (base.Root == current) base.Root = pivot;

            return pivot;
        }

        BinaryTreeNode<T> rotateRight(BinaryTreeNode<T> current)
        {
            var pivot = current.Left;
            pivot.Parent = current.Parent;
            current.Left = pivot.Right;

            pivot.Right = current;
            current.Parent = pivot;

            if (base.Root == current) base.Root = pivot;
            //if (null != pivot.Parent) pivot.Parent.Left = pivot;

            return pivot;
        }

        BinaryTreeNode<T> rotateLeftRight(BinaryTreeNode<T> current)
        {
            if (null != current.Left)
                current.Left = this.rotateLeft(current.Left);
            return this.rotateRight(current);
        }

        BinaryTreeNode<T> rotateRightLeft(BinaryTreeNode<T> current)
        {
            if (null != current.Right)
                current.Right = this.rotateRight(current.Right);
            return this.rotateLeft(current);
        }

        void balance(BinaryTreeNode<T> current)
        {
            if (null != current)
            {
                var parent = current.Parent;
                int bf = this.balanceFactor(current);
                Debug.Write(string.Format("balance({0}) [bf: {1}], ", current.Value, bf));

                if (bf > 1)
                {
                    //we have left heavy tree
                    if (this.balanceFactor(current.Left) > 0)
                    {
                        Debug.Write(string.Format("rotateRight({0}), ", current.Value));
                        current = this.rotateRight(current);
                        if (null != parent) parent.Left = current;
                    }
                    else
                    {
                        Debug.Write(string.Format("rotateLeftRight({0}), ", current.Value));
                        current = this.rotateLeftRight(current);
                        if (null != parent) parent.Left = current;
                    }
                }
                else if (bf < -1)
                {
                    //we have right heavy tree
                    if (this.balanceFactor(current.Right) > 0)
                    {
                        Debug.Write(string.Format("rotateRightLeft({0}), ", current.Value));
                        current = this.rotateRightLeft(current);
                        if (null != parent) parent.Right = current;
                    }
                    else
                    {
                        Debug.Write(string.Format("rotateLeft({0}), ", current.Value));
                        current = this.rotateLeft(current);
                        if (null != parent) parent.Right = current;
                    }
                }
                else
                    Debug.Write("no rotation, ");

                if (null != current.Parent)
                    this.balance(current.Parent);
            }
        }
        
        public override BinaryTreeNode<T> Add(T data)
        {
            Debug.Write(string.Format("Adding {0}, ", data));
            var newNode = base.Add(data);
            this.balance(newNode.Parent);

            Debug.WriteLine("");
            return newNode;
        }
        
        public T[] BreadthFirst()
        {
            var list = new List<T>();
            base.BreadthFirst(base.Root, o => list.Add(o.Value));
            return list.ToArray();
        }
    }
}
