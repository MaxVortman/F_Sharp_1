namespace LocalNetwork 

open System
/// <summary>
/// Модель антивируса компьютера в сети
/// </summary>
type Antivirus(name : string, companyDeveloper : string, secureLevel : LevelEnum) = 
    member val Name = name with get
    member val SecureLevel = secureLevel with get
    member val Company = companyDeveloper with get
    /// <summary>
    /// Переопределение ToString object-а
    /// </summary>
    override this.ToString() = String.Format("{0}\nby {1}\nReliability {2}%\n", this.Name, this.Company, int this.SecureLevel)
    

