namespace Phonebook

open System

[<StructuredFormatDisplay("{Title}")>]
type ICommand =
    abstract member Title : string
    abstract member Action : unit -> unit
