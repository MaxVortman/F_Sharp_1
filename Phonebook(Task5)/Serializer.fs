namespace Phonebook

open System
open System.IO
open System.Runtime.Serialization.Formatters.Binary

/// <summary>
/// Класс, обеспечивающий работу с средствами сериализации
/// </summary>
type Serializer() = 

    let createStream path (mode : FileMode) = new FileStream(path, mode)
    let formatter = new BinaryFormatter()

    member s.Serialize path (obj : 'a) =         
        use stream = createStream path FileMode.Create
        formatter.Serialize(stream, box obj)

    member s.Deserialize path =
        use stream = createStream path FileMode.Open
        let res = formatter.Deserialize(stream)
        unbox res

