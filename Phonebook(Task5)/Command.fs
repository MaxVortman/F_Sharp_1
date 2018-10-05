namespace Phonebook

type Command =
| SerializeCommand of string * (string -> unit)
| DeserializeCommand of string * (string -> ContactBook option)
| AddContactCommand of string * (string -> string -> ContactBook)
| FindContactCommand of string * (string -> unit)
| PrintContactsCommand of string * (unit -> unit)