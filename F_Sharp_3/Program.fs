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

    type ArithmeticExpression<'a, 'f> =
    | Operation of 'f * ArithmeticExpression<'a, 'f> * ArithmeticExpression<'a, 'f>
    | Number of 'a

    let countExpression arExp =
        let rec countExpTR arExp =
            match arExp with
            | Operation(op, left, right) -> op <| countExpTR (left) <| countExpTR (right)
            | Number(n) -> n
        countExpTR arExp

    [<Test>]
    let ``Compute an arithmetic expression (1 + 2) * (4 + 10) + 40 should be equal 82`` =
        let arExp = Operation((+), 
                        Operation((*),
                            Operation((+), Number(1), Number(2)),
                            Operation((+), Number(4), Number(10))),
                        Number(40))

        arExp |> countExpression |> should equal 82

module F_Sharp_3_4 =
    
    let infinitePrimeSeq = 
        let isPrime n =
            let sqrtn = int (sqrt <| double n)
            let rec check i =
                if i > sqrtn then true
                elif n % i <> 0 then check (i + 1)
                else false
            check 2

        let unlimitedMinimization prevPrime = 
            let rec nextPrime i = 
                if isPrime i then i else nextPrime (i + 2)
            nextPrime (prevPrime + 2)

        let rec infinitePrimeSeqFrom number =         
            seq { yield number 
                  yield! unlimitedMinimization number |> infinitePrimeSeqFrom}

        Seq.append <| [ 2 ] <| infinitePrimeSeqFrom 3 

    [<Test>]
    let ``Take first 12 prime numbers its should be "2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37"`` =
        infinitePrimeSeq |> Seq.take 12 |> should equal [2; 3; 5; 7; 11; 13; 17; 19; 23; 29; 31; 37] 
      
[<EntryPoint>]
let main argv =
    F_Sharp_3_4.infinitePrimeSeq |> Seq.iter (printf "%d ")
    Console.ReadKey() |> ignore
    0 // return an integer exit code
