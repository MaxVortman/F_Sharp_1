namespace Phonebook

open System

type ICommand =
    abstract member Title : string
    abstract member Action : unit -> unit
