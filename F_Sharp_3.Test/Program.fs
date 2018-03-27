// Learn more about F# at http://fsharp.org

open System
open NUnit.Framework
open FsUnit

module ``3_1Tests`` =

    let ``Count even numbers of list [0 .. 10] should be equal 6`` countEvenNumbers =
        [0 .. 10] |> countEvenNumbers |> should equal 6

    [<Test>]
    let ``for first option`` = ``Count even numbers of list [0 .. 10] should be equal 6`` Program.F_Sharp_3_1.FirstOption.countEvenNumbers

    [<Test>]
    let ``for second option`` = ``Count even numbers of list [0 .. 10] should be equal 6`` Program.F_Sharp_3_1.SecondOption.countEvenNumbers

    [<Test>]
    let ``for third option`` = ``Count even numbers of list [0 .. 10] should be equal 6`` Program.F_Sharp_3_1.ThirdOption.countEvenNumbers

module ``3_2Tests`` =
    open Program.F_Sharp_3_2

    [<Test>]
    let ``Map the Tree [2, [1], [4, [3], [5]]] with pown by 2`` =
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

[<EntryPoint>]
let main argv = 0 // return an integer exit code
