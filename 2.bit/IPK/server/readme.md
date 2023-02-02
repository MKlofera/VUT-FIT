# Server v jazyce C++
Server napsaný v jazyce C++, komunikující prostřednictvím protokolu HTTP, odpovídá na 3 typy dotazů. Server naslouchá na zadaném portu, který si zvolí uživatel a prostřednictvím argumentu jej nastaví. 

Server je přeložitelný pomocí Makefile, který vytvoří spustitelný soubor hinfosvc.

## Spuštění serveru

1. Vytvoření spustitelného souboru
   - Otevřít v termínálu adresář s projektem a spustit příkaz **make**. Tím se vytvoří spustitelný soubor.
2. Spuštění serveru 
   - Nyní je nutné spustit server pomocí příkazu **./hinfosvc <číslo portu>** (číslo portu může být například 8080, 2233, ...)

Nyní je server spušten a lze jej vuyžít k získávání informací.

## Použití serveru

##### 1. Získání doménového jména
   - Vrací síťové jméno počítače včetně domény, například:
   - curl http://localhost:<číslo portu zadané jako argument při spuštění>/hostname
   - merlin.fit.vutbr.cz

##### 2. Získání informací o CPU 
   - Vrací informaci o procesoru, například:
   - curl http://localhost:<číslo portu zadané jako argument při spuštění>/cpu-name
   - Intel(R) Xeon(R) CPU E5-2640 0 @ 2.50GHz

##### 3. Aktuální zátěž 
   - Vrací aktuální informace o zátěži, například:
   - curl http://localhost:<číslo portu zadané jako argument při spuštění>/load
   - 7%
## Ukončení serveru
- Server je možné ukončit pomocí příkazu CTRL+C.