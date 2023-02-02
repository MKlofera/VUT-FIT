<?php
include_once "./test_libraries/scriptArgs.php";
include_once "./test_libraries/ScanDirectory.php";
include_once "./test_libraries/HTMLGenerator.php";

function Main(array $argv){
    $scriptArgs = new scriptArgs($argv);
    $scanned = new ScanDirectory();
    $scanned->scanDir($scriptArgs->directory, $scriptArgs->recursive);
    $scanned->getTestsFromFiles();

    $htmlGenerator = new HTMLGenerator();

    if($scriptArgs->int_only){
        $commandBase = "python3.8 " . $scriptArgs->int_script . " --source=";
        runScript($scanned, $scriptArgs, $htmlGenerator, $commandBase);
    }
    elseif($scriptArgs->parse_only){
        $commandBase = "php8.1 " . $scriptArgs->parse_script . " < ";
        runScript($scanned, $scriptArgs, $htmlGenerator, $commandBase);
    }
    else{
        $commandBase = "python3.8 " . $scriptArgs->int_script . " --source=";
        runScript($scanned, $scriptArgs, $htmlGenerator, $commandBase);

        $commandBase = "php8.1 " . $scriptArgs->parse_script . " < ";
        runScript($scanned, $scriptArgs, $htmlGenerator, $commandBase);
    }

    $htmlGenerator->generateHTML();
}

function runScript($scanned, $scriptArgs, $htmlGenerator, $commandBasePart){
    $testingParser = false;
    if(str_starts_with($commandBasePart, "php")){
        $testingParser = true;
    }

    foreach ($scanned->tests as $test){

        $scanned->setTestValues($scanned->tests[0]);

        // set input if there is some
        if($scanned->in != ""){
            $commandToExecute = $commandBasePart . $test . ".src --input=" . $test . ".in";
        }else{
            $commandToExecute = $commandBasePart . $test . ".src";
        }
        unset($output);
        exec($commandToExecute, $scriptOutput, $scriptReturnExitCode);
        // array to string
        $scriptOutput = implode("\n", $scriptOutput);

        //compare exit codes
        $testPassed = compareResults($scanned, $scriptReturnExitCode, $scriptOutput, $testingParser, $scriptArgs);
        if($testPassed){
            array_push($htmlGenerator->succesTests, $test);
        }else{
            array_push($htmlGenerator->failedTests, $test);
        }
        array_shift($scanned->tests);
    }
}

/** function compares test results with script results
 * @param $scanned
 * @param $scriptReturnExitCode
 * @param $scriptOutput
 * @param $testingParser
 * @param $scriptArgs
 * @return bool if exit codes matches and != 0 return TRUE,
 *  if exit codes matches and == 0 compare outputs, if they match TRUE,
 *  else FALSE
 */
function compareResults ($scanned, $scriptReturnExitCode, $scriptOutput, $testingParser, $scriptArgs){
    //exit codes equals
    if($scanned->rc == $scriptReturnExitCode){
        if($scriptReturnExitCode == 0){
            $outputsMatches = compareOutputs($scanned, $scriptOutput, $testingParser, $scriptArgs);
            if($outputsMatches)
                return true;
        }else
            return true;
    }
    return false;
}

function compareOutputs($scanned, $scriptOutput, $testingParser, $scriptArgs){
    if($testingParser){

        unset($xml_retval);
        file_put_contents("scriptOutput.xml", $scriptOutput);
        $command = "java -jar ".$scriptArgs->jexampath."jexamxml.jar scriptOutput.xml ". $scanned->tests[0] . ".out" . " ".$scriptArgs->jexampath."options";
        exec($command, $jexamOutput, $jexamResultCode);
        if($jexamResultCode == 0){
            return true;
        }
    }else{
        if($scanned->out == $scriptOutput){
            return true;
        }
    }
    return false;
}

Main($argv);

?>
