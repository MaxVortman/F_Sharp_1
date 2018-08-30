module F_Sharp_4

type Term =
| Varible of char
| Application of Term * Term
| Abstraction of char * Term

let (*) (x : char) (A : Term) = Abstraction(x, A)
let (^) (A :Term) (B : Term) = Application(A, B)

let pods (F : Term) (x : Term) = 
    let postVar (c : char) (f : Term) =
        let rec postVarInternal (g : Term) =
            match g with
            |Varible(h) when h = c -> x
            |Varible(_) -> g
            |Application(l, r) -> postVarInternal l ^ postVarInternal r
            |Abstraction(s, r) -> s * postVarInternal r 
        postVarInternal f
    match F with
    |Abstraction(c, r) -> postVar c r
    |_ -> F

let depth (a : Term) =
    let rec depthInternal acc (f : Term) =
        match f with
        |Application(l, r) -> depthInternal (acc + 1) l
        |Abstraction(c, x) -> depthInternal (acc + 1) x
        |_ -> acc
    depthInternal 0 a

exception NormalizedFormNotFound

let normalize (A : Term) =
    let depthA = depth A
    let check (a : Term) =
        let deptha = depth a
        match deptha with
        |x when depthA > x -> a
        |_ -> raise NormalizedFormNotFound 
    let rec normalizeInternal (a : Term) =
        match a with
        |Varible(_) | Abstraction(_) -> a
        |Application(l, r) ->   match l with 
                                |Abstraction(_) -> pods l r |> check |> normalizeInternal
                                |Application(_) -> ((normalizeInternal l) ^ r) |> normalizeInternal
                                |_ -> l ^ r
    normalizeInternal A