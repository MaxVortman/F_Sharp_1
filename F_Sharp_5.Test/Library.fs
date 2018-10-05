namespace F_Sharp_5.Test

open NUnit.Framework
open FsUnit
module ``1`` =
    open NUnit.Framework.Constraints
    
    [<Test>]
    let ``should be correct at sample : "(2 + 5)"`` () = 
        "(2 + 5)" |> GenericTasks.``1``.isCorrectSeq |> should equal true

    [<Test>]
    let ``should be correct at sample : "[2 + 5]"`` () = 
        "[2 + 5]" |> GenericTasks.``1``.isCorrectSeq |> should equal true

    [<Test>]
    let ``should be correct at sample : "{2 + 5}"`` () = 
        "{2 + 5}" |> GenericTasks.``1``.isCorrectSeq |> should equal true

    [<Test>]
    let ``should be true (empty string)`` () = 
        "" |> GenericTasks.``1``.isCorrectSeq |> should equal true

    [<Test>]
    let ``should be false (one bracket)`` () = 
        "(" |> GenericTasks.``1``.isCorrectSeq |> should equal false

    [<Test>]
    let ``should be incorrect at sample: "[(])"`` () = 
        "[(])" |> GenericTasks.``1``.isCorrectSeq |> should equal false
        
open FsCheck.NUnit

[<TestFixture>]
module ``2`` =

    [<Property>]
    let ``point free test`` (x : int) (l : list<int>) = Generic_Tasks.``2``.func x l = Generic_Tasks.``2``.func'4 x l
