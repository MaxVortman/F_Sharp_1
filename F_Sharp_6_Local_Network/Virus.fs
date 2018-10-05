namespace LocalNetwork

open System
/// <summary>
/// Модель для представления вируса, гуляющего по сети
/// </summary>
type Virus(name : string, description : string, strength : LevelEnum) = 
    member val Name = name with get
    member val Description = description with get
    /// <summary>
    /// Переопределение ToString object-а
    /// </summary>
    override this.ToString() = String.Format("This virus {0}\nDescription:\n{1}\nIts power: {2}%\n", 
                                                        this.Name, this.Description, int this.TruePower)
    member val TruePower = strength with get