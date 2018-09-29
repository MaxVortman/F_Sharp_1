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
        let command = (ui.GetCommand 1) :?> AddContactCommand
        command.Func name number
        contactBook.Contacts |> List.exactlyOne |> contactTuple |> should equal (name, number)

    [<Test>]
    let ``should add and find contact``() =
        let contactBook = new ContactBook()
        let ui = UIFactory.createUI contactBook
        let name = "Max"
        let number = "12315513"
        let addCommand = (ui.GetCommand 1) :?> AddContactCommand
        addCommand.Func name number
        let findByNameContact = contactBook.FindByName name |> Option.get
        let findByNumberContact = contactBook.FindByNumber number |> Option.get
        findByNameContact |> contactTuple |> should equal (name, number)
        findByNumberContact |> contactTuple |> should equal (name, number)

    [<Test>]
    let ``should serialize and deserialize contact book``() =
        let contactBook = new ContactBook()
        let ui = UIFactory.createUI contactBook
        let name = "Max"
        let number = "12315513"
        let path = @"C:\\testSerialize.test"
        contactBook.Add (new Contact(name, number))
        let serializeCommand = (ui.GetCommand 5) :?> SerializeCommand
        serializeCommand.Func path
        let deserializeCommand = (ui.GetCommand 6) :?> DeserializeCommand
        let deserializedContactBook = deserializeCommand.Func path
        deserializedContactBook.Value.Contacts |> List.exactlyOne |> contactTuple |> should equal (name, number)
        File.Delete path