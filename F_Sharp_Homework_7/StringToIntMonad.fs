namespace F_Sharp_Homework_7
module StringToIntMonad =
    
    open System

    type StringToIntBuilder() =
        member this.Bind(x, f) =
            match Int32.TryParse x with            
            | true, z -> f z
            | _ -> None
        member this.Return(x) = 
            Some x

    let calculate = new StringToIntBuilder()

