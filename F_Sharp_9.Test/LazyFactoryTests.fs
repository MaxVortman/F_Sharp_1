namespace F_Sharp_9.Test

open FsUnit
open NUnit.Framework
open F_Sharp_9
open System

module SingleThreadedTest =

    [<Test>]
    let ``should test on single thread``() =
        let mutable value = -1
        let r = Random()
        let testLazy = (new LazyFactory<int>()).CreateSingleThreadedLazy (fun _ ->  let computedValue = r.Next()
                                                                                    if value = -1 then value <- computedValue
                                                                                    computedValue)
        testLazy.Get() |> ignore
        testLazy.Get() |> should equal value

module MultiThreadedTest =
    open System.Threading

    [<Test>]
    let ``should test on multi thread``() =
        let mutable value = -1
        let r = Random()
        let testLazy = (new LazyFactory<int>()).CreateMultiThreadedLazy (fun _ ->  let computedValue = r.Next()
                                                                                   if value = -1 then value <- computedValue
                                                                                   computedValue)
        let threads = [for _ in 1..10 do   let t = Thread(ThreadStart(fun _ -> testLazy.Get() |> ignore))
                                           t.Start()
                                           yield t]
        for t in threads do
            t.Join()
        testLazy.Get() |> should equal value

module MultiThreadedLockFreeTest =
    open System.Threading

    [<Test>]
    let ``should test on free lock multi thread``() =
        let r = Random()
        let testLazy = (new LazyFactory<int>()).CreateLockFreeMultiThreadedLazy (fun _ -> r.Next())
        let threads = [for _ in 1..100 do  let t = Thread(ThreadStart(fun _ -> testLazy.Get() |> ignore))
                                           t.Start()
                                           yield t]
        for t in threads do
            t.Join()
        let instance = testLazy.Get()
        for _ in 1 .. 1000 do
            testLazy.Get() |> should equal instance