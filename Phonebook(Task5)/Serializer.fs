namespace Phonebook

open System
open System.IO
open System.Runtime.Serialization.Formatters.Binary


type Serializer() = 

    let createStream path (mode : FileMode) = new FileStream(path, mode)
    let formatter = new BinaryFormatter()

    member s.Serialize path (obj : 'a) =         
        let stream = createStream path FileMode.Create
        formatter.Serialize(stream, box obj)
        stream.Close()

    member s.Deserialize path =
        let stream = createStream path FileMode.Open
        let res = formatter.Deserialize(stream)
        stream.Close()
        unbox res

