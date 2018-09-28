module UIFactory

open Phonebook
open System
open System.IO

let createUI (contactBook : ContactBook) =
    let ui = new UI()
    let serializer = new Serializer()
    ui.AddCommand 1 (new UnitCommand ("1: Add new contact",
                                (fun () ->
                                printfn "Enter a name: "
                                let name = Console.ReadLine()
                                printfn "Enter a phone number: "
                                let number = Console.ReadLine()
                                let contact = new Contact(name, number)
                                contactBook.Add contact))
                                )
    ui.AddCommand 2 (new UnitCommand ("2: Find contact by name",
                                (fun () ->
                                printfn "Enter a name: "
                                let name = Console.ReadLine()
                                let contact = contactBook.FindByName name
                                match contact with
                                | None -> printfn "Nobody with that name is here"
                                | Some(v) ->   v.Print))
                                )
    ui.AddCommand 3 (new UnitCommand ("3: Find contact by number",
                                (fun () ->
                                printfn "Enter a number: "
                                let number = Console.ReadLine()
                                let contact = contactBook.FindByNumber number
                                match contact with
                                | None ->  printfn "Nobody with that number is here"
                                | Some(v) ->   v.Print))
                                )
    ui.AddCommand 4 (new UnitCommand ("4: Print all contacts",
                                (fun () ->
                                contactBook.Print))
                                )
    ui.AddCommand 5 (new UnitCommand ("5: Save in file", 
                                (fun () ->
                                printfn "Enter a file full path: "
                                let path = Console.ReadLine()
                                try
                                    serializer.Serialize path contactBook
                                    printfn "Done!"
                                with
                                | :? IOException -> printfn "%s not found" path))
                                )
    ui.AddCommand 6 (new ContactBookCommand ("6: Read from file", 
                                (fun () ->
                                printfn "Enter a file full path: "
                                let path = Console.ReadLine()
                                try
                                    Some(serializer.Deserialize path)
                                with 
                                | :? IOException -> printfn "%s not found" path
                                                    None))
                                )
    ui