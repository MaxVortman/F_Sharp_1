module UIFactory

open Phonebook

let createUI = 
    let ui = new UI()
    ui.AddCommand 1 {new ICommand with
                         member this.Action(): unit = 
                             raise (System.NotImplementedException())
                         member this.Title: string = 
                             "1: Add new contact"}
    ui.AddCommand 2 {new ICommand with
                         member this.Action(): unit = 
                             raise (System.NotImplementedException())
                         member this.Title: string = 
                             "2: Find contact by name"}
    ui.AddCommand 3 {new ICommand with
                         member this.Action(): unit = 
                             raise (System.NotImplementedException())
                         member this.Title: string = 
                              "3: Find contact by number"}
    ui.AddCommand 4 {new ICommand with
                         member this.Action(): unit = 
                             raise (System.NotImplementedException())
                         member this.Title: string = 
                             "4: Print all contacts" }
    ui.AddCommand 5 {new ICommand with
                         member this.Action(): unit = 
                             raise (System.NotImplementedException())
                         member this.Title: string = 
                             "5: Save in file [full path]" }
    ui.AddCommand 6 {new ICommand with
                         member this.Action(): unit = 
                             raise (System.NotImplementedException())
                         member this.Title: string = 
                             "6: Read from file [full path]" }
    ui