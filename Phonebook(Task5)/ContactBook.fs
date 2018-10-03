namespace Phonebook

/// <summary>
/// Класс, обеспечивающий работу списка контактов телефонной книги
/// </summary>
type ContactBook(contacts : Contact list) = 
    new() = ContactBook([])
    member this.Contacts = contacts
    member this.Add contact = new ContactBook(contact :: this.Contacts)
    member this.FindByName name = this.Contacts |> List.tryFind (fun c -> c.Name = name)
    member this.FindByNumber number = this.Contacts |> List.tryFind (fun c -> c.Number = number)
    member this.Print = 
        let rec printContact (contacts : Contact list) = 
            match contacts with
            | h :: t -> h.Print
                        printContact t
            | _ -> ()
        printContact this.Contacts