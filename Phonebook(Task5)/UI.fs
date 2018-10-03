namespace Phonebook

/// <summary>
/// Класс, обеспечивающий работу с пользовательским интерфейсом
/// </summary>
/// <param name="commands">Команды пользовательского интерфейса</param>
type UI(commands : Map<int, Command>) = 
    member this.AddCommand key command : UI = 
        new UI(commands.Add(key, command))
    
    member this.PrintCommands : unit =
        let rec printCommandsInternal cmds = 
            match cmds with
            | h :: t -> match h with
                        | SerializeCommand(title, _) | DeserializeCommand(title, _) | AddContactCommand(title, _)
                        | FindContactCommand(title, _) | PrintContactsCommand(title, _) ->  printfn "%s" title
                                                                                            printCommandsInternal t
            | _ -> ()
        commands |> Map.toList |> List.map (snd) |> printCommandsInternal
            
    member this.GetCommand key : Command = commands.[key]