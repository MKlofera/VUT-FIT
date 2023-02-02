'''
* Project: interpret.py
* 2021/2022
* @author Marek Klofera <xklofe01@stud.fit.vutbr.cz> - VUT FIT student
'''

from interpret_libraries.ErrorHandler import errorHandler
import sys

class ArgumentsChecker:
    def __init__(self):
        super().__init__()
        self.sourceFileXML = None
        self.sourceFileInput = None
        self.inputFileLine = 0

    def checkArgs(self):
        # check types of arguments
        for argument in sys.argv[1:]:
            if argument == '--help' or argument == '-h':
                print("Program načte IPPcode22 v reprezentaci XML a poté tento vstup interpretuje a generuje výstup")
                exit(0)

            elif argument.startswith('--source='):
                # source file with XML reprezentation
                path = argument[9:]
                path = self.getRidOfQuotationMarks(path)
                self.sourceFileXML = path

            elif argument.startswith('--input='):
                # source file with IPPcode22
                path = argument[8:]
                path = self.getRidOfQuotationMarks(path)
                self.sourceFileInput = path
        if(self.sourceFileXML == None and self.sourceFileInput == None):
            errorHandler(10, "ArgumentsChecker", "check", "missing argument")

    def getRidOfQuotationMarks(self, string):
        return string.replace('\"', '')

    def getXMLPath(self):
        if self.sourceFileXML == "":
            return ""
        return self.sourceFileXML

    def getInputPath(self):
        return self.sourceFileInput