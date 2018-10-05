namespace GenericTasks
 
module ``1`` =

    type Brackets = 
        {
            Open : char;
            Close : char
        }

    let roundBr = {Open = '(';
                       Close = ')'}
    let squareBr = {Open = '[';
                        Close = ']'}
    let braces = {Open = '{';
                        Close = '}'}

    exception IncorrectBracketsSeqException

    let isCorrectSeq (str : string) = 
        let brackets = [roundBr; squareBr; braces]
        let rec travesol stack s =
            let stackModify (ch : char) (br : Brackets) =
                match ch with 
                | _ when ch = br.Open -> br.Close :: stack
                | _ when br.Close = ch && List.head stack = ch -> List.tail stack
                | _ when ch = br.Close -> raise IncorrectBracketsSeqException
                | _ -> stack
            match s with
            | h :: t when List.exists (fun (br : Brackets) -> br.Open = h || br.Close = h) brackets -> 
                                            travesol (stackModify h (brackets |> List.find (fun (br : Brackets) -> br.Open = h || br.Close = h))) t
            | _ :: t -> travesol stack t
            | _ -> stack.IsEmpty 
        try
            travesol [] (Seq.toList str)
        with 
        | IncorrectBracketsSeqException -> false

module ``2`` = 

    let func x l = List.map (fun y -> y * x) l

    let func'1 x = List.map (fun y -> y * x)

    let func'2 x = List.map (fun y -> (*) y x)

    let func'3 x = List.map ((*) x)

    let func'4 : int -> int list -> int list = List.map << (*
