namespace F_Sharp_Homework_7
module StringAddMonad =
    
    open System

    type StringAddBuilder() =
        member this.Bind(x, f) =
            let mutable z = 0
            match Int32.TryParse(x, ref z) with
            | false -> None
            | true -> f z
        member this.Return(x) = 
            Some x

    let calculate = new StringAddBuilder()

