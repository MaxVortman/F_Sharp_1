namespace Phonebook

/// <summary>
/// Класс, представляющий модель контакта телефонной книги.
/// </summary>
type Contact(name : string, number : string) =
    member val Name = name with get, set
    member val Number = number with get, set
    member this.Print = printfn "Name: %s\nNumber: %s" name number