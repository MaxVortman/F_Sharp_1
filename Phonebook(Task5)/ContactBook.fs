namespace Phonebook

open System.Linq

type ContactBook() = 
    member this.Contacts : Contact list = []
    member this.Add contact = contact :: this.Contacts
    member this.FindByName name = this.Contacts.FirstOrDefault(fun c -> c.Name = name)
    member this.FindByNumber number = this.Contacts.FirstOrDefault(fun c -> c.Number = number)
    member this.Print = 
        let rec printContact (contacts : Contact list) = 
            match contacts with
            | h :: t -> h.Print
                        printContact t
            | _ -> ()
        printContact this.Contacts
    