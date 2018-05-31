module Start

open System
open LocalNetwork

let osArray = [|OS.Ubuntu; OS.WindowsXP; OS.Dos; OS.Windows7|]
let antivirusArray = [|Antiviruses.Avast; Antiviruses.Avira; Antiviruses.Kaspersky; Antiviruses.Nod32|]
let virusArray = [|Viruses.Conficker; Viruses.ILOVEYOU; Viruses.Slammer; Viruses.SW|]



let readComputersData n =

    let createComputerObj i osNumber antivirNumber = 
        let os = osArray.[osNumber]
        if os.InstallAntivirus antivirusArray.[antivirNumber] then
            printfn "Antivirus installed!"
        else printfn "Antivirus installation failed"
        new Computer(i, os)

    let rec readComputerData i acc= 
        match i with
        | 0 -> acc
        | i ->  printfn "Enter a number of os and antivirus: "
                let splitedLine = Console.ReadLine().Split()
                let osNumber = int splitedLine.[0]
                let antivirNumber = int splitedLine.[1]
                readComputerData (i - 1) (createComputerObj i osNumber antivirNumber :: acc)
    readComputerData n []
        

let readMatrix n =     
    printfn "Now enter a %ix%i adjucency matrix" n n
    let rec readLine i acc = 
        match i with
        | 0 -> acc  
        | i ->  let splitedLine = Array.ConvertAll(Console.ReadLine().Split(), fun s -> int s)
                readLine (i - 1) (splitedLine :: acc)
    readLine n []

[<EntryPoint>]
let main argv = 
    printfn "Enter a number of computers in local network"
    let n = Console.Read()
    let computers = readComputersData n
    let matrix = readMatrix n

    0 // return an integer exit code
