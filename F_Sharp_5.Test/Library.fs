namespace F_Sharp_5.Test

open NUnit.Framework
open FsUnit

module ``1`` =
    open NUnit.Framework.Constraints
    
    [<Test>]
    let ``should be correct at sample : "(2 + 5)"`` () = 
        "(2 + 5)" |> Generic_Tasks.``1``.isCorrectRoundBrSeq |> should equal true

    [<Test>]
    let ``should be correct at sample : "[2 + 5]"`` () = 
        "[2 + 5]" |> Generic_Tasks.``1``.isCorrectSquareBrSeq |> should equal true

    [<Test>]
    let ``should be correct at sample : "{2 + 5}"`` () = 
        "{2 + 5}" |> Generic_Tasks.``1``.isCorrectBracesSeq |> should equal true

    [<Test>]
    let ``should be true (empty string)`` () = 
        "" |> Generic_Tasks.``1``.isCorrectBracesSeq |> should equal true

    [<Test>]
    let ``should be false (one bracket)`` () = 
        "(" |> Generic_Tasks.``1``.isCorrectRoundBrSeq |> should equal false

    [<Test>]
    let ``should be correct at sample (braces) : "{FloatingPointNumerics.ReinterpretAsDouble+ds(2 + 5)}]dsdf]ytr][["`` () = 
        "{FloatingPointNumerics.ReinterpretAsDouble+ds(2 + 5)}]dsdf]ytr][[" |> Generic_Tasks.``1``.isCorrectBracesSeq |> should equal true

    [<Test>]
    let ``should be correct at sample (round) : "{FloatingPointNumerics.ReinterpretAsDouble+ds(2 + 5)}]dsdf]ytr][["`` () = 
        "{FloatingPointNumerics.ReinterpretAsDouble+ds(2 + 5)}]dsdf]ytr][[" |> Generic_Tasks.``1``.isCorrectRoundBrSeq |> should equal true

    [<Test>]
    let ``should be incorrect at sample (square) : "{FloatingPointNumerics.ReinterpretAsDouble+ds(2 + 5)}]dsdf]ytr][["`` () = 
        "{FloatingPointNumerics.ReinterpretAsDouble+ds(2 + 5)}]dsdf]ytr][[" |> Generic_Tasks.``1``.isCorrectSquareBrSeq |> should equal false

    [<Test>]
    let ``should be incorrect at sample: "[(])"`` () = 
        "[(])" |> Generic_Tasks.``1``.isCorrectSquareBrSeq |> should equal false