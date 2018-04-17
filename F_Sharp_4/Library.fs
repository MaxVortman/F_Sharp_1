namespace F_Sharp_4

type Term =
|Norm
|Redex of char * Term * Term

let normalize t =
    let rec pods ch t1 t2 = 
    match t1 with
    let rec norm t = 
    match t with
    |Redex(ch, t1, t2) -> norm <| pods ch t1 t2
    |Norm -> t
    
let test = Redex('f', Redex('x', f))