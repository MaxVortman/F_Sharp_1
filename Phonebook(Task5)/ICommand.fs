namespace Phonebook

type ICommand =
    abstract member Title : string

type SerializeCommand(title : string, func : string -> unit) =
    interface ICommand with
        member this.Title = title
    member this.Func = func

type DeserializeCommand(title : string, func : string -> ContactBook option) =
    interface ICommand with
        member this.Title = title
    member this.Func = func

type AddContactCommand(title : string, func : string -> string -> unit) =
    interface ICommand with
        member this.Title = title
    member this.Func = func

type FindContactCommand(title : string, func : string -> unit) =
    interface ICommand with
        member this.Title = title
    member this.Func = func

type PrintContactsCommand(title : string, action : unit -> unit) =
    interface ICommand with
        member this.Title = title
    member this.Func = action