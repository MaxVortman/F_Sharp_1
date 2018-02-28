// Learn more about F# at http://fsharp.org

open System
open NUnit.Framework
open FsUnit

module F_Sharp_3_1 =

    module FirstOption = 
        let countEvenNumbers list =            
            list |> List.map (fun x -> if x % 2 = 0 then 1 else 0) |> List.fold (+) 0

        [<Test>]
        let ``Count even numbers of list [0 .. 10] should be equal 6`` =
            [0 .. 10] |> countEvenNumbers |> should equal 6

    module SecondOption = 
        let countEvenNumbers list = 
            list |> List.filter (fun x -> x % 2 = 0) |> List.length

        [<Test>]
        let ``Count even numbers of list [0 .. 10] should be equal 6`` =
            [0 .. 10] |> countEvenNumbers |> should equal 6

    module ThirdOption = 
        let countEvenNumbers list = 
            list |> List.fold (fun acc x -> if x % 2 = 0 then acc + 1 else acc) 0

        [<Test>]
        let ``Count even numbers of list [0 .. 10] should be equal 6`` =
            [0 .. 10] |> countEvenNumbers |> should equal 6

module F_Sharp_3_2 =
    type BinaryTree<'a> = 
    | Node of 'a * BinaryTree<'a> * BinaryTree<'a>
    | Empty

    let mapTree f tree = 
        let rec mapTreeTR tree =
            match tree with
            | Node(data, left, right) -> Node(f data, mapTreeTR left, mapTreeTR right)
            | Empty -> Empty
        mapTreeTR tree
    
    [<Test>]
    let ``Map the Tree [2, [1], [4, [3], [5]]] with pown by 2`` =
        let binTree = Node(2,
                        Node(1, Empty, Empty),
                        Node(4,
                            Node(3, Empty, Empty),
                            Node(5, Empty, Empty)))
        let actualBinTree = Node(4,
                                Node(1, Empty, Empty),
                                Node(16,
                                    Node(9, Empty, Empty),
                                    Node(25, Empty, Empty)))

        binTree |> mapTree (fun x -> pown x 2) |> should equal actualBinTree

module F_Sharp_3_3 =

    type ArithmeticExpression<'a> =
    | Operation of ('a -> 'a -> 'a) * ArithmeticExpression<'a> * ArithmeticExpression<'a>
    | Number of 'a

    let countExpression arExp =
        let rec countExp arExp =
            match arExp with
            | Operation(op, left, right) -> op <| countExp (left) <| countExp (right)
            | Number(n) -> n
        countExp arExp

    [<Test>]
    let ``Compute an arithmetic expression (1 + 2) * (4 + 10) + 40 should be equal 82`` =
        let arExp = Operation((+), 
                        Operation((*),
                            Operation((+), Number(1), Number(2)),
                            Operation((+), Number(4), Number(10))),
                        Number(40))

        arExp |> countExpression |> should equal 82

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"
    0 // return an integer exit code
