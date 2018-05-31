module Start

open System
open LocalNetwork

let osArray = [|OS.Ubuntu; OS.WindowsXP; OS.Dos; OS.Windows7|]
let antivirusArray = [|Antiviruses.Avast; Antiviruses.Avira; Antiviruses.Kaspersky; Antiviruses.Nod32|]
let virusArray = [|Viruses.Conficker; Viruses.ILOVEYOU; Viruses.Slammer; Viruses.SW|]


let readComputersData n =

    let createComputerObj i osNumber antivirNumber = 
        let os = osArray.[osNumber]
        os.InstallAntivirus antivirusArray.[antivirNumber]
        printfn "Antivirus installed!"
        new Computer(i, os)

    let rec readComputerData i acc= 
        match i with
        | i when i = n -> acc
        | i ->  printfn "Enter a number of os and antivirus: "
                let splitedLine = Console.ReadLine().Split()
                let osNumber = int splitedLine.[0]
                let antivirNumber = int splitedLine.[1]
                readComputerData (i + 1) (createComputerObj i osNumber antivirNumber :: acc)
    readComputerData 0 []
        

let readMatrix n =     
    printfn "Now enter a %ix%i adjucency matrix" n n
    let rec readLine i acc = 
        match i with
        | 0 ->  acc  
        | i ->  let splitedLine = Array.ConvertAll(Console.ReadLine().Split(), fun s -> int s)
                readLine (i - 1) (splitedLine :: acc)
    readLine n []

[<EntryPoint>]
let main argv = 
    let createLN () = 
        printfn "Enter a number of computers in local network"
        let n = int (Console.ReadLine())
        let computers = readComputersData n
        let matrix = readMatrix n
        new LocalNetwork.LocalNetwork(computers, matrix)

    let printArray (arr : array<'a>) = 
        let n = arr.Length
        let rec print i = 
            match i with
            | i when i = n ->   ()
            | i ->              printfn "%i - %O" i arr.[i]
                                print (i + 1)
        print 0

    let mutable localNetwork : Option<LocalNetwork.LocalNetwork> = None

    let rec loop () =        
        printfn "1 - display available operating systems\n2 - display available antiviruses\n3 - display available viruses\n4 - create new local network"
        if localNetwork <> None then
            printfn "5 - move\n6 - print info"
        printfn "7 - exit"
        let x = int (Console.ReadLine())
        Console.Clear() |> ignore
        match x with
        | 1 ->                              printArray osArray
                                            loop ()
        | 2 ->                              printArray antivirusArray
                                            loop ()
        | 3 ->                              printArray virusArray
                                            loop ()
        | 4 ->                              localNetwork <- Some(createLN ())
                                            loop ()
        | 5 when localNetwork <> None ->    localNetwork.Value.MoveStep ()
                                            loop ()
        | 6 when localNetwork <> None ->    localNetwork.Value.PrintInfo
                                            loop ()
        | 7 ->                              ()
        | _ ->                              printfn "Wrong number!"
                                            loop ()
    loop ()
    0 // return an integer exit code
