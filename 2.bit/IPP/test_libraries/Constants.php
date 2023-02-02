<?php

const HELP_MESSAGE = "Hello and welcome advanturer! You have entered help for test.php script\n
    --help viz společný parametr všech skriptů v sekci 2.2;\n
    --directory=path testy bude hledat v zadaném adresáři (chybí-li tento parametr, skript prochází aktuální adresář);\n
    --recursive testy bude hledat nejen v zadaném adresáři, ale i rekurzivně ve všech jeho podadresářích;\n
    --parse-script=file soubor se skriptem v PHP 8.1 pro analýzu zdrojového kódu v IPPcode22 (chybí-li tento parametr, implicitní hodnotou je parse.php uložený v aktuálním adresáři);\n
    --int-script=file soubor se skriptem v Python 3.8 pro interpret XML reprezentace kódu v IPPcode22 (chybí-li tento parametr, implicitní hodnotou je interpret.py uložený v aktuálním adresáři);\n
    --parse-only bude testován pouze skript pro analýzu zdrojového kódu v IPPcode22 (tento parametr se nesmí kombinovat s parametry --int-only a --int-script), výstup s referenčním výstupem (soubor s příponou out) porovnávejte nástrojem A7Soft JExamXML (viz [2]);\n
    --int-only bude testován pouze skript pro interpret XML reprezentace kódu v IPPcode22 (tento parametr se nesmí kombinovat s parametry --parse-only, --parse-script a --jexampath). Vstupní program reprezentován pomocí XML bude v souboru s příponou src.\n
    --jexampath=path cesta k adresáři obsahující soubor jexamxml.jar s JAR balíčkem s nástrojem A7Soft JExamXML a soubor s konfigurací jménem options. Je-li parametr vynechán, uvažuje se implicitní umístění /pub/courses/ipp/jexamxml/ na serveru Merlin, kde bude test.php hodnocen. Koncové lomítko v path je případně nutno doplnit.\n
    --noclean během činnosti test.php nebudou mazány pomocné soubory s mezivýsledky, tj. skript ponechá soubory, které vznikají při práci testovaných skriptů (např. soubor s výsledným XML po spuštění parse.php atd.).\n";

const HTML_HEADER ="<!DOCTYPE html>\n
    <html lang='en'>\n
    <head>\n
        <meta charset='UTF-8'>\n
        <meta http-equiv='X-UA-Compatible' content='IE=edge'>\n
        <meta name='viewport' content='width=device-width, initial-scale=1.0'>\n
        <title>Document</title>\n
        <style>\n
            .title{\n
                display: flex;\n
                flex-direction: column;\n
                justify-content: center;\n
                align-items: center;\n
            }\n
            .test{\n
                display: flex;\n
                justify-content: center;\n
                align-items: center;\n
            }\n
            .test h4{\n
                margin: 20px;\n
            }\n
            .test p{\n
                margin: 20px;\n
            }\n
        </style>\n
    </head>";