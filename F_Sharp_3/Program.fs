// Learn more about F# at http://fsharp.org

module F_Sharp_3_1

    module FirstOption = 
        let countEvenNumbers list =            
            list |> List.map (fun x -> if x % 2 = 0 then 1 else 0) |> List.sum
    
    module SecondOption = 
        let countEvenNumbers list = 
            list |> List.filter (fun x -> x % 2 = 0) |> List.length       

    module ThirdOption = 
        let countEvenNumbers list = 
            list |> List.fold (fun acc x -> if x % 2 = 0 then acc + 1 else acc) 0


module F_Sharp_3_2 =
    type BinaryTree<'a> = 
    | Node of 'a * BinaryTree<'a> * BinaryTree<'a>
    | Empty

    let mapTree f tree = 
        let rec map tree =
            match tree with
            | Node(data, left, right) -> Node(f data, map left, map right)
            | Empty -> Empty
        map tree
         

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"
    0 // return an integer exit code
