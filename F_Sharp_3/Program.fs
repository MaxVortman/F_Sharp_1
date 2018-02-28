// Learn more about F# at http://fsharp.org

open System
open NUnit.Framework
open FsUnit

module F_Sharp_3_1 =

    module FirstOption = 
        let countEvenNumbers list =            
            list |> List.map (fun x -> if x % 2 = 0 then 1 else 0) |> List.fold (+) 0

        [<Test>]
        let ``Count even numbers of list [0 .. 10] should be equal 6`` =
            [0 .. 10] |> countEvenNumbers |> should equal 6

    module SecondOption = 
        let countEvenNumbers list = 
            list |> List.filter (fun x -> x % 2 = 0) |> List.length

        [<Test>]
        let ``Count even numbers of list [0 .. 10] should be equal 6`` =
            [0 .. 10] |> countEvenNumbers |> should equal 6

    module ThirdOption = 
        let countEvenNumbers list = 
            list |> List.fold (fun acc x -> if x % 2 = 0 then acc + 1 else acc) 0

        [<Test>]
        let ``Count even numbers of list [0 .. 10] should be equal 6`` =
            [0 .. 10] |> countEvenNumbers |> should equal 6

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"
    0 // return an integer exit code
