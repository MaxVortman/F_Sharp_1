namespace F_Sharp_9.Test

open FsUnit
open NUnit.Framework
open F_Sharp_9

module SingleThreadedTest =

    type TestLazy() =
        interface ILazy<int> with 
            member this.Get() = LazyFactory.CreateSingleThreadedLazy (fun _ -> 10)

    [<Test>]
    let ``should test on single thread``() =
        let testLazy = new TestLazy() 
        (testLazy :> ILazy<int>).Get() |> should equal 10

module MultiThreadedTest =
    open System.Threading

    type TestLazy() =
        interface ILazy<int> with 
            member this.Get() = LazyFactory.CreateMultiThreadedLazy (fun _ -> 10)

    [<Test>]
    let ``should test on multi thread``() =
        let testLazy = new TestLazy() 
        for i in 1..10 do
            let t = Thread(ThreadStart(fun _ -> (testLazy :> ILazy<int>).Get() |> ignore))
            t.Start()
        (testLazy :> ILazy<int>).Get() |> should equal 10

module MultiThreadedLockFreeTest =
    open System.Threading

    type TestLazy() =
        interface ILazy<int> with 
            member this.Get() = LazyFactory.CreateLockFreeMultiThreadedLazy (fun _ -> 10)

    [<Test>]
    let ``should test on free lock multi thread``() =
        let testLazy = new TestLazy() 
        for i in 1..100 do
            let t = Thread(ThreadStart(fun _ ->     (testLazy :> ILazy<int>).Get() |> ignore))
            t.Start()
            Thread.Sleep(100)
        (testLazy :> ILazy<int>).Get() |> should equal 10
        (testLazy :> ILazy<int>).Get() |> should equal 10