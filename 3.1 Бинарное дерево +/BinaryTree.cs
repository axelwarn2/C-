using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics.BinaryTrees
{
    public class BinaryTree<T> : IEnumerable<T> where T : IComparable<T>
    {
        public T Value { get; private set; }
        private BinaryTree<T> _left;
        private BinaryTree<T> _right;
        private bool _isValueSet;

        public BinaryTree()
        {
            _isValueSet = false;
        }

        public BinaryTree(T value)
        {
            Value = value;
            _isValueSet = true;
        }

        public BinaryTree<T> Left
        {
            get => _left;
            private set
            {
                if (!ReferenceEquals(_left, null))
                    throw new InvalidOperationException("Left node is already assigned.");
                _left = value;
            }
        }

        public BinaryTree<T> Right
        {
            get => _right;
            private set
            {
                if (!ReferenceEquals(_right, null))
                    throw new InvalidOperationException("Right node is already assigned.");
                _right = value;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (_isValueSet)
            {
                if (Left != null)
                {
                    foreach (var value in Left)
                    {
                        yield return value;
                    }
                }

                yield return Value;

                if (Right != null)
                {
                    foreach (var value in Right)
                    {
                        yield return value;
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            if (!_isValueSet)
            {
                InitializeRoot(item);
            }
            else
            {
                AddToChildren(item);
            }
        }

        private void InitializeRoot(T item)
        {
            Value = item;
            _isValueSet = true;
        }

        private void AddToChildren(T item)
        {
            if (Value.CompareTo(item) > 0)
            {
                AddToLeft(item);
            }
            else if (Value.CompareTo(item) < 0)
            {
                AddToRight(item);
            }
            else
            {
                AddToLeft(item);
            }
        }

        private void AddToLeft(T item)
        {
            if (Left == null)
            {
                Left = new BinaryTree<T>(item);
            }
            else
            {
                Left.Add(item);
            }
        }

        private void AddToRight(T item)
        {
            if (Right == null)
            {
                Right = new BinaryTree<T>(item);
            }
            else
            {
                Right.Add(item);
            }
        }
    }

    public static class BinaryTree
    {
        public static BinaryTree<T> Create<T>(params T[] items)
            where T : IComparable<T>
        {
            var tree = new BinaryTree<T>();
            foreach (var item in items)
            {
                tree.Add(item);
            }
            return tree;
        }
    }
}