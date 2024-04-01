using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates.TreeTraversal
{
    public static class Traversal
    {
        public static IEnumerable<TResult> TraverseTree<TInput, TResult>(TInput tree, Func<TInput, bool> shouldVisit,
            Func<TInput, IEnumerable<TInput>> getChildNodes, Func<TInput, TResult> selectValue)
        {
            if(tree == null)
            {
                yield break;
            }

            if (shouldVisit(tree))
            {
                var value = selectValue(tree);
                if(value != null)
                {
                    yield return value;
                }
            }

            var childNodes = getChildNodes(tree);
            if(childNodes != null)
            {
                foreach(var child in childNodes)
                {
                    foreach(var nodesValue in TraverseTree(child, shouldVisit, getChildNodes, selectValue))
                    {
                        yield return nodesValue;
                    }
                }
            }
        }

        public static IEnumerable<Product> GetProducts(ProductCategory root)
        {
            return TraverseTree(root,
                x => x.Products.Any(),
                x => x.Categories,
                x => x.Products).SelectMany(x => x);
        }

        public static IEnumerable<Job> GetEndJobs(Job root)
        {
            return TraverseTree(root,
                j => j.Subjobs.Count == 0,
                j => j.Subjobs,
                j => j);
        }

        public static IEnumerable<T> GetBinaryTreeValues<T>(BinaryTree<T> root)
        {
            return TraverseTree(root,
                t => t != null && t.Left == null && t.Right == null,
                t => new List<BinaryTree<T>> { t.Left, t.Right}.Where(child => child != null),
                t => t.Value);
        }
    }
}
