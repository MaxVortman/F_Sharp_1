open Phonebook
open System


let rec interactiveMode = 
     printfn "My Phinebook\n"
     let ui = new UI()
     ui.PrintCommands
     printfn "Enter a command or press Escape to exit...\n"
     let x = Console.ReadKey()
     if x.Key = ConsoleKey.Escape then ()
     else 
     let command = ui.GetCommand (Convert.ToInt32(x.KeyChar.ToString()))
     command.Action |> ignore


[<EntryPoint>]
let main argv =
    interactiveMode
    0 // return an integer exit code    
