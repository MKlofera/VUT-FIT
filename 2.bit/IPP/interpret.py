'''
* Project: interpret.py
* 2021/2022
* @author Marek Klofera <xklofe01@stud.fit.vutbr.cz> - VUT FIT student
'''

from interpret_libraries.ErrorHandler import errorHandler, successExit
from interpret_libraries.ArgumentsChecker import ArgumentsChecker
from interpret_libraries.XMLParser import XMLParser
from interpret_libraries.Frame import Frame
from interpret_libraries.DataStack import DataStack
from interpret_libraries.InstructionList import InstructionList
from interpret_libraries.Variable import Variable
from pprint import pprint
import sys


def printInstructionList(instructionList):
    for instruction in instructionList:
        pprint(vars(instruction))


def main():
    def getSymbType(arg, checkTypeIsNone=False):
        requiredType = None
        if (arg["attribType"] == "VAR"):
            variable = frame.getVar(arg["data"])
            if(checkTypeIsNone and variable.type == None):
                errorHandler(56, "interpret.py", "main", "variable has type None")
            requiredType = variable.type
        else:
            requiredType = arg["attribType"]
        return requiredType

    def getSymbValue(arg, checkValueIsNone=False):
        requiredValue = None
        if (arg["attribType"] == "VAR"):
            variable = frame.getVar(arg["data"])
            if (checkValueIsNone and variable.valueSet == False):
                errorHandler(56, "interpret.py", "main", "variable has value None")
            requiredValue = variable.value
            if (variable.type == "STRING"):
                requiredValue = frame.getRidOfSpecialChars(requiredValue)
        else:
            requiredValue = arg["data"]
            if(arg["attribType"] == "STRING"):
                requiredValue = frame.getRidOfSpecialChars(requiredValue)
        return requiredValue

    # check script arguments and get path to XML or IPPcode22
    scriptArgs = ArgumentsChecker()
    scriptArgs.checkArgs()

    # Create objects
    instructionList = InstructionList()
    frame = Frame()
    dataStack = DataStack()

    # check XML, sort by right order, parse it and save to instructionList.list
    parsedXML = XMLParser(scriptArgs.sourceFileXML)
    parsedXML.parse(instructionList)

    # printInstructionList(instructionList.list)

    # number of instruction executed
    instructionList.setLabels()
    while (1):
        instruction = instructionList.getInstruction()

        # BIG SWITCH WITH ALL INSTRUCTIONS
        # Instructions with 0 arguments
        if (instruction.name == "CREATEFRAME"):
            frame.createFrame()
        elif (instruction.name == "PUSHFRAME"):
            frame.pushFrame()
        elif (instruction.name == "POPFRAME"):
            frame.popFrame()
        elif (instruction.name == "RETURN"):
            try:
                returnToLabel = instructionList.callStack.pop()
                instructionList.execInstNum = returnToLabel
            except Exception as e:
                errorHandler(56, "interpret.py", "main", "RETURN - empty frame")

        elif (instruction.name == "BREAK"):
            frame.printFrames()
        # instruction with 1 argument
        # VAR
        elif (instruction.name == "DEFVAR"):
            arg = instruction.args[0]
            if(frame.varExistsNoErrorHandeling(arg["data"])):
                errorHandler(52, "interpret.py", "main", "DEFVAR - redefinition of variable")
            if (arg["attribType"].upper() != "VAR"):
                errorHandler(32, "interpret.py", "main", "DEFVAR - arg type not var")
            else:
                newVariable = Variable(arg["data"])
                frame.addToFrame(newVariable)

        elif (instruction.name == "POPS"):
            var1 = instruction.args[0]["data"]
            type, value = dataStack.popValue()

            varGettingResult = frame.getVar(var1)
            varGettingResult.type = type
            varGettingResult.value = value
            varGettingResult.valueSet = True

            # LABEL

        elif (instruction.name == "CALL"):
            instructionList.callStack.append(instructionList.execInstNum)
            jumpTo = instruction.args[0]["data"]
            instructionList.jumpTo = jumpTo

        elif (instruction.name == "LABEL"):
            # labels are added in instructionList.labels at the beggining
            continue

        elif (instruction.name == "JUMP"):
            jumpTo = instruction.args[0]["data"]
            instructionList.jumpTo = jumpTo

        elif (instruction.name == "PUSHS"):
            symbType = getSymbType(instruction.args[0], True)
            symbValue = getSymbValue(instruction.args[0], True)
            arg = instruction.args[0]
            dataStack.pushValue(arg, frame)

        elif (instruction.name == "WRITE"):
            symbType = getSymbType(instruction.args[0], True)
            symbValue = getSymbValue(instruction.args[0], True)

            # get rid of special characters (\032,...)

            if (symbType == "STRING"):
                symbValue = frame.getRidOfSpecialChars(symbValue)

            if (symbType == "FLOAT"):
                try:
                    symbValue = float.hex(symbValue)
                except:
                    pass

            if (symbType == "NIL"):
                print("", end="")
            elif (symbType.upper() == "TRUE" or symbType.upper() == "FALSE"):
                print(symbValue.lower(), end="")
            else:
                print(symbValue, end="")

        elif (instruction.name == "EXIT"):
            exitType = getSymbType(instruction.args[0], True)
            if (exitType == "INT"):
                exitNumber = int(getSymbValue(instruction.args[0], True))
                if(exitNumber >= 0 and exitNumber <= 49):
                    exit(exitNumber)
                else:
                    errorHandler(57, "interpret.py", "main", "non-valid exit number")
            else:
                errorHandler(53, "interpret.py", "main", "non-valid exit type")

        elif (instruction.name == "DPRINT"):
            symbValue = getSymbValue(instruction.args[0])
            sys.stderr.write(symbValue)

        # instruction with 2 arguments
        # VAR, SYMB
        elif (instruction.name == "MOVE"):  # move symb to var
            newType = getSymbType(instruction.args[1], True)
            newValue = getSymbValue(instruction.args[1], True)

            if (newType == "FLOAT"):
                newValue = float.fromhex(newValue)

            varGettingNewValue = frame.getVar(instruction.args[0]["data"])
            varGettingNewValue.value = newValue
            varGettingNewValue.type = newType
            varGettingNewValue.valueSet = True

        elif (instruction.name == "INT2CHAR"):
            var1 = instruction.args[0]
            symb1Type = getSymbType(instruction.args[1], True)
            if(symb1Type != "INT"):
                errorHandler(53, "interpret.py", "main", "wrong symbol type")


            convertingValue = getSymbValue(instruction.args[1], True)
            if (convertingValue == None):
                errorHandler(56, "interpret.py", "main", "variable has value None")

            try:
                convertedChar = chr(int(convertingValue))
                varGettingNewValue = frame.getVar(var1["data"])
                varGettingNewValue.value = convertedChar
                varGettingNewValue.type = "STRING"
                varGettingNewValue.valueSet = True

            except Exception as e:
                errorHandler(58, "interpret.py", "Main", "INT2CHAR - can't convert to Unicode char")

        elif (instruction.name == "INT2FLOAT"):
            var1 = instruction.args[0]  # var
            symb1 = instruction.args[1]  # symb

            convertingValue = getSymbValue(symb1)
            if (convertingValue == None):
                errorHandler(56, "interpret.py", "main", "variable has value None")

            try:
                varGettingNewValue = frame.getVar(var1["data"])
                varGettingNewValue.value = float(int(convertingValue))
                varGettingNewValue.valueSet = True
                varGettingNewValue.type = "FLOAT"
            except Exception as e:
                errorHandler(53, "interpret.py", "Main", "INT2FLOAT - can't convert to float")

        elif (instruction.name == "FLOAT2INT"):
            var1 = instruction.args[0]  # var
            symb1 = instruction.args[1]  # symb

            convertingValue = getSymbValue(symb1)
            if (convertingValue == None):
                errorHandler(56, "interpret.py", "main", "variable has value None")
            try:
                varGettingNewValue = frame.getVar(var1["data"])
                varGettingNewValue.value = int(convertingValue)
                varGettingNewValue.valueSet = True
                varGettingNewValue.type = "INT"
            except Exception as e:
                errorHandler(53, "interpret.py", "Main", "FLOAT2INT - can't convert to float")

        elif (instruction.name == "STRLEN"):
            string = getSymbValue(instruction.args[1], True)
            symbType = getSymbType(instruction.args[1], True)
            stringLength = 0

            if(symbType != "STRING"):
                errorHandler(53, "interpret.py", "main", "STRLEN - wrong symb type")

            if(string != "" and string != None):
                stringLength = len(string)

            varGettingResult = frame.getVar(instruction.args[0]["data"])
            varGettingResult.type = "INT"
            varGettingResult.value = stringLength
            varGettingResult.valueSet = True

        elif (instruction.name == "NOT"):
            symb1Type = getSymbType(instruction.args[1], True)
            if (symb1Type != "BOOL"):
                errorHandler(53, "interpret.py", "main", "wrong types of symb1")

            symb1Value = getSymbValue(instruction.args[1], True)
            try:
                if(symb1Value.upper() == "FALSE"):
                    result = True
                else:
                    result = False

                varGettingResult = frame.getVar(instruction.args[0]["data"])
                varGettingResult.type = "BOOL"
                varGettingResult.value = str(result).lower()
                varGettingResult.valueSet = True

            except Exception as e:
                errorHandler(99, "interpret.py", "main", "NOT")

        elif (instruction.name == "TYPE"):
            symb1Type = getSymbType(instruction.args[1])
            result = ""

            try:
                if (symb1Type != "" and symb1Type != None):
                    result = symb1Type
                varGettingResult = frame.getVar(instruction.args[0]["data"])
                varGettingResult.type = "STRING"
                varGettingResult.value = result.lower()
                varGettingResult.valueSet = True
            except Exception as e:
                errorHandler(99, "interpret.py", "main", "TYPE")
            # VAR, TYPE

        elif (instruction.name == "READ"):
            inputValue = None
            if (scriptArgs.sourceFileInput != None):
                file = None
                try:
                    file = open(scriptArgs.sourceFileInput, 'r')
                except Exception as e:
                    errorHandler(11, "interpret.py", "Main", "can't open input file")

                fileLines = file.readlines()
                inputValue = fileLines[scriptArgs.inputFileLine]
                scriptArgs.inputFileLine += 1
                inputValue = inputValue.rstrip()

            else:
                inputValue = input()

            toType = instruction.args[1]["data"]
            varGettingResult = frame.getVar(instruction.args[0]["data"])

            if (toType.upper() == "BOOL"):
                if (inputValue.lower() == "true"):
                    varGettingResult.type = "BOOL"
                    varGettingResult.value = "true"
                else:
                    varGettingResult.type = "BOOL"
                    varGettingResult.value = "false"

            elif (toType.upper() == "INT"):
                if(inputValue.lstrip("-").isdigit()):
                    varGettingResult.type = "INT"
                    varGettingResult.value = str(inputValue)
                else:
                    varGettingResult.type = "NIL"
                    varGettingResult.value = "nil"

            elif (toType.upper() == "STRING"):
                varGettingResult.type = "STRING"
                varGettingResult.value = inputValue
            else:
                varGettingResult.type = "NIL"
                varGettingResult.value = "NIL"

            varGettingResult.valueSet = True

        # instruction with 3 arguments
        # VAR, SYMB1, SYMB2
        elif (
                instruction.name == "ADD" or
                instruction.name == "SUB" or
                instruction.name == "MUL" or
                instruction.name == "IDIV" or
                instruction.name == "DIV"
        ):
            isFloat = False

            # check type
            symb1Type = getSymbType(instruction.args[1])
            symb2Type = getSymbType(instruction.args[2])
            symb1Value = getSymbValue(instruction.args[1], True)
            symb2Value = getSymbValue(instruction.args[2], True)

            if (symb1Type != symb2Type and (symb1Type != "INT" or symb1Type != "FLOAT")):
                errorHandler(53, "interpret.py", "main", "ADD, SUB, MUL, IDIV - wrong type")

            # convert to int or float
            try:
                if (symb1Type == "INT"):
                    symb1Value = int(symb1Value)
                    symb2Value = int(symb2Value)
                else:
                    isFloat = True
                    symb1Value = float(symb1Value)
                    symb2Value = float(symb2Value)
            except Exception as e:
                errorHandler(53, "interpret.py", "main", "ADD, SUB, MUL, IDIV - wrong operands")

            try:
                result = None
                if (instruction.name == "ADD"):
                    result = symb1Value + symb2Value
                elif (instruction.name == "SUB"):
                    result = symb1Value - symb2Value
                elif (instruction.name == "MUL"):
                    result = symb1Value * symb2Value
                elif (instruction.name == "IDIV" or instruction.name == "DIV"):
                    if (symb2Value == 0):
                        errorHandler(57, "interpret.py", "main", "IDIV - division by 0")
                    result = int(symb1Value / symb2Value)

                # set right type and value to instruction
                varGettingResult = frame.getVar(instruction.args[0]["data"])
                if (isFloat):
                    varGettingResult.value = str(float.hex(result))
                    varGettingResult.type = "FLOAT"
                else:
                    varGettingResult.type = "INT"
                    varGettingResult.value = str(result)
                varGettingResult.valueSet = True

            except Exception as e:
                errorHandler(99, "interpret.py", "main", "ADD, SUB, MUL, IDIV - maybe overflow?")

        elif (instruction.name == "LT" or instruction.name == "GT" or instruction.name == "EQ"):
            def saveResult():
                varName = instruction.args[0]["data"]
                varGettingResult = frame.getVar(varName)
                varGettingResult.type = "BOOL"
                varGettingResult.value = str(result).lower()
                varGettingResult.valueSet = True

            result = None
            symb1Type = getSymbType(instruction.args[1], True)
            symb2Type = getSymbType(instruction.args[2], True)
            symb1Value = getSymbValue(instruction.args[1], True)
            symb2Value = getSymbValue(instruction.args[2], True)

            if (symb1Type == "STRING"):
                symb1Value = frame.getRidOfSpecialChars(symb1Value)
                symb2Value = frame.getRidOfSpecialChars(symb2Value)

            if (
                    (symb1Value == None and symb2Value != None) or
                    (symb1Value != None and symb2Value == None) or
                    (symb1Value == None and symb2Value == None)):
                result = True

            else:
                if (symb1Type == symb2Type and symb2Type in ["INT", "BOOL", "STRING"]):
                    if (instruction.name == "LT"):
                        if (symb1Type == "INT"):
                            result = "TRUE" if int(symb1Value) < int(symb2Value) else "FALSE"
                        elif (symb1Type == "STRING"):
                            result = "TRUE" if symb1Value < symb2Value else "FALSE"
                        elif (symb1Type == "BOOL"):
                            result = "TRUE" if symb1Value == "FALSE" and symb2Value == "TRUE" else "FALSE"
                        elif (symb1Type == "NIL"):
                            errorHandler(53, "interpret.py", "main", "LT - comparing NIL")
                    elif (instruction.name == "GT"):
                        if (symb1Type == "INT"):
                            result = "TRUE" if int(symb1Value) > int(symb2Value) else "FALSE"
                        elif (symb1Type == "STRING"):
                            result = "TRUE" if symb1Value > symb2Value else "FALSE"
                        elif (symb1Type == "BOOL"):
                            result = "TRUE" if symb1Value == "TRUE" and symb2Value == "FALSE" else "FALSE"
                        elif (symb1Type == "NIL"):
                            errorHandler(53, "interpret.py", "main", "GT - comparing NIL")
                    elif (instruction.name == "EQ"):
                        result = "TRUE" if symb1Value == symb2Value else "FALSE"

                elif ((symb1Type == "NIL" or symb2Type == "NIL") and instruction.name == "EQ"):
                    result = "TRUE" if symb1Value == symb2Value else "FALSE"
                else:
                    errorHandler(53, "interpret.py", "main", "LT, GT, EQ")

            saveResult()

        elif (instruction.name == "AND" or instruction.name == "OR"):
            symb1Type = getSymbType(instruction.args[1], True)
            symb2Type = getSymbType(instruction.args[2], True)
            if (symb1Type != "BOOL" or symb2Type != "BOOL"):
                errorHandler(53, "interpret.py", "main", "wrong types of symb1 or symb2")

            symb1Value = getSymbValue(instruction.args[1]).lower()
            symb2Value = getSymbValue(instruction.args[2]).lower()
            result = False
            try:
                if (instruction.name == "AND" and symb1Value == symb2Value == "true"):
                    result = True
                elif (instruction.name == "OR" and (symb1Value == "true" or symb2Value == "true")):
                    result = True

                varGettingResult = frame.getVar(instruction.args[0]["data"])
                varGettingResult.type = "BOOL"
                varGettingResult.value = str(result).lower()
                varGettingResult.valueSet = True

            except Exception as e:
                errorHandler(99, "interpret.py", "main", "AND, OR")

        elif (instruction.name == "STRI2INT"):
            symb1Type = getSymbType(instruction.args[1], True)
            symb2Type = getSymbType(instruction.args[2], True)

            if (symb1Type != "STRING" or symb2Type != "INT"):
                errorHandler(53, "interpret.py", "main", "wrong types of symb1 or symb2")

            symb1Value = getSymbValue(instruction.args[1], True)
            symb2Value = int(getSymbValue(instruction.args[2], True))

            if (symb2Value < 0 or symb2Value >= len(symb1Value)):
                errorHandler(58, "interpret.py", "main", "SETCHAR - index out of range")

            result = ord(symb1Value[symb2Value])
            varGettingResult = frame.getVar(instruction.args[0]["data"])
            varGettingResult.type = "INT"
            varGettingResult.value = result
            varGettingResult.valueSet = True

        elif (instruction.name == "CONCAT"):
            symb1Type = getSymbType(instruction.args[1], True)
            symb2Type = getSymbType(instruction.args[2], True)

            if (symb1Type != "STRING" or symb2Type != "STRING"):
                errorHandler(53, "interpret.py", "main", "wrong types of symb1 or symb2")

            result = None
            symb1Value = getSymbValue(instruction.args[1])
            symb2Value = getSymbValue(instruction.args[2])

            if(symb1Value == None and symb2Value != None):
                result = symb2Value
            elif(symb1Value != None and symb2Value == None):
                result = symb1Value
            elif(symb1Value == None and symb2Value == None):
                result = ""
            else:
                result = symb1Value + symb2Value

            varGettingResult = frame.getVar(instruction.args[0]["data"])
            varGettingResult.type = "STRING"
            varGettingResult.value = result
            varGettingResult.valueSet = True

        elif (instruction.name == "GETCHAR"):
            symb1Type = getSymbType(instruction.args[1], True)
            symb2Type = getSymbType(instruction.args[2], True)

            if (symb1Type != "STRING" or symb2Type != "INT"):
                errorHandler(53, "interpret.py", "main", "wrong types of symb1 or symb2")

            result = None
            symb1Value = getSymbValue(instruction.args[1], True)
            symb2Value = int(getSymbValue(instruction.args[2], True))

            if(symb2Value < 0 or symb2Value >= len(symb1Value)):
                errorHandler(58, "interpret.py", "main", "GETCHAR - index out of range")

            try:
                result = symb1Value[symb2Value]
                varGettingResult = frame.getVar(instruction.args[0]["data"])
                varGettingResult.type = "STRING"
                varGettingResult.value = result
                varGettingResult.valueSet = True

            except Exception as e:
                errorHandler(99, "interpret.py", "main", "GETCHAR")

        elif (instruction.name == "SETCHAR"):
            stringToModifyType = getSymbType(instruction.args[0], True)
            stringToModify = getSymbValue(instruction.args[0], True)

            symb1Type = getSymbType(instruction.args[1], True)
            symb2Type = getSymbType(instruction.args[2], True)

            if (symb1Type != "INT" or symb2Type != "STRING" or stringToModifyType != "STRING"):
                errorHandler(53, "interpret.py", "main", "wrong types of symb1 or symb2")

            symb1Value = int(getSymbValue(instruction.args[1], True))
            symb2Value = getSymbValue(instruction.args[2], True)

            if(symb1Value < 0 or symb1Value >= len(stringToModify)):
                errorHandler(58, "interpret.py", "main", "SETCHAR - index out of range")
            if(symb2Value == "" or symb2Value == None):
                errorHandler(58, "interpret.py", "main", "SETCHAR - empty char")

            result = stringToModify
            result = result[:symb1Value] + symb2Value[0] + result[symb1Value + 1:]
            varGettingResult = frame.getVar(instruction.args[0]["data"])
            varGettingResult.type = "STRING"
            varGettingResult.value = result
            varGettingResult.valueSet = True

            # LABEL, SYMB1, SYMB2

        elif (instruction.name == "JUMPIFEQ" or instruction.name == "JUMPIFNEQ"):
            labelName = instruction.args[0]["data"]
            if (labelName not in instructionList.labels):
                errorHandler(52, "interpret.py", "Main" "JUMPIFNEQ - jump to non-existing label")

            symb1Type = getSymbType(instruction.args[1], True)
            symb2Type = getSymbType(instruction.args[2], True)

            if (symb1Type == symb2Type or symb1Type == "NIL" or symb2Type == "NIL"):
                symb1Value = getSymbValue(instruction.args[1], True)
                symb2Value = getSymbValue(instruction.args[2], True)

                if (instruction.name == "JUMPIFEQ" and symb1Value == symb2Value):
                    instructionList.jumpTo = labelName

                elif (instruction.name == "JUMPIFNEQ" and symb1Value != symb2Value):
                    instructionList.jumpTo = labelName
            else:
                errorHandler(53, "interpret.py", "main", "JUMPIEQ")


main()

