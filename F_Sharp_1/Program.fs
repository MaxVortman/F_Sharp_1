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


let reverse list =
        let rec rev rlist list =
            match list with
            | [] -> rlist
            | h :: t -> rev (h :: rlist) t
        rev [] list



let createSpecialList n m = 
    let rec createSLRec i list b =     
        if i = m then b :: list
        else         
            createSLRec (i + 1) (b :: list) (b <<< 1)
    if n = 0 then
        createSLRec 0 [] 1 |> List.rev 
    else
        createSLRec 0 [] (2 <<< n) |> List.rev

[<EntryPoint>]
let main argv =
    printfn "Choose the problem:\n1-factorial\n2-fibonacci\n3-reverse\n4-special list"
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
        printfn "%A " (reverse list)
    elif problemNumber = 4 then
        printfn "Enter the \"n\" and \"m\""
        let list = Console.ReadLine().Split() |> List.ofArray |> List.map (fun a -> int a)
        printfn "Created list : "
        printfn "%A " (createSpecialList list.[0] list.[1])

    Console.ReadKey() |> ignore
    0 // return an integer exit code