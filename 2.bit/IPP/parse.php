<?php
/**
* Název souboru: parse.php
* @author Marek Klofera <xklofe01@stud.fit.vutbr.cz>
*/

ini_set('display_errors', 'stderr');

//regex's
const REGEX_TYPE = '/^(bool|int|string)$/';
const REGEX_INT = '/^(int)@(\+|\-)?(\d)+$/';
const REGEX_CONSTANT = '/^(string|bool|int|nil)@((\\\\\d{3})|[^\s#\\\\    ])*$/';  // /^(string)@((\\\\\d{3})|[^\s#\\\\    ])*$/
const REGEX_LABEL = '/^([a-zA-Z]|[_\-$&%*!?])(\w|[_\-$&%*!?])*$/';  // '/[a-zA-Z_\-$&%*!?][a-zA-Z_\-$&%*!?0-9]*/'
const REGEX_VAR = '/(LF|TF|GF)@[a-zA-Z_\-$&%*!?][a-zA-Z_\-$&%*!?0-9]*/';
const REGEX_BOOL = '/^(bool)@(true|false)$/';
const REGEX_NIL = '/^(nil@nil)$/';
const REGEX_STRING = '/^(string)@((d{3})|[^\s#])*$/'; //  '/^(string)@((\\\\\d{3})|[^\s#\\\\    ])*$/';

Main($argv);
function Main ($argv){
    CheckHelpInArguments($argv);

    $codeLine = 1;
    $instructionNum = 1;
    $arguments = [];

    while ($line = fgets(STDIN)) {
        // get rid of spaces and parse to array
        $lineParsed = explode(" ", $line);
        $lineParsed = DeleteComents($lineParsed);
        $lineParsed = DeleteEndOfLine($lineParsed);
        $lineParsed = DeleteExtraSpaces($lineParsed);


        if (isset($lineParsed[0]) && $lineParsed[0] != "" && $lineParsed[0] != " " && $lineParsed[0] != "\n"){
            // check .IPPcode22 header
            if($codeLine == 1){
                echo "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n";
                echo "<program language=\"IPPcode22\"";
            }

            // u kazde instrukce musim kontrolovat pocet argumentu a jaké to jsou argumenty
            switch (strtoupper($lineParsed[0])){

                case ".IPPCODE21":
                    if ($codeLine != 1){
                        ExitCode(22, "Main", "switch, case \".IPPCODE22\"");
                    }
                    break;
                // instruction with 0 arguments
                case "CREATEFRAME":
                case "PUSHFRAME":
                case "POPFRAME":
                case "RETURN":
                case "BREAK":
                    // No arguments to check -> output xml
                    if (count($lineParsed) == 1){
                        CreateInstruction($instructionNum, $lineParsed[0], $arguments);
                    }else{
                        ExitCode(23, "Main", "switch, instructions with 0 arguments");
                    }
                    break;
                // instructions with 1 argument
                //symb
                case "PUSHS":
                case "WRITE":
                case "EXIT":
                case "DPRINT":
                    if (count($lineParsed) == 2 && ValidateSymb($lineParsed[1], $arguments)){
                        CreateInstruction($instructionNum, $lineParsed[0], $arguments);
                    }else{
                        ExitCode(23, "Main", "switch, instructions with 1 argument (symb)");

                    }
                    break;
                //label
                case "CALL":
                case "LABEL":
                case "JUMP":
                    if (count($lineParsed) == 2 && ValidateLabel($lineParsed[1], $arguments)){
                        CreateInstruction($instructionNum, $lineParsed[0], $arguments);
                    }else{
                        ExitCode(23, "Main", "switch, instructions with 1 argument (label)");

                    }
                    break;
                //var
                case "DEFVAR":
                case "POPS":
                    if (count($lineParsed) == 2 && ValidateVar($lineParsed[1], $arguments)){
                        CreateInstruction($instructionNum, $lineParsed[0], $arguments);
                    }else{
                        ExitCode(23, "Main", "switch, instructions with 1 argument (var)");

                    }
                    break;

                // instructions with 2 arguments
                // var, symb
                case "MOVE":
                case "INT2CHAR":
                case "STRLEN":
                case "NOT":
                case "TYPE":
                    if (count($lineParsed) == 3 &&
                        ValidateVar($lineParsed[1], $arguments) &&
                        ValidateSymb($lineParsed[2], $arguments))
                    {
                        CreateInstruction($instructionNum, $lineParsed[0], $arguments);
                    }else{
                        ExitCode(23, "Main", "switch, instructions with 2 arguments (var, symb)");

                    }
                    break;
                // var, type
                case "READ":
                    if (count($lineParsed) == 3 &&
                        ValidateVar($lineParsed[1], $arguments) &&
                        ValidateType($lineParsed[2], $arguments))
                    {

                        CreateInstruction($instructionNum, $lineParsed[0], $arguments);
                    }else{
                        ExitCode(23, "Main", "switch, instructions with 2 arguments (var, type)");

                    }
                    break;
                // instructions with 3 arguments
                // var, symb1, symb2
                case "ADD":
                case "SUB":
                case "MUL":
                case "IDIV":
                case "LT":
                case "GT":
                case "EQ":
                case "AND":
                case "OR":
                case "STRI2INT":
                case "CONCAT":
                case "GETCHAR":
                case "SETCHAR":
                    if (count($lineParsed) == 4 &&
                        ValidateVar($lineParsed[1], $arguments) &&
                        ValidateSymb($lineParsed[2], $arguments) &&
                        ValidateSymb($lineParsed[3], $arguments)
                    ){

                        CreateInstruction($instructionNum, $lineParsed[0], $arguments);
                    }else{
                        ExitCode(23, "Main", "switch, instructions with 3 arguments (var, symb1, symb2)");

                    }
                    break;
                // label, symb1, symb2
                case "JUMPIFEQ":
                case "JUMPIFNEQ":
                    if (count($lineParsed) == 4 &&
                        ValidateLabel($lineParsed[1], $arguments) &&
                        ValidateSymb($lineParsed[2], $arguments) &&
                        ValidateSymb($lineParsed[3], $arguments)
                    ){
                        CreateInstruction($instructionNum, $lineParsed[0], $arguments);
                    }else{
                        ExitCode(23, "Main", "switch, instructions with 3 arguments (label, symb1, symb2)");

                    }
                    break;
                default:
                    ExitCode(22, "Main", "switch, default");

            }
            $codeLine++;
        }
    }
    if ($instructionNum == 1){
        echo "/>\n";
    }else{
        echo "</program>\n";

    }
}
/** Check all arguments, if there is --help argument, script will print to STDOUT some help guid.
 * --help argument is not possible combine with another arguments otherwise exit with error code 10
 * @param array $arguments script arguments passed by user
 * @return void if --help is only argument passed to script exit(0), else exit(10)
 */
function CheckHelpInArguments(array $arguments){
    // check arguments and look for --help"
    foreach ($arguments as $argument){
        if ($argument === "--help"){
            // if there are more arguments, than name of script and "--help" exit with error
            if(count($arguments) > 2){
                exit(10); // 10 = error code
            }
            echo "Hello and welcome advanturer! You have entered help for parse.php script\n".
                "enter \"php parse.php < <name_of_file_with_your_input.txt>\" and it will parse your input to XML \n";
            exit (0); // no error just exit
        }
    }
}

/** Handle exiting script
 * @param int $exitCode - number that will be exited script with
 * @param string $functionName - (optinal) name of a function where exiting script
 * @param string $errorIn - (optinal) description where exactly exiting script
 * @return void no return, just exit
 */
function ExitCode(int $exitCode, string $functionName = "", string $errorIn = "" ){
    if ($functionName != ""){
        echo "Exit in function: " . $functionName;
        if ($errorIn){
            echo ", in : " . $functionName . "\n";
        }
    }
    echo "\n";
    switch ($exitCode){
        case 21:
            echo "chybná nebo chybějící hlavička ve zdrojovém kódu zapsaném v IPPcode22 \n";
            exit(21);
        case 22:
            echo "neznámý nebo chybný operační kód ve zdrojovém kódu zapsaném v IPPcode22 \n";
            exit (22);
        case 23:
            echo "lexikální nebo syntaktická chyba zdrojového kódu zapsaného v IPPcode22 \n";
            exit (23);
    }
}

/**
 * Detecting special XML characters and replacing them to their XML representation
 * @param string $value - XML sentence of printable chars.
 * @return string - modified string with replaced special XML chars to their special representation
 */
function CheckForSpecialCharacters(string $value): string
{
//    & -> &#38;
//    < -> &lt;
//    > -> &gt;
//    ' -> &#38;
//    " -> &#38;
    if (str_contains($value, "&")){
        $value = str_replace("&", "&amp;", $value);
    }
    if (str_contains($value, "<")){
        $value = str_replace("<", "&lt;", $value);
    }
    if (str_contains($value, ">")){
        $value = str_replace(">", "&gt;", $value);
    }
    if (str_contains($value, "'")){
        $value = str_replace("'", "&apos;", $value);
    }
    if (str_contains($value, "\"")){
        $value = str_replace("\"", "&quot;", $value);
    }
    return $value;
}

/** Deletes all coments
 * @param $lineParsed array - line from stdin in array parsed by space or multiple spaces
 * @return array with no comments left
 */
function DeleteComents(array $lineParsed): array
{
    $linesWithNoComents = [];
    $isEnd = false;
    foreach($lineParsed as $element){
        if (str_contains($element, '#')) {
            // in element is a comment
            if(str_starts_with($element, "#")){
                // whole element is a comment
                break;
            }else{
                // only part of a element is a comment
                $newLine = explode("#", $element);
                $element = $newLine[0];
                $isEnd = true;
            }
        }
        array_push($linesWithNoComents, $element);
        if($isEnd){
            break;
        }
    }
    return $linesWithNoComents;
}

/** Deletes char indicating end of line "\n"
 * @param $lineParsed - array of strings - single line from stdin
 * @return array - with deleted chars indicating end of line "\n"
 */
function DeleteEndOfLine(array $lineParsed): array
{
    $noComentedLines = [];
    foreach($lineParsed as $line){
        $line = trim(preg_replace('/\s\s+/', ' ', $line));
        array_push($noComentedLines, $line);
    }
    return $noComentedLines;
}

/** Creates XML instruction with arguments and sends them to STDOUT
 * @param $instructionNum int - Number of already created and echo-?ed? instructions, started by 1
 * @param $opcode string - Name of instruction
 * @param $arguments array of strings - instruction arguments
 */
function CreateInstruction(int &$instructionNum, string $opcode, array &$arguments){
    // there will be instruction, if there was no instruction on stdin, xml will be ended the short way. coz tests
    if ($instructionNum == 1){
        echo">\n";
    }

    echo "    <instruction order=\"".$instructionNum."\" opcode=\"".strtoupper($opcode)."\"";
    $argsNumber = 1;
    if(!empty($arguments)) {
        echo ">";
        echo "\n";
        foreach ($arguments as $argument) {
            $parsedArgument = explode("=>", $argument);
            $type = $parsedArgument[0];
            $value = $parsedArgument[1];
            $value = CheckForSpecialCharacters($value);
            echo "        <arg" . $argsNumber . " type=\"" . $type . "\">" . $value . "</arg" . $argsNumber . ">";
            echo "\n";
            $argsNumber++;
        }
        echo "    </instruction>\n";
    }else{
        // no arguments
        echo " />";
        echo "\n";
    }
    $arguments = [];
    $instructionNum++;
}

/**
 * Validate argument with regex and adds argument to array for xml printing
 * @param string $instrArg = instruction argument, it is a argument passed after instruction (symb, var, label)
 * @param array $instrArgArray reference to variable where are stored instruction arguments in array, type and value ("string=>somestring", "var=>GL@variable" ,...)
 * @return true/false if it is valid argument (var) return true, else return false
 */
function ValidateVar(string $instrArg, array &$instrArgArray): bool
{
    if (preg_match(REGEX_VAR, $instrArg)){
        array_push($instrArgArray, "var=>".$instrArg);
        return true;
    }
    return false;
}

/**
 * Validate argument (symb) with regex and adds argument to array for xml printing
 * @param string $instrArg = instruction argument, it is a argument passed after instruction (symb, var, label)
 * @param array $instrArgArray reference to variable where are stored instruction arguments in array, type and value ("string=>somestring", "var=>GL@variable" ,...)
 * @return true/false if it is valid argument (symb) return true, else return false
 */
function ValidateSymb(string $instrArg, array &$instrArgArray): bool
{
   if (preg_match(REGEX_CONSTANT, $instrArg)){
       // symb is a constant
        $constantType = explode("@", $instrArg);
        //check if it match string, bool, int, nil and if so then add to array of arguments
        if (preg_match(REGEX_STRING, $instrArg) || preg_match(REGEX_BOOL, $instrArg) ||
            preg_match(REGEX_INT, $instrArg) || preg_match(REGEX_NIL, $instrArg)
        ){
            array_push($instrArgArray, $constantType[0]."=>".$constantType[1]);

            return true;
        }

        return false;
   } else if(ValidateVar($instrArg, $instrArgArray)){
        // symb is variable

       return true;
   }else {
        return false;
   }
}

/**
 * Validate argument (label) with regex and adds argument to array for xml printing
 * @param string $instrArg = instruction argument, it is a argument passed after instruction (symb, var, label)
 * @param array $instrArgArray reference to variable where are stored instruction arguments in array, type and value ("string=>somestring", "var=>GL@variable" ,...)
 * @return true/false if it is valid argument (label) return true, else return false
 */
function ValidateLabel(string $instrArg, array &$instrArgArray): bool
{
    if (preg_match(REGEX_LABEL, $instrArg)){
        array_push($instrArgArray, "label=>".$instrArg);
        return true;
    }
    else {
        return false;
    }
}

/**
 * Validate argument (type) with regex and adds argument to array for xml printing
 * @param string $instrArg = instruction argument, it is a argument passed after instruction (symb, var, label)
 * @param array $instrArgArray reference to variable where are stored instruction arguments in array, type and value ("string=>somestring", "var=>GL@variable" ,...)
 * @return true/false if it is valid argument (type) return true, else return false
 */
function ValidateType(string $instrArg, array &$instrArgArray): bool
{
    if (preg_match(REGEX_TYPE, $instrArg)){
        array_push($instrArgArray, "type=>".$instrArg);
        return true;
    }
    else {
        return false;
    }
}

/** Deletes whitespaces from array's values
 * @param array $lineParsed
 * @return array with no extra spaces
 */
function DeleteExtraSpaces(array $lineParsed){
    $lineParsedWithoutExtraSpaces = [];
    foreach ($lineParsed as $item){
        if (strcmp($item, " ") != 0 && strcmp($item, "") != 0){
            array_push($lineParsedWithoutExtraSpaces, $item);
        }
    }
    return $lineParsedWithoutExtraSpaces;
}

// ------------------------------------------------------------------------------------------------------------------
// functions for --stats extension


