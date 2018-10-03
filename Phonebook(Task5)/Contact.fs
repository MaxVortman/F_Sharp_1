namespace Phonebook

/// <summary>
/// Класс, представляющий модель контакта телефонной книги.
/// </summary>
type Contact(name : string, number : string) =
    member this.Name = name
    member this.Number = number
    member this.Print = printfn "Name: %s\nNumber: %s" name number