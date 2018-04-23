module UIFactory

open Phonebook
open System
open System.IO

let createUI = 
    let mutable contactBook = new ContactBook()
    let ui = new UI()
    let serializer = new Serializer()
    ui.AddCommand 1 {new ICommand with
                         member this.Action(): unit = 
                             printfn "Enter a name: "
                             let name = Console.ReadLine()
                             printfn "Enter a phone number: "
                             let number = Console.ReadLine()
                             let contact = new Contact(name, number)
                             contactBook.Add contact

                         member this.Title: string = 
                             "1: Add new contact"}
    ui.AddCommand 2 {new ICommand with
                         member this.Action(): unit = 
                             printfn "Enter a name: "
                             let name = Console.ReadLine()
                             let contact = contactBook.FindByName name
                             match contact with
                             | null -> printfn "Nobody with that name is here"
                             | _ -> contact.Print                                                         

                         member this.Title: string = 
                             "2: Find contact by name"}
    ui.AddCommand 3 {new ICommand with
                         member this.Action(): unit = 
                             printfn "Enter a number: "
                             let number = Console.ReadLine()
                             let contact = contactBook.FindByNumber number
                             match contact with
                             | null -> printfn "Nobody with that number is here"
                             | _ -> contact.Print  

                         member this.Title: string = 
                              "3: Find contact by number"}
    ui.AddCommand 4 {new ICommand with
                         member this.Action(): unit = 
                             contactBook.Print

                         member this.Title: string = 
                             "4: Print all contacts" }
    ui.AddCommand 5 {new ICommand with
                         member this.Action(): unit = 
                             printfn "Enter a file full path: "
                             let path = Console.ReadLine()
                             try
                                serializer.Serialize path contactBook
                                printfn "Done!"
                             with
                             | :? IOException -> printfn "%s not found" path

                         member this.Title: string = 
                             "5: Save in file" }
    ui.AddCommand 6 {new ICommand with
                         member this.Action(): unit = 
                             printfn "Enter a file full path: "
                             let path = Console.ReadLine()
                             try
                                contactBook <- serializer.Deserialize path
                                printfn "Done!"
                             with 
                             | :? IOException -> printfn "%s not found" path

                         member this.Title: string = 
                             "6: Read from file" }
    ui