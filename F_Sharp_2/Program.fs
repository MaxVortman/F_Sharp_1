// Learn more about F# at http://fsharp.org

open System

let mulDigits x = 
    let rec mul n acc = 
        if n % 10 = n then acc * n
        else mul (n / 10) (acc * (n % 10))
    mul x 1

let indexOf x list = 
    let rec index i list = 
        match list with
        | [] -> -1
        | [x] -> i
        | h :: t -> if h = x then i 
                    else index (i + 1) t
    index 0 list

[<EntryPoint>]
let main argv =
    printfn "Choose the problem:\n1 - mul digits in number\n2 - index of\n"
    let problemNumber = Convert.ToInt32(Console.ReadLine())
    if problemNumber = 1 then 
        printfn "Enter a number: "
        let x = Convert.ToInt32(Console.ReadLine())
        printfn "%i" <| mulDigits x
    elif problemNumber = 2 then 
        printfn "Enter the list elem: "
        let list = Console.ReadLine().Split() |> List.ofArray |> List.map (fun x -> int x)
        printfn "Enter the elem to find: "
        let x = Convert.ToInt32(Console.ReadLine())
        printfn "Index of %i in list : %i" <| x <| indexOf x list

    Console.ReadKey() |> ignore

    0 // return an integer exit code
