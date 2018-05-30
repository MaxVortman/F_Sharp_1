module Start

open System

//let readComputersData =
//    printfn "Enter a number of computers in local network"
//    let n = Console.Read()

//    let readComputerData i = 
        

let readMatrix n =     
    printfn "Now enter a %ix%i adjucency matrix" n n
    let rec readLine i acc = 
        match i with
        | 0 -> acc
        | i ->  let splitedLine = Console.ReadLine().Split()
                readLine (i - 1) (splitedLine :: acc)
    readLine n []

[<EntryPoint>]
let main argv = 
    
    0 // return an integer exit code
