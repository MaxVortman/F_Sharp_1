namespace F_Sharp_6_Binary_Tree

open System.Collections.Generic
open System

/// <summary>
/// Узел дерева
/// </summary>
type Node<'a, 'b> (key : 'a, value : 'b, right, left) =
    member val Key = key with get
    member val Value = value with get
    member val Right : Node<'a, 'b> option = right with get, set
    member val Left : Node<'a, 'b> option = left with get, set
    new(key, value) = Node<'a, 'b>(key, value, None, None)

/// <summary>
/// Двоичное дерево поиска
/// </summary>
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

    let rec minimum (root : Node<'a, 'b> option) (parent : Node<'a, 'b> option) =
        match root with
        | None ->                                   None, parent
        | Some(value) when value.Left <> None ->    minimum value.Left root
        | Some(_) ->                                root, parent
    
    let treeTravesal() =
        let rec travesal (node : Node<'a, 'b> option) =
            match node with
            | None -> Seq.empty
            | Some(value) ->  seq { yield!  travesal value.Left
                                    yield   value
                                    yield!  travesal value.Right }
        travesal root
    /// <summary>
    /// Добавление значения по ключу в дерево
    /// </summary>
    /// <param name="key">ключ</param>
    /// <param name="value">значение</param>
    member this.Add key value =         
        let rec add (node : Node<'a, 'b> option) =
            match node with
            | None ->                                   Some(new Node<'a, 'b>(key, value))
            | Some(value) when value.Key < key ->       Some(new Node<'a, 'b>(value.Key, value.Value, add value.Right, value.Left))
            | Some(value) ->                            Some(new Node<'a, 'b>(value.Key, value.Value, value.Right, add value.Left))
        match this.Find key with
        | None -> root <- add root
        | Some(_) -> raise (ArgumentException("There is already such a key"))
    /// <summary>
    /// Поиск значения по ключу
    /// </summary>
    /// <param name="key">ключ</param>
    member this.Find key =
        let (node, parent) = findNode key
        match node with
        | None ->           None
        | Some(value) ->    Some(value.Value)
    /// <summary>
    /// Удаление узла по ключу
    /// </summary>
    /// <param name="key">ключ</param>
    member this.Remove key =
        let removeNode (parent : Node<'a, 'b> option) (nodeValue : Node<'a, 'b>) =   
            match parent with
            | None ->                                                   root <- None
                                                                        true
            | Some(parentValue) when parentValue.Key < nodeValue.Key -> parentValue.Right <- None
                                                                        true
            | Some(parentValue) ->                                      parentValue.Left <- None
                                                                        true
        let replaceNodeWithOneChild (fromNodeValue : Node<'a, 'b>) (toNode : Node<'a, 'b> option) (parent : Node<'a, 'b> option) = 
            match parent with
            | None ->                                                       root <- toNode
                                                                            true
            | Some(parentValue) when parentValue.Key < fromNodeValue.Key -> parentValue.Right <- toNode
                                                                            true
            | Some(parentValue) ->                                          parentValue.Left <- toNode
                                                                            true
                                               
        let replaceNodeWithTwoChild (fromNodeValue : Node<'a, 'b>) (toNode : Node<'a, 'b> option) (parent : Node<'a, 'b> option) =
            match toNode with
            | None ->               false
            | Some(toNodeValue) ->  let newNode = new Node<'a, 'b>(toNodeValue.Key, toNodeValue.Value)
                                    if fromNodeValue.Right <> toNode  then
                                        newNode.Right <- fromNodeValue.Right
                                    if fromNodeValue.Left <> toNode then
                                        newNode.Left <- fromNodeValue.Left
                                    match parent with 
                                    | None ->                                                       root <- Some(newNode)
                                                                                                    true
                                    | Some(parentValue) when parentValue.Key < fromNodeValue.Key -> parentValue.Right <- Some(newNode)
                                                                                                    true
                                    | Some(parentValue) ->                                          parentValue.Left <- Some(newNode)
                                                                                                    true

        let (node, parent) = findNode key
        let rec remove (node : Node<'a, 'b> option) parent = 
            match node with
            | None ->                                                                   false
            | Some(nodeValue) when nodeValue.Right = None && nodeValue.Left = None ->   removeNode parent nodeValue
            | Some(nodeValue) when nodeValue.Right = None ->                            replaceNodeWithOneChild nodeValue nodeValue.Left parent
            | Some(nodeValue) when nodeValue.Left = None ->                             replaceNodeWithOneChild nodeValue nodeValue.Right parent
            | Some(nodeValue) ->                                                        let (toNode, toNodeParent) = minimum nodeValue.Right node
                                                                                        match replaceNodeWithTwoChild nodeValue toNode parent with
                                                                                        | true -> remove toNode toNodeParent
                                                                                        | false -> false                                                                                        
        remove node parent
    
    
    interface IEnumerable<Node<'a, 'b>> with
        /// <summary>
        /// Получение enumerator-a дерева
        /// </summary>
        member this.GetEnumerator(): IEnumerator<Node<'a, 'b>> = 
            let seq = treeTravesal()
            seq.GetEnumerator()
        /// <summary>
        /// Получение enumerator-a дерева
        /// </summary>
        member this.GetEnumerator(): System.Collections.IEnumerator = 
            (this:>IEnumerable<Node<'a, 'b>>).GetEnumerator():>System.Collections.IEnumerator