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

    [<Test>]
    let ``should round to 0 digits`` () =
        let res = rounding 0 {
            //это 0
            let! a = 1. / 3.
            let! b = 3.
            //0 * 3 = 0
            return a * b
        }
        res |> should equal 0.

    [<Test>]
    let ``should round to 1 digits`` () =
        let res = rounding 1 {
            //это 0.3
            let! a = 1. / 3.
            let! b = 3.
            //0.3 * 3 = 0.9
            return a * b
        }
        res |> should equal 0.9