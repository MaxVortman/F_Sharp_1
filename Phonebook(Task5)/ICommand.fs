namespace Phonebook


/// <summary>
/// Интерфейс, описывающий модель поведения команд телефонной книги
/// </summary>
type ICommand =
    abstract member Title : string

/// <summary>
/// Класс, представляющий команду сериализации списка контактов
/// </summary>
type SerializeCommand(title : string, func : string -> unit) =
    interface ICommand with
        member this.Title = title
    member this.Func = func

/// <summary>
/// Класс, представляющий команду десериализации списка контактов
/// </summary>
type DeserializeCommand(title : string, func : string -> ContactBook option) =
    interface ICommand with
        member this.Title = title
    member this.Func = func

/// <summary>
/// Класс, представляющий команду добавления контакта к списку контактов
/// </summary>
type AddContactCommand(title : string, func : string -> string -> unit) =
    interface ICommand with
        member this.Title = title
    member this.Func = func

/// <summary>
/// Класс, представляющий команду поиска контакта
/// </summary>
type FindContactCommand(title : string, func : string -> unit) =
    interface ICommand with
        member this.Title = title
    member this.Func = func

/// <summary>
/// Класс, представляющий команду печати списка контактов
/// </summary>
type PrintContactsCommand(title : string, action : unit -> unit) =
    interface ICommand with
        member this.Title = title
    member this.Func = action