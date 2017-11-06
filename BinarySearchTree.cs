using System;

namespace DataStructures
{
    class BinarySearchTree<T> : BinaryTree<T> where T : IComparable<T>
    {
        public virtual BinaryTreeNode<T> Add(T data)
        {
            BinaryTreeNode<T> current = base.Root, parent = null;
            while (current != null)
            {
                int result = current.Value.CompareTo(data);
                if (result == 0)
                    return parent;
                else if (result > 0)
                {
                    parent = current;
                    current = current.Left;
                }
                else if (result < 0)
                {
                    parent = current;
                    current = current.Right;
                }
            }

            var newNode = new BinaryTreeNode<T>(data);
            newNode.Parent = parent;

            if (parent == null)
                base.Root = newNode;
            else
            {
                int result = parent.Value.CompareTo(data);
                if (result > 0)
                    parent.Left = newNode;
                else
                    parent.Right = newNode;
            }

            return newNode;
        }

        public override bool Contains(T data)
        {
            BinaryTreeNode<T> current = base.Root;
            while (current != null)
            {
                int result = current.Value.CompareTo(data);
                if (result == 0)
                    return true;
                else if (result > 0)
                    current = current.Left;
                else if (result < 0)
                    current = current.Right;
            }

            return false;
        }

        public override bool Remove(T data)
        {
            if (base.Root == null) return false;

            BinaryTreeNode<T> current = base.Root, parent = null;
            int result = current.Value.CompareTo(data);
            while (result != 0)
            {
                if (result > 0)
                {
                    parent = current;
                    current = current.Left;
                }
                else if (result < 0)
                {
                    parent = current;
                    current = current.Right;
                }
                if (current == null)
                    return false;
                else
                    result = current.Value.CompareTo(data);
            }

            if (current.Right == null)
            {
                // if no right child, left child takes over
                if (parent == null)
                    base.Root = current.Left;
                else
                {
                    result = parent.Value.CompareTo(current.Value);
                    if (result > 0)
                        parent.Left = current.Left;
                    else if (result < 0)
                        parent.Right = current.Left;
                }
            }
            else if (current.Right.Left == null)
            {
                // if no left child, right child takes over
                current.Right.Left = current.Left;

                if (parent == null)
                    base.Root = current.Right;
                else
                {
                    result = parent.Value.CompareTo(current.Value);
                    if (result > 0)
                        parent.Left = current.Right;
                    else if (result < 0)
                        parent.Right = current.Right;
                }
            }
            else
            {
                // if left child, right child's left most descended takes over
                BinaryTreeNode<T> leftMost = current.Right.Left, leftMostParent = current.Right;
                while (leftMost.Left != null)
                {
                    leftMostParent = leftMost;
                    leftMost = leftMost.Left;
                }

                leftMostParent.Left = leftMost.Right;
                leftMost.Left = current.Left;
                leftMost.Right = current.Right;

                if (parent == null)
                    base.Root = leftMost;
                else
                {
                    result = parent.Value.CompareTo(current.Value);
                    if (result > 0)
                        parent.Left = leftMost;
                    else if (result < 0)
                        parent.Right = leftMost;
                }
            }

            return true;
        }
    }
}
