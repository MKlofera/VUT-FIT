'''
* Project: interpret.py
* 2021/2022
* @author Marek Klofera <xklofe01@stud.fit.vutbr.cz> - VUT FIT student
'''

# external lib.
import xml.etree.ElementTree as ET
import sys

# my libraries
from interpret_libraries.ErrorHandler import errorHandler
from interpret_libraries.Instruction import Instruction
import interpret_libraries.Constants as const

class XMLParser():
    def __init__(self, XMLpath):
        super().__init__()
        self.XMLpath = XMLpath

        self.sortedArgs = []
        self.sortedInstructions = []

        self.tree = None
        self.root = None

    def parse(self, instructionList):
        self.checkXMLFile()
        self.checkXMLRoot()
        self.checkXMLHeader()
        self.checkXMLInstructions()
        self.checkXMLArgInInstruction()

        self.sortInstructionsAndArgs()
        self.checkArgNumbers()
        self.checkInstructionsNumbers()
        self.parseXML(instructionList)

    def checkXMLFile(self):
        '''
        check file with XML representation of IPPcode22
        :return:
        '''
        try:
            if (self.XMLpath == None):
                self.tree = ET.parse(sys.stdin)
            else:
                self.tree = ET.parse(self.XMLpath)
        except (FileNotFoundError, PermissionError, OSError):
            errorHandler(11, "XMLParser.py", "checkXMLFile")
        except Exception as e:
            errorHandler(31, "XMLParser.py", "checkXMLFile", e)

    def checkXMLRoot(self):
        try:
            self.root = self.tree.getroot()
        except:
            errorHandler(31, "XMLParser", "checkXMLRoot")

    def checkXMLHeader(self):
        if (self.root.tag.upper() == "PROGRAM"):
            for attribute, value in self.root.items():
                # check attributes
                if (
                        attribute.upper() not in const.ROOT_ATTRIBUTES or
                        (attribute.upper() == const.ROOT_ATTRIBUTES[0] and value.upper() != const.HEADER)
                ):
                    errorMessage = "wrong attribute: " + attribute + ", with value: " + value
                    errorHandler(32, "XMLParser", "checkXMLHeader", errorMessage)
        else:
            errorHandler(32, "XMLParser", "checkXMLHeader", "wrong root tag")

    def checkXMLInstructions(self):
        # for every instruction
        for child in self.root:
            if (child.tag == "instruction"):
                # order and opode must be in every instruction
                if ("order" not in child.attrib or "opcode" not in child.attrib):
                    errorHandler(32, "XMLParser.py", "checkXMLInstructions",
                                 "instruction does not have opcode or order")

                # for every attribute in instruction
                for attribute, value in child.attrib.items():
                    # check if instructions atributes are valid
                    if (attribute.upper() not in const.INSTRUCTION_ATTRIBUTES):
                        errorHandler(32, "XMLParser.py", "checkXMLInstructions", "not allowed attribute in instruction")

                    # if order has number
                    if (attribute.upper() == "ORDER" and not value.isnumeric()):
                        errorHandler(32, "XMLParser.py", "checkXMLInstructions", "not int in order attribute")

                    # if opcode has allowed instruction
                    if (attribute.upper() == "OPCODE" and value.upper() not in const.INSTRUCTIONS):
                        errorMessage = "unkown instruction: " + value
                        errorHandler(32, "XMLParser.py", "checkXMLInstructions", errorMessage)

            else:
                errorHandler(32, "XMLParser.py", "checkXMLInstructions", "only instructions are allowed")

    def checkXMLArgInInstruction(self):
        for instructions in self.root:
            for args in instructions:
                # instraction can have only arg child
                if(args.tag.startswith("arg")):
                    argNumber = args.tag[3:]
                    # after arg should be number of arg (1|2|3)
                    if(argNumber not in const.ARG_NUMBERS):
                        errorHandler(32, "XMLParser.py", "checkXMLArgInInstruction",
                                     "arg in instruction does not have a valid number")

                    for argAttrib, value in args.attrib.items():
                        if(value.upper() == "INT" and not args.text.lstrip("-").isnumeric()):
                            message = args.text + " is wrong value for type INT"
                            errorHandler(32, "XMLParser.py", "checkXMLArgInInstruction", message)

                        # is insturction arg attribute unkown?
                        if (argAttrib.upper() != const.ARG_ATTRIBUTE):
                            message = "Instruction arg has unkown attribute: " + argAttrib
                            errorHandler(32, "XMLParser.py", "checkXMLArgInInstruction", message)
                            
                        if(value.upper() not in const.ARG_TYPES):
                            message = "Instruction arg has unkown type value: " + value
                            errorHandler(32, "XMLParser.py", "checkXMLArgInInstruction", message)
                else:
                    errorHandler(32, "XMLParser.py", "checkXMLArgInInstruction",
                                 "instruction can only have 'arg' child")

    def sortInstructionsAndArgs(self):
        # sorting by
        def argSort(e):
            return e['argNumber']

        # sorting by
        def instructionSort(e):
            return int(e["order"])

        for instructionXML in self.root:
            # add instruction args to list
            for arg in instructionXML:
                self.sortedArgs.append({
                    'argNumber': arg.tag[3:],
                    'attribType': arg.attrib["type"].upper(),
                    "value": arg.text
                })
            # sort args
            if(len(self.sortedArgs) > 1):
                self.sortedArgs = sorted(self.sortedArgs, key=argSort)



            # add args to instruction with inst. number and name
            self.sortedInstructions.append({
                "order": instructionXML.attrib["order"],
                "name": instructionXML.attrib["opcode"],
                "args": self.sortedArgs
            })
            self.sortedArgs = []
        # sort instructions
        self.sortedInstructions = sorted(self.sortedInstructions, key=instructionSort)

    def checkArgNumbers(self):
        '''
        function will check instruction arguments numbers if there is not any missing, duplicated (can't exist arg2 without arg1)
        :return: None
        '''
        for XMLInstruction in self.sortedInstructions:
            for i, arg in enumerate(XMLInstruction["args"]):
                i += 1
                if (i != int(arg["argNumber"])):
                    message = "not expected instruction arg number (probably there is a missing argument) "
                    errorHandler(32, "XMLParser.py", "checkXMLArgInInstruction", message)

    def checkInstructionsNumbers(self):
        '''
        function will check instructions numbers if there is not any missing, duplicated (can't exist arg2 without arg1)
        :return: None
        '''
        lastInstructionNumber = 0
        for instruction in self.sortedInstructions:
            if (lastInstructionNumber < int(instruction["order"])):
                lastInstructionNumber = int(instruction["order"])
            else:
                message = "not expected instruction number (missing, duplicated,...) "
                errorHandler(32, "XMLParser.py", "checkXMLArgInInstruction", message)

    def parseXML(self, instructionList):
        for XMLInstruction in self.sortedInstructions:
            instruction = Instruction(XMLInstruction["name"], int(XMLInstruction["order"]))
            instruction.addArgs(XMLInstruction["args"])
            instructionList.addInstruction(instruction)




