namespace Phonebook

type Contact(name : string, number : string) =
    member val Name = name with get, set
    member val Number = number with get, set