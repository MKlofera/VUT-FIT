# IMS projekt - Bitva u Thermopyl (vojenské simulátory)

## Obsah

- [IMS projekt - Bitva u Thermopyl (vojenské simulátory)](#ims-projekt---bitva-u-thermopyl-vojenské-simulátory)
  - [Obsah](#obsah)
  - [1. Úvod](#1-úvod)
    - [1.1 Autoři, zdroje](#11-autoři-zdroje)
    - [1.2 Experimentální ověřování validity modelu](#12-experimentální-ověřování-validity-modelu)
  - [2. Rozbor tématu a použitých metod/technologií](#2-rozbor-tématu-a-použitých-metodtechnologií)
    - [2.1 Použité postupy](#21-použité-postupy)
    - [2.2 Popis původu použitých metod/technologií](#22-popis-původu-použitých-metodtechnologií)
  - [3. Koncepce modelu](#3-koncepce-modelu)
    - [3.1 Popis konceptuálního modelu](#31-popis-konceptuálního-modelu)
    - [3.2 Forma konceptuálního modelu](#32-forma-konceptuálního-modelu)
  - [4. Architektura simulačního modelu](#4-architektura-simulačního-modelu)
    - [4.1 Spouštění simulačního modelu](#41-spouštění-simulačního-modelu)
  - [5. Podstata simulačních experimentů a jejich průběh](#5-podstata-simulačních-experimentů-a-jejich-průběh)
    - [5.1 Postup experimentování](#51-postup-experimentování)
    - [5.2 Dokumentace experimentování](#52-dokumentace-experimentování)
      - [5.2.1 Experiment 1](#521-experiment-1)
      - [5.2.2 Experiment 2](#522-experiment-2)
    - [5.3 Závěry experimentování](#53-závěry-experimentování)
  - [6. Shrnutí simulačních experimentů a závěr](#6-shrnutí-simulačních-experimentů-a-závěr)

<div style="page-break-after: always;"></div>

## 1. Úvod

V této práci je řešen proces sestavování modelu [slide 7] bitvy u Thermopyl a jeho následná simulace [slide 33].
Na základě tohoto modelu a simulačních experimentů [slide 9] bude ukázán výsledek bitvy v podobě počtu přezišvích vojáků na obou stranách.
Smyslem experimentu je demonstrovat, jak jednotlivé atributy a počty vojáků mají vliv na průběh a výsledek bitvy.
Pro zpracování modelu bylo nutno nastudovat a zpracovat způsob interakce mezi jednotlivými vojáky v podobě kalendáře událostí [slide 173] tak, aby akce jednotlivých vojáků působily realisticky.

### 1.1 Autoři, zdroje

Autoři:

- Marek Klofera - xklofe01@stud.fit.vutbr.cz
  
- Jan Šuman - xsuman02@stud.fit.vutbr.cz

Zdroje:

- Obecné:
  - <https://moodle.vut.cz/pluginfile.php/496048/mod_resource/content/1/IMS.pdf>
  - <https://moodle.vut.cz/pluginfile.php/493606/mod_resource/content/2/opora-ims.pdf>
- K faktům:
  - <https://en.wikipedia.org/wiki/Battle_of_Thermopylae>
  - <https://www.armyweb.cz/clanek/bitva-u-thermopyl-7000-reckych-bojovniku-proti-perske-presile>
  - <https://www.memento-historia.cz/clanek/4/jak-ve-skutecnosti-vypadala-bitva-u-thermopyl>

### 1.2 Experimentální ověřování validity modelu

Experimentální ověřování validity [slide 33] modelu probíhalo stylem porovnávání výstupů s historickými odhady zapsanými historiky dané doby. Vstupní data byla nastudována z článků na webu, pojednávajících o této bitvě.

## 2. Rozbor tématu a použitých metod/technologií

Jelikož je takřka nemožné získat přesná data z období této bitvy, použitá fakta byla odvozena ze všech nalezených informací.

Bitva se odehrávala roku 480 př.n.l. u Thermopylské soutěsky. Vojsko Řeků čítalo ± 7 700 bojovníků, z toho ± 300 Sparťanů, ± 1 000 spartských otroků, ± 3 000 Poloponésanů, ± 1 000 Malianů, ± 400 Thebanů, ± 1 000 Phocianů a ± 1000 Locrianů. K perskému vojsku v bitvě u Thermopyl nelze dohledat přiliš podrobná data (celkový počet je odhadován na 100-300 tisíc vojáků), čili pro simulace byl zvolen počet vojáků 120 tisíc, z toho 80 000 otroků, 10 000 Nesmrtelných a 30 000 vojáků (obecně).

Podle historiků je dále uvedeno, že bitva trvala 3 dny, z toho 2 dny se Řekové dokázali bránit, dokud nebyli zrazeni (nepříteli byla prozrazena alternativní cesta a efektivně tak vpadl do zad obráncům). Pro účely simulací tato skutečnost nebyla zahrnuta do modelu a jednotky zkrátka bojují do té doby, dokud jedna z armád nepadne.

### 2.1 Použité postupy

Pro vytvoření modelu byl použit programovací jazyk C++.

### 2.2 Popis původu použitých metod/technologií

Byly použity standardní knihovny, třídy a funkce jazyka C++ za dodržení standardu C++17.
Pro překlad zdrojových souborů byl použit nástroj GNU Make.

## 3. Koncepce modelu

Tato kapitola se zabývá způsobem vytvoření konceptuálního modelu [slide 48], který slouží pro ulehčení implementace simulačního modelu [slide 44]. Cílem simulátoru je demonstrovat, jakou roli hrají počty vojáků a jejich konkrétní atributy na průběh a výsledek bitvy. Do simulace bitvy není zahrnut čas, jelikož cílem není zjistit, jak dlouho bitva potrvá, ale pouze počet přeživších a určit vítěze. Atributy konkrétních vojáků jsou v základu fixně dané. Simulace je konstruována tak, aby skončila tehdy, je-li jedna z bojujících armád bez živých vojáků.

Každý voják má fixní atributy v podobě:
Název | Zdraví | Síla útoku | Rychlost | Síla | Zkušenosti | Výdrž | Morálka
--- | --- |--- |--- |--- |--- |--- |---
Leonidas | 300 | 100 | 70 | 100 | 70 | 95 | 100
Spartans | 250 | 100 | 70 | 100 | 70 | 95 | 100
SpartanSlaves | 200 | 50 | 70 | 50 | 70 | 80 | 80
Poloponnesians | 100 | 50 | 65 | 50 | 70 | 80 | 80
Malians | 100 | 50 | 65 | 40 | 70 | 80 | 80
Thebans | 200 | 40 | 50 | 40 | 70 | 80 | 80
Phocians | 100 | 50 | 65 | 40 | 70 | 80 | 80
Locrians | 100 | 50 | 50 | 40 | 80 | 80 | 80
... | ... | ... | ... | ... | ... | ... | ...
Slaves | 50 | 20 | 40 | 30 | 40 | 10 | 10
Immortals | 100 | 50 | 50 | 50 | 70 | 60 | 80
BasicSoldiers | 100 | 40 | 50 | 50 | 50 | 50 | 50

<div style="page-break-after: always;"></div>

### 3.1 Popis konceptuálního modelu

Vstupem simulace jsou data konfigurace jednotlivých vojáků a konkrétních armád (počty, sekce v armádě). Simulace tedy nejprve inicializuje vojáky do armád na základě této konfigurace a z konstanty `FIRST_LINE_LENGTH` určí, kolik jednotek se nachází v přední linii, a tím pádem může současně bojovat. Každá jednotka může konat celkem 2 akce (útok, obrana).

Pokud jednotka útočí, poškození, které může dát je spočítání následovně:

```text
poškození = síla útoku * ((síla / 100) + (výdrž / 100) + (morálka / 100))
```

Při každé obraně útoku se rozhoduje, zda se jednotka útoku ubraní, či nikoliv. Na základě atributů,
které se vojákům nastaví, se počítá šance, se kterou se útoku ubrání. Pokud je voják zkušenější a rychlejší,
má větší šanci na vykrytí, než voják s nižšími hodnotami.

Pokud tedy obránce útok vykryje, neobdrží žádné poškození.
Pokud útok nevykryje, obdrží poškození způsobené útočníkem.
Jakmile životy obránce klesnou na/pod 0, jednotka umírá a je nahrazena nějakou jinou z další řady (pokud nějaká taková existuje).

### 3.2 Forma konceptuálního modelu

Model je vizualizován pseudokódem níže:

```text
Inicializace armády Řeků, jejich praporů a vojáků v praporu
Inicializace armády Peršanů, jejich praporů a vojáků v praporu

inicializace přední linie pro Řecké vojáky a jejich přesun do přední linie
inicializace přední linie pro Perské vojáky a jejich přesun do přední linie

While ((přední linie řeků nebo jejich armáda obsahuje vojáky) a (přední linie Peršanů nebo jejich armáda obsahuje vojáky)){

    while(přední linie řeků a peršanů není prázdná){
        doplnění přední linie řeků do maximálního počtu
        doplnění přední linie řeků do maximálního počtu

        vytvoření kolekce útoků (Events)
        náhodné zamýchání útoků
        provedení útoků
        odstranění mrtvých jednotek z přední linie
    }
}

Vrácení vítězných vojáků z přední linie do armády

Vypsání výsledku
```

<div style="page-break-after: always;"></div>

## 4. Architektura simulačního modelu

V implementaci je využit generátor typu Event [slide 170], který generuje jednotlivé úkony vojáků (útok, obrana) a vkládá je do kalendáře událostí. Kalendář je následně procházen a akce vykonávány. Jakmile kalendář dojde na konec, nahradí se případní mrtví vojáci živími z další sekce a celý proces se opakuje. Jakmile jedné armádě "dojdou" živí vojáci, simulace končí a na výstup jsou zobrazeny výsledky bitvy.

### 4.1 Spouštění simulačního modelu

Překlad a spouštění s výchozími hodnotami:

```shell
$ make
g++  -o simulator src/*cpp
$ make run
...
```

Pro překlad a spouštění s jinými hodnotami je postup stejný, nicméně je nutné provést změny v souboru (src/BattleGround.cpp).

## 5. Podstata simulačních experimentů a jejich průběh

Cílem experimentů bylo ověřit validitu modelu a hodnoty vstupních dat tak, aby se výsledek simulace blížil skutečnosti.

### 5.1 Postup experimentování

Každý experiment spočívá ve spuštění simulace celkem 10x po sobě v cyklu s nastavenými hodnotami. Na konci každé simulace se výsledky zobrazí na výstup. Tyto výsledky byly následně zprůměrovány a porovnány s odhady tehdejších historiků a s odchylkou (reálná bitva neprobíhala stejným způsobem, jako ta v simulaci) vyhodnoceny.

### 5.2 Dokumentace experimentování

Bylo provedeno několik experimentů tak, abychom se pokusili přiblížit počty a atributy vojáků reálnému výsledku. Zde jsou tedy zobrazeny 2 experimenty.

<div style="page-break-after: always;"></div>

#### 5.2.1 Experiment 1

První experiment byl snahou přiblížit se co nejvíce skutečnosti, tzn. počet přeživších na straně peršanů měl dosahovat cca 100 000 vojáků a ve výsledku znamenat výhru v jejich prospěch.

Ovšem jak již bylo řečeno, simulace trvá, dokud jedna strana obsahuje přeživší. Jelikož v reálné bitvě přežilo na straně sparťanů cca 4 000 vojáků, zde budou ztráty peršanů vyšší a 0 přeživších řeků.

Strana | Počet vojáků | Počet přeživších | Vítěz
---|---|---|---
Peršané | 120 000 | 88 000 | Ano
Řekové | 7700 | 0 | Ne

#### 5.2.2 Experiment 2

Druhý experiment byl měl za cíl ukázat, kolika jednotkami by řekové museli disponovat tak, aby zvítězili.
S 95% přesností (2000 simulací) simulace prokázala, že Řeků by muselo být dvakrát tolik, aby porazili perskou přesilu.

Strana | Počet vojáků | Počet přeživších | Vítěz
---|---|---|---
Peršané | 120 000 | 0 | Ne
Řekové | 14400 | 1368 | Ano

### 5.3 Závěry experimentování

Bylo provedeno více než 100 000 simulací s různými hodnotami atributů vojáků tak, abychom podle výsledků odhadli jejich přibližnou ideální konfiguraci, která výsledně odpovídá realitě. V průběhu těchto experimentů byla prokázána validita modelu a zjištěno několik možných budoucích rozšíření modelu.
Experimenty lze považovat za validní, jelikož jejich zprůměrované výsledky se blížily historickým záznamům.

## 6. Shrnutí simulačních experimentů a závěr

V této práci bylo zkoumáno, jakým způsobem mají vliv počty vojáků a jejich konkrétních atributů.
Výsledkem tohoto zkoumání je funkční nástroj na simulaci bitvy u Thermopyl v jazyce C++.
Byla prokázána validita modelu a výsledky simulací se blížily reálným odhadům zapsaným historiky.
