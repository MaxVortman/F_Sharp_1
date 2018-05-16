namespace LocalNetwork

open System

type OperatingSystem(name : string, description : string, author : string, holeLevel : LevelEnum) =
    
    let defaultAntivirus = Antivirus("None", "No one", LevelEnum.None)

    let mutable antivirus : Antivirus = defaultAntivirus

    member val Name = name with get
    member val Description = description with get
    member val Author = author with get
    member val HoleLevel = holeLevel with get
    member val Antivirus = antivirus with get

    member this.InstallAntivirus ?newAntivirus = 
        antivirus = defaultArg newAntivirus defaultAntivirus
    override this.ToString() = String.Format("{0}\nby {1}\n{2}\Insecurity {3}%", this.Name, this.Author, this.Description, this.HoleLevel)