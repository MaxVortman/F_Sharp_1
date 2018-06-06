module WebPageStatisticsTests

open FsUnit
open NUnit.Framework
open WebPageStatistics

[<Test>]
let ``should work with no one link`` () =
    let url = @"https://gallery.mailchimp.com/dc3a7ef4d750c0abfc19202a3/files/704291d2-365e-45bf-a9f5-719959dfe415/Ng_MLY01.pdf"
    printPageStatAsync url |> Async.RunSynchronously |> should equal (seq{ 
        yield @"https://gallery.mailchimp.com/dc3a7ef4d750c0abfc19202a3/files/704291d2-365e-45bf-a9f5-719959dfe415/Ng_MLY01.pdf --- 1508805"})

[<Test>]
let ``should work with many links`` () =
    let url = @"http://www.i-maika.ru/price/"
    printPageStatAsync url |> Async.RunSynchronously |> Seq.length |> should equal 6