// Learn more about F# at http://fsharp.org

open System

module F_Sharp_3_1 =

    module FirstOption = 
        let countEvenNumbers list =            
            list |> List.map (fun x -> if x % 2 = 0 then 1 else 0) |> List.sum
    
    module SecondOption = 
        let countEvenNumbers list = 
            list |> List.filter (fun x -> x % 2 = 0) |> List.length       

    module ThirdOption = 
        let countEvenNumbers list = 
            list |> List.fold (fun acc x -> if x % 2 = 0 then acc + 1 else acc) 0

       
[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"
    0 // return an integer exit code
