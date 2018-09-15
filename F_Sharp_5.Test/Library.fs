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