// Learn more about F# at http://fsharp.org

open System
open System.Numerics

let factorial x : bigint =
    match x with 
        | 0 -> 1I
        | _ -> [1..x] |> Seq.fold (fun acc elem -> acc*bigint elem) 1I

let fibSeq = Seq.unfold (fun state ->
        Some(fst state + snd state, (snd state, fst state + snd state))) (0I, 1I)

let fibonacci x : bigint = 
    match x with 
        | 0 -> 0I
        | 1 -> 1I
        | _ -> fibSeq |> Seq.take (x - 1) |> Seq.last


[<EntryPoint>]
let main argv =
    let x = Convert.ToInt32(Console.ReadLine())
    printfn "Factorial : %A \nFibonacci : %A" (factorial x) (fibonacci x)
    Console.ReadKey() |> ignore
    0 // return an integer exit code