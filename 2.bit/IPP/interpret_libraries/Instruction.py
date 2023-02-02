'''
* Project: interpret.py
* 2021/2022
* @author Marek Klofera <xklofe01@stud.fit.vutbr.cz> - VUT FIT student
'''

import sys
# my libraries
from interpret_libraries.ErrorHandler import errorHandler
import interpret_libraries.Constants as const


class Instruction:

    def __init__(self, name, order):
        super().__init__()

        self.name = name.upper()
        self.argCount = 0
        self.maxArgs = 3
        self.args = []
        self.order = order


    def addArgs(self, args):
        numberOfArgs = len(args)
        self.checkArgsNumberPerInstruction(numberOfArgs)

        for arg in args:
            type = arg['attribType']
            data = arg["value"]
            # argNumber = arg["argNumber"]
            if(self.argCount <= self.maxArgs):
                # arg = {'argNumber': argNumber, 'type': type, 'data': data}
                arg = {'attribType': type, 'data': data}
                self.args.append(arg)
                self.argCount += 1
            else:
                errorHandler(99, "Instruction", "addArg", "Instruction can't have more than 3 args")

    def getVarFrame(self, variable):
        return variable[0:2]

    def getVarValue(self, variable):
        return variable[3:]

    def checkArgsNumberPerInstruction(self, numberOfArgs):
        def CallError(correctArgsNumber):
            errorHandler(32, "XMLParser.py", "checkXMLArgInInstruction", "instruction has too many args")

        if (self.name in const.INSTRUCTIONS_WITH_ZERO_ARG and numberOfArgs != 0):
            CallError(0)
        elif (self.name in const.INSTRUCTIONS_WITH_ONE_ARG and numberOfArgs != 1):
            CallError(1)
        elif (self.name in const.INSTRUCTIONS_WITH_TWO_ARG and numberOfArgs != 2):
            CallError(2)
        elif (self.name in const.INSTRUCTIONS_WITH_THREE_ARG and numberOfArgs != 3):
            CallError(3)
        return True