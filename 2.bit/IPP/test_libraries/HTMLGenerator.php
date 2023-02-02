<?php

include_once "Constants.php";

class HTMLGenerator
{
    public $succesTests;
    public $failedTests;

    public function __construct()
    {
        $this->failedTests = [];
        $this->succesTests = [];
    }

    public function generateHTML(){
        $counter = 0;
        $numOfSuccessTests = count($this->succesTests);
        $numOfFailedTests = count($this->failedTests);
        $generatedHTML = HTML_HEADER .
        "<body>
        <div class='title'>
            <h1>TEST RESULTS</h1>
            <h2>test passed:" . $numOfSuccessTests . "</h2>
            <h2>test failed: " . $numOfFailedTests . " </h2>
            <h2>test sum:" . $numOfSuccessTests + $numOfFailedTests . " </h2>
        </div>
        ";
        foreach ($this->failedTests as $test){
            $generatedHTML .= "
                <div class='test'>
            <h4>".$counter."</h4>
            <p>". $test ."</p>
            <p style='color:red;' >Fail</p>
        </div>";
            $counter ++;
        }
        foreach ($this->succesTests as $test){
            $generatedHTML .= "
                <div class='test'>
            <h4>".$counter."</h4>
            <p>". $test ."</p>
            <p style='color:green;' >Success</p>
        </div>";
            $counter ++;
        }


        $generatedHTML .= "</body>
                </html>";
        echo $generatedHTML;
    }


    public function printEverything(){
        echo("HTML GENERATOR\n");
        echo("succesTests: \n");
        print_r($this->succesTests);
        echo("failedTests: \n");
        print_r($this->failedTests);
        echo("---------------------------------------------------------------------\n");
    }

}