module UIFactory

open Phonebook
open System
open System.IO

let createUI (contactBook : ContactBook) =
    let ui = new UI()
    let serializer = new Serializer()
    ui.AddCommand 1 (new AddContactCommand ("1: Add new contact",
                                (fun name number  ->
                                let contact = new Contact(name, number)
                                contactBook.Add contact))
                                )
    ui.AddCommand 2 (new FindContactCommand ("2: Find contact by name",
                                (fun name ->
                                let contact = contactBook.FindByName name
                                match contact with
                                | None -> printfn "Nobody with that name is here"
                                | Some(v) ->   v.Print))
                                )
    ui.AddCommand 3 (new FindContactCommand ("3: Find contact by number",
                                (fun number ->
                                let contact = contactBook.FindByNumber number
                                match contact with
                                | None ->  printfn "Nobody with that number is here"
                                | Some(v) ->   v.Print))
                                )
    ui.AddCommand 4 (new PrintContactsCommand ("4: Print all contacts",
                                (fun () ->
                                contactBook.Print))
                                )
    ui.AddCommand 5 (new SerializeCommand ("5: Save in file", 
                                (fun path ->
                                try
                                    serializer.Serialize path contactBook
                                    printfn "Done!"
                                with
                                | :? IOException -> printfn "%s not found" path))
                                )
    ui.AddCommand 6 (new DeserializeCommand ("6: Read from file", 
                                (fun path ->
                                try
                                    Some(serializer.Deserialize path)
                                with 
                                | :? IOException -> printfn "%s not found" path
                                                    None))
                                )
    ui