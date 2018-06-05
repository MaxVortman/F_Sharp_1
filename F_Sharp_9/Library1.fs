module F_Sharp_9

open System.Threading

    type ILazy<'a> =
        abstract member Get: unit -> 'a

    type LazyFactory () = 
        static let mutable singleThreadedInstance = None
        [<VolatileField>]
        static let mutable multiThreadedInstance = None
        static let mutable multiThreadedLockFreeInstance = None

        static member CreateSingleThreadedLazy           supplier =
            match singleThreadedInstance with
            | None ->       let instance = supplier ()
                            singleThreadedInstance <- Some(instance)
                            instance
            | Some(v) ->    v
            
        
        static member CreateMultiThreadedLazy            supplier =
            match Volatile.Read(ref multiThreadedInstance) with
            | None ->       let instance = supplier ()
                            Volatile.Write(ref multiThreadedInstance, Some(instance))
                            instance
            | Some(v) ->    v
                
        static member CreateLockFreeMultiThreadedLazy    supplier =
            let mutable initialValue = None
            let rec CAS () =               
                let computedValue = supplier ()
                initialValue <- multiThreadedLockFreeInstance
                match Interlocked.CompareExchange(ref multiThreadedLockFreeInstance, Some(computedValue), initialValue) = initialValue with
                | true ->   computedValue
                | false ->  CAS()
            match multiThreadedLockFreeInstance with
            | None -> CAS()
            | Some(v) -> v
                    