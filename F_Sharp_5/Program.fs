namespace Generic_Tasks

namespace Generic_Tasks

module ``2`` = 

//func x l = List.map (fun y -> y * x) l

    let func x l = List.map (fun y -> y * x) l

    let func'1 x = List.map (fun y -> y * x)

    let func'2 x = List.map (fun y -> (*) y x)

    let func'3 x = List.map ((*) x)

    let func'4 : int -> int list -> int list = List.map << (*)


