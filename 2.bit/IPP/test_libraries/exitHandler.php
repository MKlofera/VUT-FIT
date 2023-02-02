<?php

/**
 * function handles all types of exit's
 * @param int $exitNumber
 * @param string $note - what caused exit
 * @param string $file - in which file is exiting script
 * @param string $function - in which function is exiting script
 * @return void - none, just exiting script
 */
function exitHandler(int $exitNumber, string $note = "", string $file = "", string $function = ""){

    echo("EXIT -" . $exitNumber);

    if($file != "")
        echo("File: " . $file . "\n");

    if ($function != "")
        print("Function: " . $function . "\n");

    if ( $note != "")
        print("Note: " . $note . "\n");

    switch ($exitNumber){

        case 0:
            exit(0); // everything ok
        case 10:
            echo ($exitNumber . " - chybějící parametr skriptu (je-li třeba) nebo použití zakázané kombinace parametrů;");
            exit (10);
    }

}