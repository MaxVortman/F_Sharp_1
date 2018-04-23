namespace Generic_Tasks

module ``1`` =

    type Brackets = 
        {
            Open : char;
            Close : char
        }

    let isCorrectBrSeq (str : string) (br : Brackets) = 
        let len = str.Length - 1
        let rec count acc ch i =
            //must help with samples kinda "] 5 + 1 ["
            if acc < 0 then acc
            else
            match ch with
            | ch when i = len -> if ch = br.Open then (acc + 1)
                                 elif ch = br.Close then (acc - 1)
                                 else acc
            | ch when ch = br.Open -> count (acc + 1) str.[i + 1] (i + 1)
            | ch when ch = br.Close -> count (acc - 1) str.[i + 1] (i + 1)            
            | _ -> count acc str.[i + 1] (i + 1)
        if len < 0 then true else
        let acc = count 0 str.[0] 0
        if acc = 0 then true
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