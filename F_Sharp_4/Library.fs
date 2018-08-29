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

let normalize (A : Term) =
    let rec normalizeInternal (a : Term) =
        match a with
        |Varible(_) | Abstraction(_) -> a
        |Application(l, r) ->   match l with 
                                |Abstraction(_) -> pods l r |> normalizeInternal
                                |_ -> normalizeInternal l ^ r |> normalizeInternal
    normalizeInternal A