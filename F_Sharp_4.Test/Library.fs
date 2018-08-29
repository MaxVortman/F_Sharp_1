module  F_Sharp_4.Test

open NUnit.Framework
open FsUnit
open F_Sharp_4

[<Test>]
let ``Should test a simple insertion lambda a.a b``() =
    let f = 'a' * Varible('a')
    let x = Varible('b')
    pods f x |> should equal (Varible('b'))

[<Test>]
let ``Should test a insertion (lambda x.lambda y. x y) a``() =
    let f = 'x' *  ('y' * (Varible('x') ^ Varible('y')))
    let x = Varible('a')
    pods f x |> should equal ('y' * (Varible('a') ^ Varible('y')))

[<Test>]
let ``Should test a normalizing (lambda x.lambda y. x y) a``() =
    let f = ('x' *  ('y' * (Varible('x') ^ Varible('y')))) ^ Varible('a')
    normalize f |> should equal ('y' * (Varible('a') ^ Varible('y')))