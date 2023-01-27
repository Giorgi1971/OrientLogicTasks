public class Program
{
    public class BinaryTreeNode
    {
        public int Value;
        public BinaryTreeNode? Left;
        public BinaryTreeNode? Right;

        public BinaryTreeNode(int value)
        {
            Value = value;
            Left = null;
            Right = null;
        }
    }

    private static List<int> BranchSums(BinaryTreeNode root)
    {
        var sum = new List<int>();
        if(root.Right != null)

        // ...
        //int breanchSum = root.Value;



        while(root.Left == null)
        {

        }


        return sum;
    }


    public static void Main()
    {
        var root = new BinaryTreeNode(1);

        root.Left = new BinaryTreeNode(2);
        root.Right = new BinaryTreeNode(3);

        root.Left.Left = new BinaryTreeNode(4);
        root.Left.Right = new BinaryTreeNode(5);
        Console.WriteLine(root.Left.Right.Value);

        root.Right.Left = new BinaryTreeNode(6);
        root.Right.Right = new BinaryTreeNode(7);

        root.Left.Left.Left = new BinaryTreeNode(8);
        root.Left.Left.Right = new BinaryTreeNode(9);

        root.Left.Right.Left = new BinaryTreeNode(10);

        var sums = BranchSums(root);
        Console.WriteLine(string.Join(", ", sums)); // უნდა გამოიტანოს 15, 16, 18, 10, 11
    }
}