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
        [<VolatileField>]
        static let mutable instance : 'a option = None
        static let lockObj = new Object()
        /// <summary>
        /// Предоставляет однопоточную реализацию интрейфейса ILazy
        /// </summary>
        /// <param name="supplier">Функция, вычисляющая значение instance</param>
        static member CreateSingleThreadedLazy supplier =
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
        /// <param name="supplier">Функция, вычисляющая значение instance</param>
        static member CreateMultiThreadedLazy supplier =
            { new ILazy<'a> with
                        member this.Get() =         
                            match instance with
                            | None ->       lock lockObj (fun() ->  match instance with
                                                                    | None ->       let newInstance = supplier ()
                                                                                    instance <- Some(newInstance)
                                                                                    newInstance
                                                                    | Some(v) ->    v)
                            | Some(v) ->    v
                                                }
        /// <summary>
        /// Предоставляет lock-free многопоточную реализацию интрейфейса ILazy
        /// </summary>
        /// <param name="supplier">Функция, вычисляющая значение instance</param>
        static member CreateLockFreeMultiThreadedLazy supplier =
            { new ILazy<'a> with
                        member this.Get() =
                            let rec CAS () =
                                match instance with
                                | None ->       CASInternal ()
                                | Some(v) ->    v
                            and CASInternal () =
                                let currentValue = instance
                                let computedValue = supplier ()
                                match Interlocked.CompareExchange(&instance, Some(computedValue), currentValue) = currentValue with
                                | true ->   computedValue
                                | false ->  CAS ()
                            CAS ()
                                                }