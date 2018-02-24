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

let isPalindrome str = 
    let rec checkForEquals (list1, list2) i n = 
        if i = n then true
        else
            match (list1, list2) with
            | ([], _) -> false
            | (_, []) -> false
            | ([], []) -> true
            | ([a], [b]) when a = b -> true
            | ([_], [_]) -> false
            | (h1 :: t1, h2 :: t2) -> if h1 <> h2 then false
                                      else checkForEquals (t1, t2) (i + 1) n
    checkForEquals (str, List.rev str) 0 ((List.length str) / 2)

[<EntryPoint>]
let main argv =
    printfn "Choose the problem:\n1 - mul digits in number\n2 - index of\n3 - palindrome\n"
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
    elif problemNumber = 3 then
          printfn "Enter the string: "
          let str = Console.ReadLine()
          printfn "%O" (Seq.toList str |> isPalindrome)
    Console.ReadKey() |> ignore

    0 // return an integer exit code
