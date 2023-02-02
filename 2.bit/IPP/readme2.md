Implementační dokumentace k 2. úloze do IPP 2021/2022
Jméno a příjmení: Marek Klofera
Login: xklofe01

# Interpret.py
## Stručný popis scriptu
Při spuštění scriptu interpret.py se nejprve ověří argumenty scriptu a následně načítá IPPcode22 v XML reprezentaci. Proběhne kontrola XML kódu a seřadí se intrukce dle *order*. Následně se pro každou instrukci vytvoří objekt a přidá se do kolekce. Před samotným provádění instrukcí se nejprve načtou všechny *labels* do kolekce kvůli případným skokům.

Načte se daná instrukce, zkontrolují se argumenty (typ, hodnota případně obojí, záleží na instrukci) a poté se tato instrukce vykoná. Tento proces probíhá v cyklu, dokud script nedojde na konec kolekce instrukcí. V případě chyby dojde ke správnému ukončení programu s daným exit kódem.
## 1. Kontrola argumentů
zpracování XML má na starost třída *ArgumentsChekcer.py*. Při spuštění scriptu interpret.py se nejprve ověří argumenty scriptu, jejich kombinace a typ argumetů. Pokud byl při spuštění scriptu napsán argument *--help* je vypsána nápověda scriptu. V opačném případě script pokračuje na načtění kódu IPPcode22 v XML reprezentaci a jeho následné zpracování.

## 2. Načtení a zpracování XML
Načtení a zpracování XML má na starost třída *XMLChekcer.py*. Zdrojový kód je načten ze souboru, který je určen uživatelem pomocí argumentu *--source=<cesta_k_souboru>* nebo ze standartního vstupu. Poté se kontroluje *root* XML, hlavička, jednotlivé instrukce, instrukční atributy a argumenty jednotlivych instrukcí.

Po zkontrolování XML formy se instrukce a jejich argumenty seřadí podle pořadí vzestupně. Následně se pro každou instrukci vytváří objekt ze třídy *Instruction*. Tyto instance se poté uloží do kolekce. 
## 3. Zpracování kódu IPPcode22
Tato část je srdcem programu a nachází se ve funkci *Main* v souboru *Interpret.py*. V cyklu se načítají instrukce a podle názvu se poté instrukce zpracovává.

U instrukcí, které mají argumenty a je to zapotřebí se kontrolují typy argumentů a jejich hodnoty pro správné zpracování instrukce. Pokud jsou argumenty validní, instrukce se provede, skončí iterace a proces se opakuje s novou instrukcí. V případě, že script došel na konec kolekce instrukcí, program se ukončí s exit kódem 0.


# Test.php
Při spuštění scriptu test.php se nejprve ověří argumenty scriptu. O zpracování argumentů a kontrole jejich kombinací se stará třída *ScriptArgs.php*. Následně proběhne scan složky zadané uživatelem pomocí argumentu *--directory=<nazev>*. Scan zajistí cestu k souborům (testům) a tyto soubory uloží do kolekce. Poté je volán patřičný script, který si uživatel zvolí pomocí argumentů a script je spuštěn. Proběhne kontrola výsledků, nejprve se porovnává exit code a v případě shody se porovnává output. Výsledky testů se ukladájí do kolekcí společeně s cestou a názvem testu. Tyto kolekce slouží ke generování HTML souboru k přehlednému zobrazení výsledků jednotlivých testů a celkovému výsledku.
