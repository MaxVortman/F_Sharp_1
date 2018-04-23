namespace Phonebook

open System.Collections.Generic

type UI() = 

    let mutable commands : Dictionary<int, ICommand> = new Dictionary<int, ICommand>()
    member this.AddCommand key command : unit = 
        commands.Add(key, command)
    
    member this.PrintCommands : unit = 
        let mutable enum = commands.Values.GetEnumerator()
        let rec print cur = 
            printfn "%A" cur
            if enum.MoveNext() then print enum.Current
        print enum.Current

    member this.GetCommand key : ICommand = commands.[key]
    
