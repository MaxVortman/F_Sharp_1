namespace F_Sharp_9

open System.Threading
open System
    
    /// <summary>
    /// Интерфейс, предоставляющий ленивое вычисление
    /// </summary>
    type ILazy<'a> =
        abstract member Get: unit -> 'a

    /// <summary>
    /// Фабрика, предоставляющая реализации интерфейса ILazy
    /// </summary>
    type LazyFactory<'a when 'a : equality> () = 
        static let mutable instance : 'a option = None
        static let lockObj = new Object()
        /// <summary>
        /// Предоставляет однопоточную реализацию интрейфейса ILazy
        /// </summary>
        /// <param name="supplier"></param>
        static member CreateSingleThreadedLazy           supplier =
            { new ILazy<'a> with
                        member this.Get() = 
                            match instance with
                            | None ->       let newInstance = supplier ()
                                            instance <- Some(newInstance)
                                            newInstance
                            | Some(v) ->    v
                                                }
            
        
        /// <summary>
        /// Предоставляет многопоточную реализацию интрейфейса ILazy
        /// </summary>
        /// <param name="supplier"></param>
        static member CreateMultiThreadedLazy            supplier =
            { new ILazy<'a> with
                        member this.Get() =         
                            match instance with
                            | None ->       lock lockObj (fun() -> 
                                                                    let newInstance = supplier ()
                                                                    instance <- Some(newInstance)
                                                                    newInstance)
                            | Some(v) ->    v
                                                }
        /// <summary>
        /// Предоставляет free lock многопоточную реализацию интрейфейса ILazy
        /// </summary>
        /// <param name="supplier"></param>
        static member CreateLockFreeMultiThreadedLazy    supplier =
            { new ILazy<'a> with
                        member this.Get() =
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
                                                }