// Learn more about F# at http://fsharp.org

open System

let mulDigits x = 
    let rec mul n acc = 
        if n % 10 = n then acc * n
        else mul (n / 10) (acc * (n % 10))
    mul x 1

[<EntryPoint>]
let main argv =
    printfn "Choose the problem:\n1 - mul digits in number\n"
    let problemNumber = Convert.ToInt32(Console.ReadLine())
    if problemNumber = 1 then 
        printfn "Enter a number: "
        let x = Convert.ToInt32(Console.ReadLine())
        printfn "%i" <| mulDigits x

    Console.ReadKey() |> ignore

    0 // return an integer exit code
