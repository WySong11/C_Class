using System;
using System.Collections.Generic;

public class BinaryTree
{
    public Node? Root { get; set; }

    public BinaryTree()
    {
        Root = null;
    }

    public BinaryTree(Node root)
    {
        Root = root;
    }

    public bool Contains(char value)
    {
        return ContainsRecursive(Root, value);
    }

    private Node? FindNode(Node? node, char value)
    {
        if (node == null) return null;
        if (node.Value == value) return node;

        Node? foundNode = FindNode(node.Left, value);

        if (foundNode != null) return foundNode;
        return FindNode(node.Right, value);
    }

    public void Add(char value)
    {
        if (Root == null)
        {
            Root = new Node(value);
            return;
        }

        if(Contains(value))
        {
            Console.WriteLine($"Node with value '{value}' already exists.");
            return;
        }

        if(Root.Value == value)
        {
            Console.WriteLine($"Root node with value '{value}' already exists.");
            return;
        }

        if (Root.Left == null )
        {
            Root.Left = new Node(value);
            Console.WriteLine($"Added '{value}' as left child of root '{Root.Value}'");
            return;
        }

        if (Root.Right == null )
        {
            Root.Right = new Node(value);
            return;
        }
    }

    public void AddLeft(char parentValue, char value)
    {
        Node? parentNode = FindNode(Root, parentValue);        
        if (parentNode == null)
        {
            Console.WriteLine($"Parent node '{parentValue}' not found.");
            return;
        }
        if (parentNode.Left != null)
        {
            Console.WriteLine($"Left child of '{parentValue}' already exists.");
            return;
        }

        parentNode.Left = new Node(value);
        Console.WriteLine($"Added '{value}' as left child of '{parentValue}'");
    }

    public void AddRight(char parentValue, char value)
    {
        Node? parentNode = FindNode(Root, parentValue);
        if (parentNode == null)
        {
            Console.WriteLine($"Parent node '{parentValue}' not found.");
            return;
        }
        if (parentNode.Right != null)
        {
            Console.WriteLine($"Right child of '{parentValue}' already exists.");
            return;
        }

        parentNode.Right = new Node(value);
        Console.WriteLine($"Added '{value}' as right child of '{parentValue}'");
    }

    public void InOrderTraversal(Action<char> action)
    {
        InOrderTraversalRecursive(Root, action);
    }

    private void InOrderTraversalRecursive(Node? node, Action<char> action)
    {
        if (node == null) return;
        InOrderTraversalRecursive(node.Left, action);
        action(node.Value);
        InOrderTraversalRecursive(node.Right, action);
    }

    public void PreOrderTraversal(Action<char> action)
    {
        PreOrderTraversalRecursive(Root, action);
    }

    private void PreOrderTraversalRecursive(Node? node, Action<char> action)
    {
        if (node == null) return;
        action(node.Value);
        PreOrderTraversalRecursive(node.Left, action);
        PreOrderTraversalRecursive(node.Right, action);
    }

    public void PostOrderTraversal(Action<char> action)
    {
        PostOrderTraversalRecursive(Root, action);
    }

    private void PostOrderTraversalRecursive(Node? node, Action<char> action)
    {
        if (node == null) return;
        PostOrderTraversalRecursive(node.Left, action);
        PostOrderTraversalRecursive(node.Right, action);
        action(node.Value);
    }

    public void LevelOrderTraversal(Action<char> action)
    {
        if (Root == null) return;
        Queue<Node> queue = new Queue<Node>();
        queue.Enqueue(Root);
        while (queue.Count > 0)
        {
            Node current = queue.Dequeue();
            action(current.Value);
            if (current.Left != null) queue.Enqueue(current.Left);
            if (current.Right != null) queue.Enqueue(current.Right);
        }
    }

    public int Height()
    {
        return HeightRecursive(Root);
    }

    private int HeightRecursive(Node? node)
    {
        if (node == null) return 0;
        int leftHeight = HeightRecursive(node.Left);
        int rightHeight = HeightRecursive(node.Right);
        return Math.Max(leftHeight, rightHeight) + 1;
    }

    public int Count()
    {
        return CountRecursive(Root);
    }

    private int CountRecursive(Node? node)
    {
        if (node == null) return 0;
        return 1 + CountRecursive(node.Left) + CountRecursive(node.Right);
    }

    private bool ContainsRecursive(Node? node, char value)
    {
        if (node == null) return false;
        if (node.Value == value) return true;
        if (value < node.Value)
        {
            return ContainsRecursive(node.Left, value);
        }
        else
        {
            return ContainsRecursive(node.Right, value);
        }
    }

    public void Clear()
    {
        Root = null;
    }

    public void Print()
    {
        InOrderTraversal(value => Console.Write(value + " "));
        Console.WriteLine();
    }

    public void PrintPreOrder()
    {
        PreOrderTraversal(value => Console.Write(value + " "));
        Console.WriteLine();
    }

    public void PrintPostOrder()
    {
        PostOrderTraversal(value => Console.Write(value + " "));
        Console.WriteLine();
    }

    public void PrintLevelOrder()
    {
        if (Root == null) return;
        Queue<Node> queue = new Queue<Node>();
        queue.Enqueue(Root);
        while (queue.Count > 0)
        {
            Node current = queue.Dequeue();
            Console.Write(current.Value + " ");
            if (current.Left != null) queue.Enqueue(current.Left);
            if (current.Right != null) queue.Enqueue(current.Right);
        }
        Console.WriteLine();
    }

    public void PrintHeight()
    {
        Console.WriteLine($"Height of the tree: {Height()}");
    }

    public void PrintCount()
    {
        Console.WriteLine($"Number of nodes in the tree: {Count()}");
    }

    public void PrintClear()
    {
        Clear();
        Console.WriteLine("Tree cleared.");
    }

    public void PrintAllWithNode(Node? node)
    {
        if (node == null)
        {
            Console.WriteLine("Node is null.");
            return;
        }
        Console.WriteLine($"Node Value: {node.Value}");
        Console.WriteLine("Left Child:");
        PrintAllWithNode(node.Left);
        Console.WriteLine("Right Child:");
        PrintAllWithNode(node.Right);
    }
}