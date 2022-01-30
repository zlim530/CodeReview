using CloneTheTree;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
int[] a = { 1, 2, 3, 4, 5, 6, 7};
var tree = Utilites.BuildTree(a, 0, a.Length - 1);
var newTree = Clone(tree);
Utilites.DFS(newTree);

static Node Clone(Node root)
{ 
    var queue = new Queue<Node>();
    var newRoot = new Node(root.val);
    queue.Enqueue(root);
    queue.Enqueue(newRoot);
    while (queue.Count > 0) // 即 queue 不为空的时候
    { 
        var original = queue.Dequeue();
        var cloned = queue.Dequeue();
        if (original.left != null)
        {
            cloned.left = new Node(original.left.val);
            queue.Enqueue(original.left);
            queue.Enqueue(cloned.left);
        }
        
        if (original.right != null)
        {
            cloned.right = new Node(original.right.val);
            queue.Enqueue(original.right);
            queue.Enqueue(cloned.right);
        }

    }

    return newRoot;
}