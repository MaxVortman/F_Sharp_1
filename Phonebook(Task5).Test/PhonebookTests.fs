namespace Phonebook_Task5_.Test
open NUnit.Framework
open FsUnit
open Phonebook
open System.IO

module Test =
    
    let contactTuple (contact : Contact) = (contact.Name, contact.Number)

    [<Test>]
    let ``should add contact``() =
        let contactBook = new ContactBook()
        let ui = UIFactory.createUI contactBook
        let name = "Max"
        let number = "12315513"
        match (ui.GetCommand 1) with 
        | AddContactCommand(_, func) -> (func name number).Contacts |> List.exactlyOne |> contactTuple |> should equal (name, number)
        | _ -> failwith "Wrong command"

    [<Test>]
    let ``should add and find contact``() =
        let ui = UIFactory.createUI (new ContactBook())
        let name = "Max"
        let number = "12315513"
        match (ui.GetCommand 1) with
        | AddContactCommand(_, func) -> let updateContactBook = func name number
                                        let findByNameContact = updateContactBook.FindByName name |> Option.get
                                        let findByNumberContact = updateContactBook.FindByNumber number |> Option.get
                                        findByNameContact |> contactTuple |> should equal (name, number)
                                        findByNumberContact |> contactTuple |> should equal (name, number)
        | _ -> failwith "Wrong command"

    [<Test>]
    let ``should serialize and deserialize contact book``() =
        let name = "Max"
        let number = "12315513"
        let path = @"testSerialize.test"
        let contactBook = (new ContactBook()).Add(new Contact(name, number))
        let ui = UIFactory.createUI contactBook
        match (ui.GetCommand 5), (ui.GetCommand 6) with 
        | SerializeCommand(_, funcSer), DeserializeCommand(_, funcDes) ->   funcSer path
                                                                            let deserializedContactBook = funcDes path
                                                                            deserializedContactBook.Value.Contacts |> List.exactlyOne |> contactTuple |> should equal (name, number)
                                                                            File.Delete path
        | _ -> failwith "Wrong command"