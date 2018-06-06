namespace LocalNetwork

open System
/// <summary>
/// Класс операционной системы компьютера
/// </summary>
type OperatingSystem(name : string, description : string, author : string, holeLevel : LevelEnum) =
    
    let defaultAntivirus = Antivirus("None", "No one", LevelEnum.None)

    let mutable antivirus : Antivirus = defaultAntivirus

    member val Name = name with get
    member val Description = description with get
    member val Author = author with get
    member val HoleLevel = holeLevel with get
    /// <summary>
    /// Получение текущего антивируса в операционной системы
    /// </summary>
    member this.Antivirus = antivirus
    /// <summary>
    /// Установка антивируса в систему
    /// </summary>
    /// <param name="newAntivirus">Option на новый антивирус</param>
    member this.InstallAntivirus ?newAntivirus = 
        antivirus <- defaultArg newAntivirus defaultAntivirus
    /// <summary>
    /// Переопределение ToString object-а
    /// </summary>
    override this.ToString() = String.Format("{0}\nby {1}\n{2}\nInsecurity {3}%\nAntirus:\n{4}\n", this.Name, this.Author, this.Description, int this.HoleLevel, this.Antivirus)