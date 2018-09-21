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
            |Application(l, r) -> depthInternal (acc + 1) l
            |Abstraction(c, x) -> depthInternal (acc + 1) x
            |_ -> acc
        depthInternal 0 a

    let depthOfSourceTerm = depth sourceTerm

    let check (a : Term) =
        let depthOfCurrentTerm = depth a
        match depthOfCurrentTerm with
        |x when depthOfSourceTerm > x -> a
        |_ -> raise NormalizedFormNotFoundException 

    let substitution (ch : string) (destinationTerm : Term) (term : Term) = 
        let postVar (c : string) (f : Term) =
            let rec postVarInternal (g : Term) =
                match g with
                |Varible(h) when h = c -> term
                |Varible(_) -> g
                |Application(l, r) -> postVarInternal l ^ postVarInternal r
                |Abstraction(s, r) -> s * postVarInternal r 
            postVarInternal f
        postVar ch destinationTerm
    
    let alfaReduction (setOfVar : Set<string>) (mainTerm : Term) =
        let rec alfaReductionTravelsol (term : Term) =
            match term with
            |Varible(str) | Abstraction(str, _) when setOfVar.Contains str -> alfaReductionRenaming term str
            |Varible(_) -> term
            |Abstraction(str, t) -> str * alfaReductionTravelsol t
            |Application(l, r) -> alfaReductionTravelsol l ^ alfaReductionTravelsol r
        and alfaReductionRenaming (term : Term) (varible : string) =
            let unicName =
                let rec findUnicName i =
                    let tryName = varible + (string i)
                    match tryName with
                    |_ when setOfVar.Contains tryName -> findUnicName <| i + 1
                    |_ -> tryName
                findUnicName 0
            let afterReductionTerm =
                let rec afterReductionInternal internalTerm = 
                    match internalTerm with
                    |Varible(str) when str = varible -> Varible(unicName)
                    |Varible(_) -> internalTerm
                    |Abstraction(str, t) when str = varible -> unicName * afterReductionInternal t
                    |Abstraction(str, t) -> str * afterReductionInternal t
                    |Application(l, r) -> afterReductionInternal l ^ afterReductionInternal r
                afterReductionInternal term
            alfaReductionTravelsol afterReductionTerm
        alfaReductionTravelsol mainTerm

    let rec normalizeInternal (a : Term) =
        match a with
        |Varible(_) | Abstraction(_) -> a
        |Application(l, r) ->   match l with 
                                |Abstraction(ch, t) -> alfaReduction (getSetOfVar l) r |> substitution ch t |> check |> normalizeInternal
                                |Application(_) -> ((normalizeInternal l) ^ r) |> normalizeInternal
                                |_ -> l ^ r
    normalizeInternal sourceTerm