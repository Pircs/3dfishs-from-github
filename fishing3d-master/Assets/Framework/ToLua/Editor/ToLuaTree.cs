﻿/*
Copyright (c) 2015-2016 topameng(topameng@qq.com)

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
using UnityEngine;
using System.Collections.Generic;
using System;

public class ToLuaNode<T>
{
    public List<ToLuaNode<T>> childs = new List<ToLuaNode<T>>();
    public ToLuaNode<T> parent = null;
    public T value;
    //添加命名空间节点所在位置，解决A.B.C/A.C存在相同名称却在不同命名空间所造成的Wrap问题
    public int layer;
}

public class ToLuaTree<T> 
{       
    public ToLuaNode<T> _root = null;

    public ToLuaTree()
    {
        _root = new ToLuaNode<T>();
    }

    //加入pos跟root里的pos比较，只有位置相同才是统一命名空间节点
    ToLuaNode<T> FindParent(List<ToLuaNode<T>> root, Predicate<T> match, int layer)
    {
        if (root == null)
        {
            return null;
        }

        for (int i = 0; i < root.Count; i++)
        {
            //加入pos跟root里的pos比较，只有位置相同才是统一命名空间节点
            if (match(root[i].value) && root[i].layer == layer)
            {
                return root[i];
            }

            ToLuaNode<T> node = FindParent(root[i].childs, match, layer);

            if (node != null)
            {
                return node;
            }
        }

        return null;
    }

    /*public void BreadthFirstTraversal(Action<ToLuaNode<T>> action)
    {
        List<ToLuaNode<T>> root = _root.childs;        
        Queue<ToLuaNode<T>> queue = new Queue<ToLuaNode<T>>();

        for (int i = 0; i < root.Count; i++)
        {
            queue.Enqueue(root[i]);
        }

        while (queue.Count > 0)
        {
            ToLuaNode<T> node = queue.Dequeue();
            action(node);

            if (node.childs != null)
            {
                for (int i = 0; i < node.childs.Count; i++)
                {
                    queue.Enqueue(node.childs[i]);
                }
            }
        }
    }*/

    public void DepthFirstTraversal(Action<ToLuaNode<T>> begin, Action<ToLuaNode<T>> end, ToLuaNode<T> node)
    {
        begin(node);

        for (int i = 0; i < node.childs.Count; i++)
        {            
            DepthFirstTraversal(begin, end, node.childs[i]);
        }

        end(node);
    }

    //只有位置相同才是统一命名空间节点
    public ToLuaNode<T> Find(Predicate<T> match, int layer)
    {
        return FindParent(_root.childs, match, layer);
    }

    public ToLuaNode<T> GetRoot()
    {
        return _root;
    }
}
