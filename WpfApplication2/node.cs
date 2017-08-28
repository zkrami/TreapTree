using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Treaps
{
  
    class node<T> : IComparable< node<T> > 
    {
        public WpfApplication2.VisualNode vnode = null; 
        public node<T> left, right, parent;
        public T key;
        private int val;
        private IComparable<T> comparer;
        
        public node()
        {
            left = right = parent = null;
            if (key is IComparable<T>)
            {

                comparer = (IComparable<T>)key;
            }
            else
            {
                throw new Exception("The template is not comparable.");
            }    
        
        }
        public node(T key)
        {
            this.key = key;
            val = -1;
            left = parent = right = null;
            if (key is IComparable<T>)
            {

                comparer = (IComparable<T>)key;
            }
            else
            {
                throw new Exception("The template is not comparable.");
            }
        }
        public node(T key, int v)
        {
            this.key = key;
            this.val = v;
            left = parent = right = null;
            
            if (key is IComparable<T>)
            {
                
                comparer = (IComparable<T>)key;
            }
            else
            {
                throw new Exception("The template is not comparable.");
            }
        }
        public int value ()
        {
            return val;
        }
        public void left_rotation(ref node<T> temp)
        {
            node<T> t = temp;
            temp = t.right;
            t.right = temp.left;
            temp.left = t;
        }
        public void right_rotation(ref node<T> temp)
        {
            node<T> t = temp;
            temp = t.left;
            t.left = temp.right;
            temp.right = t;
        }
        public bool equal(node<T> other)
        {
            return comparer.CompareTo(other.key) == 0; 
        }
        public int CompareTo(node<T> other)
        {
            
            return comparer.CompareTo(other.key);
        }
        public static bool operator < (node<T> first, node<T> second)
        {
            if (first.CompareTo(second) < 0) return true;
            return false; 
        }
        public static bool operator  > (node<T> first, node<T> second)
        {
            if (first.CompareTo(second) > 0) return true;
            return false;
        }
        //public static bool operator == (node<T> first, node<T> second)
        //{
        //    if (first == null || second == null) return true;
        //     if (first.CompareTo(second) == 0) return true;
        //    return false;
        //}
        //public static bool operator != (node<T> first, node<T> second)
        //{
        //    //if (first == null || second != null) return false;
        //    if (first.CompareTo(second) == 0) return false;
        //    return true;
        //}
    }
}
