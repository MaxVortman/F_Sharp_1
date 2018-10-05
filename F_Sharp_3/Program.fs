// Learn more about F# at http://fsharp.org

module F_Sharp_3

    module F_Sharp_3_1 =

        module FirstOption = 
            let countEvenNumbers list =            
                list |> List.map (fun x -> if x % 2 = 0 then 1 else 0) |> List.sum
   
        module SecondOption = 
            let countEvenNumbers list = 
                list |> List.filter (fun x -> x % 2 = 0) |> List.length       

        module ThirdOption = 
            let countEvenNumbers list = 
                list |> List.fold (fun acc x -> if x % 2 = 0 then acc + 1 else acc) 0


    module F_Sharp_3_2 =
        type BinaryTree<'a> = 
        | Node of 'a * BinaryTree<'a> * BinaryTree<'a>
        | Empty

        let mapTree f tree = 
            let rec mapTreeTR tree =
                match tree with
                | Node(data, left, right) -> Node(f data, mapTreeTR left, mapTreeTR right)
                | Empty -> Empty
            mapTreeTR tree
    
    module F_Sharp_3_3 =

        type ArithmeticExpression<'a, 'f> =
        | Operation of 'f * ArithmeticExpression<'a, 'f> * ArithmeticExpression<'a, 'f>
        | Number of 'a

        let countExpression arExp =
            let rec countExpTR arExp =
                match arExp with
                | Operation(op, left, right) -> op <| countExpTR (left) <| countExpTR (right)
                | Number(n) -> n
            countExpTR arExp

        

    module F_Sharp_3_4 =
        
        let infinitePrimeSeq = 
            let isPrime n =
                let sqrtn = int (sqrt <| double n)
                let rec check i =
                    if i > sqrtn then true
                    elif n % i <> 0 then check (i + 1)
                    else false
                check 2

            let unlimitedMinimization prevPrime = 
                let rec nextPrime i = 
                    if isPrime i then i else nextPrime (i + 2)
                nextPrime (prevPrime + 2)

            let rec infinitePrimeSeqFrom number =         
                seq { yield number 
                      yield! unlimitedMinimization number |> infinitePrimeSeqFrom}

            Seq.append <| [ 2 ] <| infinitePrimeSeqFrom 3
