namespace F_Sharp_9

open System.Threading
open System

    type ILazy<'a> =
        abstract member Get: unit -> 'a

    type LazyFactory<'a when 'a : equality> () = 
        static let mutable instance : 'a option = None
        static let lockObj = new Object()
        static member CreateSingleThreadedLazy           supplier =
            match instance with
            | None ->       let newInstance = supplier ()
                            instance <- Some(newInstance)
                            newInstance
            | Some(v) ->    v
            
        
        static member CreateMultiThreadedLazy            supplier =
            
            match instance with
            | None ->       lock lockObj (fun() -> 
                                                    let newInstance = supplier ()
                                                    instance <- Some(newInstance)
                                                    newInstance)
            | Some(v) ->    v
                
        static member CreateLockFreeMultiThreadedLazy    supplier =
            let rec CAS () =
                let currentValue = instance
                let computedValue = supplier ()
                match Interlocked.CompareExchange(&instance, Some(computedValue), currentValue) = currentValue with
                | true ->   computedValue
                | false ->  Thread.SpinWait 10
                            CAS()
            match instance with
            | None ->       CAS()
            | Some(v) ->    v