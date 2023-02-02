<?php

include_once "exitHandler.php";
include_once "Constants.php";

class scriptArgs
{
    public $help;
    public $directory;
    public $recursive;
    public $parse_script;
    public $int_script;
    public $int_script_defined;
    public $parse_only;
    public $parse_script_defined;
    public $int_only;
    public $jexampath;
    public $jexampath_defined;
    public $nonclean;

    public function __construct($argv)
    {
        $this->help = false;
        $this->directory = "./";
        $this->recursive = false;
        $this->parse_script = "parse.php";
        $this->parse_script_defined = false;
        $this->int_script = "interpret.py";
        $this->int_script_defined = false;
        $this->parse_only = false;
        $this->int_only = false;
        $this->jexampath = "/pub/courses/ipp/jexamxml/";
        $this->jexampath_defined = false;

        $this->nonclean = false;

        $this->getArgs($argv);
        $this->checkArgsCombinations($argv);

    }

    /**
     * @param $argv
     * @return void
     */
    public function getArgs(array $argv)
    {

        foreach ($argv as $argument) {
            if ($argument == "--help" or $argument == "-h") {
                if (count($argv) == 2) {
                    echo HELP_MESSAGE;
                    exit(0);
                } else
                    exitHandler(10, "cant combinate help arg with other args");
            }
            if (str_starts_with($argument, "--directory=")) {
                // if path's last char is not "/" then add it
                $path = substr($argument, 12);
                if (substr($path, -1) != "/"){
                    $path = $path . "/";
                }
                $this->directory = $path;

            } else if ($argument == "--recursive") {
                $this->recursive = true;

            } else if (str_starts_with($argument, "--parse-script=")) {
                $this->parse_script = substr($argument, 15);
                $this->parse_script_defined = true;

            } else if (str_starts_with($argument, "--int-script=")) {
                $this->int_script = substr($argument, 13);
                $this->int_script_defined = true;


            } else if ($argument == "--parse-only") {
                $this->parse_only = true;

            } else if ($argument == "--int-only") {
                $this->int_only = true;
            } else if (str_starts_with($argument, "--jexampath=")) {
                $this->jexampath = substr($argument, 12);

            } else if ($argument == "--nonclean") {
                $this->nonclean = true;
            }
        }
    }

    public function checkArgsCombinations(){
        if (
            ($this->parse_only and $this->int_only) or
            ($this->parse_only and $this->int_script_defined)
        ){
            exitHandler(10, "parse-only and (int-only | int-script) args can not be combinated");
        }
        if (
            ($this->int_only and $this->parse_only) or
            ($this->int_only and $this->parse_script_defined)
        ){
            exitHandler(10, "int-only and (parse-only | parse-script) args can not be combinated");
        }
    }

    public function printEverything(){
        /** testing command
         * php8.1 test.php --directory=./directory/ --recursive --parse-script=./parse.php --int-script=../interpret/interpret.py --parse-only --int-only --jexampath=./././jexampath --nonclean
         */
        echo("\nSCRIPT ARGUMENTS \n");
        echo("help: ". $this->help . "\n");
        echo("directory: ". $this->directory . "\n");
        echo("recursive: ". $this->recursive . "\n");
        echo("parse_script: ". $this->parse_script . "\n");
        echo("int_script: ". $this->int_script . "\n");
        echo("parse_only: ". $this->parse_only . "\n");
        echo("int_only: ". $this->int_only . "\n");
        echo("jexampath: ". $this->jexampath . "\n");
        echo("nonclean: ". $this->nonclean . "\n");
        echo("--------------------------------------------------------\n");
    }
}