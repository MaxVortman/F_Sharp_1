namespace F_Sharp_9.Test

open FsUnit
open NUnit.Framework
open F_Sharp_9

module SingleThreadedTest =

    [<Test>]
    let ``should test on single thread``() =
        let testLazy = LazyFactory.CreateSingleThreadedLazy (fun _ -> 10) 
        testLazy.Get() |> should equal 10

module MultiThreadedTest =
    open System.Threading

    [<Test>]
    let ``should test on multi thread``() =
        let testLazy = LazyFactory.CreateMultiThreadedLazy (fun _ -> 10) 
        for i in 1..10 do
            let t = Thread(ThreadStart(fun _ -> testLazy.Get() |> ignore))
            t.Start()
        testLazy.Get() |> should equal 10

module MultiThreadedLockFreeTest =
    open System.Threading

    [<Test>]
    let ``should test on free lock multi thread``() =
        let testLazy = LazyFactory.CreateLockFreeMultiThreadedLazy (fun _ -> 10)  
        for i in 1..100 do
            let t = Thread(ThreadStart(fun _ ->     testLazy.Get() |> ignore))
            t.Start()
            Thread.Sleep(100)
        testLazy.Get() |> should equal 10
        testLazy.Get() |> should equal 10