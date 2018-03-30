
open System
open NUnit.Framework
open FsUnit
open F_Sharp_3

module ``3_1Tests`` =

    let ``Count even numbers of list [0 .. 10] should be equal 6`` countEvenNumbers =
        [0 .. 10] |> countEvenNumbers |> should equal 6

    [<Test>]
    let ``for first option``() = ``Count even numbers of list [0 .. 10] should be equal 6`` F_Sharp_3_1.FirstOption.countEvenNumbers

    [<Test>]
    let ``for second option``() = ``Count even numbers of list [0 .. 10] should be equal 6`` F_Sharp_3_1.SecondOption.countEvenNumbers

    [<Test>]
    let ``for third option``() = ``Count even numbers of list [0 .. 10] should be equal 6`` F_Sharp_3_1.ThirdOption.countEvenNumbers

module ``3_2Tests`` =
    open F_Sharp_3_2

    [<Test>]
    let ``Map the Tree [2, [1], [4, [3], [5]]] with pown by 2``() =
        let binTree = Node(2,
                        Node(1, Empty, Empty),
                        Node(4,
                            Node(3, Empty, Empty),
                            Node(5, Empty, Empty)))
        let expectedBinTree = Node(4,
                                Node(1, Empty, Empty),
                                Node(16,
                                    Node(9, Empty, Empty),
                                    Node(25, Empty, Empty)))

        binTree |> mapTree (fun x -> pown x 2) |> should equal expectedBinTree

    [<Test>]
    let ``Map empty tree``() =
        let emptyTree = BinaryTree<int>.Empty
        emptyTree |> mapTree (fun x -> pown x 2) |> should equal emptyTree

module ``3_3Tests`` = 
    open F_Sharp_3_3

    [<Test>]
    let ``Compute an arithmetic expression (1 + 2) * (4 + 10) + 40 should be equal 82``() =
        let arExp = Operation((+), 
                        Operation((*),
                            Operation((+), Number(1), Number(2)),
                            Operation((+), Number(4), Number(10))),
                        Number(40))

        arExp |> countExpression |> should equal 82

module ``3_4Tests`` = 
    open F_Sharp_3_4

    [<Test>]
    let ``Take first 12 prime numbers its should be "2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37"``() =
        infinitePrimeSeq |> Seq.take 12 |> should equal [2; 3; 5; 7; 11; 13; 17; 19; 23; 29; 31; 37] 