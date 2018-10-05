namespace F_Sharp_Homework_7

module RoundingMonad =

    type RoundingBuilder(digits : int) =
        member this.Bind(x : float, f) =            
            f (System.Math.Round (x, digits))
        member this.Return(x : float) = 
            System.Math.Round (x, digits)
     
    let rounding digits = RoundingBuilder(digits)


