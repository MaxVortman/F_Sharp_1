namespace F_Sharp_Homework_7.Test

module RoundingMonadTests =
    open NUnit.Framework
    open FsUnit
    open F_Sharp_Homework_7.RoundingMonad
    
    [<Test>]
    let ``should round to 3 digits (0.048)``() = 
        let res = rounding 3 {
            let! a = 2.0 / 12.0
            let! b = 3.5
            return a / b
        }
        res |> should equal 0.048