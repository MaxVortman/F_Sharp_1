open Phonebook
open System

let rec interactiveMode contactBook = 
    printfn "\nMy Phonebook\n"
    let ui = UIFactory.createUI contactBook
    ui.PrintCommands
    printfn "Enter a command or press Escape to exit...\n"
    let x = Console.ReadKey()
    printfn "\n"
    match x.Key with
    |ConsoleKey.Escape -> ()
    |_ ->   let command = ui.GetCommand (Convert.ToInt32(x.KeyChar.ToString()))
            match command with
            | :? AddContactCommand as c ->  printfn "Enter a name: "
                                            let name = Console.ReadLine()
                                            printfn "Enter a phone number: "
                                            let number = Console.ReadLine()
                                            c.Func name number
                                            interactiveMode contactBook
            | :? FindContactCommand as c -> printfn "Enter a find parameter: "
                                            let param = Console.ReadLine()
                                            c.Func param
                                            interactiveMode contactBook
            | :? PrintContactsCommand as c ->   c.Func()
                                                interactiveMode contactBook
            | :? SerializeCommand as c ->   printfn "Enter a file full path: "
                                            let path = Console.ReadLine()
                                            c.Func path
                                            interactiveMode contactBook
            | :? DeserializeCommand as c -> printfn "Enter a file full path: "
                                            let path = Console.ReadLine()
                                            let newContactBook = c.Func path
                                            match newContactBook with
                                            | None -> interactiveMode contactBook
                                            | Some(v) -> interactiveMode v
            | _ -> failwith "Wrong argument"

[<EntryPoint>]
let main argv =
    interactiveMode (new ContactBook())
    0 // return an integer exit code