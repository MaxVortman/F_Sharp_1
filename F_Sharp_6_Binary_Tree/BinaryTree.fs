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
    let findNode key = 
        let rec find (node : Node<'a, 'b> option) (parent : Node<'a, 'b> option) =
            match node with
            | None -> None, parent
            | Some(value) when value.Key = key ->   node, parent
            | Some(value) when value.Key < key ->   find value.Right node
            | Some(value) ->                        find value.Left node
        find root None

    let rec minimum (root : Node<'a, 'b> option) =
        match root with
        | None ->                                   None
        | Some(value) when value.Left <> None ->    minimum value.Left
        | Some(_) ->                                root

    member this.Add key value = 
        let rec add (node : Node<'a, 'b> option) =
            match node with
            | None ->                                   Some(new Node<'a, 'b>(key, value))
            | Some(value) when value.Key < key ->       Some(new Node<'a, 'b>(value.Key, value.Value, add value.Right, value.Left))
            | Some(value) ->                            Some(new Node<'a, 'b>(value.Key, value.Value, value.Right, add value.Left))
        root <- add root

    member this.Find key =
        let (node, parent) = findNode key
        match node with
        | None ->           None
        | Some(value) ->    Some(value.Value)

    member this.Remove key =
        let removeNode (parent : Node<'a, 'b> option) (nodeValue : Node<'a, 'b>) =   
            match parent with
            | None -> false
            | Some(parentValue) when parentValue.Key < nodeValue.Key -> parentValue.Right <- None
                                                                        true
            | Some(parentValue) ->                                      parentValue.Left <- None
                                                                        true
        let replaceNode (fromNodeValue : Node<'a, 'b>) (toNodeValue : Node<'a, 'b> option) (parent : Node<'a, 'b> option) = 
            match parent with
            | None -> false
            | Some(parentValue) when parentValue.Key < fromNodeValue.Key -> parentValue.Right <- toNodeValue
                                                                            true
            | Some(parentValue) ->                                          parentValue.Left <- toNodeValue
                                                                            true
        let (node, parent) = findNode key
        match node with
        | None ->           false
        | Some(nodeValue) when nodeValue.Right = None && nodeValue.Left = None ->   removeNode parent nodeValue
        | Some(nodeValue) when nodeValue.Right = None ->                            replaceNode nodeValue nodeValue.Left parent
        | Some(nodeValue) when nodeValue.Left = None ->                             replaceNode nodeValue nodeValue.Right parent
        | Some(nodeValue) ->                                                        replaceNode nodeValue (minimum nodeValue.Right) parent