<?php

include_once "exitHandler.php";

class ScanDirectory
{
    public $src;
    public $out;
    public $rc;
    public $in;

    public $filesInDirectory;
    public $foldersInDirectory;


    public function __construct()
    {
        $this->src = null;
        $this->out = null;
        $this->rc = null;
        $this->in = null;

        $this->filesInDirectory = [];
        $this->foldersInDirectory = [];
        $this->tests = [];
    }

    /**
     * Scans directory, sort files and folders and pushes them into class properties
     * @param string $path
     * @return void
     */
    public function scanDir(string $path, bool $recursiveSearch){
        $folderItems = scandir($path);
        foreach ($folderItems as $item){
            if(is_dir($path. "/" . $item)){
                if($item == "." or $item == "..")
                    continue;
                array_push($this->foldersInDirectory, $path . $item . "/");
            }
            else{
                array_push($this->filesInDirectory, $path . $item);
            }
        }
        if($recursiveSearch and !empty($this->foldersInDirectory)){
            $this->scanDir(array_pop($this->foldersInDirectory), true);
        }
    }
    public function getTestsFromFiles(){
        foreach($this->filesInDirectory as $test){
            if(str_ends_with($test, ".src")){
                $filename = substr($test, 0, -4);
                array_push($this->tests, $filename);
            }
        }
    }

    public function generateFile(string $path, string $fileType, string $content = ""){
        $file = fopen($path.".".$fileType, "w") or
            exitHandler(12, "can't open generated file");
        $txt = $content."\n";
        fwrite($file, $txt);
        fclose($file);
    }

    public function setTestValues($path){
        if(file_exists($path.".src")){
            $this->src = file_get_contents($path . ".src");
        }else{
            exitHandler(11, "can't get content of". $path. ".src file");
        }

        if(file_exists($path.".rc")){
            $this->rc = file_get_contents($path . ".rc");
        }else{
            $this->generateFile($path, "rc", "0");
            $this->rc = 0;
        }

        if(file_exists($path.".in")){
            $this->in = file_get_contents($path . ".in");
        }else{
            $this->generateFile($path, "in");
            $this->in = null;
        }

        if(file_exists($path.".out")){
            $this->out = file_get_contents($path . ".out");

        }else{
            $this->generateFile($path, "out");
            $this->out = null;
        }
    }


    public function printEverything(){
        echo("scanned Directory \n");
        echo("src: " . $this->src . "\n");
        echo("out: " . $this->out . "\n");
        echo("rc: " . $this->rc . "\n");
        echo("in: " . $this->in . "\n");

        echo("filesInDirectory: \n");
        print_r($this->filesInDirectory);

        echo("foldersInDirectory: \n");
        print_r($this->foldersInDirectory);
        echo("tests: \n");
        print_r($this->tests);
        echo ("---------------------------------------------------\n");
    }
}