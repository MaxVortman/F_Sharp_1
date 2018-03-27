// Learn more about F# at http://fsharp.org

open System
open NUnit.Framework
open FsUnit

module ``3_1Test`` =

    let ``Count even numbers of list [0 .. 10] should be equal 6`` countEvenNumbers =
        [0 .. 10] |> countEvenNumbers |> should equal 6

    [<Test>]
    let ``for first option`` = ``Count even numbers of list [0 .. 10] should be equal 6`` Program.F_Sharp_3_1.FirstOption.countEvenNumbers

    [<Test>]
    let ``for second option`` = ``Count even numbers of list [0 .. 10] should be equal 6`` Program.F_Sharp_3_1.SecondOption.countEvenNumbers

    [<Test>]
    let ``for third option`` = ``Count even numbers of list [0 .. 10] should be equal 6`` Program.F_Sharp_3_1.ThirdOption.countEvenNumbers


[<EntryPoint>]
let main argv = 0 // return an integer exit code
