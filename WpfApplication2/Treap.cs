using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Treaps
{
   

    class Treap<T>
    {
        private node<T> root;
        public node<T> getRoot()
        {
            return root;
        }
        private int size;
        public int getsize()
        {
            return size;
        }

        private node<T> getroot()
        {
            return root;
        }
        public Treap()
        {
            root = null;
            size = 0;
        }
        private node<T> insert(node<T> temp, node<T> New)
        {
            if (temp == null)
            {
                return New;
            }
            if (temp < New)
            {
                temp.right = insert(temp.right, New);
                if (temp.right.value() > temp.value())
                    temp.left_rotation(ref temp);
            }
            else if (temp > New)
            {
                temp.left = insert(temp.left, New);
                if (temp.left.value() > temp.value())
                    temp.right_rotation(ref temp);
            }
            
            return temp;
        }
        public void insert(T key)
        {
            Random s = new Random(DateTime.Now.Millisecond);
            int rr = s.Next(1, (int)1e6);
            size++;
            root = insert(root, new node<T>(key, rr));
        }
        
        private void print(node<T> temp) // in-order method
        {
            if (temp == null)
                return;
            Console.WriteLine("key is : " + temp.key + " , value is : " + temp.value());
            print(temp.left);

            print(temp.right);
        }
        public void print()
        {
            print(root);
        }
        private node<T> Search(node<T> Root, node<T> temp)
        {
            if (Root == null)
                return null;
            node<T> res;
            if (Root < temp)
                res = Search(Root.right, temp);

            else if (Root > temp)
                res = Search(Root.left, temp);
            else
                res = Root;

            return res;
        }
        public node<T> Search(T key)
        {
            node<T> temp = new node<T>(key);
            return Search(root, temp);
        }
        private node<T> delete(node<T> Root, node<T> temp)
        {
            if (Root == null) return Root;
            if (Root.equal(temp))
            {
                if (Root.right == null) // have one child
                {
                    Root = Root.left;
                }
                else if (Root.left == null) // have one child
                {
                    Root = Root.right;
                }
                else  // have two children
                {
                    if (Root.right.value() > Root.left.value())
                    {
                        Root.left_rotation(ref Root);

                        Root.left = delete(Root.left, temp);
                    }

                    else
                    {
                        Root.right_rotation(ref Root);
                        Root.right = delete(Root.right, temp);
                    }


                }
            }
            else if (Root < temp)
            {
                Root.right = delete(Root.right, temp);
            }
            else
            {
                Root.left = delete(Root.left, temp);
            }
            return Root;
        }
        public void delete(T key)
        {
            root = delete(root, new node<T>(key));
        }
        private void split(node<T> Root, node<T> Left, node<T> Right,node<T> temp)
        {

            if (Root == null)
            { Left = null; Right = null; }
            else if(temp>Root)
            {
                Left = Root;
                split(Root.right, Left.right, Right, temp);
            }
            else
            {

                Right = Root;
                split(Root.left, Left, Right.left, temp);

            }
               



        }
        
        private void join(node<T> temp)
        {
            if (temp == null)
                return;
            this.insert(temp.key);
            join(temp.left);
            join(temp.right);

        }
        public void join(Treap<T> t)
        {
            join(t.getroot());
        }
        private void copy(node<T> root, ref node<T> res)
        {

            if (root == null)
                res = null;
            else
            {
                res = new node<T>(root.key, root.value());
                copy(root.left, ref res.left);
                copy(root.right, ref res.right);


            }
        }
        private void split(ref node<T> Root, ref node<T> Left, ref node<T> Right, node<T> temp)
        {

            if (Root == null)
            { Left = null; Right = null; }
            else if (temp > Root)
            {
                Left = Root;
                split(ref Root.right, ref Left.right, ref Right, temp);
            }
            else
            {

                Right = Root;
                split(ref Root.left, ref Left, ref Right.left, temp);

            }




        }
        public void split(ref Treap<T> left, ref Treap<T> right, T key)
        {
            Treap<T> res = new Treap<T>();
            copy(root, ref res.root);
            split(ref res.root, ref left.root, ref right.root, new node<T>(key));
        }
    }
}
