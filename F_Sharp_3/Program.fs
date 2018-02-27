// Learn more about F# at http://fsharp.org

open System
open NUnit.Framework
open FsUnit

module F_Sharp_3_1 =

    let countEvenNumbers list = 
        let subListLength list = 
            List.length list - (List.fold (+) 0 list)
        list |> List.map (fun x -> x % 2) |> subListLength

    [<Test>]
    let ``Count even numbers of list [0 .. 10] should be equal 6`` =
        [0 .. 10] |> countEvenNumbers |> should equal 6

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"
    0 // return an integer exit code
