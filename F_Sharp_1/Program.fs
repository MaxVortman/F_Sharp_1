// Learn more about F# at http://fsharp.org

open System

let factorial x : bigint =
    match x with 
        | 0 -> 1I
        | _ -> [1..x] |> Seq.fold (fun acc elem -> acc*bigint elem) 1I


[<EntryPoint>]
let main argv =
    let x = Console.Read()
    Console.WriteLine(factorial x)
    Console.ReadKey()
    0 // return an integer exit code