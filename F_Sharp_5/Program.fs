namespace Generic_Tasks

module ``1`` =

    type Brackets = 
        {
            Open : char;
            Close : char
        }

    let isCorrectBrSeq (str : string) (br : Brackets) = 
        let len = str.Length - 1
        let rec count accOpen accClose ch i = 
            match ch with
            | ch when i = len -> if ch = br.Open then (accOpen + 1, accClose)
                                 elif ch = br.Close then (accOpen, accClose + 1)
                                 else (accOpen, accClose)
            | ch when ch = br.Open -> count (accOpen + 1) accClose str.[i + 1] (i + 1)
            | ch when ch = br.Close -> count accOpen (accClose + 1) str.[i + 1] (i + 1)            
            | _ -> count accOpen accClose str.[i + 1] (i + 1)
        let (opens, closes) = count 0 0 str.[0] 0
        if opens = closes then true
        else false

    let isCorrectRoundBrSeq str = 
        let roundBr = {Open = '(';
                       Close = ')'}
        isCorrectBrSeq str roundBr

    let isCorrectSquareBrSeq str =
        let squareBr = {Open = '[';
                        Close = ']'}
        isCorrectBrSeq str squareBr
    
    let isCorrectBracesSeq str =
        let braces = {Open = '{';
                        Close = '}'}
        isCorrectBrSeq str braces