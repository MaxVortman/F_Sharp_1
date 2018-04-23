namespace F_Sharp_5.Test

open NUnit.Framework
open FsUnit
open FsCheck.NUnit

[<TestFixture>]
module ``2`` =

    [<Property>]
    let ``point free test`` (x : int) (l : list<int>) = Generic_Tasks.``2``.func x l = Generic_Tasks.``2``.func'3 x l