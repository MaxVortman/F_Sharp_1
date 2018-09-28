namespace Phonebook

type ICommand =
    abstract member Title : string

type UnitCommand(title : string, action : unit -> unit) =
    interface ICommand with
        member this.Title = title
    member this.Action = action

type ContactBookCommand(title : string, func : unit -> ContactBook option) =
    interface ICommand with
        member this.Title = title
    member this.Func = func