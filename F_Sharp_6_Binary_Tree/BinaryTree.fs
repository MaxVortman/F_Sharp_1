namespace F_Sharp_6_Binary_Tree

open System.Collections


type Node<'a, 'b> (key, value, right, left) =
    member val Key = key with get
    member val Value = value with get
    member val Right : Node<'a, 'b> option = right with get, set
    member val Left : Node<'a, 'b> option = left with get, set
    new(key, value) = Node<'a, 'b>(key, value, None, None)

type BST<'a, 'b when 'a : comparison> ()  = 
    let mutable root : Node<'a, 'b> option = None
    member this.Add key value = 
        let rec add (node : Node<'a, 'b> option) =
            match node with
            | None ->                                   Some(new Node<'a, 'b>(key, value))
            | Some(value) when value.Key < key ->       Some(new Node<'a, 'b>(value.Key, value.Value, add value.Right, value.Left))
            | Some(value) ->                            Some(new Node<'a, 'b>(value.Key, value.Value, value.Right, add value.Left))
        root <- add root

    member this.Find key =
        let rec find (node : Node<'a, 'b> option) =
            match node with
            | None -> None
            | Some(value) when value.Key = key ->   Some(value.Value)
            | Some(value) when value.Key < key ->   find value.Right
            | Some(value) ->                        find value.Left
        find root

    member this.Remove key =
        