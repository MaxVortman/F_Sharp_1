module UIFactory

open Phonebook
open System.IO

let createUI (contactBook : ContactBook) =
    let serializer = new Serializer()
    let commands =  [1, AddContactCommand ("1: Add new contact",
                                (fun name number  ->
                                let contact = new Contact(name, number)
                                contactBook.Add contact))
                                ;
                    2, FindContactCommand ("2: Find contact by name",
                                (fun name ->
                                let contact = contactBook.FindByName name
                                match contact with
                                | None -> printfn "Nobody with that name is here"
                                | Some(v) ->   v.Print))
                                ;
                    3, FindContactCommand ("3: Find contact by number",
                                (fun number ->
                                let contact = contactBook.FindByNumber number
                                match contact with
                                | None ->  printfn "Nobody with that number is here"
                                | Some(v) ->   v.Print))
                                ;
                    4, PrintContactsCommand ("4: Print all contacts",
                                (fun () ->
                                contactBook.Print))
                                ;
                    5, SerializeCommand ("5: Save in file", 
                                (fun path ->
                                try
                                    serializer.Serialize path contactBook
                                    printfn "Done!"
                                with
                                | :? IOException -> printfn "%s not found" path))
                                ;
                    6, DeserializeCommand ("6: Read from file", 
                                (fun path ->
                                try
                                    Some(serializer.Deserialize path)
                                with 
                                | :? IOException -> printfn "%s not found" path
                                                    None))
                                ] |> Map.ofList
    new UI(commands)