namespace Phonebook

type ContactBook() = 
    member val Contacts : Contact list = [] with get, set
    member this.Add contact = this.Contacts <- (contact :: this.Contacts)
    member this.FindByName name = this.Contacts |> List.tryFind (fun c -> c.Name = name)
    member this.FindByNumber number = this.Contacts |> List.tryFind (fun c -> c.Number = number)
    member this.Print = 
        let rec printContact (contacts : Contact list) = 
            match contacts with
            | h :: t -> h.Print
                        printContact t
            | _ -> ()
        printContact this.Contacts
    