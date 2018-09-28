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
            | :? UnitCommand as c ->    c.Action()
                                        interactiveMode contactBook
            | :? ContactBookCommand as c -> let newContactBook = c.Func()
                                            match newContactBook with
                                            | None -> interactiveMode contactBook
                                            | Some(v) -> interactiveMode v
            | _ -> failwith "Wrong argument"

[<EntryPoint>]
let main argv =
    interactiveMode (new ContactBook())
    0 // return an integer exit code    
