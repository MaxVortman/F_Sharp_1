module F_Sharp_9

open System.Threading

    type ILazy<'a> =
        abstract member Get: unit -> 'a

    type LazyFactory () = 
        static let mutable instance = None

        static member CreateSingleThreadedLazy           supplier =
            match instance with
            | None ->       let newInstance = supplier ()
                            instance <- Some(newInstance)
                            newInstance
            | Some(v) ->    v
            
        
        static member CreateMultiThreadedLazy            supplier =
            
            match instance with
            | None ->       lock instance (fun() -> 
                                                    let newInstance = supplier ()
                                                    instance <- Some(newInstance)
                                                    newInstance)
            | Some(v) ->    v
                
        static member CreateLockFreeMultiThreadedLazy    supplier =
            let mutable initialValue = None
            let rec CAS () =               
                let computedValue = supplier ()
                initialValue <- instance
                match Interlocked.CompareExchange(ref instance, Some(computedValue), initialValue) = initialValue with
                | true ->   computedValue
                | false ->  CAS()
            match instance with
            | None -> CAS()
            | Some(v) -> v
                    