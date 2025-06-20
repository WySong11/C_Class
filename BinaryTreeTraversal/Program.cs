using System;
using System.Reflection.Emit;

public class Program
{
    static void Main(string[] args)
    {
        BinaryTree tree = new BinaryTree();

        tree.Add('A');
        tree.AddLeft('A', 'B');
        tree.AddRight('A', 'C');
        tree.AddLeft('B', 'D');
        tree.AddRight('B', 'E');
        tree.AddLeft('C', 'F');
        tree.AddRight('C', 'G');
        tree.AddLeft('D', 'H');
        tree.AddRight('D', 'I');
        tree.AddLeft('E', 'J');
        tree.AddRight('E', 'K');
        tree.AddLeft('F', 'L');
        tree.AddRight('F', 'M');
        tree.AddLeft('G', 'N');
        tree.AddRight('G', 'O');
        Console.WriteLine();

//                            A
//                  B                         C
//            D           E              F         G
//        H      I    J      K        L     M    N      O


        /*중위 순회(Inorder Traversal)
        1. 왼쪽 서브트리 중위 순회
        2. 루트 노드 방문
        3. 오른쪽 서브트리 중위 순회*/

        Console.WriteLine("중위 순회 In-order traversal");
        tree.Print();
        Console.WriteLine();

        /*전위 순회(Preorder Traversal)
        1. 루트 노드 방문
        2. 왼쪽 서브트리 전위 순회
        3. 오른쪽 서브트리 전위 순회*/

        Console.WriteLine("전위 순회 Pre-order traversal");
        tree.PrintPreOrder();
        Console.WriteLine();

        /*후위 순회(Postorder Traversal)
        1. 왼쪽 서브트리 후위 순회
        2. 오른쪽 서브트리 후위 순회
        3. 루트 노드 방문*/

        Console.WriteLine("후위 순회 Post-order traversal");
        tree.PrintPostOrder();
        Console.WriteLine();

        /*레벨 순회(Level-order Traversal)
        레벨 1부터 시작하여 각 레벨의 노드를 왼쪽에서 오른쪽 순서대로 방문합니다.*/
        Console.WriteLine("레벨 순회 Level-order traversal");
        tree.PrintLevelOrder();
        Console.WriteLine();

        tree.PrintHeight();
        tree.PrintCount();
    }
}

