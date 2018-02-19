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


let rec reverseList list =
    match list with
    | [] -> []
    | [x] -> [x]
    | head :: tail -> reverseList tail @ [head]


[<EntryPoint>]
let main argv =
    printfn "Choose the problem:\n1-factorial\n2-fibonacci\n3-reverse\n"
    let problemNumber = Convert.ToInt32(Console.ReadLine())
    if problemNumber = 1 then
        printfn "Enter the number"
        let x = Convert.ToInt32(Console.ReadLine())
        printfn "Factorial : %A" (factorial x)
    elif problemNumber = 2 then
        printfn "Enter the number"
        let x = Convert.ToInt32(Console.ReadLine())
        printfn "Fibonacci : %A" (fibonacci x)
    elif problemNumber = 3 then
        printfn "Enter the list elem"
        let list = Console.ReadLine().Split() |> List.ofArray
        printfn "Reversed list : "
        printfn "%A " (reverseList list)

    Console.ReadKey() |> ignore
    0 // return an integer exit code