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
        | [] -> None
        | [a] -> if a = x then Some(i) else None
        | h :: t -> if h = x then Some(i)
                    else index (i + 1) t
    index 0 list

[<EntryPoint>]
let main argv =
    printfn "Choose the problem:\n1 - mul digits in number\n2 - index of\n"
    let problemNumber = Convert.ToInt32(Console.ReadLine())
    if problemNumber = 1 then 
        printfn "Enter a number: "
        let f, x = Int32.TryParse(Console.ReadLine())
        if f = true then
            printfn "%i" <| mulDigits x
    elif problemNumber = 2 then 
        printfn "Enter the list elem: "
        let list = Console.ReadLine().Split() |> List.ofArray |> List.map int
        printfn "Enter the elem to find: "
        let f, x = Int32.TryParse(Console.ReadLine())
        if f = true then
            let index = indexOf x list
            if index = None then 
                printfn "%i not in list" x
                else
                    printfn "Index of %i in list : %i" <| x <| (Option.get index)

    Console.ReadKey() |> ignore

    0 // return an integer exit code
