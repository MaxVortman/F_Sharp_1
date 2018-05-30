namespace LocalNetwork

module OS =

    let Ubuntu = new OperatingSystem("Ubuntu 18.04 LTS", @"Ubuntu ([ʊˈbʊntuː]; от зулу ubuntu — человечность[5]; «Убу́нту») 
                                                        — операционная система, основанная на Debian GNU/Linux. 
                                                        Основным разработчиком и спонсором является компания Canonical. 
                                                        В настоящее время проект активно развивается и поддерживается свободным сообществом[6].",
                                                        "Canonical",
                                                        LevelEnum.Low)
    let WindowsXP = new OperatingSystem("Windows XP", @"Windows XP (кодовое название при разработке — Whistler; 
                                                                внутренняя версия — Windows NT 5.1) — 
                                                                операционная система семейства Windows NT корпорации Microsoft. 
                                                                Была выпущена 25 октября 2001 года и является развитием Windows 2000 Professional. 
                                                                Название XP происходит от англ. experience («опыт», «впечатления»[9]).",
                                                                "Microsoft", LevelEnum.Perfect)
    let Dos = new OperatingSystem("MS-DOS", @"MS-DOS (англ. Microsoft Disk Operating System) — 
                                        дисковая операционная система для компьютеров на базе архитектуры x86. 
                                        MS-DOS — самая известная ОС среди семейства DOS-совместимых операционных 
                                        систем и самая используемая среди IBM PC-совместимых компьютеров c 1980-х 
                                        до середины 1990-х годов, пока её не вытеснили операционные системы с 
                                        графическим пользовательским интерфейсом, в основном из семейства Microsoft Windows[1].",
                                        "Microsoft", LevelEnum.None)
    let Windows7 = new OperatingSystem("Windows 7", @"Windows 7 — пользовательская операционная система семейства Windows NT, 
                                                  следует по времени выхода за Windows Vista и предшественник Windows 8.",
                                                  "Microsoft", LevelEnum.Average)

module Antiviruses = 

    let Nod32 = new Antivirus("ESET NOD32", "ESET", LevelEnum.Perfect)
    let Kaspersky = new Antivirus("Антивирус Касперского", "Лаборатория Касперского", LevelEnum.High)
    let Avast = new Antivirus("Avast!", "AVAST Software", LevelEnum.Average)
    let Avira = new Antivirus("AntiVir", "Avira GmbH", LevelEnum.Low)

module Viruses = 

    let SW = new Virus("Storm Worm", @"В 2007 году вирус заразил миллионы компьютеров, 
                                                 рассылая спам и похищая личные данные.",
                                                 LevelEnum.Low)
    let Slammer = new Virus("Slammer", @"Самый агрессивный вирус. 
                                        В 2003-м уничтожил данные с 75 тыс. компьютеров за 10 минут.",
                                        LevelEnum.Average)
    let Conficker = new Virus("Conficker", @"Один из опаснейших из известных на сегодняшний день компьютерных червей.
                                            Вредоносная программа была написана на Microsoft Visual C++ и впервые появилась в сети 21 ноября 2008. 
                                            Атакует операционные системы семейства Microsoft Windows (от Windows 2000 до Windows 7 и 
                                            Windows Server 2008 R2). На январь 2009 вирус поразил 12 млн компьютеров во всём мире. 12 февраля 2009 
                                            Microsoft обещал $250 тыс. за информацию о создателях вируса.",
                                            LevelEnum.High)
    let ILOVEYOU = new Virus("ILOVEYOU",    @"При открытии вложения вирус рассылал копию самого себя всем контактам в адресной книге Windows, 
                                            а также на адрес, указанный как адрес отправителя. Он также совершал ряд вредоносных изменений в системе пользователя.
                                            Вирус был разослан на почтовые ящики с Филиппин в ночь с 4 мая на 5 мая 2000 года; в теме письма содержалась строка 
                                            «ILoveYou», а к письму был приложен скрипт «LOVE-LETTER-FOR-YOU.TXT.vbs». Расширение «.vbs» было по умолчанию скрыто, 
                                            что и заставило ничего не подозревающих пользователей думать, что это был простой текстовый файл. 
                                            В общей сложности, вирус поразил более 3 млн компьютеров по всему миру. 
                                            Предполагаемый ущерб, который червь нанес мировой экономике, оценивается в размере $10 – 15 млрд, 
                                            за что вошел в Книгу рекордов Гиннесса, как самый разрушительный компьютерный вирус в мире.",
                                            LevelEnum.Perfect)
                                                 