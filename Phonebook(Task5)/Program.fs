open Phonebook
open System



let rec interactiveMode() = 
    printfn "\nMy Phonebook\n"
    let ui = UIFactory.createUI
    ui.PrintCommands
    printfn "Enter a command or press Escape to exit...\n"
    let x = Console.ReadKey()
    printfn "\n"
    if x.Key = ConsoleKey.Escape then ()
    else 
        let command = ui.GetCommand (Convert.ToInt32(x.KeyChar.ToString()))
        command.Action()
        interactiveMode()

[<EntryPoint>]
let main argv =
    interactiveMode()
    0 // return an integer exit code    
