namespace F_Sharp_5.Test

open NUnit.Framework
open FsUnit

module ``1`` =
    
    [<Test>]
    let ``should be correct at sample : "(2 + 5)"`` () = 
        "(2 + 5)" |> Generic_Tasks.``1``.isCorrectRoundBrSeq |> should equal true