module F_Sharp_4

type Term =
| Varible of string
| Application of Term * Term
| Abstraction of string * Term

let (*) (ch : string) (term : Term) = Abstraction(ch, term)
let (^) (term1 :Term) (term2 : Term) = Application(term1, term2)

exception NormalizedFormNotFoundException

let normalize (sourceTerm : Term) =
    let getSetOfVar term =
        let rec travelsolSourceTerm internalTerm = 
            seq {
            match internalTerm with
            |Varible(ch) -> yield ch
            |Abstraction(ch, t) ->  yield ch
                                    yield! travelsolSourceTerm t
            |Application(l, r) ->   yield! travelsolSourceTerm l
                                    yield! travelsolSourceTerm r
            }
        new Set<string>(travelsolSourceTerm term)

    let depth (a : Term) =
        let rec depthInternal acc (f : Term) =
            match f with
            |Application(l, r) -> depthInternal (acc + 1) l + depthInternal 0 r
            |Abstraction(_, x) -> depthInternal (acc + 1) x
            |_ -> acc
        depthInternal 0 a

    let depthOfSourceTerm = depth sourceTerm

    let check (a : Term) =
        let depthOfCurrentTerm = depth a
        match depthOfCurrentTerm with
        |x when depthOfSourceTerm > x -> a
        |_ -> raise NormalizedFormNotFoundException 

    let substitution (ch : string) (destinationTerm : Term) (term : Term) = 
        let rec substitutionInternal (g : Term) =
            match g with
            |Varible(h) when h = ch -> term
            |Varible(_) -> g
            |Application(l, r) -> substitutionInternal l ^ substitutionInternal r
            |Abstraction(s, r) -> s * substitutionInternal r 
        substitutionInternal destinationTerm
    
    let alphaConversion (setOfVar : Set<string>) (mainTerm : Term) =
        let rec alphaConversionTravelsol (term : Term) =
            match term with
            |Varible(str) | Abstraction(str, _) when setOfVar.Contains str -> alphaConversionRenaming term str
            |Varible(_) -> term
            |Abstraction(str, t) -> str * alphaConversionTravelsol t
            |Application(l, r) -> alphaConversionTravelsol l ^ alphaConversionTravelsol r
        and alphaConversionRenaming (term : Term) (varible : string) =
            let unicName =
                let rec findUnicName i =
                    let tryName = varible + (string i)
                    match tryName with
                    |_ when setOfVar.Contains tryName -> findUnicName <| i + 1
                    |_ -> tryName
                findUnicName 0
            let afterConversionTerm =
                let rec afterConversionTermInternal internalTerm = 
                    match internalTerm with
                    |Varible(str) when str = varible -> Varible(unicName)
                    |Varible(_) -> internalTerm
                    |Abstraction(str, t) when str = varible -> unicName * afterConversionTermInternal t
                    |Abstraction(str, t) -> str * afterConversionTermInternal t
                    |Application(l, r) -> afterConversionTermInternal l ^ afterConversionTermInternal r
                afterConversionTermInternal term
            alphaConversionTravelsol afterConversionTerm
        alphaConversionTravelsol mainTerm

    let rec normalizeInternal (a : Term) =
        match a with
        |Varible(_) | Abstraction(_) -> a
        |Application(l, r) ->   match l with 
                                |Abstraction(ch, t) -> alphaConversion (getSetOfVar l) r |> substitution ch t |> check |> normalizeInternal
                                |Application(_) -> ((normalizeInternal l) ^ r) |> normalizeInternal
                                |_ -> l ^ r
    normalizeInternal sourceTerm