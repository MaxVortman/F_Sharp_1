namespace F_Sharp_6_Binary_Tree.Test

open FsUnit
open NUnit.Framework
open F_Sharp_6_Binary_Tree

module BinarySearchTreeTests =
    open System.Collections.Generic
    open System
    open NUnit.Framework.Internal

    [<Test>]
    let ``should add elem 10 with key 1`` () =
        let binTree = new BST<int, int>()
        binTree.Add 1 10
        binTree.Find 1 |> should equal (Some(10))

    [<Test>]
    let ``should find None`` () =
        let binTree = new BST<int, int>()
        binTree.Find 1 |> should equal (None)

    let bst () =
        let binTree = new BST<int, string>()
        binTree.Add 1 "root"
        binTree.Add 2 "right leaf"
        binTree.Add 0 "left leaf"
        binTree

    [<Test>]
    let ``should find leafs`` () =
        let binTree = bst()
        binTree.Find 0 |> should equal (Some("left leaf"))
        binTree.Find 2 |> should equal (Some("right leaf"))

    [<Test>]
    let ``should remove leaf and find None`` () =
        let binTree = bst()
        match binTree.Remove 0 with
        | true -> binTree.Find 0 |> should equal None
        | false -> failwith "Didn't remove leaf"

    [<Test>]
    let ``should remove root and take new root (right leaf)``() =
        let binTree = bst()
        match binTree.Remove 1 with
        | true ->   let enumerator = (binTree :> IEnumerable<Node<int, string>>).GetEnumerator()
                    match enumerator.MoveNext() && enumerator.MoveNext() with
                    | true ->   enumerator.Current.Value |> should equal "right leaf"
                    | false ->  failwith "Didn't move"
        | false ->  failwith "Didn't remove leaf"

    [<Test>]
    let ``should throw ArgumentException``() =
        let binTree = bst()        
        (fun () -> binTree.Add 2 "new right leaf" |> ignore) |> should throw typeof<ArgumentException>

    [<Test>]
    let ``should remove node wuth not simple children`` () = 
        let hardBST = 
            let binTree = new BST<int, string>()
            binTree.Add 1 "root"
            binTree.Add 3 "right node"
            binTree.Add 0 "left node"
            binTree.Add -1 "left left node"
            binTree.Add 2 "right left node"
            binTree.Add 4 "right right node"
            binTree
        match hardBST.Remove 1 with 
        | true ->   hardBST.Find 1 |> should equal None                    
        | false ->  failwith "Didn't remove root"

    [<Test>]
    let ``should check IEnumerable`` () =
        let binTree = bst()
        (binTree :> IEnumerable<Node<int, string>>) |>
        Seq.cast |> Seq.filter (fun (x : Node<int, string>) -> x.Value = "right leaf") |>
        Seq.length |> should equal 1