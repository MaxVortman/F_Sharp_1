module WebPageStatistics

open System.Net
open System.Text.RegularExpressions
open System.IO

let printPageStatAsync url =
    let urlRegex = new Regex("<a href=\"(https?:\/\/[\w|\W]+?)\"", RegexOptions.Compiled)
    let parseHtml html = 
        urlRegex.Matches(html) |> Seq.cast<Match> |> Seq.map (fun m -> m.Groups.[1].Value)
    async {
        let getHtml (url : string) = async {
            let request = WebRequest.Create(url)
            use! response = request.AsyncGetResponse()
            use stream = response.GetResponseStream()
            use reader = new StreamReader(stream)
            let! html = reader.ReadToEndAsync() |> Async.AwaitTask
            return (url, html)
            }
        let printStatFor (url, html : string) = 
            let stat = sprintf "%s --- %i" url html.Length
            printfn "%s" stat
            stat
        let! _, html = getHtml url
        let seqOfStat = seq{    
            yield printStatFor (url, html)
            yield! parseHtml html |> Seq.map getHtml |> Async.Parallel |> Async.RunSynchronously |> Seq.map printStatFor }        
        return seqOfStat
    }