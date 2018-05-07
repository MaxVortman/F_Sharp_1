namespace LocalNetwork 

open System

type Antivirus(name : string, companyDeveloper : string, secureLevel : LevelEnum) = 
    member val Name = name with get
    member val SecureLevel = secureLevel with get
    member val Company = companyDeveloper with get
    override this.ToString() = String.Format("{0}\nby {1}\nReliability {2}%", this.Name, this.Company, this.SecureLevel)
    

