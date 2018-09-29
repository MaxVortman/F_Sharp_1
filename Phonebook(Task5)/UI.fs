namespace Phonebook

open System.Collections.Generic

/// <summary>
/// Класс, обеспечивающий работу с пользовательским интерфейсом
/// </summary>
type UI() = 
    let commands = new Dictionary<int, ICommand>()
    member this.AddCommand key command : unit = 
        commands.Add(key, command)
    
    member this.PrintCommands : unit =
        for KeyValue(k, v) in commands do
            printfn "%s" v.Title

    member this.GetCommand key : ICommand = commands.[key]