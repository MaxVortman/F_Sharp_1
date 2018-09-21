module F_Sharp_4

type Term =
| Varible of char
| Application of Term * Term
| Abstraction of char * Term

let (*) (ch : char) (term : Term) = Abstraction(ch, term)
let (^) (term1 :Term) (term2 : Term) = Application(term1, term2)

let substitution (ch : char) (destinationTerm : Term) (term : Term) = 
    let postVar (c : char) (f : Term) =
        let rec postVarInternal (g : Term) =
            match g with
            |Varible(h) when h = c -> term
            |Varible(_) -> g
            |Application(l, r) -> postVarInternal l ^ postVarInternal r
            |Abstraction(s, r) -> s * postVarInternal r 
        postVarInternal f
    postVar ch destinationTerm

let depth (a : Term) =
    let rec depthInternal acc (f : Term) =
        match f with
        |Application(l, r) -> depthInternal (acc + 1) l
        |Abstraction(c, x) -> depthInternal (acc + 1) x
        |_ -> acc
    depthInternal 0 a

exception NormalizedFormNotFoundException

let normalize (sourceTerm : Term) =
    let depthOfSourceTerm = depth sourceTerm
    let check (a : Term) =
        let depthOfCurrentTerm = depth a
        match depthOfCurrentTerm with
        |x when depthOfSourceTerm > x -> a
        |_ -> raise NormalizedFormNotFoundException 
    let rec normalizeInternal (a : Term) =
        match a with
        |Varible(_) | Abstraction(_) -> a
        |Application(l, r) ->   match l with 
                                |Abstraction(ch, t) -> substitution ch t r |> check |> normalizeInternal
                                |Application(_) -> ((normalizeInternal l) ^ r) |> normalizeInternal
                                |_ -> l ^ r
    normalizeInternal sourceTerm