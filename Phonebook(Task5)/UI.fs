namespace Phonebook

open System.Collections.Generic

type UI() = 

    let mutable commands : Dictionary<int, ICommand> = new Dictionary<int, ICommand>()
    member this.AddCommand key command : unit = 
        commands.Add(key, command)
    
    member this.PrintCommands : unit = 
        let mutable enum = commands.Values.GetEnumerator()
        let rec print (cur : ICommand) = 
            printfn "%s" cur.Title
            if enum.MoveNext() then print enum.Current
        if enum.MoveNext() then
            print enum.Current
        else ()

    member this.GetCommand key : ICommand = commands.[key]
    
