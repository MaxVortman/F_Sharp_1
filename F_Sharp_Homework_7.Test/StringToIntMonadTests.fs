namespace F_Sharp_Homework_7.Test

module StringToIntMonadTests =
    open NUnit.Framework
    open FsUnit
    open F_Sharp_Homework_7.StringToIntMonad
    
    [<Test>]
    let ``should add 1 to 2`` () =
        let result = calculate {
            let! x = "1"
            let! y = "2"
            let z = x + y
            return z
        }
        result |> should equal (Some(3))

    [<Test>]
    let ``should return None`` () =
        let res = calculate {
            let! x = "1"
            let! y = "Ъ"
            let z = x + y
            return z
        }
        res |> should equal (None)
