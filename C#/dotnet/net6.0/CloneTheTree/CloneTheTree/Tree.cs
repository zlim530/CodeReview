namespace CloneTheTree;

class Node
{
    public int val;
    public Node left;
    public Node right;
    public Node(int val)
    {
        this.val = val;
    }
}

static class Utilites
{
    /// <summary>
    /// 把一个数组转换为一个平衡的二叉树
    /// </summary>
    /// <param name="a">待转换的数组</param>
    /// <param name="li">左边边界索引</param>
    /// <param name="hi">右边边界索引</param>
    /// <returns></returns>
    public static Node BuildTree(int[] a, int li, int hi)
    {
        if (hi < li) return null;
        int mi = li + (hi - li) / 2;
        var node = new Node(a[mi]);
        node.left = BuildTree(a, li, mi - 1);
        node.right = BuildTree(a,mi + 1, hi);
        return node;
    }

    /// <summary>
    /// 中序的 DFS 遍历
    /// </summary>
    /// <param name="root">根节点</param>
    public static void DFS(Node root)
    {
        if (root == null) return;
        DFS(root.left);
        Console.WriteLine(root.val);
        DFS(root.right);
    }

}
