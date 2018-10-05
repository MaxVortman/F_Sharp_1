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
            | AddContactCommand(_, func) -> printfn "Enter a name: "
                                            let name = Console.ReadLine()
                                            printfn "Enter a phone number: "
                                            let number = Console.ReadLine()
                                            interactiveMode <| func name number
            | FindContactCommand(_, func) -> printfn "Enter a find parameter: "
                                             let param = Console.ReadLine()
                                             func param
                                             interactiveMode contactBook
            | PrintContactsCommand(_, func) ->  func()
                                                interactiveMode contactBook
            | SerializeCommand(_, func) ->  printfn "Enter a file full path: "
                                            let path = Console.ReadLine()
                                            func path
                                            interactiveMode contactBook
            | DeserializeCommand(_, func) -> printfn "Enter a file full path: "
                                             let path = Console.ReadLine()
                                             let newContactBook = func path
                                             match newContactBook with
                                             | None -> interactiveMode contactBook
                                             | Some(v) -> interactiveMode v
            | _ -> failwith "Wrong argument"

[<EntryPoint>]
let main argv =
    interactiveMode (new ContactBook())
    0