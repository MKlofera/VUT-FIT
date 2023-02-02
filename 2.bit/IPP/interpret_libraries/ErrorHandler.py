'''
* Project: interpret.py
* interpreting IPPcode22
* 2021/2022
* @author Marek Klofera <xklofe01@stud.fit.vutbr.cz> - VUT FIT student
'''


def errorHandler(errorNumber, file ="", function ="", note =""):
    print("\n")
    print("ERROR -", errorNumber)
    if(file != ""):
        print("File: ", file, "")
    if (function != ""):
        print("Function: ", function, "")
    if ( note != ""):
        print("Note: ", note, "\n")


    # exit codes
    if(errorNumber == 10):
        print(errorNumber, "- chybějící parametr skriptu (je-li třeba) nebo použití zakázané kombinace parametrů\n")
        exit(10)

    elif(errorNumber == 11):
        print (errorNumber, "- chyba při otevírání vstupních souborů (např. neexistence, nedostatečné oprávnění)\n")
        exit(11)

    elif (errorNumber == 12):
        print(errorNumber, "- chyba při otevření výstupních souborů pro zápis (např. nedostatečné oprávnění, "
                           "chyba při zápisu)\n")
        exit(12)

    elif (errorNumber == 31):
        print(errorNumber, "-  chybný XML formát ve vstupním souboru (soubor není tzv. dobře formátovaný, "
                           "angl. well-formed, viz [1])\n")
        exit(31)

    elif (errorNumber == 32):
        print(errorNumber, "- neočekávaná struktura XML (např. element pro argument mimo element pro instrukci, "
                           "instrukce s duplicitním pořadím nebo záporným pořadím)\n")
        exit(32)

    elif (errorNumber == 52):
        print(errorNumber, "- chyba při sémantických kontrolách vstupního kódu v IPPcode22 "
                           "(např. použití nedefinovaného návěští, redefinice proměnné)\n")
        exit(52)

    elif (errorNumber == 53):
        print(errorNumber, "-  běhová chyba interpretace – špatné typy operandů\n")
        exit(53)

    elif (errorNumber == 54):
        print(errorNumber, "-  běhová chyba interpretace – přístup k neexistující proměnné (rámec existuje)\n")
        exit(54)

    elif (errorNumber == 55):
        print(errorNumber, "- běhová chyba interpretace – rámec neexistuje (např. čtení z prázdného zásobníku rámců)\n")
        exit(55)

    elif (errorNumber == 56):
        print(errorNumber, "- běhová chyba interpretace – chybějící hodnota "
                           "(v proměnné, na datovém zásobníku nebo v zásobníku volání)\n")
        exit(56)

    elif (errorNumber == 57):
        print(errorNumber, "- běhová chyba interpretace – špatná hodnota operandu "
                           "(např. dělení nulou, špatná návratová hodnota instrukce EXIT)\n")
        exit(57)

    elif (errorNumber == 58):
        print(errorNumber, "- běhová chyba interpretace – chybná práce s řetězcem.\n")
        exit(58)

    elif (errorNumber == 99):
        print(errorNumber, "- interní chyba (neovlivněná vstupními soubory či parametry příkazové řádky; "
                           "např. chyba alokace paměti).\n")
        exit(99)

def successExit(file ="", function ="", note =""):
    # print("\n")
    # print("SUCCESSFULL EXIT")
    # if(file != ""):
    #     print("File: ", file, "")
    # if (function != ""):
    #     print("Function: ", function, "")
    # if ( note != ""):
    #     print("Note: ", note, "\n")
    exit(0)
